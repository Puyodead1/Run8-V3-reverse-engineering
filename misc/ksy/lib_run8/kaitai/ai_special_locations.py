# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild
# type: ignore

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO
from . import common
from enum import IntEnum


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 11):
    raise Exception("Incompatible Kaitai Struct Python API: 0.11 or later is required, but you have %s" % (kaitaistruct.__version__))

class AiSpecialLocations(KaitaiStruct):
    """Contains a list of AI Special Locations."""

    class AiSpecialLocationType(IntEnum):
        spawn_point = 0
        crew_change = 1
        crew_change_and_hold = 2
        passenger = 3
        passenger_crew_change = 4
        passenger_crew_change_and_hold = 5
        reliquish = 6
        passenger_reliquish = 7
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
            self.entries.append(AiSpecialLocations.AiSpecialLocation(self._io, self, self._root))


    class AiSpecialLocation(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.reserved = self._io.read_s4le()
            self.name = common.Common.R8string(self._io)
            self.type = KaitaiStream.resolve_enum(AiSpecialLocations.AiSpecialLocationType, self._io.read_u1())
            self.int1 = self._io.read_s4le()
            self.int2 = self._io.read_s4le()
            self.int3 = self._io.read_s4le()
            self.float1 = self._io.read_f4le()
            self.int4 = self._io.read_s4le()
            self.bool1 = common.Common.Boolean(self._io)



