from __future__ import annotations

import json
import math
import struct
from enum import Enum
from os import SEEK_CUR
from typing import BinaryIO, List

import bpy
import mathutils
from bpy.props import StringProperty
from bpy.types import Operator
from bpy_extras.io_utils import ImportHelper

# Some constants
ENDIAN_PREFIXES = ("@", "<", ">", "=", "!")

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
    def read_vector3(self) -> Vector:
        return Vector((self.read_float(), self.read_float(), self.read_float()))

    def read_vector2(self) -> Vector:
        return Vector((self.read_float(), self.read_float()))

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

class Vertex(object):
    position: mathutils.Vector = None
    normal: mathutils.Vector = None
    uv: mathutils.Vector = None

    def to_json(self) -> dict:
        return {
            "position": self.position.to_tuple(),
            "normal": self.normal.to_tuple(),
            "uv": self.uv.to_tuple()
        }
    
    def __str__(self) -> str:
        return json.dumps(self.to_json())


class ETileType(Enum):
    NONE = 0
    TER = 1
    TR2 = 2
    TR3 = 3
    TR4 = 4


class Chunk(object):
    elevations: List[List[float]] = []
    chunk_size: int
    tile_row: int
    tile_col: int
    vertices: List[Vertex] = []
    index_buffer: List[int] = []

    def to_json(self) -> dict:
        return {
            "elevations": self.elevations,
            "chunk_size": self.chunk_size,
            "tile_row": self.tile_row,
            "tile_col": self.tile_col,
            "vertices": [x.to_json() for x in self.vertices],
            # "index_buffer": self.index_buffer,
            "bool_0": self.bool_0
        }
    
    def __str__(self) -> str:
        return json.dumps(self.to_json())
    
    def generate_grid_mesh_triangle_indices(self, chunk_size: int, vertex_count: int) -> list[int]:
        num = vertex_count - chunk_size * chunk_size
        for row in range(0, chunk_size - 1):
            for col in range(0, chunk_size - 1):
                bl = col + row * chunk_size + num
                br = col + 1 + row * chunk_size + num
                tl = col + (row + 1) * chunk_size + num
                tr = col + 1 + (row + 1) * chunk_size + num
                
                # first triangle
                self.index_buffer.append(tl)
                self.index_buffer.append(bl)
                self.index_buffer.append(br)

                # second triangle
                self.index_buffer.append(tl)
                self.index_buffer.append(br)
                self.index_buffer.append(tr)
    
    def calculate_face_normals(self):
        # initialize normals to zero
        for vertex in self.vertices:
            vertex.normal = mathutils.Vector((0.0, 0.0, 0.0))

        vertex_grid_size = int(math.sqrt(len(self.vertices)))

        # calculate face normals
        for row in range(vertex_grid_size - 1):
            for col in range(vertex_grid_size - 1):
                vertex_index1 = row * vertex_grid_size + col + 1
                vertex_index2 = row * vertex_grid_size + col
                vertex_index3 = (row + 1) * vertex_grid_size + col
                vertex_index4 = (row + 1) * vertex_grid_size + (col + 1)
                vertex_index5 = (row + 1) * vertex_grid_size + col
                vertex_index6 = (row + 1) * vertex_grid_size + (col + 1)
                vertex_index7 = row * vertex_grid_size + col + 1

                face_normal1 = self.vertices[vertex_index1].position - self.vertices[vertex_index2].position
                face_normal2 = self.vertices[vertex_index3].position - self.vertices[vertex_index2].position
                face_normal2.cross(face_normal1)

                self.vertices[vertex_index1].normal += face_normal2
                self.vertices[vertex_index2].normal += face_normal2
                self.vertices[vertex_index3].normal += face_normal2
                self.vertices[vertex_index4].normal += face_normal2
                self.vertices[vertex_index5].normal += face_normal2
                self.vertices[vertex_index6].normal += face_normal2
                self.vertices[vertex_index7].normal += face_normal2

        # normalize the vertex normals
        for vertex in self.vertices:
            vertex.normal.normalize()

    def draw(self):
        name = f"Chunk_{self.tile_row}_{self.tile_col}"
        vertices = [x.position.to_tuple() for x in self.vertices]
        normals = [x.normal.to_tuple() for x in self.vertices]
        uvs = [x.uv.to_tuple() for x in self.vertices]
        indices = self.index_buffer

        # Create a new mesh object
        mesh = bpy.data.meshes.new(name)

        # create faces
        faces = []
        for i in range(0, len(indices), 3):
            faces.append((indices[i], indices[i + 1], indices[i + 2]))

        # Assign vertex data to the mesh using from_pydata
        mesh.from_pydata(vertices, [], faces)

        # assign normals
        mesh.normals_split_custom_set_from_vertices(normals)

        # Assign UVs to the mesh
        uv_layer = mesh.uv_layers.new()
        mesh.uv_layers.active = uv_layer

        for face in mesh.polygons:
            for vert_idx, loop_idx in zip(face.vertices, face.loop_indices):
                uv_layer.data[loop_idx].uv = (uvs[vert_idx][0], -uvs[vert_idx][1])  # flip the V coordinate

        # Update mesh geometry
        mesh.update()

        mesh_obj = bpy.data.objects.new(name, mesh)
        bpy.context.collection.objects.link(mesh_obj)
        bpy.context.view_layer.objects.active = mesh_obj


class TerrainTile(object):
    _reader: BinaryReader
    tile_type: ETileType = ETileType.NONE
    x: float
    y: float
    string_0: str
    string_1: str
    string_2: str
    string_3: str
    string_4: str
    string_5: str
    int_0: int
    chunks: List[List[Chunk]]
    float_0: float
    float_1: float
    float_2: float
    float_3: float
    float_4: float
    float_5: float
    bool_0: bool

    def __init__(self, reader: BinaryReader, tile_type: ETileType, b: float, b2: float) -> None:
        self._reader = reader
        self.tile_type = tile_type
        self.x = b
        self.y = b2


class TER(TerrainTile):
    def __init__(self, reader: BinaryReader, x: float, y: float) -> None:
        super().__init__(reader, ETileType.TER, x, y)

        self.read()

    def read(self):
        pass


class TR2(TerrainTile):
    def __init__(self, reader: BinaryReader, x: float, y: float) -> None:
        super().__init__(reader, ETileType.TR2, x, y)

        self.read()

    def read(self):
        self.string_0 = self._reader.read_cs_string()
        self.string_1 = self._reader.read_cs_string()
        self.string_2 = self._reader.read_cs_string()
        self.string_3 = self._reader.read_cs_string()

        print("String0: " + self.string_0)
        print("String1: " + self.string_1)
        print("String2: " + self.string_2)
        print("String3: " + self.string_3)
        
        self.int_0 = 99

        self.chunks = []
        num = 0
        for i in range(25):
            row = []
            for j in range(25):
                chunk = Chunk()
                chunk.chunk_size = self._reader.read_int32()
                chunk.elevations = [[0] * chunk.chunk_size] * chunk.chunk_size
                chunk.tile_row = i
                chunk.tile_col = j
                for chunk_row in range(chunk.chunk_size):
                    for chunk_col in range(chunk.chunk_size):
                        elevation = self._reader.read_float()
                        if elevation <= 1.2 + 0.2: # HRS_Southeast is -2
                            num += 1
                        chunk.elevations[chunk_row][chunk_col] = elevation
                print(f"Read chunk {chunk.tile_row} {chunk.tile_col}")
                row.append(chunk)
            self.chunks.append(row)

        # IsRegionSouthernCA() && num > 100
        self.bool_0 = num > 100

        try:
            self.float_2 = self._reader.read_float()
            self.float_3 = self._reader.read_float()
            self.float_4 = self._reader.read_float()
            self.float_5 = self._reader.read_float()
            self.string_5 = self._reader.read_cs_string()

            print("Float2: " + str(self.float_2))
            print("Float3: " + str(self.float_3))
            print("Float4: " + str(self.float_4))
            print("Float5: " + str(self.float_5))
            print("String5: " + self.string_5)
        except:
            pass

        # create vertices
        x_coefficient = 33.772842
        y_coefficient = 41.043285
        for tile_row in range(0, 25):
            for tile_col in range(0, 25):
                chunk = self.chunks[tile_row][tile_col]
                chunk_size = chunk.chunk_size

                vertex_count = 0
                self.chunks[tile_row][tile_col].vertices = [0] * (chunk_size * chunk_size)
                for chunk_row in range(0, chunk_size):
                    for chunk_col in range(0, chunk_size):
                        vertex = Vertex()
                        position_x = tile_row * x_coefficient + x_coefficient / (chunk_size - 1) * chunk_row
                        position_y = (tile_col * y_coefficient + y_coefficient / (chunk_size - 1) * chunk_col)
                        position_z = chunk.elevations[chunk_row][chunk_col]
                        uv_x = position_x / 844.3211
                        uv_y = -position_z / 1026.0822

                        vertex.position = mathutils.Vector((position_x, position_y, position_z)) # we swap the y and z coordinates
                        vertex.uv = mathutils.Vector((uv_x, uv_y))
                        self.chunks[tile_row][tile_col].vertices[vertex_count] = vertex
                        vertex_count += 1

                chunk.generate_grid_mesh_triangle_indices(chunk_size, vertex_count)
                chunk.calculate_face_normals()

        # # print this data to a file
        # a = json.dumps([[x.to_json() for x in y] for y in self.chunks])
        # with open("C:\\Users\\23562\\Documents\\Code\\Run8-V3-reverse-engineering\\blender_scripts\\TR2_1.json", "w") as f:
        #     f.write(a)
        self.smethod_0()

    def merge_chunk_vertices(self, chunk1: Chunk, chunk2: Chunk, x: bool = True):
        vertex_max = max([v.position.x if x else v.position.y for v in chunk1.vertices])
        vertex_min = min([v.position.x if x else v.position.y for v in chunk2.vertices])

        print("Max: " + str(vertex_min))
        print("Min: " + str(vertex_max))

        def are_floats_close(float1: float, float2: float):
            return abs(float1 - float2) < 0.001
        
        def is_close_to_max(vertex: Vertex):
            return are_floats_close(vertex.position.x if x else vertex.position.y, vertex_max)
        
        def is_close_to_min(vertex: Vertex):
            return are_floats_close(vertex.position.x if x else vertex.position.y, vertex_min)

        # create a list of vertices that are close to the max and min
        max_list = list(filter(is_close_to_max, chunk1.vertices))
        min_list = list(filter(is_close_to_min, chunk2.vertices))

        print("max Size: " + str(len(max_list)))
        print("min Size: " + str(len(min_list)))

        if len(max_list) > len(min_list):
            self.merge_vertices_in_range(min_list, 0, len(min_list) - 1, max_list, 0, len(max_list) - 1)
            num = 0
            for i in range(len(chunk1.vertices)):
                vertex = chunk1.vertices[i]
                if is_close_to_max(vertex):
                    chunk1.vertices[i] = max_list[num]
                    num += 1
            return
        
        if len(max_list) < len(min_list):
            self.merge_vertices_in_range(max_list, 0, len(max_list) - 1, min_list, 0, len(min_list) - 1)
            num = 0
            for i in range(len(chunk2.vertices)):
                vertex = chunk2.vertices[i]
                if is_close_to_max(vertex):
                    chunk2.vertices[i] = min_list[num]
                    num += 1
            return

        index = 0
        for k in range(len(chunk2.vertices)):
            v = chunk2.vertices[k]
            if is_close_to_max(v):
                print("k: " + str(k) + "; vertex size: " + str(len(chunk2.vertices)) + "; index: " + str(index) + "; max size: " + str(len(max_list)))
                chunk2.vertices[k] = max_list[index]
            index += 1

    def smethod_0(self):
        for i in range(25):
            for j in range(24):
                chunk1 = self.chunks[j][i]
                chunk2 = self.chunks[j + 1][i]
                if chunk1.chunk_size == chunk2.chunk_size:
                    self.merge_chunk_vertices(chunk1, chunk2, True)


        for i in range(25):
            for j in range(24):
                chunk1 = self.chunks[i][j + 1]
                chunk2 = self.chunks[i][j]
                if chunk1.chunk_size == chunk2.chunk_size:
                    print("merge z vertices")
                    self.merge_chunk_vertices(chunk1, chunk2, False)

        for i in range(25):
            for j in range(24):
                chunk1 = self.chunks[j][i]
                chunk2 = self.chunks[j + 1][i]
                if chunk1.chunk_size != chunk2.chunk_size:
                    self.merge_chunk_vertices(chunk1, chunk2, True)

        for i in range(25):
            for j in range(24):
                chunk1 = self.chunks[i][j + 1]
                chunk2 = self.chunks[i][j]
                if chunk1.chunk_size != chunk2.chunk_size:
                    print("merge z vertices")
                    self.merge_chunk_vertices(chunk1, chunk2, False)

    def merge_vertices_in_range(self, src_list: list[Vertex], source_start_index: int, source_end_index: int, dst_list: list[Vertex], dst_start_index: int, dst_end_index: int):
        middle_index = (dst_end_index - dst_start_index) / 2 + dst_start_index
        if (source_end_index - source_start_index) == 1:
            for i in range(dst_start_index, middle_index):
                source_vertex = dst_list[i]
                source_vertex.position.x = src_list[source_start_index].position.x
                source_vertex.position.y = src_list[source_start_index].position.y
                source_vertex.position.z = src_list[source_start_index].position.z
                source_vertex.normal = src_list[source_start_index].normal;
                dst_list[i] = source_vertex

            for k in range(middle_index, dst_end_index):
                source_vertex = dst_list[k]
                source_vertex.position.x = src_list[source_end_index].position.x
                source_vertex.position.y = src_list[source_end_index].position.y
                source_vertex.position.z = src_list[source_end_index].position.z
                source_vertex.normal = src_list[source_end_index].normal;
                dst_list[k] = source_vertex
            
            return
        
        middle_index_source = (source_end_index - source_start_index) / 2 + source_start_index
        self.merge_vertices_in_range(src_list, source_start_index, middle_index_source, dst_list, dst_start_index, middle_index)
        self.merge_vertices_in_range(src_list, middle_index_source, source_end_index, dst_list, middle_index, dst_end_index)

    def draw(self):
        for i, row in enumerate(self.chunks):
            for j, chunk in enumerate(row):
                print(f"Drawing chunk {i} {j}")
                chunk.draw()

class TR3(TerrainTile):
    def __init__(self, reader: BinaryReader, x: float, y: float) -> None:
        super().__init__(reader, ETileType.TR3, x, y)

        self.read()

    def read(self):
        pass


class TR4(TerrainTile):
    def __init__(self, reader: BinaryReader, x: float, y: float) -> None:
        super().__init__(reader, ETileType.TR4, x, y)

        self.read()

    def read(self):
        pass


def import_model(context, filepath):
    print("\n" * 10)
    print("Importing Run8 Terrain Tile: %r..." % (filepath))

    with open(filepath, "rb") as f:
        reader = BinaryReader(f)

        if filepath.endswith(".ter"):
            tile = TER(reader, 0, 0)
        elif filepath.endswith(".tr2"):
            tile = TR2(reader, 0, 0)
        elif filepath.endswith(".tr3"):
            tile = TR3(reader, 0, 0)
        elif filepath.endswith(".tr4"):
            tile = TR4(reader, 0, 0)
        else:
            raise Exception("Unknown file type")
        
        tile.draw()

    return {"FINISHED"}


class ImportRun8TerrainTile(Operator, ImportHelper):
    bl_idname = "run8_lib.import_terrain_tile"
    bl_label = "Import Tile"

    filename_ext = ".tr4"

    filter_glob: StringProperty(
        default="*.tr2;*.ter;*.tr4",
        options={"HIDDEN"},
        maxlen=255,  # Max internal buffer length, longer would be clamped.
    )

    def execute(self, context):
        return import_model(context, self.filepath)


def run8_terrain_menu_func_import(self, context):
    self.layout.operator(ImportRun8TerrainTile.bl_idname, text="Run8 Terrain Tile (.tr3/.tr4/.ter)")


def register():
    bpy.utils.register_class(ImportRun8TerrainTile)

    # prevent duplicate menu entries
    if hasattr(bpy.types.TOPBAR_MT_file_import.draw, "_draw_funcs"):
        if run8_terrain_menu_func_import.__name__ not in (f.__name__ for f in bpy.types.TOPBAR_MT_file_import.draw._draw_funcs):
            bpy.types.TOPBAR_MT_file_import.append(run8_terrain_menu_func_import)
    else:
        bpy.types.TOPBAR_MT_file_import.append(run8_terrain_menu_func_import)


def unregister():
    clazz = bpy.types.NodeTree.bl_rna_get_subclass_py("run8_lib.import_terrain_tile")
    bpy.utils.unregister_class(clazz)
    bpy.types.TOPBAR_MT_file_import.remove(run8_terrain_menu_func_import)


if __name__ == "__main__":
    register()
