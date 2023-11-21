import argparse
import math

import numpy as np
import rasterio
from binwriter import BinaryWriter
from noise import snoise2

CHUNKS_X = 25
CHUNKS_Z = 25
TILE_X = 1026.0822  # meters
TILE_Z = 844.3211  # meters
HIXELS = 30  # number of points per chunk side
CHUNK_SIZE_X = TILE_X / CHUNKS_X
CHUNK_SIZE_Z = TILE_Z / CHUNKS_Z


def main(file: str):
    with rasterio.open("./USGS_1M_13_x51y456_WY_LaramieLidar_2021_D21.tif") as heightmap:
        height_values = heightmap.read(1)

    x_points_required = CHUNKS_X * HIXELS
    z_points_required = CHUNKS_Z * HIXELS

    x_points_available = height_values.shape[0]
    z_points_available = height_values.shape[1]

    if x_points_available < x_points_required:
        print(f"Not enough x points available: {x_points_available} < {x_points_required}")
    else:
        print(f"X points available: {x_points_available} >= {x_points_required}")

    if z_points_available < z_points_required:
        print(f"Not enough z points available: {z_points_available} < {z_points_required}")
    else:
        print(f"Z points available: {z_points_available} >= {z_points_required}")

    min_x_value = np.amin(height_values, axis=0)
    min_z_value = np.amin(height_values, axis=1)

    min_x_value = np.min(min_x_value)
    min_z_value = np.min(min_z_value)

    with open(file, "wb") as f:
        writer = BinaryWriter(f)

        # writer texture names
        writer.write_cstring("sandy")
        writer.write_cstring("jwg_greenGrass")
        writer.write_cstring("jwg_cameron02")
        writer.write_cstring("jwg_goldenGrass")

        for x in range(CHUNKS_X):
            for z in range(CHUNKS_Z):
                writer.write_int32(HIXELS)
                for cx in range(HIXELS):
                    for cz in range(HIXELS):
                        x_idx = x * HIXELS + cx
                        z_idx = z * HIXELS + cz

                        if x_idx >= x_points_available or z_idx >= z_points_available:
                            value = min_x_value
                        else:
                            value = height_values[x_idx, z_idx]
                            if value < 0:
                                print("oops")
                                value = math.fabs(value)
                        writer.write_float(value)


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Generate TR2 terrain")
    parser.add_argument("file", help="Output file")

    args = parser.parse_args()

    main(args.file)
