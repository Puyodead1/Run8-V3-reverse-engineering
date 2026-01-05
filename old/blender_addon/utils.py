import json

import mathutils


class Vertex(object):
    position: mathutils.Vector = None
    normal: mathutils.Vector = None
    uv: mathutils.Vector = None

    def to_json(self) -> dict:
        return {"position": self.position.to_tuple(), "normal": self.normal.to_tuple(), "uv": self.uv.to_tuple()}

    def __str__(self) -> str:
        return json.dumps(self.to_json())

    # from kaitai vertex
    @classmethod
    def from_kaitai(cls, kaitai_vertex) -> "Vertex":
        v = cls()
        v.position = mathutils.Vector((kaitai_vertex.position.x, kaitai_vertex.position.y, kaitai_vertex.position.z))
        v.normal = mathutils.Vector((kaitai_vertex.normal.x, kaitai_vertex.normal.y, kaitai_vertex.normal.z))
        v.uv = mathutils.Vector((kaitai_vertex.uv.u, kaitai_vertex.uv.v))
        return v


# vector2 kaitai to mathutils
def vector2_kaitai_to_mathutils(v) -> mathutils.Vector:
    return mathutils.Vector((v.x, v.y))


# vector3 kaitai to mathutils
def vector3_kaitai_to_mathutils(v) -> mathutils.Vector:
    return mathutils.Vector((v.x, v.y, v.z))


# convert coordinate system
def convert_coordinate_system(v: mathutils.Vector) -> mathutils.Vector:
    return mathutils.Vector((v.x, v.z, -v.y))
