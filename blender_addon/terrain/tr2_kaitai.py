import math

import bpy
from mathutils import Vector

from ..lib_run8.run8_tr2 import Run8Tr2
from ..utils import Vertex
from .terrain_utils import Chunk

DEBUG = False


def generate_grid_mesh_triangle_indices(chunk: Run8Tr2.Chunk, vertex_count: int) -> list[int]:
    chunk.index_buffer = []
    chunk_size = chunk.chunk_size

    num = vertex_count - chunk_size * chunk_size
    for row in range(0, chunk_size - 1):
        for col in range(0, chunk_size - 1):
            bl = col + row * chunk_size + num
            br = col + 1 + row * chunk_size + num
            tl = col + (row + 1) * chunk_size + num
            tr = col + 1 + (row + 1) * chunk_size + num

            # first triangle
            chunk.index_buffer.append(tl)
            chunk.index_buffer.append(bl)
            chunk.index_buffer.append(br)

            # second triangle
            chunk.index_buffer.append(tl)
            chunk.index_buffer.append(br)
            chunk.index_buffer.append(tr)


def calculate_face_normals(chunk: Run8Tr2.Chunk):
    # initialize normals to zero
    for vertex in chunk.vertices:
        vertex.normal = Vector((0.0, 0.0, 0.0))

    vertex_grid_size = int(math.sqrt(len(chunk.vertices)))

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

            face_normal1 = chunk.vertices[vertex_index1].position - chunk.vertices[vertex_index2].position
            face_normal2 = chunk.vertices[vertex_index3].position - chunk.vertices[vertex_index2].position
            face_normal2.cross(face_normal1)

            chunk.vertices[vertex_index1].normal += face_normal2
            chunk.vertices[vertex_index2].normal += face_normal2
            chunk.vertices[vertex_index3].normal += face_normal2
            chunk.vertices[vertex_index4].normal += face_normal2
            chunk.vertices[vertex_index5].normal += face_normal2
            chunk.vertices[vertex_index6].normal += face_normal2
            chunk.vertices[vertex_index7].normal += face_normal2

    # normalize the vertex normals
    for vertex in chunk.vertices:
        vertex.normal.normalize()


def draw_chunk(chunk: Run8Tr2.Chunk, tile_row: int, tile_col: int, tile_x: int, tile_z: int, parent):
    name = f"Chunk_{tile_row}_{tile_col}"
    vertices = [x.position.to_tuple() for x in chunk.vertices]
    normals = [x.normal.to_tuple() for x in chunk.vertices]
    uvs = [x.uv.to_tuple() for x in chunk.vertices]
    indices = chunk.index_buffer

    tile_x = tile_x * 845
    tile_z = tile_z * 1024
    real_tile_offset = Vector((tile_x, tile_z, 0))

    # offset the vertices
    for i in range(0, len(vertices)):
        vertices[i] = (
            vertices[i][0] + real_tile_offset.x,
            vertices[i][1] + real_tile_offset.y,
            vertices[i][2] + real_tile_offset.z,
        )

    # Create a new mesh object
    mesh = bpy.data.meshes.new(name)

    # create faces
    faces = []
    for i in range(0, len(indices), 3):
        faces.append((indices[i], indices[i + 1], indices[i + 2]))

    # Assign vertex data to the mesh using from_pydata
    mesh.from_pydata(vertices, [], faces)

    # invert normals
    for i in range(0, len(normals)):
        normals[i] = (-normals[i][0], -normals[i][1], -normals[i][2])

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
    # bpy.context.collection.objects.link(mesh_obj)
    parent.objects.link(mesh_obj)
    bpy.context.view_layer.objects.active = mesh_obj


class TR2Kaitai(object):
    def __init__(self, filename: str, x: float, y: float) -> None:
        self.x = x
        self.y = y
        self.tile = Run8Tr2.from_file(filename)

        # create vertices
        x_coefficient = 33.772842  # width of a chunk (X)
        y_coefficient = 41.043285  # length of a chunk (Y)
        for tile_row in range(0, 25):
            for tile_col in range(0, 25):
                chunk = self.tile.chunks[tile_row].chunks[tile_col]
                chunk_size = chunk.chunk_size

                vertex_count = 0
                self.tile.chunks[tile_row].chunks[tile_col].vertices = [0] * (chunk_size * chunk_size)
                for chunk_row in range(0, chunk_size):
                    for chunk_col in range(0, chunk_size):
                        vertex = Vertex()
                        position_x = tile_row * x_coefficient + x_coefficient / (chunk_size - 1) * chunk_row
                        position_y = tile_col * y_coefficient + y_coefficient / (chunk_size - 1) * chunk_col
                        position_z = chunk.elevations[chunk_row].elevation[chunk_col]
                        uv_x = position_x / 844.3211  # total width of a tile
                        uv_y = -position_z / 1026.0822  # total length of a tile

                        vertex.position = Vector(
                            (position_x, position_y, position_z)
                        )  # we swap the y and z coordinates
                        vertex.uv = Vector((uv_x, uv_y))
                        self.tile.chunks[tile_row].chunks[tile_col].vertices[vertex_count] = vertex
                        vertex_count += 1

                generate_grid_mesh_triangle_indices(chunk, vertex_count)
                calculate_face_normals(chunk)

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
                chunk1 = self.tile.chunks[j].chunks[i]
                chunk2 = self.tile.chunks[j + 1].chunks[i]
                if chunk1.chunk_size == chunk2.chunk_size:
                    self.merge_chunk_vertices(chunk1, chunk2, True)

        for i in range(25):
            for j in range(24):
                chunk1 = self.tile.chunks[i].chunks[j + 1]
                chunk2 = self.tile.chunks[i].chunks[j]
                if chunk1.chunk_size == chunk2.chunk_size:
                    if DEBUG:
                        print("merge z vertices")
                    self.merge_chunk_vertices(chunk1, chunk2, False)

        for i in range(25):
            for j in range(24):
                chunk1 = self.tile.chunks[j].chunks[i]
                chunk2 = self.tile.chunks[j + 1].chunks[i]
                if chunk1.chunk_size != chunk2.chunk_size:
                    self.merge_chunk_vertices(chunk1, chunk2, True)

        for i in range(25):
            for j in range(24):
                chunk1 = self.tile.chunks[i].chunks[j + 1]
                chunk2 = self.tile.chunks[i].chunks[j]
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

        for i, row in enumerate(self.tile.chunks):
            for j, chunk in enumerate(row.chunks):
                if DEBUG:
                    print(f"Drawing chunk {i} {j}")
                draw_chunk(chunk, i, j, self.x, self.y, collection)
