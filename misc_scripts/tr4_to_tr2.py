import argparse
import io
import zlib

from binreader import BinaryReader


def main(file):
    with open(file, "rb") as f:
        data = f.read()
        data = zlib.decompress(data, -15)

        reader = BinaryReader(io.BytesIO(data))

        print(reader.read_cs_string())
        print(reader.read_cs_string())
        print(reader.read_cs_string())
        print(reader.read_cs_string())

        chunks = []
        min_value = 9999999
        max_value = -9999999

        # heightmap
        for x in range(25):
            for y in range(25):
                # read chunk hixels
                hixel_count = reader.read_int32()
                print(f"Chunk at {x},{y} has a density of {hixel_count}x{hixel_count} points")

                chunk_points = []
                for cx in range(hixel_count):
                    for cy in range(hixel_count):
                        value = reader.read_float()
                        # print(f"{x},{y},{cx},{cy} = {value}")
                        chunk_points.append(value)
                        min_value = min(min_value, value)
                        max_value = max(max_value, value)

                print(f"Chunk at {x},{y} has {len(chunk_points)} points")
                chunks.append(chunk_points)
        print(f"Total Points: {sum([len(x) for y in chunk_points for x in chunks])}")

        # find the min and max values
        print(f"Min: {min_value}")
        print(f"Max: {max_value}")

        try:
            print(f"East: {reader.read_float()}")
            print(f"West: {reader.read_float()}")
            print(f"North: {reader.read_float()}")
            print(f"South: {reader.read_float()}")
            print(reader.read_cs_string())
        except:
            pass

        size = reader.tell()
        reader.seek(0)
        data = reader.read(size)
        with open("output.tr2", "wb") as f:
            f.write(data)


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Convert TR4 to TR2")
    parser.add_argument("file", help="TR2 File")

    args = parser.parse_args()

    main(args.file)
