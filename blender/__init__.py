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

from .tr2.import_ import operators as tr2_import_operators

_needs_reload = "bpy" in locals()
if _needs_reload:
    import importlib

    importlib.reload(tr2_import_operators)

import bpy
from bpy.props import PointerProperty


def tr2_import_menu_func(self, context):
    self.layout.operator(
        tr2_import_operators.TR2_OT_Import.bl_idname, text="TR2 (.tr2)"
    )


_modules = (tr2_import_operators,)


def register():
    for module in _modules:
        module.register()
    # bpy.types.TOPBAR_MT_file_export.append(psk_export_menu_func)
    bpy.types.TOPBAR_MT_file_import.append(tr2_import_operators)


def unregister():
    # bpy.types.TOPBAR_MT_file_export.remove(psk_export_menu_func)
    bpy.types.TOPBAR_MT_file_import.remove(tr2_import_operators)
    # bpy.types.TOPBAR_MT_file_export.remove(psa_export_menu_func)
    # bpy.types.TOPBAR_MT_file_import.remove(psa_import_menu_func)
    for module in reversed(_modules):
        module.unregister()


if __name__ == "__main__":
    register()
