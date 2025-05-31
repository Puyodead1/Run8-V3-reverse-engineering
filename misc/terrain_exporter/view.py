import struct
import tkinter as tk
from tkinter import filedialog, messagebox

import matplotlib.pyplot as plt
import numpy as np
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
from matplotlib.figure import Figure


class ImageViewer:
    def __init__(self, root):
        self.root = root
        root.title("F32 Image Viewer")
        root.geometry("800x600")

        # Create matplotlib figure
        self.figure = Figure(figsize=(8, 6))
        self.ax = self.figure.add_subplot(111)
        self.canvas = FigureCanvasTkAgg(self.figure, master=root)
        self.canvas.draw()
        self.canvas.get_tk_widget().pack(fill=tk.BOTH, expand=True)

        # Add open button
        open_button = tk.Button(root, text="Open File", command=self.open_file)
        open_button.pack(pady=5)

    def read_f32_image(self, file_path):
        with open(file_path, "rb") as f:
            # Read width and height from header (2 int32s)
            width, height = struct.unpack("ii", f.read(8))

            # read bounds (4 float 32s)
            bounds = struct.unpack("ffff", f.read(16))

            # Read the rest as float32 data
            data = np.fromfile(f, dtype=np.float32)

            if width * height != len(data):
                raise ValueError(f"File data length {len(data)} doesn't match dimensions {width}x{height}")

            return data.reshape(height, width)

    def open_file(self):
        file_path = filedialog.askopenfilename(title="Open F32 Image", filetypes=[("Binary Files", "*.bin")])

        if file_path:
            try:
                image_data = self.read_f32_image(file_path)
                self.display_image(image_data)
            except Exception as e:
                messagebox.showerror("Error", str(e))

    def display_image(self, image_data):
        self.ax.clear()
        vmin, vmax = np.percentile(image_data, [2, 98])
        im = self.ax.imshow(image_data, cmap="viridis", vmin=vmin, vmax=vmax)
        self.figure.colorbar(im, ax=self.ax, label="Intensity")
        self.ax.set_title(f"Image: {image_data.shape[1]}x{image_data.shape[0]}")
        self.canvas.draw()


def main():
    root = tk.Tk()
    viewer = ImageViewer(root)
    root.mainloop()


if __name__ == "__main__":
    main()
