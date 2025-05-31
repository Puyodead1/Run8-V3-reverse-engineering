import json
import os
import tkinter as tk
from io import BytesIO
from tkinter import messagebox, ttk

import numpy as np
import requests
import tkintermapview
from PIL import Image


class TerrainGenerator:
    def __init__(self):
        self.tile_width_meters = 846
        self.tile_length_meters = 1028
        self.tile_width_deg = self.tile_width_meters / 111320
        self.tile_length_deg = self.tile_length_meters / 111320

        self.origin_pin = None
        self.origin_set = False
        self.selected_tiles = {}

        self.selection_active = False
        self.map_widget_move = True

        self.root = tk.Tk()
        self.root.title("3DEP Terrain Generator")
        self.root.geometry("1200x800")
        self.setup_gui()

    def setup_gui(self):
        map_frame = ttk.Frame(self.root)
        map_frame.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)

        self.map_widget = tkintermapview.TkinterMapView(map_frame, width=800, height=600)
        self.map_widget.pack(fill=tk.BOTH, expand=True)
        self.map_widget.set_tile_server("https://a.tile.openstreetmap.org/{z}/{x}/{y}.png")
        self.map_widget.set_position(34.8983820, -117.0553344)
        self.map_widget.set_zoom(15)

        controls = ttk.Frame(self.root)
        controls.pack(side=tk.RIGHT, fill=tk.Y)

        self.selection_mode = tk.BooleanVar(value=False)
        ttk.Checkbutton(
            controls, text="Selection Mode", variable=self.selection_mode, command=self.toggle_selection_mode
        ).pack(pady=5)
        ttk.Button(controls, text="Reset Origin", command=self.reset_origin).pack(pady=5)
        ttk.Button(controls, text="Clear Tiles", command=self.clear_tiles).pack(pady=5)
        ttk.Button(controls, text="Generate", command=self.generate_heightmaps).pack(pady=5)

    def toggle_selection_mode(self):
        if self.selection_mode.get():
            # Enable selection mode
            self.map_widget.canvas.unbind("<B1-Motion>")
            self.map_widget.canvas.unbind("<ButtonPress-1>")
            self.map_widget.canvas.unbind("<ButtonRelease-1>")
            self.map_widget.canvas.bind("<Button-1>", self.handle_click)
        else:
            # Enable map movement
            self.map_widget.canvas.bind("<B1-Motion>", self.map_widget.mouse_move)
            self.map_widget.canvas.bind("<ButtonPress-1>", self.map_widget.mouse_down)
            self.map_widget.canvas.bind("<ButtonRelease-1>", self.map_widget.mouse_release)
            self.map_widget.canvas.unbind("<Button-1>")

    def handle_click(self, event):
        coords = self.map_widget.convert_canvas_coords_to_decimal_coords(event.x, event.y)

        if not self.origin_set:
            self.set_origin(coords[0], coords[1])
        else:
            self.add_tile_relative_to_origin(coords[0], coords[1])

    def set_origin(self, lat, lon):
        if self.origin_pin:
            self.origin_pin.delete()

        self.origin_pin = self.map_widget.set_marker(lat, lon, text="X0,Y0")
        self.origin_lat = lat
        self.origin_lon = lon
        self.origin_set = True
        self.add_tile_relative_to_origin(lat, lon)  # Add first tile at origin

    def reset_origin(self):
        if self.origin_pin:
            self.origin_pin.delete()
        self.clear_tiles()
        self.origin_set = False

    def clear_tiles(self):
        for tile in self.selected_tiles.values():
            tile["polygon"].delete()
        self.selected_tiles.clear()

    def add_tile_relative_to_origin(self, lat, lon):
        dx = round((lon - self.origin_lon) / self.tile_length_deg)
        dy = round((lat - self.origin_lat) / self.tile_width_deg)

        tile_lat = self.origin_lat + (dy * self.tile_width_deg)
        tile_lon = self.origin_lon + (dx * self.tile_length_deg)

        tile_id = f"{dx}_{dy}"

        if tile_id not in self.selected_tiles:
            corners = [
                (tile_lat - self.tile_width_deg / 2, tile_lon - self.tile_length_deg / 2),
                (tile_lat - self.tile_width_deg / 2, tile_lon + self.tile_length_deg / 2),
                (tile_lat + self.tile_width_deg / 2, tile_lon + self.tile_length_deg / 2),
                (tile_lat + self.tile_width_deg / 2, tile_lon - self.tile_length_deg / 2),
            ]

            polygon = self.map_widget.set_polygon(corners, fill_color="red", outline_color="black")

            self.selected_tiles[tile_id] = {
                "polygon": polygon,
                "bounds": (
                    tile_lat + self.tile_width_deg / 2,
                    tile_lat - self.tile_width_deg / 2,
                    tile_lon + self.tile_length_deg / 2,
                    tile_lon - self.tile_length_deg / 2,
                ),
            }

    def get_3dep_elevation(self, bounds):
        url = "https://elevation.nationalmap.gov/arcgis/rest/services/3DEPElevation/ImageServer/exportImage"
        width_pixels = int(round(self.tile_width_meters))
        height_pixels = int(round(self.tile_length_meters))

        params = {
            "bbox": f"{bounds[3]},{bounds[1]},{bounds[2]},{bounds[0]}",  # W,S,E,N
            "bboxSR": 4326,
            "size": f"{width_pixels},{height_pixels}",
            "imageSR": 4326,
            "format": "TIFF",
            "pixelType": "F32",
            "noDataInterpretation": "esriNoDataMatchAny",
            "interpolation": "+RSP_BilinearInterpolation",
            "f": "pjson",
        }

        try:
            response = requests.get(url, params=params)
            if response.status_code == 200:
                data = response.json()
                img_response = requests.get(data["href"])

                with open(f"debug_response.tiff", "wb") as f:
                    f.write(img_response.content)

                elevation_data = np.array(Image.open(BytesIO(img_response.content)))
                elevation_data = elevation_data.astype(np.float32)

                print(f"Elevation range: {elevation_data.min():.2f}m to {elevation_data.max():.2f}m")
                print(f"Data shape: {elevation_data.shape}")

                return elevation_data

        except Exception as e:
            print(f"Error getting elevation data: {e}")
            return np.zeros((height_pixels, width_pixels), dtype=np.float32)

    def generate_heightmaps(self):
        if not self.selected_tiles:
            messagebox.showwarning("No Selection", "Please select tiles first")
            return

        if not os.path.exists("heightmaps"):
            os.makedirs("heightmaps")

        for tile_id, tile in self.selected_tiles.items():
            elevation_data = self.get_3dep_elevation(tile["bounds"])

            # Write header and data
            with open(f"heightmaps/{tile_id}.bin", "wb") as f:
                np.array([elevation_data.shape[1], elevation_data.shape[0]], dtype=np.int32).tofile(f)  # Width, Height
                # prepend the N, S, E, W bounds to the file
                np.array(tile["bounds"], dtype=np.float32).tofile(f)
                elevation_data.tofile(f)

            print(f"Generated {tile_id}.bin ({elevation_data.shape[1]}x{elevation_data.shape[0]})")

        messagebox.showinfo("Complete", f"Generated {len(self.selected_tiles)} heightmaps")

    def run(self):
        self.root.mainloop()


if __name__ == "__main__":
    app = TerrainGenerator()
    app.run()
