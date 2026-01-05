import bpy
from bpy.props import StringProperty
from bpy.types import Operator
from bpy_extras.io_utils import ImportHelper

from .binreader import BinaryReader
from .terrain.ter import TER
from .terrain.tr2 import TR2
from .terrain.tr3 import TR3
from .terrain.tr4 import TR4

ID = "io_run8.import_terrain_tile"


def import_model(context, filepath):
    print("\n" * 10)
    print("Importing Run8 Terrain Tile: %r..." % (filepath))

    with open(filepath, "rb") as f:
        reader = BinaryReader(f)
        split = filepath.split("\\")[-1].split("_")
        x = int(split[0])
        y = int(split[1].split(".")[0])

        if filepath.endswith(".ter"):
            tile = TER(reader, x, y)
        elif filepath.endswith(".tr2"):
            tile = TR2(filepath, x, y)
        elif filepath.endswith(".tr3"):
            tile = TR3(reader, x, y)
        elif filepath.endswith(".tr4"):
            tile = TR4(reader, x, y)
        else:
            raise Exception("Unknown file type")

        tile.draw()

    return {"FINISHED"}


class ImportRun8TerrainTile(Operator, ImportHelper):
    bl_idname = ID
    bl_label = "Import Terrain Tile"

    filter_glob: StringProperty(
        default="*.tr2;*.tr2;*.tr4;*.ter",
        options={"HIDDEN"},
        maxlen=255,  # Max internal buffer length, longer would be clamped.
    )  # type: ignore

    def execute(self, context):
        return import_model(context, self.filepath)


CLASSES = (ImportRun8TerrainTile,)


def menu_func_import(self, context):
    self.layout.operator(ImportRun8TerrainTile.bl_idname, text="Run8 Terrain Tile (.tr2/.tr3/.tr4/.ter)")


MENU_ITEMS = ((bpy.types.TOPBAR_MT_file_import, menu_func_import),)
