import math
import struct

import bpy
import mathutils
from bpy.props import StringProperty
from bpy.types import Operator
from bpy_extras.io_utils import ImportHelper


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

    def read_matrix(self):
        return mathutils.Matrix([[self.read(float) for i in range(4)] for j in range(4)])


def quaternion_from_euler(x: float, y: float, z: float) -> mathutils.Quaternion:
    # Convert degrees to radians
    roll = math.radians(x)
    pitch = math.radians(y)
    yaw = math.radians(z)

    # Create an Euler object from the roll, pitch, and yaw angles
    euler = mathutils.Euler((yaw, pitch, roll), "XYZ")

    # Create a rotation matrix from the Euler object
    rotation_matrix = euler.to_matrix()

    # Create a quaternion from the rotation matrix
    return rotation_matrix.to_quaternion()


class Class246(object):
    quaternion_0: mathutils.Quaternion = None
    vector3_0: mathutils.Vector = None
    quaternion_1: mathutils.Quaternion = None
    vector3_1: mathutils.Vector = None

    def to_json(self):
        return {"quaternion_0": self.quaternion_0, "vector3_0": self.vector3_0, "quaternion_1": self.quaternion_1, "vector3_1": self.vector3_1}


class VertexStruct(object):
    position: mathutils.Vector = mathutils.Vector()
    normal: mathutils.Vector = mathutils.Vector()
    uv: mathutils.Vector = mathutils.Vector()
    tangent: mathutils.Vector = mathutils.Vector()
    binormal: mathutils.Vector = mathutils.Vector()

    def to_json(self):
        return {"position": self.position, "normal": self.normal, "uv": self.uv, "tangent": self.tangent, "binormal": self.binormal}


class ExtraMeshData(object):
    texutre_0: str = None
    mrao_texture: str = None
    texture_2: str = None
    base_vertex_location: int = None
    start_index_location: int = None
    index_count: int = None

    def to_json(self):
        return {
            "texutre_0": self.texutre_0,
            "mrao_texture": self.mrao_texture,
            "texture_2": self.texture_2,
            "base_vertex_location": self.base_vertex_location,
            "start_index_location": self.start_index_location,
            "index_count": self.index_count,
        }


class Mesh(object):
    string_0: str = None
    string_1: str = None
    quaternion_1: mathutils.Quaternion = None
    quaternion_2: mathutils.Quaternion = None
    vector3_0: mathutils.Vector = mathutils.Vector()
    vector3_1: mathutils.Vector = mathutils.Vector()
    vector3_2: mathutils.Vector = mathutils.Vector()
    vector3_3: mathutils.Vector = mathutils.Vector()
    class_246_0: Class246 = None
    vertex_structs: list[VertexStruct] = []
    index_buffer: list[int] = []
    textures: list[str] = []
    extra_mesh_datas: list[ExtraMeshData] = []

    def to_json(self):
        return {
            "string_0": self.string_0,
            "string_1": self.string_1,
            "quaternion_1": self.quaternion_1,
            "quaternion_2": self.quaternion_2,
            "vector3_0": self.vector3_0,
            "vector3_1": self.vector3_1,
            "vector3_2": self.vector3_2,
            "vector3_3": self.vector3_3,
            "class_246_0": self.class_246_0,
        }


class Model(object):
    matrix_0: mathutils.Matrix = None
    vector3_0: mathutils.Vector = None
    bounding_sphere_radius: float = 0.0
    meshes: list[Mesh] = []

    def to_json(self):
        return {
            "matrix_0": self.matrix_0,
            "vector3_0": self.vector3_0,
            "bounding_sphere_radius": self.bounding_sphere_radius,
            "meshes": [mesh.to_json() for mesh in self.meshes],
            "vertex_structs": [vertex_struct.to_json() for vertex_struct in self.vertex_structs],
            "textures": self.textures,
            "index_buffer": self.index_buffer,
            "extra_mesh_datas": [extra_mesh_data.to_json() for extra_mesh_data in self.extra_mesh_datas],
        }


def calculate_tangents_and_binormals(index1: int, index2: int, index3: int, vertex_structs: list[VertexStruct]):
    vertex1 = vertex_structs[index1]
    vertex2 = vertex_structs[index2]
    vertex3 = vertex_structs[index3]

    position_delta1 = vertex2.position - vertex1.position
    position_delta2 = vertex3.position - vertex1.position

    uv_delta1_x = vertex2.uv.x - vertex1.uv.x
    uv_delta1_y = vertex2.uv.y - vertex1.uv.y
    uv_delta2_x = vertex3.uv.x - vertex1.uv.x
    uv_delta2_y = vertex3.uv.y - vertex1.uv.y

    determinant = uv_delta1_x * uv_delta2_y - uv_delta2_x * uv_delta1_y

    if math.isclose(determinant, 0):
        # Handle the case when the determinant is close to zero
        # You can set num5 to a default value or handle it as per your requirements
        num5 = 0.0
    else:
        num5 = 1 / determinant

    tangent = mathutils.Vector()
    tangent.x = (uv_delta2_y * position_delta1.x - uv_delta1_y * position_delta2.x) * num5
    tangent.y = (uv_delta2_y * position_delta1.y - uv_delta1_y * position_delta2.y) * num5
    tangent.z = (uv_delta2_y * position_delta1.z - uv_delta1_y * position_delta2.z) * num5
    tangent.normalize()

    vertex1.tangent = tangent
    vertex2.tangent = tangent
    vertex3.tangent = tangent

    binormal = tangent.cross(vertex1.normal).normalize()

    vertex1.binormal = binormal
    vertex2.binormal = binormal
    vertex3.binormal = binormal

    vertex_structs[index1] = vertex1
    vertex_structs[index2] = vertex2
    vertex_structs[index3] = vertex3


def import_mesh(mesh_name: str, vertex_structs: list[VertexStruct], index_buffer: list[int]):
    # scaling_matrix = mathutils.Matrix.Scale(0.95, 4)
    vertices = [a.position for a in vertex_structs]
    normals = [a.normal for a in vertex_structs]
    uvs = [a.uv for a in vertex_structs]

    # Create a new mesh object
    mesh = bpy.data.meshes.new(mesh_name)
    mesh_obj = bpy.data.objects.new(mesh_name, mesh)
    bpy.context.collection.objects.link(mesh_obj)
    bpy.context.view_layer.objects.active = mesh_obj
    mesh_obj.select_set(True)

    # Create vertices and faces using from_pydata
    faces = []

    for i in range(0, len(index_buffer), 3):
        v1_index, v2_index, v3_index = index_buffer[i : i + 3]
        a = [v1_index, v2_index, v3_index]
        faces.append(a)

    # Assign vertex data to the mesh using from_pydata
    mesh.from_pydata(vertices, [], faces)

    # apply scaling matrix
    # mesh.transform(scaling_matrix)

    # mesh.update()

    # Assign per-vertex normals to the mesh
    normals_flat = [n for normal in normals for n in normal]
    mesh.vertices.foreach_set("normal", normals_flat)

    # Assign UVs to the mesh
    uv_layer = mesh.uv_layers.new()
    mesh.uv_layers.active = uv_layer

    for face in mesh.polygons:
        for vert_idx, loop_idx in zip(face.vertices, face.loop_indices):
            uv_layer.data[loop_idx].uv = (uvs[vert_idx][0], -uvs[vert_idx][1])

    # rotate upright
    rotation_angle = math.radians(90)
    mesh_obj.rotation_euler[0] = rotation_angle
    mesh_obj.update_tag()

    mesh_obj.update_tag()

    return (mesh, mesh_obj)


def import_model(context, filepath):
    vector3_1 = mathutils.Vector((0.0, 0.0, 0.0))

    model = Model()
    model.matrix_0 = mathutils.Matrix.Identity(4)
    model.vector3_0 = vector3_1

    with open(filepath, "rb") as f:
        reader = BinaryReader(f.read())

        num_meshes = 1
        flag = False
        num2 = reader.read(int)
        if num2 == -969696:
            num = reader.read(int)
            flag = True
        elif num2 == -969697:
            num = reader.read(int)
            model.vector3_0 = mathutils.Vector((reader.read(float), reader.read(float), reader.read(float)))
            vector3_1 = mathutils.Vector()
            flag = True
        else:
            # go back to the start of the file
            f.seek(0)
            reader = BinaryReader(f.read())

        for i in range(num_meshes):
            mesh = Mesh()
            # TODO: finish this, used for locomotives
            # if flag:
            #     class_247.string_0 = reader.read(str)
            #     class_247.string_1 = reader.read(str)
            #     class_247.vector3_3 = mathutils.Vector((reader.read(float), reader.read(float), reader.read(float)))
            #     class_247.vector3_1 = mathutils.Vector((reader.read(float), reader.read(float), reader.read(float)))
            #     class_247.quaternion_1 = quaternion_from_euler(reader.read(float), reader.read(float), reader.read(float))
            #     # read unused scaling matrix, 3 floats
            #     reader.read(float)
            #     reader.read(float)
            #     reader.read(float)
            #     # unused quaternion
            #     quaternion_from_euler(reader.read(float), reader.read(float), reader.read(float))
            #     # unused quaternion
            #     quaternion_from_euler(reader.read(float), reader.read(float), reader.read(float))
            #     class_247.vector3_2 = mathutils.Vector((reader.read(float), reader.read(float), reader.read(float)))
            #     class_247.quaternion_2 = quaternion_from_euler(reader.read(float), reader.read(float), reader.read(float))
            #     # unused scaling matrix, 3 floats
            #     reader.read(float)
            #     reader.read(float)
            #     reader.read(float)
            #     # number of unknown matrices
            #     num = reader.read(int)
            #     matrix_array = [None for i in range(num)]
            #     for i in range(num):
            #         if class_247.class_246_0 == None:
            #             class_247.class_246_0 = Class246()
            #             class_247.class_246_0.quaternion_0 = [None for i in range(num)]
            #             class_247.class_246_0.vector3_0 = [None for i in range(num)]
            #         matrix_array[i]  = reader.read_matrix()
            # else:
            #     class_247.string_0 = ""
            #     class_247.string_1 = ""
            num_verticies = int(reader.read(int) / 7)
            for i in range(num_verticies):
                vertex_struct = VertexStruct()

                # unused float
                reader.read(float)
                position_x = reader.read(float) * 63.7 - mesh.vector3_3.x
                normal_y = reader.read(float) / -1.732
                position_z = reader.read(float) / 16 - mesh.vector3_3.z
                texcoord_x = reader.read(float) / 4.8
                normal_x = reader.read(float) / 10.962
                # unused float
                reader.read(float)
                normal_z = reader.read(float) / 11.432
                texcoord_y = reader.read(float) / 9.6
                position_y = -reader.read(float) * 6 - mesh.vector3_3.y

                vertex_struct.position = mathutils.Vector((position_x, position_y, position_z))
                vertex_struct.normal = mathutils.Vector((normal_x, normal_y, normal_z))
                vertex_struct.uv = mathutils.Vector((texcoord_x, texcoord_y))
                vertex_struct.binormal = mathutils.Vector()
                vertex_struct.tangent = mathutils.Vector()

                mesh.vertex_structs.append(vertex_struct)

                # num6 = max(abs(position_x), max(abs(position_y), abs(position_z)))
                # if num6 > model.bounding_sphere_radius:
                #     model.bounding_sphere_radius = num6

            num_textures = reader.read(int) + 6
            for i in range(num_textures):
                mesh.textures.append(reader.read(str))

            is_ushort = reader.read(bool)  # we dont care about this since its always read as int32
            index_buffer_size = reader.read(int)

            for i in range(index_buffer_size):
                mesh.index_buffer.append(reader.read(int))

            for i in range(len(mesh.index_buffer) - 3):
                calculate_tangents_and_binormals(mesh.index_buffer[i], mesh.index_buffer[i + 2], mesh.index_buffer[i + 1], mesh.vertex_structs)

            # num of mesh data structs
            num5 = reader.read(int) - 9
            if num5 == 0:
                extra_mesh_data = ExtraMeshData()
                extra_mesh_data.index_count = len(mesh.index_buffer)
                extra_mesh_data.start_index_location = 0
                extra_mesh_data.base_vertex_location = 0

                mesh.extra_mesh_datas.append(extra_mesh_data)
            else:
                for i in range(num5):
                    extra_mesh_data = ExtraMeshData()
                    # unused float
                    reader.read(float)
                    num13 = reader.read(int)
                    # TODO: does stuff with textures
                    extra_mesh_data.index_count = reader.read(int)
                    extra_mesh_data.start_index_location = reader.read(int)
                    extra_mesh_data.base_vertex_location = reader.read(int)

                    mesh.extra_mesh_datas.append(extra_mesh_data)

            model.meshes.append(mesh)

    for i, mesh in enumerate(model.meshes):
        mesh_name = "Model_{}".format(i)
        if len(model.meshes) == 1:
            # 1 mesh, use the filename for the mesh name
            mesh_name = filepath.split("\\")[-1].split(".")[0]
        import_mesh(mesh_name, mesh.vertex_structs, mesh.index_buffer)
        for texture in mesh.textures:
            print(texture)

    # we can dispose of the model now
    del model

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
