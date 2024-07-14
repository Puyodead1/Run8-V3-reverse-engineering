# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 9):
    raise Exception("Incompatible Kaitai Struct Python API: 0.9 or later is required, but you have %s" % (kaitaistruct.__version__))

class Run8Common(KaitaiStruct):
    def __init__(self, _io, _parent=None, _root=None):
        self._io = _io
        self._parent = _parent
        self._root = _root if _root else self
        self._read()

    def _read(self):
        pass

    class Vector2(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root if _root else self
            self._read()

        def _read(self):
            self.x = self._io.read_f4le()
            self.y = self._io.read_f4le()


    class Tilexz(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root if _root else self
            self._read()

        def _read(self):
            self.x = self._io.read_s4le()
            self.z = self._io.read_s4le()


    class CsString(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root if _root else self
            self._read()

        def _read(self):
            self.len = self._io.read_u1()
            self.value = (self._io.read_bytes(self.len)).decode(u"UTF-8")


    class Matrix4(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root if _root else self
            self._read()

        def _read(self):
            self.m11 = self._io.read_f4le()
            self.m12 = self._io.read_f4le()
            self.m13 = self._io.read_f4le()
            self.m14 = self._io.read_f4le()
            self.m21 = self._io.read_f4le()
            self.m22 = self._io.read_f4le()
            self.m23 = self._io.read_f4le()
            self.m24 = self._io.read_f4le()
            self.m31 = self._io.read_f4le()
            self.m32 = self._io.read_f4le()
            self.m33 = self._io.read_f4le()
            self.m34 = self._io.read_f4le()
            self.m41 = self._io.read_f4le()
            self.m42 = self._io.read_f4le()
            self.m43 = self._io.read_f4le()
            self.m44 = self._io.read_f4le()


    class Vector3(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root if _root else self
            self._read()

        def _read(self):
            self.x = self._io.read_f4le()
            self.y = self._io.read_f4le()
            self.z = self._io.read_f4le()



