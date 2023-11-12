import argparse
import hashlib
from pathlib import Path

from binreader import BinaryReader


def compute_file_hash(filepath: str) -> str:
    md5 = hashlib.md5()
    with open(filepath, "rb") as f:
        for chunk in iter(lambda: f.read(4096), b""):
            md5.update(chunk)

    return md5.hexdigest()


def main(folder_path: str):
    exe_path = Path(folder_path, "Run-8 Train Simulator V3.exe")
    skydome_path = Path(folder_path, "Content", "Misc", "SkyDomeData.rn8")

    if not exe_path.exists():
        raise FileNotFoundError(f"Could not find Run-8 Train Simulator V3.exe at {exe_path}")

    if not skydome_path.exists():
        raise FileNotFoundError(f"Could not find SkyDomeData.rn8 at {skydome_path}")

    file_hash = compute_file_hash(exe_path).encode("utf-8")

    with open(skydome_path, "rb") as f:
        size = f.seek(0, 2)
        assert size == 38888, f"Expected 38888 bytes, got {size} bytes"
        f.seek(0)

        reader = BinaryReader(f)
        data = []
        hash_data = bytearray(len(file_hash))

        i = 0
        while i < 9722:
            data.append(reader.read_int32())
            i += 1

        num = data[17]  # 17 is the index of the file hash length
        assert num == len(file_hash), "Expected index 17 to be the length of the file hash"

        i = 0
        while i < num:
            num2 = data[i + 18]
            hash_data[i] = data[num2]
            i += 1

        hash_data = bytes(hash_data)
        assert file_hash.decode("utf-8").upper() == hash_data.decode("utf-8").upper(), "File hash does not match"

        print("Hashes match")


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="SkyDomeData test parser")
    parser.add_argument("path", help="Path to run8v3 folder")

    args = parser.parse_args()
    main(args.path)
