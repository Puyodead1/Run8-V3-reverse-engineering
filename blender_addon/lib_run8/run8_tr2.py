# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

from typing import List
import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO
from .run8_common import Run8Common


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 9):
    raise Exception("Incompatible Kaitai Struct Python API: 0.9 or later is required, but you have %s" % (kaitaistruct.__version__))

class Run8Tr2(KaitaiStruct):
    def __init__(self, _io, _parent=None, _root=None):
        self._io = _io
        self._parent = _parent
        self._root = _root if _root else self
        self._read()

    def _read(self):
        self.texture_1 = Run8Common.CsString(self._io, self, self._root)
        self.texture_2 = Run8Common.CsString(self._io, self, self._root)
        self.texture_3 = Run8Common.CsString(self._io, self, self._root)
        self.texture_4 = Run8Common.CsString(self._io, self, self._root)
        self.chunks: List[Run8Tr2.ChunkRow] = []
        for i in range(25):
            self.chunks.append(Run8Tr2.ChunkRow(self._io, self, self._root))

        self.lon_east = self._io.read_f4le()
        self.lon_west = self._io.read_f4le()
        self.lat_north = self._io.read_f4le()
        self.lat_south = self._io.read_f4le()

    class ChunkRow(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root if _root else self
            self._read()

        def _read(self):
            self.chunks: List[Run8Tr2.Chunk] = []
            for i in range(25):
                self.chunks.append(Run8Tr2.Chunk(self._io, self, self._root))



    class Chunk(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root if _root else self
            self._read()

        def _read(self):
            self.chunk_size = self._io.read_u4le()
            self.elevations: List[Run8Tr2.ElevationCol] = []
            for i in range(self.chunk_size):
                self.elevations.append(Run8Tr2.ElevationCol(self._io, self, self._root))



    class ElevationCol(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root if _root else self
            self._read()

        def _read(self):
            self.elevation: List[float] = []
            for i in range(self._parent.chunk_size):
                self.elevation.append(self._io.read_f4le())




