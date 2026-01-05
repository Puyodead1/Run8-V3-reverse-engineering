import json
import math
from enum import Enum
from typing import List

import bpy
from mathutils import Vector

from ..binreader import BinaryReader
from ..utils import Vertex


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
            "bool_0": self.bool_0,
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
            vertex.normal = Vector((0.0, 0.0, 0.0))

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

    def draw(self, tile, parent):
        name = f"Chunk_{self.tile_row}_{self.tile_col}"
        vertices = [x.position.to_tuple() for x in self.vertices]
        normals = [x.normal.to_tuple() for x in self.vertices]
        uvs = [x.uv.to_tuple() for x in self.vertices]
        indices = self.index_buffer

        real_tile_offset = Vector((tile.x * 844.3211, tile.y * 1026.0822, 0))

        # offset the vertices
        for i in range(0, len(vertices)):
            vertices[i] = (
                vertices[i][0] + real_tile_offset.x,
                -vertices[i][1] + real_tile_offset.y,
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
