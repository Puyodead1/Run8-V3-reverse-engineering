# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild
# type: ignore

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO
from lib_run8.kaitai import common


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 11):
    raise Exception("Incompatible Kaitai Struct Python API: 0.11 or later is required, but you have %s" % (kaitaistruct.__version__))

class ServiceAreaDatabase(KaitaiStruct):
    def __init__(self, _io, _parent=None, _root=None):
        self._io = _io
        self._parent = _parent
        self._root = _root if _root else self
        self._read()

    def _read(self):
        self.reserved = self._io.read_s4le()
        self.num_service_areas = self._io.read_s4le()
        self.service_areas = []
        for i in range(self.num_service_areas):
            self.service_areas.append(ServiceAreaDatabase.ServiceArea(self._io, self, self._root))


    class Class646(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.enum50_0 = self._io.read_s1()
            self.enum60_0 = self._io.read_s1()
            self.double1 = self._io.read_f8le()
            self.double2 = self._io.read_f8le()


    class Path1(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.tile_xz = common.Common.Tilexz(self._io)
            self.position = common.Common.Vector3(self._io)
            self.float0 = self._io.read_f4le()
            self.bool0 = common.Common.Boolean(self._io)
            self.bool1 = common.Common.Boolean(self._io)
            self.bool2 = common.Common.Boolean(self._io)
            self.bool3 = common.Common.Boolean(self._io)


    class Path2(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.tile_xz = common.Common.Tilexz(self._io)
            self.position = common.Common.Vector3(self._io)
            self.float0 = self._io.read_f4le()
            self.class646 = ServiceAreaDatabase.Class646(self._io, self, self._root)


    class ServiceArea(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.num = self._io.read_s4le()
            if self.num == 1:
                self.path1 = ServiceAreaDatabase.Path1(self._io, self, self._root)

            if self.num == 2:
                self.path2 = ServiceAreaDatabase.Path2(self._io, self, self._root)




