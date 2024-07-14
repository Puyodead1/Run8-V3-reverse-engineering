# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO
from .run8_common import Run8Common

if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 9):
    raise Exception("Incompatible Kaitai Struct Python API: 0.9 or later is required, but you have %s" % (kaitaistruct.__version__))

class Run8Tilescenery(KaitaiStruct):
    def __init__(self, _io, _parent=None, _root=None):
        self._io = _io
        self._parent = _parent
        self._root = _root if _root else self
        self._read()

    def _read(self):
        self.num_assets = self._io.read_s4le()
        self.assets = []
        for i in range(self.num_assets):
            self.assets.append(Run8Tilescenery.Asset(self._io, self, self._root))


    class Asset(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root if _root else self
            self._read()

        def _read(self):
            self.num_decals = self._io.read_s4le()
            self.decals = []
            for i in range(self.num_decals):
                self.decals.append(Run8Tilescenery.Decal(self._io, self, self._root))

            self.disregard_bounding_test = self._io.read_bits_int_be(1) != 0
            self._io.align_to_byte()
            self.model_name = Run8Common.CsString(self._io, self, self._root)
            self.position = Run8Common.Vector3(self._io, self, self._root)
            self.rotation = Run8Common.Vector3(self._io, self, self._root)
            self.scale = Run8Common.Vector3(self._io, self, self._root)
            self.tile_xz = Run8Common.Tilexz(self._io, self, self._root)


    class Decal(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root if _root else self
            self._read()

        def _read(self):
            self.color_r = self._io.read_f4le()
            self.color_g = self._io.read_f4le()
            self.color_b = self._io.read_f4le()
            self.num_digits = self._io.read_s4le()
            self.digits = []
            for i in range(self.num_digits):
                self.digits.append(Run8Tilescenery.Digit(self._io, self, self._root))

            self.offset = Run8Common.Vector3(self._io, self, self._root)
            self.rotation_deg = Run8Common.Vector3(self._io, self, self._root)
            self.size = self._io.read_f4le()
            self.texture_name = Run8Common.CsString(self._io, self, self._root)


    class Digit(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root if _root else self
            self._read()

        def _read(self):
            self.digit = self._io.read_s4le()



