# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild
# type: ignore

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 11):
    raise Exception("Incompatible Kaitai Struct Python API: 0.11 or later is required, but you have %s" % (kaitaistruct.__version__))

class AiTrackSpeedDatabase(KaitaiStruct):
    """Contains a list of AI Track Speeds."""
    def __init__(self, _io, _parent=None, _root=None):
        self._io = _io
        self._parent = _parent
        self._root = _root if _root else self
        self._read()

    def _read(self):
        self.reserved = self._io.read_s4le()
        self.num_entries = self._io.read_s4le()
        self.entries = []
        for i in range(self.num_entries):
            self.entries.append(AiTrackSpeedDatabase.AiTrackSpeed(self._io, self, self._root))


    class AiTrackSpeed(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.reserved = self._io.read_s4le()
            self.int1 = self._io.read_s4le()
            self.num_entries = self._io.read_s4le()
            self.entries = []
            for i in range(self.num_entries):
                self.entries.append(AiTrackSpeedDatabase.Class322(self._io, self, self._root))



    class Class322(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.reserved = self._io.read_s4le()
            self.int1 = self._io.read_s4le()
            self.int2 = self._io.read_s4le()
            self.int3 = self._io.read_s4le()



