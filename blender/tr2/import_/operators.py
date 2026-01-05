import os
from pathlib import Path

from bpy.props import StringProperty
from bpy.types import Context, Operator
from bpy_extras.io_utils import ImportHelper
from ..importer import import_tr2


class TR2_OT_Import(Operator, ImportHelper):
    bl_idname = "tr2.import"
    bl_label = "Import"
    bl_options = {"INTERNAL", "UNDO", "PRESET"}
    bl_description = "Import a TR2 file"
    filename_ext = ".tr2"
    filter_glob: StringProperty(default="*.tr2", options={"HIDDEN"})
    filepath: StringProperty(
        name="File Path",
        description="Filepath used for importing the TR2 file",
        maxlen=1024,
        default="",
    )

    def execute(self, context: Context):
        print("Execute TR2 import")
        try:
            tr2 = read_tr2_from_file(self.filepath)
        except OSError as e:
            self.report({"ERROR"}, f"Failed to read TR2 file: {e}")
            return {"CANCELLED"}

        name = os.path.splitext(os.path.basename(self.filepath))[0]
        result = import_tr2(tr2, context, name)

        return {"FINISHED"}
