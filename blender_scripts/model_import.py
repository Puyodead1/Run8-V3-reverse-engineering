from __future__ import annotations

import json
import math
import struct
from os import SEEK_CUR
from typing import BinaryIO

import bpy
import mathutils
from bpy.props import StringProperty
from bpy.types import Operator
from bpy_extras.io_utils import ImportHelper

# Some constants
VECTOR_FORWARDRH = mathutils.Vector((0.0, 0.0, -1.0))
VECTOR_UP = mathutils.Vector((0.0, 1.0, 0.0))
VECTOR_RIGHT = mathutils.Vector((1.0, 0.0, 0.0))
VECTOR_ZERO = mathutils.Vector((0.0, 0.0, 0.0))
ENDIAN_PREFIXES = ("@", "<", ">", "=", "!")


class Matrix(mathutils.Matrix):
    @property
    def M11(self) -> float:
        return self[0][0]

    @M11.setter
    def M11(self, value: float) -> None:
        self[0][0] = value

    @property
    def M12(self) -> float:
        return self[0][1]

    @M12.setter
    def M12(self, value: float) -> None:
        self[0][1] = value

    @property
    def M13(self) -> float:
        return self[0][2]

    @M13.setter
    def M13(self, value: float) -> None:
        self[0][2] = value

    @property
    def M14(self) -> float:
        return self[0][3]

    @M14.setter
    def M14(self, value: float) -> None:
        self[0][3] = value

    @property
    def M21(self) -> float:
        return self[1][0]

    @M21.setter
    def M21(self, value: float) -> None:
        self[1][0] = value

    @property
    def M22(self) -> float:
        return self[1][1]

    @M22.setter
    def M22(self, value: float) -> None:
        self[1][1] = value

    @property
    def M23(self) -> float:
        return self[1][2]

    @M23.setter
    def M23(self, value: float) -> None:
        self[1][2] = value

    @property
    def M24(self) -> float:
        return self[1][3]

    @M24.setter
    def M24(self, value: float) -> None:
        self[1][3] = value

    @property
    def M31(self) -> float:
        return self[2][0]

    @M31.setter
    def M31(self, value: float) -> None:
        self[2][0] = value

    @property
    def M32(self) -> float:
        return self[2][1]

    @M32.setter
    def M32(self, value: float) -> None:
        self[2][1] = value

    @property
    def M33(self) -> float:
        return self[2][2]

    @M33.setter
    def M33(self, value: float) -> None:
        self[2][2] = value

    @property
    def M34(self) -> float:
        return self[2][3]

    @M34.setter
    def M34(self, value: float) -> None:
        self[2][3] = value

    @property
    def M41(self) -> float:
        return self[3][0]

    @M41.setter
    def M41(self, value: float) -> None:
        self[3][0] = value

    @property
    def M42(self) -> float:
        return self[3][1]

    @M42.setter
    def M42(self, value: float) -> None:
        self[3][1] = value

    @property
    def M43(self) -> float:
        return self[3][2]

    @M43.setter
    def M43(self, value: float) -> None:
        self[3][2] = value

    @property
    def M44(self) -> float:
        return self[3][3]

    @M44.setter
    def M44(self, value: float) -> None:
        self[3][3] = value

    @staticmethod
    def identity_() -> Matrix:
        return Matrix()

    def rotation_quaternion(self, rotation: Quaternion) -> Matrix:
        """
        Creates a rotation matrix from a quaternion.
        """

        xx = rotation.x * rotation.x
        yy = rotation.y * rotation.y
        zz = rotation.z * rotation.z
        xy = rotation.x * rotation.y
        zw = rotation.z * rotation.w
        zx = rotation.z * rotation.x
        yw = rotation.y * rotation.w
        yz = rotation.y * rotation.z
        xw = rotation.x * rotation.w

        result = Matrix.identity_()
        result.M11 = 1.0 - (2.0 * (yy + zz))
        result.M12 = 2.0 * (xy + zw)
        result.M13 = 2.0 * (zx - yw)
        result.M21 = 2.0 * (xy - zw)
        result.M22 = 1.0 - (2.0 * (zz + xx))
        result.M23 = 2.0 * (yz + xw)
        result.M31 = 2.0 * (zx + yw)
        result.M32 = 2.0 * (yz - xw)
        result.M33 = 1.0 - (2.0 * (yy + xx))

        return result

    @staticmethod
    def rotation_yaw_pitch_roll(yaw: float, pitch: float, roll: float) -> Matrix:
        """
        Creates a rotation matrix with a specified yaw, pitch, and roll.
        """

        matrix = Matrix()
        quaternion = Quaternion.rotation_yaw_pitch_roll(yaw, pitch, roll)
        return matrix.rotation_quaternion(quaternion)

    @staticmethod
    def scaling(x: float, y: float, z: float) -> Matrix:
        result = Matrix.identity_()
        result.M11 = x
        result.M22 = y
        result.M33 = z

        return result


class Quaternion(mathutils.Quaternion):
    @staticmethod
    def identity_() -> Quaternion:
        return Quaternion((0, 0, 0, 1))

    @staticmethod
    def rotation_matrix(matrix: Matrix) -> Quaternion:
        """
        Creates a quaternion given a rotation matrix.
        """
        result = Quaternion()
        scale = matrix.M11 + matrix.M22 + matrix.M33

        if scale > 0.0:
            sqrt = math.sqrt(scale + 1.0)
            result.w = sqrt * 0.5
            sqrt = 0.5 / sqrt

            result.x = (matrix.M23 - matrix.M32) * sqrt
            result.y = (matrix.M31 - matrix.M13) * sqrt
            result.z = (matrix.M12 - matrix.M21) * sqrt
        elif (matrix.M11 >= matrix.M22) and (matrix.M11 >= matrix.M33):
            sqrt = math.sqrt(1.0 + matrix.M11 - matrix.M22 - matrix.M33)
            half = 0.5 / sqrt

            result.x = 0.5 * sqrt
            result.y = (matrix.M12 + matrix.M21) * half
            result.z = (matrix.M13 + matrix.M31) * half
            result.w = (matrix.M23 - matrix.M32) * half
        elif matrix.M22 > matrix.M33:
            sqrt = math.sqrt(1.0 + matrix.M22 - matrix.M11 - matrix.M33)
            half = 0.5 / sqrt

            result.x = (matrix.M21 + matrix.M12) * half
            result.y = 0.5 * sqrt
            result.z = (matrix.M32 + matrix.M23) * half
            result.w = (matrix.M31 - matrix.M13) * half
        else:
            sqrt = math.sqrt(1.0 + matrix.M33 - matrix.M11 - matrix.M22)
            half = 0.5 / sqrt

            result.x = (matrix.M31 + matrix.M13) * half
            result.y = (matrix.M32 + matrix.M23) * half
            result.z = 0.5 * sqrt
            result.w = (matrix.M12 - matrix.M21) * half

        return result

    @staticmethod
    def rotation_yaw_pitch_roll(yaw: float, pitch: float, roll: float) -> Quaternion:
        """
        Creates a quaternion given a yaw, pitch, and roll value.
        """

        result = Quaternion()

        half_roll = roll * 0.5
        half_pitch = pitch * 0.5
        half_yaw = yaw * 0.5

        sin_roll = math.sin(half_roll)
        cos_roll = math.cos(half_roll)
        sin_pitch = math.sin(half_pitch)
        cos_pitch = math.cos(half_pitch)
        sin_yaw = math.sin(half_yaw)
        cos_yaw = math.cos(half_yaw)

        result.x = (cos_yaw * sin_pitch * cos_roll) + (sin_yaw * cos_pitch * sin_roll)
        result.y = (sin_yaw * cos_pitch * cos_roll) - (cos_yaw * sin_pitch * sin_roll)
        result.z = (cos_yaw * cos_pitch * sin_roll) - (sin_yaw * sin_pitch * cos_roll)
        result.w = (cos_yaw * cos_pitch * cos_roll) + (sin_yaw * sin_pitch * sin_roll)

        return result


class BinaryReader:
    def __init__(self, buf: BinaryIO, endian: str = "<") -> None:
        self.buf = buf
        self.endian = endian

    def align(self) -> None:
        old = self.tell()
        new = (old + 3) & -4
        if new > old:
            self.seek(new - old, SEEK_CUR)

    def read(self, *args) -> bytes:
        return self.buf.read(*args)

    def seek(self, *args) -> int:
        return self.buf.seek(*args)

    def tell(self) -> int:
        return self.buf.tell()

    def read_string(self, size: int = None, encoding: str = "utf-8") -> str:
        if size is None:
            ret = self.read_cstring()
        else:
            ret = struct.unpack(self.endian + "%is" % (size), self.read(size))[0]

        return ret.decode(encoding)

    def read_cstring(self) -> bytes:
        ret = []
        c = b""
        while c != b"\0":
            ret.append(c)
            c = self.read(1)
            if not c:
                raise ValueError("Unterminated string: %r" % (ret))
        return b"".join(ret)

    def read_7bit_encoded_int(self) -> int:
        value = 0
        shift = 0
        while True:
            val = ord(self.read(1))
            if val & 128 == 0:
                break
            value |= (val & 0x7F) << shift
            shift += 7
        return value | (val << shift)

    def read_cs_string(self) -> str:
        size = self.read_7bit_encoded_int()
        return self.read_string(size)

    def read_bool(self) -> bool:
        return bool(struct.unpack(self.endian + "b", self.read(1))[0])

    def read_byte(self) -> int:
        return struct.unpack(self.endian + "b", self.read(1))[0]

    def read_ubyte(self) -> int:
        return struct.unpack(self.endian + "B", self.read(1))[0]

    def read_int16(self) -> int:
        return struct.unpack(self.endian + "h", self.read(2))[0]

    def read_uint16(self) -> int:
        return struct.unpack(self.endian + "H", self.read(2))[0]

    def read_int32(self) -> int:
        return struct.unpack(self.endian + "i", self.read(4))[0]

    def read_uint32(self) -> int:
        return struct.unpack(self.endian + "I", self.read(4))[0]

    def read_int64(self) -> int:
        return struct.unpack(self.endian + "q", self.read(8))[0]

    def read_uint64(self) -> int:
        return struct.unpack(self.endian + "Q", self.read(8))[0]

    def read_float(self) -> float:
        return struct.unpack(self.endian + "f", self.read(4))[0]

    def read_double(self) -> float:
        return struct.unpack(self.endian + "d", self.read(8))[0]

    def read_struct(self, format: str) -> tuple:
        if not format.startswith(ENDIAN_PREFIXES):
            format = self.endian + format
        size = struct.calcsize(format)
        return struct.unpack(format, self.read(size))

    # Aliases
    def read_int(self) -> int:
        return self.read_int32()

    def read_uint(self) -> int:
        return self.read_uint32()

    # customs
    def read_vector3(self) -> mathutils.Vector:
        return mathutils.Vector((self.read_float(), self.read_float(), self.read_float()))

    def read_vector2(self) -> mathutils.Vector:
        return mathutils.Vector((self.read_float(), self.read_float()))

    def read_matrix4x4(self) -> Matrix:
        return Matrix(
            (
                (self.read_float(), self.read_float(), self.read_float(), self.read_float()),
                (self.read_float(), self.read_float(), self.read_float(), self.read_float()),
                (self.read_float(), self.read_float(), self.read_float(), self.read_float()),
                (self.read_float(), self.read_float(), self.read_float(), self.read_float()),
            )
        )


    def read_rotation_quaternion(self) -> mathutils.Euler:
        x = self.read_float()
        y = self.read_float()
        z = self.read_float()

        return mathutils.Euler((x, y, z)).to_quaternion()

    def read_scaling_matrix(self) -> Matrix:
        x = self.read_float()
        y = self.read_float()
        z = self.read_float()

        return Matrix.scaling(x, y, z)


class Class141:
    index_count: int
    start_index_location: int
    base_vertex_location: int
    texture_0: str
    texture_1: str
    texture_2: str


class Vertex:
    position: mathutils.Vector
    normal: mathutils.Vector
    uv: mathutils.Vector
    tangent: mathutils.Vector
    binormal: mathutils.Vector


class Class249:
    quaternion_0: list[Quaternion]
    quaternion_1: Quaternion
    vector3_0: list[mathutils.Vector]
    vector3_1: mathutils.Vector


class ModelObject:
    _reader: BinaryReader
    flag: bool = False
    name: str = ""
    parent_name: str = ""
    vector3_1: mathutils.Vector = VECTOR_ZERO
    position: mathutils.Vector = VECTOR_ZERO
    vector3_3: mathutils.Vector = VECTOR_ZERO
    quaternion_1: Quaternion = Quaternion.identity_()
    quaternion_2: Quaternion = Quaternion.identity_()
    matrix_array_1: list[Matrix] = None
    matrix_array_2: list[Matrix] = None
    class_249_0: Class249 = None
    vertices: list[Vertex]
    textures: list[str]
    index_buffer: list[int]
    class141s: list[Class141] = []

    def __init__(self, reader: BinaryReader, flag: bool, model: Model):
        self._reader = reader
        self.flag = flag
        self.read(model)

    def read(self, model: Model):
        if self.flag:
            self.name = self._reader.read_cs_string()
            self.parent_name = self._reader.read_cs_string()
            self.vector3_3 = self._reader.read_vector3()
            self.vector3_1 = self._reader.read_vector3()
            self.quaternion_1 = self._reader.read_rotation_quaternion()
            self._reader.read_scaling_matrix()  # unused
            self._reader.read_rotation_quaternion()  # unused
            self._reader.read_rotation_quaternion()  # unused
            # run8 uses Y-Up, Blender uses Z-Up, manually read these values and convert
            position_x = self._reader.read_float()
            position_y = self._reader.read_float()
            position_z = self._reader.read_float()
            self.position = mathutils.Vector((position_x, -position_z, -position_y))
            self.quaternion_2 = self._reader.read_rotation_quaternion()
            self._reader.read_scaling_matrix()  # unused
            num_matrix_1 = self._reader.read_int32()

            # read matrix array 1
            self.matrix_array_1 = [None for i in range(num_matrix_1)]
            for i in range(num_matrix_1):
                if self.class_249_0 == None:
                    self.class_249_0 = Class249()
                    self.class_249_0.quaternion_0 = [None for i in range(num_matrix_1)]
                    self.class_249_0.vector3_0 = [None for i in range(num_matrix_1)]
                self.matrix_array_1[i] = self._reader.read_matrix4x4()

            num_matrix_2 = self._reader.read_int32()

            # read matrix array 2
            self.matrix_array_2 = [None for i in range(num_matrix_2)]
            for i in range(num_matrix_2):
                self.matrix_array_2[i] = self._reader.read_matrix4x4()

            if num_matrix_2 != num_matrix_1:
                self.class_249_0 = None
            else:
                for i in range(num_matrix_2):
                    self.class_249_0.quaternion_0[i] = Quaternion.rotation_matrix(self.matrix_array_2[i])
                    self.class_249_0.vector3_0[i] = self.matrix_array_2[i].to_translation()
        else:
            self.name = ""
            self.parent_name = ""
            self.vector3_3 = VECTOR_ZERO

        num_vertices = int(self._reader.read_int32() / 7)
        self.vertices = []
        for i in range(num_vertices):
            vertex = Vertex()
            self._reader.read_float()  # unused
            position_x = self._reader.read_float() * 63.7 - self.vector3_3.x
            normal_y = self._reader.read_float() / -1.732
            position_z = self._reader.read_float() / 16 - self.vector3_3.z
            uv_x = self._reader.read_float() / 4.8
            normal_x = self._reader.read_float() / 10.962
            self._reader.read_float()  # unused
            normal_z = self._reader.read_float() / 11.432
            uv_y = self._reader.read_float() / 9.6
            position_y = -self._reader.read_float() * 6 - self.vector3_3.y

            vertex.position = mathutils.Vector((position_x, position_y, position_z))
            vertex.normal = mathutils.Vector((normal_x, normal_y, normal_z))
            vertex.uv = mathutils.Vector((uv_x, uv_y))
            vertex.binormal = VECTOR_ZERO
            vertex.tangent = VECTOR_ZERO

            self.vertices.append(vertex)

            num6 = max(abs(vertex.position.x), max(abs(vertex.position.y), abs(vertex.position.z)))
            if num6 > model.bounding_sphere_radius:
                model.bounding_sphere_radius = num6
                print("Set bounding sphere radius to: " + str(model.bounding_sphere_radius))

        num_textures = self._reader.read_int32() + 6
        self.textures = []
        for i in range(num_textures):
            self.textures.append(self._reader.read_cs_string())

        is_ushort = self._reader.read_bool()
        num_indices = self._reader.read_int32()
        self.index_buffer = [None for i in range(num_indices)]
        for i in range(num_indices):
            self.index_buffer[i] = self._reader.read_int32()
            # original code then calculates binormals and tangents for each face, but we don't need that

        num5 = self._reader.read_int32() - 9
        flag3 = False
        if num5 == 0:
            class141 = Class141()
            class141.index_count = len(self.index_buffer)
            class141.base_vertex_location = 0
            class141.start_index_location = 0
            self.class141s.append(class141)
        else:
            for i in range(num5):
                class141 = Class141()
                self._reader.read_float()  # unused
                texture_index = self._reader.read_int32()
                if len(self.textures) > 0:
                    texture = self.textures[texture_index]
                    class141.texture_0 = texture
                    class141.texture_1 = texture + "_mrao"
                    class141.texture_2 = texture + "_normal"
                    flag3 |= class141.texture_2 != None
                class141.index_count = self._reader.read_int32()
                class141.start_index_location = self._reader.read_int32()
                class141.base_vertex_location = self._reader.read_int32()
                self.class141s.append(class141)

        # if flag3, creates a new VBO of with Vertex, else converts to SharpDX VertexPositionNormalTexture


class Model:
    _reader: BinaryReader
    object_count: int = 1
    flag: bool = False
    matrix_0: Matrix = Matrix.identity_()
    vector3_0: mathutils.Vector = VECTOR_ZERO
    objects: list[ModelObject] = []
    bounding_sphere_radius: float = 0.0

    def __init__(self, reader: BinaryReader):
        self._reader = reader
        file_type = reader.read_int32()
        if file_type == -969696:
            self._read_969696()
        elif file_type == -969697:
            self._read_loco()
        else:
            # reset the reader to the start of the file
            self._reader.seek(0)
        self._read()

    def _read_969696(self):
        """
        reads "header" for -969696 files
        """
        print("Found -969696 header")
        self.object_count = self._reader.read_int32()
        self.flag = True

    def _read_loco(self):
        """
        reads "header" for -969697 files (seems to always be locomotives)
        """
        print("Found -979797 (Locomotive) header")
        self.object_count = self._reader.read_int32()
        self.vector3_0 = self._reader.read_vector3()
        self.flag = True

    def _read(self):
        for i in range(self.object_count):
            model_object = ModelObject(self._reader, self.flag, self)
            self.objects.append(model_object)


def create_mesh(model_object: ModelObject):
    vertices = [x.position.to_tuple() for x in model_object.vertices]
    normals = [x.normal.to_tuple() for x in model_object.vertices]
    uvs = [x.uv.to_tuple() for x in model_object.vertices]
    indices = model_object.index_buffer

    # Create a new mesh object
    mesh = bpy.data.meshes.new(model_object.name)
    mesh_obj = bpy.data.objects.new(model_object.name, mesh)
    bpy.context.collection.objects.link(mesh_obj)
    bpy.context.view_layer.objects.active = mesh_obj
    mesh_obj.select_set(True)

    # if there is a parent, set it
    if model_object.parent_name != "":
        mesh_obj.parent = bpy.data.objects[model_object.parent_name]

    # create faces
    faces = []
    for i in range(0, len(indices), 3):
        faces.append((indices[i], indices[i + 1], indices[i + 2]))

    # Assign vertex data to the mesh using from_pydata
    mesh.from_pydata(vertices, [], faces)

    # Convert Y-Up to Z-Up
    for vertex in mesh.vertices:
        # Swap the Y and Z coordinates
        vertex.co[1], vertex.co[2] = vertex.co[2], -vertex.co[1]

    # assign normals
    mesh.normals_split_custom_set_from_vertices(normals)

    # Assign UVs to the mesh
    uv_layer = mesh.uv_layers.new()
    mesh.uv_layers.active = uv_layer

    for face in mesh.polygons:
        for vert_idx, loop_idx in zip(face.vertices, face.loop_indices):
            uv_layer.data[loop_idx].uv = (uvs[vert_idx][0], -uvs[vert_idx][1])  # flip the V coordinate

    # apply rotation
    mesh_obj.rotation_mode = "QUATERNION"
    mesh_obj.rotation_quaternion = model_object.quaternion_2

    # apply position
    mesh_obj.location = model_object.position

    # back to object mode
    bpy.ops.object.mode_set(mode="OBJECT")

    for texture in model_object.textures:
        print(texture)


def import_model(context, filepath):
    print("\n" * 10)
    print("Importing Run8 model: %r..." % (filepath))

    with open(filepath, "rb") as f:
        reader = BinaryReader(f)
        model = Model(reader)

        for i, model_object in enumerate(model.objects):
            create_mesh(model_object)

    return {"FINISHED"}


class ImportRun8Model(Operator, ImportHelper):
    """This appears in the tooltip of the operator and in the generated docs"""

    bl_idname = "run8_lib.import_model"
    bl_label = "Import Model"

    # ImportHelper mixin class uses this
    filename_ext = ".rn8"

    filter_glob: StringProperty(
        default="*.rn8",
        options={"HIDDEN"},
        maxlen=255,  # Max internal buffer length, longer would be clamped.
    )

    def execute(self, context):
        return import_model(context, self.filepath)


def run8_model_menu_func_import(self, context):
    self.layout.operator(ImportRun8Model.bl_idname, text="Run8 Model (.rn8)")


def register():
    bpy.utils.register_class(ImportRun8Model)
    # prevent duplicate menu entries
    if hasattr(bpy.types.TOPBAR_MT_file_import.draw, "_draw_funcs"):
        if run8_model_menu_func_import.__name__ not in (f.__name__ for f in bpy.types.TOPBAR_MT_file_import.draw._draw_funcs):
            bpy.types.TOPBAR_MT_file_import.append(run8_model_menu_func_import)
    else:
        bpy.types.TOPBAR_MT_file_import.append(run8_model_menu_func_import)


def unregister():
    clazz = bpy.types.NodeTree.bl_rna_get_subclass_py("run8_lib.import_model")
    bpy.utils.unregister_class(clazz)
    bpy.types.TOPBAR_MT_file_import.remove(run8_model_menu_func_import)


if __name__ == "__main__":
    register()
