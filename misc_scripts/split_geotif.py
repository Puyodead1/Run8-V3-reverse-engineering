import argparse
from math import ceil
from pathlib import Path

import numpy as np
from osgeo import gdal


def split_geotiff_into_tiles(input_tiff, tile_width_meters=844.3211, tile_height_meters=1026.0822):
    ds = gdal.Open(input_tiff)
    gt = ds.GetGeoTransform()
    pixel_width = abs(gt[1])
    pixel_height = abs(gt[5])

    tile_width_pixels = int(ceil(tile_width_meters / pixel_width))
    tile_height_pixels = int(ceil(tile_height_meters / pixel_height))

    width = ds.RasterXSize
    height = ds.RasterYSize

    num_tiles_x = width // tile_width_pixels
    num_tiles_y = height // tile_height_pixels

    for i in range(num_tiles_y):
        for j in range(num_tiles_x):
            print(f"Processing tile {i}, {j}")
            x_start = j * tile_width_pixels
            y_start = i * tile_height_pixels

            output_file = Path("tiles", f"{i}_{j}.tif")
            output_file.parent.mkdir(exist_ok=True, parents=True)
            gdal.Translate(
                str(output_file), ds, srcWin=[x_start, y_start, tile_width_pixels, tile_height_pixels], format="GTiff"
            )


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Split a geotiff into tiles")
    parser.add_argument("input_tiff", help="Input geotiff file")
    parser.add_argument("--tile_width", type=float, default=844.3211, help="Width of each tile in meters")
    parser.add_argument("--tile_height", type=float, default=1026.0822, help="Height of each tile in meters")

    args = parser.parse_args()

    split_geotiff_into_tiles(args.input_tiff, args.tile_width, args.tile_height)
