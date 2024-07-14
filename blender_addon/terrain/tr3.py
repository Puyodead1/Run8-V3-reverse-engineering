from ..binreader import BinaryReader
from .terrain_utils import ETileType, TerrainTile


class TR3(TerrainTile):
    def __init__(self, reader: BinaryReader, x: float, y: float) -> None:
        super().__init__(reader, ETileType.TR3, x, y)

        self.read()

    def read(self):
        pass
