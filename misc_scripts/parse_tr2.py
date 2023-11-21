import argparse

from binreader import BinaryReader


def main(file):
    with open(file, "rb") as f:
        reader = BinaryReader(f)

        all_points = []

        print(reader.read_cs_string())
        print(reader.read_cs_string())
        print(reader.read_cs_string())
        print(reader.read_cs_string())

        # read chunks
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


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Parse TR2 terrain")
    parser.add_argument("file", help="Input file")

    args = parser.parse_args()

    main(args.file)
