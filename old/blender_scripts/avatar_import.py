import math
import struct

import bpy
import mathutils
from bpy.props import StringProperty
from bpy.types import Operator
from bpy_extras.io_utils import ImportHelper


class UnknownStruct(object):
    texture_index: int = None
    index_buffer_size: int = None
    start_index_location: int = None
    base_vertex_location: int = None


class AnimationKeyframe(object):
    bone: int = None
    time: float = None
    transform: mathutils.Matrix = None


class Animation(object):
    key: str = None
    duration: float = None
    keyframes: list[AnimationKeyframe] = []


class BoneIndex(object):
    key: str = None
    bone: int = None


class Avatar(object):
    verticies: list[tuple[float, float, float]] = []
    normals: list[tuple[float, float, float]] = []
    uvs: list[tuple[float, float]] = []
    blend_weights: list[tuple[float, float, float, float]] = []
    blend_indices: list[tuple[int, int, int, int]] = []
    textures: list[str] = []
    is_ushort: bool = False
    index_buffer: list[int] = []
    unknown_structs: list[UnknownStruct] = []
    skeleton_hierarchy: list[int] = []
    bone_indices: list[BoneIndex] = []
    bind_poses: list[mathutils.Matrix] = []
    inverse_bind_poses: list[mathutils.Matrix] = []
    animations: list[Animation] = []
    bind_poses_model_space: list[mathutils.Matrix] = []


def import_mesh(vertices: list[tuple[float, float, float]], normals: list[tuple[float, float, float]], uvs: list[tuple[float, float]], index_buffer: list[int]):
    scaling_matrix = mathutils.Matrix.Scale(0.95, 4)

    # Create a new mesh object
    mesh = bpy.data.meshes.new("Mesh")
    mesh_obj = bpy.data.objects.new("MeshObject", mesh)
    bpy.context.collection.objects.link(mesh_obj)
    bpy.context.view_layer.objects.active = mesh_obj
    mesh_obj.select_set(True)

    # Create vertices and faces using from_pydata
    faces = []

    for i in range(0, len(index_buffer), 3):
        v1_index, v2_index, v3_index = index_buffer[i : i + 3]
        a = [v1_index, v2_index, v3_index]
        print("Face: " + str(a))
        faces.append(a)

    # Assign vertex data to the mesh using from_pydata
    mesh.from_pydata(vertices, [], faces)

    # apply scaling matrix
    mesh.transform(scaling_matrix)

    mesh.update()

    # Assign per-vertex normals to the mesh
    normals_flat = [n for normal in normals for n in normal]
    mesh.vertices.foreach_set("normal", normals_flat)

    # Assign UVs to the mesh
    uv_layer = mesh.uv_layers.new()
    mesh.uv_layers.active = uv_layer

    for face in mesh.polygons:
        for vert_idx, loop_idx in zip(face.vertices, face.loop_indices):
            uv_layer.data[loop_idx].uv = (uvs[vert_idx][0], -uvs[vert_idx][1])  # y needs to be inverted

    # rotate upright
    rotation_angle = math.radians(-90)
    mesh_obj.rotation_euler[0] = rotation_angle
    mesh_obj.update_tag()

    return (mesh, mesh_obj)


def import_avatar(context, filepath):
    print("\n" * 10)

    # when no object exists, or when we are in edit mode when script is run
    try:
        bpy.ops.object.mode_set(mode="OBJECT")
    except:
        pass

    avatar = Avatar()
    with open(filepath, "rb") as f:
        stream = BinaryReader(f.read())

        vertex_count = stream.read(int) / 7
        for i in range(int(vertex_count)):
            stream.read_float()
            position_x = stream.read_float() * 63.7
            normal_y = stream.read_float() / -1.732
            position_z = stream.read_float() / 16
            uv_x = stream.read_float() / 4.8
            normal_x = stream.read_float() / 10.962
            stream.read_float()
            normal_z = stream.read_float() / 11.432
            uv_y = stream.read_float() / 9.6
            position_y = stream.read_float() * 6
            blend_indices_w = stream.read_byte()
            blend_weight_z = stream.read_float()
            blend_indices_x = stream.read_byte()
            blend_weight_y = stream.read_float()
            blend_indices_y = stream.read_byte()
            blend_weight_w = stream.read_float()
            blend_indices_z = stream.read_byte()
            blend_weight_x = stream.read_float()

            avatar.verticies.append((position_x, position_y, position_z))
            avatar.normals.append((normal_x, normal_y, normal_z))
            avatar.uvs.append((uv_x, uv_y))
            avatar.blend_indices.append((blend_indices_x, blend_indices_y, blend_indices_z, blend_indices_w))
            avatar.blend_weights.append((blend_weight_x, blend_weight_y, blend_weight_z, blend_weight_w))

        texture_count = stream.read(int) + 6
        for i in range(texture_count):
            avatar.textures.append(stream.read(str).decode())

        avatar.is_ushort = stream.read(bool)  # its still read as an int32, this is only used for drawing

        index_buffer_size = stream.read(int)
        for i in range(index_buffer_size):
            avatar.index_buffer.append(stream.read(int))

        unknown_struct_count = stream.read(int) - 9
        for i in range(unknown_struct_count):
            unknown_struct = UnknownStruct()
            stream.read(int)
            unknown_struct.texture_index = stream.read(int)
            unknown_struct.index_buffer_size = stream.read(int)
            unknown_struct.start_index_location = stream.read(int)
            unknown_struct.base_vertex_location = stream.read(int)
            avatar.unknown_structs.append(unknown_struct)

        skeleton_hierarchy_count = stream.read(int)
        for i in range(skeleton_hierarchy_count):
            avatar.skeleton_hierarchy.append(stream.read(int))

        bone_index_count = stream.read(int)
        for i in range(bone_index_count):
            bone_index = BoneIndex()
            bone_index.key = stream.read(str).decode()
            bone_index.bone = stream.read(int)
            avatar.bone_indices.append(bone_index)

        bind_pose_count = stream.read(int)
        for i in range(bind_pose_count):
            m11 = stream.read_float()
            m12 = stream.read_float()
            m13 = stream.read_float()
            m14 = stream.read_float()

            m21 = stream.read_float()
            m22 = stream.read_float()
            m23 = stream.read_float()
            m24 = stream.read_float()

            m31 = stream.read_float()
            m32 = stream.read_float()
            m33 = stream.read_float()
            m34 = stream.read_float()

            m41 = stream.read_float()
            m42 = stream.read_float()
            m43 = stream.read_float()
            m44 = stream.read_float()

            a = ((m11, m12, m13, m14), (m21, m22, m23, m24), (m31, m32, m33, m34), (m41, m42, m43, m44))
            bind_pose = mathutils.Matrix(a)
            avatar.bind_poses.append(bind_pose.to_4x4())

        invsere_bind_pose_count = stream.read(int)
        for i in range(invsere_bind_pose_count):
            m11 = stream.read_float()
            m12 = stream.read_float()
            m13 = stream.read_float()
            m14 = stream.read_float()

            m21 = stream.read_float()
            m22 = stream.read_float()
            m23 = stream.read_float()
            m24 = stream.read_float()

            m31 = stream.read_float()
            m32 = stream.read_float()
            m33 = stream.read_float()
            m34 = stream.read_float()

            m41 = stream.read_float()
            m42 = stream.read_float()
            m43 = stream.read_float()
            m44 = stream.read_float()

            inverse_bind_pose = mathutils.Matrix(((m11, m12, m13, m14), (m21, m22, m23, m24), (m31, m32, m33, m34), (m41, m42, m43, m44)))
            avatar.inverse_bind_poses.append(inverse_bind_pose)

        animation_count = stream.read(int)
        for i in range(animation_count):
            animation = Animation()
            animation.key = stream.read(str).decode()
            animation.duration = stream.read_double()
            keyframe_count = stream.read(int)
            for j in range(keyframe_count):
                keyframe = AnimationKeyframe()
                keyframe.bone = stream.read(int)
                keyframe.time = stream.read_double()

                m11 = stream.read_float()
                m12 = stream.read_float()
                m13 = stream.read_float()
                m14 = stream.read_float()

                m21 = stream.read_float()
                m22 = stream.read_float()
                m23 = stream.read_float()
                m24 = stream.read_float()

                m31 = stream.read_float()
                m32 = stream.read_float()
                m33 = stream.read_float()
                m34 = stream.read_float()

                m41 = stream.read_float()
                m42 = stream.read_float()
                m43 = stream.read_float()
                m44 = stream.read_float()

                transform = mathutils.Matrix(((m11, m12, m13, m14), (m21, m22, m23, m24), (m31, m32, m33, m34), (m41, m42, m43, m44)))
                keyframe.transform = transform.to_4x4()

    (mesh, mesh_obj) = import_mesh(avatar.verticies, avatar.normals, avatar.uvs, avatar.index_buffer)

    return {"FINISHED"}


class ImportRun8Avatar(Operator, ImportHelper):
    """This appears in the tooltip of the operator and in the generated docs"""

    bl_idname = "run8_lib.import_avatar"
    bl_label = "Import Avatar"

    # ImportHelper mixin class uses this
    filename_ext = ".rn8"

    filter_glob: StringProperty(
        default="*.rn8",
        options={"HIDDEN"},
        maxlen=255,  # Max internal buffer length, longer would be clamped.
    )

    def execute(self, context):
        return import_avatar(context, self.filepath)


def run8_avatar_menu_func_import(self, context):
    self.layout.operator(ImportRun8Avatar.bl_idname, text="Run8 Avatar")


def register():
    bpy.utils.register_class(ImportRun8Avatar)
    # prevent duplicate menu entries
    if hasattr(bpy.types.TOPBAR_MT_file_import.draw, "_draw_funcs"):
        if run8_avatar_menu_func_import.__name__ not in (f.__name__ for f in bpy.types.TOPBAR_MT_file_import.draw._draw_funcs):
            bpy.types.TOPBAR_MT_file_import.append(run8_avatar_menu_func_import)
    else:
        bpy.types.TOPBAR_MT_file_import.append(run8_avatar_menu_func_import)


def unregister():
    clazz = bpy.types.NodeTree.bl_rna_get_subclass_py("run8_lib.import_avatar")
    bpy.utils.unregister_class(clazz)
    bpy.types.TOPBAR_MT_file_import.remove(run8_avatar_menu_func_import)


if __name__ == "__main__":
    register()


# TODO: move this to a separate file
class BinaryStream(object):
    _types = {
        int: struct.Struct("<i"),
        float: struct.Struct("<f"),
        bool: struct.Struct("<?"),
    }
    _formats = {
        int: "i",
        float: "f",
        bool: "?",
    }


class BinaryReader(BinaryStream):
    _sizes = {
        int: 4,
        float: 4,
        bool: 1,
    }

    def __init__(self, serial):
        self.stream = serial
        self.index = 0

    def read(self, type):
        try:
            value = self._types[type].unpack_from(self.stream, self.index)[0]
            self.index += self._sizes[type]
        except KeyError:
            if type == str:
                size = self.read_7bit_int()
                value = self.stream[self.index : self.index + size]
                self.index += size
            elif hasattr(type, "load"):
                value = type.load(self.read(str))
            else:
                raise
        return value

    def next(self, count):
        return self.stream[self.index : self.index + count]

    def pull(self, count):
        s = self.stream[self.index : self.index + count]
        self.index += count
        return s

    def remainder(self):
        v = self.stream[self.index :]
        self.index = len(self.stream)
        return v

    def peek(self, type):
        index = self.index
        value = self.read(type)
        self.index = index
        return value

    def peek_list(self, type):
        index = self.index
        value = self.read_list(type)
        self.index = index
        return value

    def peek_dict(self, k, v):
        index = self.index
        value = self.read_dict(k, v)
        self.index = index
        return value

    def read_double(self):
        return struct.unpack("d", self.pull(8))[0]

    def read_float(self) -> float:
        return self.read(float)

    def read_byte(self):
        return struct.unpack("b", self.pull(1))[0]

    def read_7bit_int(self):
        value = 0
        shift = 0
        while True:
            val = ord(self.pull(1))
            if val & 128 == 0:
                break
            value |= (val & 0x7F) << shift
            shift += 7
        return value | (val << shift)

    def read_list(self, type):
        size = self.read(int)
        if type in self._formats:
            value = list(struct.unpack_from("<" + (self._formats[type] * size), self.stream, self.index))
            self.index += size * self._sizes[type]
        elif type == bytes:
            value = [self.read(str) for i in range(size)]
        elif hasattr(type, "load"):
            value = [type.load(self.read(str)) for i in range(size)]
        else:
            raise TypeError
        return value

    def read_dict(self, k, v):
        size = self.read(int)
        D = {}
        for i in range(size):
            key = self.read(k)
            value = self.read(v)
            D[key] = value
        return D
