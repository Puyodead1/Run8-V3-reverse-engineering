import math
import random
import struct
from io import SEEK_CUR
from typing import BinaryIO

import bpy
import mathutils
from bpy.props import StringProperty
from bpy.types import Operator
from bpy_extras.io_utils import ExportHelper

ENDIAN_PREFIXES = ("@", "<", ">", "=", "!")
# create a new rotation matrix to rotate -90 degrees on the X axis
rotation_matrix = mathutils.Matrix.Rotation(math.radians(-90), 4, "X")

def matrix_to_flat_list(matrix):
    return [matrix[i][j] for j in range(4) for i in range(4)] 

class BinaryWriter:
    def __init__(self, buf: BinaryIO, endian: str = "<") -> None:
        self.buf = buf
        self.endian = endian

    def align(self) -> None:
        old = self.tell()
        new = (old + 3) & -4
        if new > old:
            self.seek(new - old, SEEK_CUR)

    def write(self, *args) -> int:
        return self.buf.write(*args)

    def seek(self, *args) -> int:
        return self.buf.seek(*args)

    def tell(self) -> int:
        return self.buf.tell()

    def write_string(self, value: str, encoding: str = "utf-8") -> int:
        """
        Writes a 7 bit prefixed string to the buffer.
        """
        encoded_string = value.encode(encoding)
        size = self.write_7bit_encoded_int(len(encoded_string))
        size += self.write(encoded_string)

        return size

    def write_cstring(self, value: str) -> int:
        return self.write_string(value + "\0")

    def write_7bit_encoded_int(self, value: int) -> int:
        while value >= 128:
            self.write(bytes([((value & 0x7F) | 0x80)]))
            value >>= 7
        return self.write(bytes([value]))

    def write_bool(self, value: bool) -> int:
        return self.write(struct.pack(self.endian + "b", int(value)))

    def write_byte(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "b", value))

    def write_ubyte(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "B", value))

    def write_int16(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "h", value))

    def write_uint16(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "H", value))

    def write_int32(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "i", value))

    def write_uint32(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "I", value))

    def write_int64(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "q", value))

    def write_uint64(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "Q", value))

    def write_float(self, value: float) -> int:
        return self.write(struct.pack(self.endian + "f", value))
    
    def write_random_float(self) -> int:
        f = random.random()
        return self.write_float(f)

    def write_double(self, value: float) -> int:
        return self.write(struct.pack(self.endian + "d", value))

    def write_struct(self, format: str, *values) -> int:
        if not format.startswith(ENDIAN_PREFIXES):
            format = self.endian + format
        data = struct.pack(format, *values)
        return self.write(data)

    # Aliases
    def write_int(self, value: int) -> int:
        return self.write_int32(value)

    def write_uint(self, value: int) -> int:
        return self.write_uint32(value)

    # customs
    def write_vector(self, vector: mathutils.Vector) -> int:
        values = vector.to_tuple()
        return self.write_struct("f" * len(values), *values)

    def write_matrix(self, matrix: mathutils.Matrix) -> int:
        values = matrix_to_flat_list(matrix)
        return self.write_struct("f" * len(values), *values)


class Vertex:
    position: mathutils.Vector = None
    normal: mathutils.Vector = None
    uv: mathutils.Vector = None

def is_mesh_object(obj):
    return obj.type == "MESH"

def write(context, filepath):
    obj = bpy.context.active_object
    if obj is None:
        raise Exception("No active object")

    if not is_mesh_object(obj):
        raise Exception("Object is not a mesh")
    mesh = obj.data

    vertices = [v.co for v in mesh.vertices]
    normals = [v.normal for v in mesh.vertices]
    uvs = [v.uv for v in mesh.uv_layers[0].data]
    indices = [i for p in mesh.polygons for i in p.vertices]

    print("Index count: " + str(len(indices)))
    print("Vertex count: " + str(len(vertices)))

    with open(filepath, "wb") as f:
        writer = BinaryWriter(f)

        # for -97 and -96, position should be `rotation_matrix @ Vector((position_x, position_y, position_z))`

        # write vertex count
        writer.write_int32(len(vertices) * 7)
        
        # write vertices
        for vertex, normal, uv in zip(vertices, normals, uvs):
            writer.write_random_float() # unused float
            writer.write_float(vertex.x / 63.7) # write pos x
            writer.write_float(normal.y * -1.732) # write normal y
            writer.write_float(vertex.y * 16) # write pos y (swapped with z)
            writer.write_float(uv.x * 4.8) # write uv x
            writer.write_float(normal.x * 10.962) # write normal x
            writer.write_random_float() # unused float
            writer.write_float(normal.z * 11.432) # write normal z
            writer.write_float(uv.y * 9.6) # write uv y (inverted)
            writer.write_float(-vertex.z / 6) # write pos z (swapped with y)

        # write texture count, 0 for now
        writer.write_int32(1 - 6)

        textures = ["test_texture"]
        for texture in textures:
            writer.write_string(texture)

        # write is_ushort
        writer.write_bool(False)

        # write index count
        writer.write_int32(len(indices))

        # write indices
        for index in indices:
            writer.write_int32(index)

        # write unknown count, 0 for now
        writer.write_int32(1 + 9)

        # write unknowns
        writer.write_random_float()
        writer.write_int32(0)
        writer.write_int32(len(indices))
        writer.write_int32(0)
        writer.write_int32(0)

    return {"FINISHED"}


class ExportRun8Model(Operator, ExportHelper):
    bl_idname = "run8_lib.export_model"
    bl_label = "Export Model"

    filename_ext = ".rn8"

    filter_glob: StringProperty(
        default="*.rn8",
        options={"HIDDEN"},
        maxlen=255,  # Max internal buffer length, longer would be clamped.
    )

    def execute(self, context):
        return write(context, self.filepath)


def run8_model_menu_func_export(self, context):
    self.layout.operator(ExportRun8Model.bl_idname, text="Run8 Model (.rn8)")


def register():
    bpy.utils.register_class(ExportRun8Model)

    # prevent duplicate menu entries
    if hasattr(bpy.types.TOPBAR_MT_file_export.draw, "_draw_funcs"):
        if run8_model_menu_func_export.__name__ not in (f.__name__ for f in bpy.types.TOPBAR_MT_file_export.draw._draw_funcs):
            bpy.types.TOPBAR_MT_file_export.append(run8_model_menu_func_export)
    else:
        bpy.types.TOPBAR_MT_file_export.append(run8_model_menu_func_export)


def unregister():
    clazz = bpy.types.NodeTree.bl_rna_get_subclass_py("run8_lib.export_model")
    bpy.utils.unregister_class(clazz)
    bpy.types.TOPBAR_MT_file_export.remove(run8_model_menu_func_export)


if __name__ == "__main__":
    register()