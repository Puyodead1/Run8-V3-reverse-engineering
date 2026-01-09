from pathlib import Path
import sys
import bpy
import os
from bpy.utils import register_classes_factory
from bpy.props import CollectionProperty, StringProperty
from bpy.types import Operator, OperatorFileListElement
import clr


def import_track_database(filepath: str):
    INCLUDE_DIR = Path(__file__).parent / ".." / "lib"
    sys.path.append(str(INCLUDE_DIR))
    clr.AddReference("LibRun8")
    from LibRun8.Formats import TrackDatabase

    tdb = TrackDatabase.Read(filepath)
    for section in tdb.Sections:
        print(f"Section: {section.Index}")
        for node in section.Nodes:
            print(f"\tNode: {node.NodeIndex}, Pos: {node.Position}")


class IMPORT_SCENE_OT_run8_track_database(Operator):
    bl_idname = "import_scene.run8_track_database"
    bl_label = "Import"
    bl_options = {"REGISTER", "UNDO"}
    bl_description = "Import Track Database"
    filename_ext = ""
    REQUIRED_FILENAME = "TrackDatabase.r8"

    filter_glob: StringProperty(
        default=REQUIRED_FILENAME,
        options={"HIDDEN"},
    )

    filepath: StringProperty(
        default=REQUIRED_FILENAME,
        subtype="FILE_PATH",
    )

    def invoke(self, context, event):
        self.filepath = self.REQUIRED_FILENAME
        context.window_manager.fileselect_add(self)
        return {"RUNNING_MODAL"}

    def execute(self, context):
        selected_filename = os.path.basename(self.filepath)

        if selected_filename != self.REQUIRED_FILENAME:
            self.report({"ERROR"}, f"Must select file named '{self.REQUIRED_FILENAME}'")
            return {"CANCELLED"}

        print(f"Importing: {self.filepath}")
        import_track_database(self.filepath)

        return {"FINISHED"}


class IMPORT_SCENE_OT_run8_track_database_drag_and_drop(bpy.types.Operator):
    bl_idname = "import_scene.run8_track_database_drag_and_drop"
    bl_label = "Import Track Database"
    bl_options = {"REGISTER", "UNDO"}
    bl_description = "Import Track Database by dragging and dropping onto the 3D view"
    REQUIRED_FILENAME = "TrackDatabase.r8"

    directory: StringProperty(subtype="FILE_PATH", options={"SKIP_SAVE", "HIDDEN"})
    files: CollectionProperty(
        type=OperatorFileListElement, options={"SKIP_SAVE", "HIDDEN"}
    )

    @classmethod
    def poll(cls, context) -> bool:
        return context.area is not None and context.area.type == "VIEW_3D"

    def draw(self, context):
        # TODO: draw
        pass

    def invoke(self, context, event):
        context.window_manager.invoke_props_dialog(self)
        return {"RUNNING_MODAL"}

    def execute(self, context):
        for file_elem in self.files:
            if file_elem.name == self.REQUIRED_FILENAME:
                filepath = os.path.join(self.directory, file_elem.name)
                print(f"Importing: {filepath}")
                import_track_database(filepath)
                return {"FINISHED"}

        self.report({"ERROR"}, f"Not a valid Track Database file")
        return {"CANCELLED"}


_classes = (
    IMPORT_SCENE_OT_run8_track_database,
    IMPORT_SCENE_OT_run8_track_database_drag_and_drop,
)
register, unregister = register_classes_factory(_classes)
