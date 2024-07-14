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
