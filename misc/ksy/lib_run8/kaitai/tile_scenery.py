# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild
# type: ignore

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO
from . import common


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 11):
    raise Exception("Incompatible Kaitai Struct Python API: 0.11 or later is required, but you have %s" % (kaitaistruct.__version__))

class TileScenery(KaitaiStruct):
    def __init__(self, _io, _parent=None, _root=None):
        self._io = _io
        self._parent = _parent
        self._root = _root if _root else self
        self._read()

    def _read(self):
        self.num_assets = self._io.read_s4le()
        self.assets = []
        for i in range(self.num_assets):
            self.assets.append(TileScenery.Asset(self._io, self, self._root))


    class Asset(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.num_decals = self._io.read_s4le()
            self.decals = []
            for i in range(self.num_decals):
                self.decals.append(TileScenery.Decal(self._io, self, self._root))

            self.disregard_bounding_test = self._io.read_s1()
            self.model_name = common.Common.CsString(self._io)
            self.position = common.Common.Vector3(self._io)
            self.rotation = common.Common.Vector3(self._io)
            self.scale = common.Common.Vector3(self._io)
            self.tile_xz = common.Common.Tilexz(self._io)


    class Decal(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.color_r = self._io.read_f4le()
            self.color_g = self._io.read_f4le()
            self.color_b = self._io.read_f4le()
            self.num_digits = self._io.read_s4le()
            self.digits = []
            for i in range(self.num_digits):
                self.digits.append(TileScenery.Digit(self._io, self, self._root))

            self.offset = common.Common.Vector3(self._io)
            self.rotation_deg = common.Common.Vector3(self._io)
            self.size = self._io.read_f4le()
            self.texture_name = common.Common.CsString(self._io)


    class Digit(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.digit = self._io.read_s4le()



