# This program is free software; you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation; either version 3 of the License, or
# (at your option) any later version.
#
# This program is distributed in the hope that it will be useful, but
# WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTIBILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
# General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program. If not, see <http://www.gnu.org/licenses/>.

DEPENDENCIES = ["kaitaistruct"]

bl_info = {
    "name": "io_run8",
    "author": "Puyodead1",
    "description": "",
    "blender": (4, 1, 1),
    "version": (0, 0, 1),
    "location": "",
    "warning": "",
    "category": "Generic",
}

# def install_dependency(name):
#     """Installs a Python package using pip."""
#     try:
#         py_exec = sys.executable
#         # ensure pip is installed & update
#         subprocess.call([str(py_exec), "-m", "ensurepip", "--user"])
#         subprocess.call([str(py_exec), "-m", "pip", "install", "--upgrade", "pip"])
#         # install the package
#         subprocess.call([str(py_exec), "-m", "pip", "install", name])
#         print(f"Successfully installed {name}")
#     except subprocess.CalledProcessError as e:
#         raise Exception(f"Error installing {name}: {e}")


# def check_dependencies():
#     for module_name in DEPENDENCIES:
#         try:
#             __import__(module_name)
#         except ImportError:
#             try:
#                 print(f"Installing missing dependency: {module_name}")
#                 install_dependency(module_name)
#             except Exception as e:
#                 raise ImportError(f"Failed to install dependency '{module_name}': {e}")

if "bpy" in locals():
    print("bpy in locals")
    import importlib

    if "loaded" in locals():
        print("reloading modules")
        importlib.reload(terrain_importer)
        importlib.reload(model_importer)
else:
    print("first time import")
    import bpy
    from . import model_importer, terrain_importer

    loaded = True

ALL_CLASSES = (
    *terrain_importer.CLASSES,
    *model_importer.CLASSES,
)

ALL_MENU_ITEMS = (
    *terrain_importer.MENU_ITEMS,
    *model_importer.MENU_ITEMS,
)


def register():
    for cls in ALL_CLASSES:
        bpy.utils.register_class(cls)

    for menu, func in ALL_MENU_ITEMS:
        menu.append(func)


def unregister():
    for menu, func in reversed(ALL_MENU_ITEMS):
        try:
            menu.remove(func)
        except (ValueError, ReferenceError):
            pass

    for cls in reversed(ALL_CLASSES):
        try:
            bpy.utils.unregister_class(cls)
        except RuntimeError:
            pass
