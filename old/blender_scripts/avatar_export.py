import struct
from io import BytesIO

import bpy
from bpy.props import StringProperty
from bpy.types import Operator
from bpy_extras.io_utils import ExportHelper

# these are the names of the animations that are required.
original_animation_names = [
    "Fidget01",
    "Fidget02",
    "Fidget03",
    "HangOn",
    "moon",
    "Conduct",
    "ConductFidget01",
    "ConductFidget02",
    "ConductFidget03",
    "Eng",
    "EngFidget01",
    "EngFidget02",
    "EngFidget03",
    "StandStill",
    "walk",
    "wave",
]


def matrix_to_flat_list(matrix):
    return [matrix[i][j] for j in range(4) for i in range(4)]


def is_mesh_object(obj):
    return obj.type == "MESH"


def get_all_mesh_indices(obj):
    mesh_indices = []

    # Check if the object has a mesh data block
    if obj.type == "MESH":
        mesh = obj.data
        faces = mesh.polygons

        for face in faces:
            face_indices = face.vertices

            if len(face_indices) != 3:
                print(f"{mesh.name} is not triangular!")
                continue

            mesh_indices.extend(face_indices)

    # Traverse child objects recursively
    for child in obj.children:
        mesh_indices.extend(get_all_mesh_indices(child))

    return mesh_indices


def get_all_mesh_data(obj):
    mesh_data = []

    # Check if the object has a mesh data block
    if obj.type == "MESH":
        mesh = obj.data

        # Retrieve vertex data
        for vertex in mesh.vertices:
            vertex_data = {}
            vertex_data["position"] = vertex.co
            vertex_data["uv"] = []
            vertex_data["normal"] = vertex.normal
            vertex_data["blend_weights"] = []
            vertex_data["blend_indices"] = []
            mesh_data.append(vertex_data)

        # Get UV coordinates
        uv_layer = mesh.uv_layers.active.data if mesh.uv_layers.active else None
        if uv_layer:
            for loop in mesh.loops:
                vertex_index = loop.vertex_index
                uv = uv_layer[loop.index].uv
                mesh_data[vertex_index]["uv"] = uv

        # Get vertex blend weights and blend indices
        # vertex_groups = obj.vertex_groups
        # for vertex_index, vertex in enumerate(mesh.vertices):
        #     blend_weights = []
        #     blend_indices = []
        #     for group in vertex.groups:
        #         blend_weights.append(group.weight)
        #         blend_indices.append(group.group)
        #     mesh_data[vertex_index]["blend_weights"] = blend_weights
        #     mesh_data[vertex_index]["blend_indices"] = blend_indices

    # Traverse child objects recursively
    for child in obj.children:
        mesh_data.extend(get_all_mesh_data(child))

    return mesh_data


def write(context, filepath):
    obj = bpy.context.active_object

    stream = BinaryWriter()

    all_indices = get_all_mesh_indices(obj)
    all_verticies = get_all_mesh_data(obj)

    print("Index count: " + str(len(all_indices)))
    print("Vertex count: " + str(len(all_verticies)))

    stream.add("i", len(all_verticies) * 7)  # write the number of verticies

    # write verticies
    for vertex in all_verticies:
        stream.add_float(0.0)  # reserved
        stream.add_float(vertex["position"].x / 63.7)
        stream.add_float(vertex["normal"].y * -1.732)
        stream.add_float(vertex["position"].z * 16)
        stream.add_float(vertex["uv"].x * 4.8)
        stream.add_float(vertex["normal"].x * 10.962)
        stream.add_float(0.0)  # reserved
        stream.add_float(vertex["normal"].z * 11.432)
        stream.add_float(vertex["uv"].y * 9.6)
        stream.add_float(vertex["position"].y / 6)
        stream.add("<b", 0)  # blend indice w
        stream.add_float(0.0)  # blend weight z
        stream.add("<b", 0)  # blend indice x
        stream.add_float(0.0)  # blend weight y
        stream.add("<b", 0)  # blend indice y
        stream.add_float(0.0)  # blend weight w
        stream.add("<b", 0)  # blend indice z
        stream.add_float(0.0)  # blend weight x

    # texture count, currently 0
    texture_count = 0
    stream.add("<i", texture_count - 6)

    # texture names
    textures = []  # TODO:
    for texture in textures:
        stream += texture

    # is ushort buffer
    stream += False

    # index buffer size
    stream.add("<i", len(all_indices))

    # write index buffer
    for indice in all_indices:
        stream.add("<i", indice)

    # unknown struct size
    stream.add("<i", 9)

    # skeleton hierarchy count
    skeleton_hierarchy = []  # TODO:
    stream.add("<i", len(skeleton_hierarchy))

    # write skeleton hierarchy
    for sh in skeleton_hierarchy:
        stream.add("<i", sh)

    # bone indice count
    bone_indices = []  # TODO:
    stream.add("<i", len(bone_indices))

    # write bone indices
    for k in bone_indices:
        v = bone_indices[k]
        stream += k
        stream.add("<i", v)

    # bind pose count
    bind_poses = []  # TODO:
    stream.add("<i", len(bind_poses))

    # write bind poses
    for pose in bind_poses:
        # array of floats
        for i in pose:
            stream.add("<f", i)

    # inverse bind pose count
    stream.add("<i", len(bind_poses))

    # write inverse bind poses
    for pose in bind_poses:
        # array of floats
        for i in pose:
            stream.add("<f", i)

    # animation count
    animations = []
    stream.add("<i", len(animations))

    # write animations
    # TODO:

    data = stream.serial()
    with open(filepath, "wb") as f:
        f.write(data)

    return {"FINISHED"}


class ExportRun8Avatar(Operator, ExportHelper):
    """This appears in the tooltip of the operator and in the generated docs"""

    bl_idname = "run8_lib.export_avatar"
    bl_label = "Export Avatar"

    # ExportHelper mixin class uses this
    filename_ext = ".rn8"

    filter_glob: StringProperty(
        default="*.rn8",
        options={"HIDDEN"},
        maxlen=255,  # Max internal buffer length, longer would be clamped.
    )

    def execute(self, context):
        return write(context, self.filepath)


# Only needed if you want to add into a dynamic menu
def run8_avatar_menu_func_export(self, context):
    self.layout.operator(ExportRun8Avatar.bl_idname, text="Run8 Avatar")


# Register and add to the "file selector" menu (required to use F3 search "Text Export Operator" for quick access).
def register():
    bpy.utils.register_class(ExportRun8Avatar)

    # prevent duplicate menu entries
    if hasattr(bpy.types.TOPBAR_MT_file_export.draw, "_draw_funcs"):
        if run8_avatar_menu_func_export.__name__ not in (f.__name__ for f in bpy.types.TOPBAR_MT_file_export.draw._draw_funcs):
            bpy.types.TOPBAR_MT_file_export.append(run8_avatar_menu_func_export)
    else:
        bpy.types.TOPBAR_MT_file_export.append(run8_avatar_menu_func_export)


def unregister():
    clazz = bpy.types.NodeTree.bl_rna_get_subclass_py("run8_lib.export_avatar")
    bpy.utils.unregister_class(clazz)
    bpy.types.TOPBAR_MT_file_export.remove(run8_avatar_menu_func_export)


if __name__ == "__main__":
    register()


# TODO: move this to a separate file
class BinaryStream(object):
    _types = {
        int: struct.Struct("<i"),
        float: struct.Struct("<d"),
        bool: struct.Struct("<?"),
    }
    _formats = {
        int: "i",
        float: "d",
        bool: "?",
    }


class BinaryWriter(BinaryStream):
    def __init__(self):
        self.stream = BytesIO()

    def clear(self):
        self.stream = BytesIO()

    def __iadd__(self, value):
        try:
            self.stream.write(self._types[type(value)].pack(value))
        except KeyError:
            if type(value) == bytes:
                self.add_string(value)
            elif type(value) == str:
                self.add_string(value.encode("ascii"))
            elif type(value) == tuple:
                for v in value:
                    self += v
            elif type(value) == list:
                self.add_list(value)
            elif type(value) == dict:
                self.add_dict(value)
            else:
                if hasattr(value, "dump"):
                    self.add_string(value.dump())
                else:
                    raise
        return self

    def add(self, format, value):
        self.stream.write(struct.pack(format, value))

    def add_7bit_int(self, value):
        temp = value
        bytess = ""
        while temp >= 128:
            bytess += chr(0x000000FF & (temp | 0x80))
            temp >>= 7
        bytess += chr(temp)
        self.stream.write(bytess.encode())

    def add_string(self, value):
        self.add_7bit_int(len(value))
        self.stream.write(value)

    def add_double(self, value):
        self.stream.write(struct.pack("<d", value))

    def add_float(self, value):
        self.stream.write(struct.pack("<f", value))

    def extend(self, value):
        self.stream.write(value)

    def add_dict(self, value):
        self += len(value)
        for k, v in list(value.items()):
            self += k
            self += v

    def add_list(self, value):
        self += len(value)
        if value:
            if type(value[0]) in self._types:
                self.stream.write(struct.pack("<" + (self._formats[type(value[0])] * len(value)), *value))
            else:
                for s in value:
                    self += s

    def serial(self):
        self.stream.seek(0)
        return self.stream.read()
