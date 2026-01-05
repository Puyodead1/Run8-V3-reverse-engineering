from pathlib import Path

import bpy
import mathutils

from .lib_run8.model import Model as Run8Model
from .utils import Vertex, convert_coordinate_system, vector3_kaitai_to_mathutils


class Model:
    def __init__(self, filepath: str):
        self.filepath = Path(filepath)
        self.filename = self.filepath.stem
        self.model = Run8Model.from_file(filepath)

    def draw(self):
        collection = bpy.data.collections.new(self.filename)
        bpy.context.scene.collection.children.link(collection)

        # convert the local model offset to mathutils
        self.model.root_offset = convert_coordinate_system(vector3_kaitai_to_mathutils(self.model.root_offset))

        for object in self.model.objects:
            print(f"Drawing object: {object.name.value}")
            print(f"Parent object: {object.parent_name.value}")
            # convert the vertices from kaitai to mathutils
            vertices = [Vertex.from_kaitai(v) for v in object.vertices]

            # position data
            positions = [convert_coordinate_system(v.position).to_tuple() for v in vertices]
            # normal data
            normals = [convert_coordinate_system(v.normal).to_tuple() for v in vertices]
            # uv data
            uvs = [v.uv.to_tuple() for v in vertices]

            # TODO: apply offsets and transforms

            # create a new mesh object
            mesh = bpy.data.meshes.new(object.name.value)

            # create faces
            faces = []
            # TODO: create faces from the vertex data

            # assign vertex data
            mesh.from_pydata(positions, [], faces)

            # TODO: uvs
            mesh_obj = bpy.data.objects.new(object.name.value, mesh)
            mesh_obj.location = self.model.root_offset
            collection.objects.link(mesh_obj)
            bpy.context.view_layer.objects.active = mesh_obj
