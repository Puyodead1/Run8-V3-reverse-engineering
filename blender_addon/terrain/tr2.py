import bpy
from mathutils import Vector

from ..binreader import BinaryReader
from ..utils import Vertex
from .terrain_utils import Chunk, ETileType, TerrainTile

DEBUG = False


class TR2(TerrainTile):
    def __init__(self, reader: BinaryReader, x: float, y: float) -> None:
        super().__init__(reader, ETileType.TR2, x, y)

        self.read()

    def read(self):
        self.string_0 = self._reader.read_cs_string()
        self.string_1 = self._reader.read_cs_string()
        self.string_2 = self._reader.read_cs_string()
        self.string_3 = self._reader.read_cs_string()

        if DEBUG:
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
                        if elevation <= 1.2 + 0.2:  # HRS_Southeast is -2
                            num += 1
                        chunk.elevations[chunk_row][chunk_col] = elevation
                if DEBUG:
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

            if DEBUG:
                print("Float2: " + str(self.float_2))
                print("Float3: " + str(self.float_3))
                print("Float4: " + str(self.float_4))
                print("Float5: " + str(self.float_5))
                print("String5: " + self.string_5)
        except:
            pass

        # create vertices
        x_coefficient = 33.772842  # width of a chunk (X)
        y_coefficient = 41.043285  # length of a chunk (Y)
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
                        position_y = tile_col * y_coefficient + y_coefficient / (chunk_size - 1) * chunk_col
                        position_z = chunk.elevations[chunk_row][chunk_col]
                        uv_x = position_x / 844.3211  # total width of a tile
                        uv_y = -position_z / 1026.0822  # total length of a tile

                        vertex.position = Vector(
                            (position_x, position_y, position_z)
                        )  # we swap the y and z coordinates
                        vertex.uv = Vector((uv_x, uv_y))
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

        if DEBUG:
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

        if DEBUG:
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
                if DEBUG:
                    print(
                        "k: "
                        + str(k)
                        + "; vertex size: "
                        + str(len(chunk2.vertices))
                        + "; index: "
                        + str(index)
                        + "; max size: "
                        + str(len(max_list))
                    )
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
                    if DEBUG:
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
                    if DEBUG:
                        print("merge z vertices")
                    self.merge_chunk_vertices(chunk1, chunk2, False)

    def merge_vertices_in_range(
        self,
        src_list: list[Vertex],
        source_start_index: int,
        source_end_index: int,
        dst_list: list[Vertex],
        dst_start_index: int,
        dst_end_index: int,
    ):
        middle_index = (dst_end_index - dst_start_index) / 2 + dst_start_index
        if (source_end_index - source_start_index) == 1:
            for i in range(dst_start_index, middle_index):
                source_vertex = dst_list[i]
                source_vertex.position.x = src_list[source_start_index].position.x
                source_vertex.position.y = src_list[source_start_index].position.y
                source_vertex.position.z = src_list[source_start_index].position.z
                source_vertex.normal = src_list[source_start_index].normal
                dst_list[i] = source_vertex

            for k in range(middle_index, dst_end_index):
                source_vertex = dst_list[k]
                source_vertex.position.x = src_list[source_end_index].position.x
                source_vertex.position.y = src_list[source_end_index].position.y
                source_vertex.position.z = src_list[source_end_index].position.z
                source_vertex.normal = src_list[source_end_index].normal
                dst_list[k] = source_vertex

            return

        middle_index_source = (source_end_index - source_start_index) / 2 + source_start_index
        self.merge_vertices_in_range(
            src_list, source_start_index, middle_index_source, dst_list, dst_start_index, middle_index
        )
        self.merge_vertices_in_range(
            src_list, middle_index_source, source_end_index, dst_list, middle_index, dst_end_index
        )

    def draw(self):
        collection = bpy.data.collections.new(f"Tile_{self.x}_{self.y}")
        bpy.context.scene.collection.children.link(collection)

        for i, row in enumerate(self.chunks):
            for j, chunk in enumerate(row):
                if DEBUG:
                    print(f"Drawing chunk {i} {j}")
                chunk.draw(self, collection)
