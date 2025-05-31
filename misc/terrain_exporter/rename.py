from pathlib import Path

dir_path = Path("heightmaps")

for file in dir_path.glob("*.bin"):
    name = file.name
    split, y = name.split(".")[0].split("Y")
    x = split.split("X")[1]

    # new name should be x_y + ext
    new_name = f"{x}_{y}.bin"
    file.rename(dir_path / new_name)
