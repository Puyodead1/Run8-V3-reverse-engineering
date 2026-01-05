import bpy
from bpy.props import StringProperty
from bpy.types import Operator
from bpy_extras.io_utils import ImportHelper
from kaitaistruct import KaitaiStream

from .model import Model

ID = "io_run8.import_model"


def import_model(context, filepath):
    print("\n" * 10)
    print("Importing Run8 3D Model: %r..." % (filepath))

    model = Model(filepath)
    model.draw()

    return {"FINISHED"}


class ImportRun8Model(Operator, ImportHelper):
    bl_idname = ID
    bl_label = "Import 3D Model"

    filter_glob: StringProperty(
        default="*.rn8",
        options={"HIDDEN"},
        maxlen=255,  # Max internal buffer length, longer would be clamped.
    )  # type: ignore

    def execute(self, context):
        return import_model(context, self.filepath)


CLASSES = (ImportRun8Model,)


def menu_func_import(self, context):
    self.layout.operator(ImportRun8Model.bl_idname, text="Run8 3D Model (.rn8)")


MENU_ITEMS = ((bpy.types.TOPBAR_MT_file_import, menu_func_import),)
