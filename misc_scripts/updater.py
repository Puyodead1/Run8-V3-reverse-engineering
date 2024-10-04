import hashlib
import os

import requests

URL = "http://www.run8-services.com/v3autoupdater/v3autoupdate.php"
FILE_DOWNLOAD_URL = "http://www.run8-services.com/v3autoupdater/v3autoupdate_dl.php"
BASE_PATH = "D:\\Programs\\Run8Studios\\Run8 Train Simulator V3"
OUTDATED_LIST = []
FILES: dict[str, str] = {}


def smethod_5(dirname: str):
    return (
        "V3Routes" in dirname
        and not "VegTextures" in dirname
        and not "TrackTextures" in dirname
        and not "TerrainTextures" in dirname
        and not "Splendid Assets" in dirname
        and not "Regions" in dirname
    )


def compute_file_hash(path: str):
    with open(path, "rb") as f:
        return hashlib.md5(f.read()).hexdigest().lower()


def check_files():
    OUTDATED_LIST.clear()

    for file in FILES.keys():
        rpath = file
        hash_ = FILES[file]

        apath = BASE_PATH + rpath
        dirname = os.path.dirname(apath)

        if os.path.exists(dirname) and (not smethod_5(dirname) or os.path.exists(dirname + "/TrackDatabase.r8")):
            if os.path.exists(apath):
                file_hash = compute_file_hash(apath)
                if file_hash != hash_.lower():
                    OUTDATED_LIST.append(rpath)
            else:
                OUTDATED_LIST.append(rpath)


def download_file(name: str):
    try:
        r = requests.post(FILE_DOWNLOAD_URL, data={"xyz": "123", "filename": name})

        if r.ok:
            apath = BASE_PATH + name
            with open(apath, "wb") as f:
                f.write(r.content)
    except Exception as e:
        print(e)


def main():
    print("Fetching manifest...")
    data = {"xyz": "123"}
    r = requests.post(URL, data=data)

    if r.ok:
        for text in list(filter(None, r.text.split("jwg96main"))):
            k, v = list(filter(None, text.split(";")))
            FILES[k] = v

        print("Checking local files against manifest...")
        check_files()

        print(f"{len(OUTDATED_LIST)} outdated files")

        if len(OUTDATED_LIST) == 0:
            print("Already up to date")
            exit(0)

        for file in OUTDATED_LIST:
            print(f"Downloading file: {file} ({OUTDATED_LIST.index(file) + 1}/{len(OUTDATED_LIST)})")

            download_file(file)

        print("Checking for errors...")
        check_files()

        if len(OUTDATED_LIST) > 0:
            print(f"Update failure! {len(OUTDATED_LIST)} files failed to verify")
        else:
            print("Update complete")
    else:
        print("Failed to get manifest.")


if __name__ == "__main__":
    main()
