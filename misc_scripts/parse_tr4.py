import argparse
import io
import zlib

from binreader import BinaryReader


def main(file):
    with open(file, "rb") as f:
        data = f.read()
        data = zlib.decompress(data, -15)

        reader = BinaryReader(io.BytesIO(data))

        all_points = []

        print(reader.read_cs_string())
        print(reader.read_cs_string())
        print(reader.read_cs_string())
        print(reader.read_cs_string())

        # heightmap
        for x in range(25):
            for y in range(25):
                # read chunk hixels
                hixels = reader.read_int32()
                print(f"{x},{y} = {hixels}")
                for cx in range(hixels):
                    for cy in range(hixels):
                        value = reader.read_float()
                        # print(f"{x},{y},{cx},{cy} = {value}")
                        all_points.append(value)
        print(f"Total Points: {len(all_points)}")

        try:
            print(f"East: {reader.read_float()}")
            print(f"West: {reader.read_float()}")
            print(f"North: {reader.read_float()}")
            print(f"South: {reader.read_float()}")
            print(reader.read_cs_string())
        except:
            pass

        # scenery assets
        asset_count = reader.read_int32()

        for i in range(asset_count):
            decal_count = reader.read_int32()
            for j in range(decal_count):
                digits = []
                rgb = (reader.read_float(), reader.read_float(), reader.read_float())
                digit_count = reader.read_int32()
                for k in range(digit_count):
                    digits.append(reader.read_int32())

                offset_xyz = (reader.read_float(), reader.read_float(), reader.read_float())
                rotation_deg_xyz = (reader.read_float(), reader.read_float(), reader.read_float())
                size = reader.read_float()
                texture_name = reader.read_cs_string()
                print(
                    f"DecalLoader(rgb: {rgb}; digits: {digits}; offset_xyz: {offset_xyz}; rotation_deg_xyz: {rotation_deg_xyz}; size: {size}; texture_name: {texture_name})"
                )

            disregard_bounding_test = reader.read_bool()
            model_name = reader.read_cs_string()
            position_xyz = (reader.read_float(), reader.read_float(), reader.read_float())
            rotation_deg = (reader.read_float(), reader.read_float(), reader.read_float())
            scale = (reader.read_float(), reader.read_float(), reader.read_float())
            tile_xz = (reader.read_int32(), reader.read_int32())

            print(
                f"SceneryAssetLoader(disregard_bounding_test: {disregard_bounding_test}; model_name: {model_name}; position_xyz: {position_xyz}; position_deg: {rotation_deg}; scale: {scale}; tile_xz: {tile_xz})"
            )

        # vegetation
        plant_count = reader.read_int32()
        plants = []
        for i in range(plant_count):
            plants.append(reader.read_vector4())

        print(f"Plant Count: {plant_count}")

        # reserved
        reader.read_int32()

        # weight map
        num = reader.read_byte()
        print(f"WeightMap byte: {num}")


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Parse TR4 terrain")
    parser.add_argument("file", help="Input file")

    args = parser.parse_args()

    main(args.file)
