bl_info = {
    "name": "Run8 Tools",
    "author": "Puyodead1",
    "description": "Tools for Run8 Train Simulator",
    "blender": (3, 0, 0),
    "version": (1, 0, 0),
    # "location"      : "File > Import > Universal Multi Importer (File / Folder) | Object > Command Batcher",
    "warning": "",
    # "doc_url"       : "https://github.com/Tilapiatsu/blender-Universal_Multi_Importer",
    # "tracker_url"   : ("https://github.com/Tilapiatsu/blender-Universal_Multi_Importer/issues/new"),
    "support": "COMMUNITY",
    "category": "Import-Export",
}


from pathlib import Path
import sys
from .importer import trackdatabase

_needs_reload = "bpy" in locals()

if _needs_reload:
    import importlib

    importlib.reload(trackdatabase)

import bpy


def track_database_import_menu_func(self, context):
    self.layout.operator(
        trackdatabase.IMPORT_SCENE_OT_run8_track_database.bl_idname,
        text="Run8 Track Database (.r8)",
    )


_modules = (trackdatabase,)


def register():
    for module in _modules:
        module.register()

    bpy.types.TOPBAR_MT_file_import.append(track_database_import_menu_func)


def unregister():
    bpy.types.TOPBAR_MT_file_import.remove(track_database_import_menu_func)

    for module in reversed(_modules):
        module.unregister()


if __name__ == "__main__":
    register()
