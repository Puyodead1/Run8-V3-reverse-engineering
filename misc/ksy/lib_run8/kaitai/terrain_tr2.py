# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild
# type: ignore

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO
from lib_run8.kaitai import common


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 11):
    raise Exception("Incompatible Kaitai Struct Python API: 0.11 or later is required, but you have %s" % (kaitaistruct.__version__))

class TerrainTr2(KaitaiStruct):
    def __init__(self, _io, _parent=None, _root=None):
        self._io = _io
        self._parent = _parent
        self._root = _root if _root else self
        self._read()

    def _read(self):
        self.texture_1 = common.Common.CsString(self._io)
        self.texture_2 = common.Common.CsString(self._io)
        self.texture_3 = common.Common.CsString(self._io)
        self.texture_4 = common.Common.CsString(self._io)
        self.chunks = []
        for i in range(25):
            self.chunks.append(TerrainTr2.ChunkRow(self._io, self, self._root))

        self.lon_east = self._io.read_f4le()
        self.lon_west = self._io.read_f4le()
        self.lat_north = self._io.read_f4le()
        self.lat_south = self._io.read_f4le()

    class Chunk(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.chunk_size = self._io.read_u4le()
            self.elevations_row = []
            for i in range(self.chunk_size):
                self.elevations_row.append(TerrainTr2.ElevationCol(self._io, self, self._root))



    class ChunkRow(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.chunks = []
            for i in range(25):
                self.chunks.append(TerrainTr2.Chunk(self._io, self, self._root))



    class ElevationCol(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.elevation = []
            for i in range(self._parent.chunk_size):
                self.elevation.append(self._io.read_f4le())




