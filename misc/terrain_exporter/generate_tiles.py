import struct
import zlib
from pathlib import Path

import numpy as np
from construct import Array, Float32l, GreedyBytes, Int8sl, Int8ul, Int32ul
from construct import Struct as cStruct
from construct import this
from scipy.ndimage import zoom


def encode_7bit_int(length):
    result = bytearray()
    while length >= 0x80:
        result.append((length & 0x7F) | 0x80)
        length >>= 7
    result.append(length)
    return bytes(result)


def build_cs_string(text):
    encoded_text = text.encode("utf8")
    length_prefix = encode_7bit_int(len(encoded_text))
    return length_prefix + encoded_text


Chunk = cStruct(
    "hixel_count" / Int32ul,
    "hixels" / Array(this.hixel_count, Array(this.hixel_count, Float32l)),
)

Vector3 = cStruct(
    "x" / Float32l,
    "y" / Float32l,
    "z" / Float32l,
)

Vector4 = cStruct(
    "x" / Float32l,
    "y" / Float32l,
    "z" / Float32l,
    "w" / Float32l,
)

TileIndex = cStruct(
    "x" / Int32ul,
    "y" / Int32ul,
)

Decal = cStruct(
    "r" / Float32l,
    "g" / Float32l,
    "b" / Float32l,
    "digit_count" / Int32ul,
    "digits" / Array(this.digit_count, Int32ul),
    "offset" / Vector3,
    "rotation" / Vector3,
    "size" / Float32l,
    "texture_name" / GreedyBytes,
)

SceneryAsset = cStruct(
    "decal_count" / Int32ul,
    "decals" / Array(this.decal_count, Decal),
    "disregard_bounding_test" / Int8ul,
    "model_name" / GreedyBytes,
    "position" / Vector3,
    "rotation" / Vector3,
    "scale" / Vector3,
    "tile_index" / TileIndex,
)

TR4 = cStruct(
    "texture_1" / GreedyBytes,
    "texture_2" / GreedyBytes,
    "texture_3" / GreedyBytes,
    "texture_4" / GreedyBytes,
    "chunks" / Array(25, Array(25, Chunk)),
    "east" / Float32l,
    "west" / Float32l,
    "north" / Float32l,
    "south" / Float32l,
    "veg_texture" / GreedyBytes,
    "scenery_asset_count" / Int32ul,
    "scenery_assets" / Array(this.scenery_asset_count, SceneryAsset),
    "plant_count" / Int32ul,
    "plants" / Array(this.plant_count, Vector4),
    "reserved" / Int32ul,
    "weight_map" / Int8sl,
)

TR2 = cStruct(
    "texture_1" / GreedyBytes,
    "texture_2" / GreedyBytes,
    "texture_3" / GreedyBytes,
    "texture_4" / GreedyBytes,
    "chunks" / Array(25, Array(25, Chunk)),
    "east" / Float32l,
    "west" / Float32l,
    "north" / Float32l,
    "south" / Float32l,
    "veg_texture" / GreedyBytes,
)


def read_f32_image(file_path):
    with open(file_path, "rb") as f:
        # Read width and height from header (2 int32s)
        width, height = struct.unpack("ii", f.read(8))

        # read bounds: N, S, E, W (4 float 32s)
        bounds = struct.unpack("ffff", f.read(16))

        # Read the rest as float32 data
        data = np.fromfile(f, dtype=np.float32)

        if width * height != len(data):
            raise ValueError(f"File data length {len(data)} doesn't match dimensions {width}x{height}")

        return width, height, bounds, data.reshape(height, width)


def main():
    tile_folder_path = Path("heightmaps")
    if not tile_folder_path.exists():
        raise FileNotFoundError(f"Folder {tile_folder_path} not found")

    tile_paths = sorted(tile_folder_path.glob("*.bin"))
    if not tile_paths:
        raise FileNotFoundError(f"No .bin files found in {tile_folder_path}")

    for tile_path in tile_paths:
        width, height, bounds, data = read_f32_image(tile_path)
        print(f"Loaded tile {tile_path.name} with shape {data.shape}")

        chunks = []
        chunk_grid = 25
        target_size = 5

        for x in range(chunk_grid):
            c = []
            for y in range(chunk_grid):
                # Get original chunk data
                x_start = x * (data.shape[0] // chunk_grid)
                x_end = min((x + 1) * (data.shape[0] // chunk_grid), data.shape[0])
                y_start = y * (data.shape[1] // chunk_grid)
                y_end = min((y + 1) * (data.shape[1] // chunk_grid), data.shape[1])

                chunk = data[x_start:x_end, y_start:y_end]

                # Calculate zoom factors to reduce to 5x5
                zoom_factor = (target_size / chunk.shape[0], target_size / chunk.shape[1])

                # Downsample the chunk
                reduced_chunk = zoom(chunk, zoom_factor, order=1)  # order=1 for bilinear interpolation

                chunk_points = []
                for i in range(target_size):
                    row = []
                    for j in range(target_size):
                        row.append(reduced_chunk[i, j])
                    chunk_points.append(row)

                c.append({"hixel_count": target_size, "hixels": chunk_points})
            chunks.append(c)

        new_tile_data = {
            "texture_1": build_cs_string("PA_Grass01##run8_Dirt"),
            "texture_2": build_cs_string("PA_DirtGrass01"),
            "texture_3": build_cs_string("PA_BallastTextureNew"),
            "texture_4": build_cs_string("PA_Asphalt"),
            "chunks": chunks,
            "east": -78.58444213867188,
            "west": -78.59439849853516,
            "north": 40.46984100341797,
            "south": 40.460601806640625,
            "veg_texture": build_cs_string("CoastalChapparal"),
            # "scenery_asset_count": 0,
            # "scenery_assets": [],
            # "plant_count": 0,
            # "plants": [],
            # "reserved": 0,
            # "weight_map": -24,
        }

        # get thge x and y from the file name
        x, y = tile_path.stem.split("_")
        x = int(x)
        y = int(y)

        def format_coords(x, y):
            return f"{'-' if x < 0 else ''}{abs(x):05d}_{'-' if y < 0 else ''}{abs(y):05d}"

        new_tile_data_bytes = TR2.build(new_tile_data)
        new_tile_path = tile_folder_path / f"{format_coords(x, y)}.tr2"
        with open(new_tile_path, "wb") as f:
            f.write(new_tile_data_bytes)
            print(f"Saved new TR2 file to {new_tile_path}")

        # new_tile_data_bytes = TR4.build(new_tile_data)
        # new_tile_path = tile_path.with_suffix(".tr4")

        # # file name should be x and y left padded with zeros to 5 characters excluding any - signs
        # new_tile_path = tile_folder_path / f"{format_coords(x, y)}.tr4"
        # with open(new_tile_path, "wb") as f:
        #     # zlib compression, no header
        #     compressed_bytes = zlib.compress(new_tile_data_bytes, level=9)[2:-4]
        #     f.write(compressed_bytes)
        #     print(f"Saved new TR2 file to {new_tile_path}")


if __name__ == "__main__":
    main()
