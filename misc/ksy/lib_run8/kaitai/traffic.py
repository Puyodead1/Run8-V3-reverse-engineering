# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild
# type: ignore

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO
from lib_run8.kaitai import common
from enum import IntEnum


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 11):
    raise Exception("Incompatible Kaitai Struct Python API: 0.11 or later is required, but you have %s" % (kaitaistruct.__version__))

class Traffic(KaitaiStruct):
    """Traffic Data."""

    class TrainCaste(IntEnum):
        utter_peon = 0
        low = 1
        medium = 2
        high = 3
        king_of_the_rails = 4

    class TrainClass(IntEnum):
        none = 0
        passenger = 1
        baretables = 2
        container_domestic = 3
        container_international = 4
        container_mixed = 5
        intermodal = 6
        mixed_intermodal = 7
        freight_mixed = 8
        unit_autorack = 9
        unit_coal = 10
        unit_oil = 11
        unit_grain = 12
        unit_reefer = 13
        power_move = 14
        unit_bethgon_coal = 15
        unit_coil_steel = 16
        unit_aggregate = 17
        saved_train = 255

    class TrainSpecialRestrictions(IntEnum):
        none = 0
        tehachapi_wb = 2
        tehachapi_eb = 4
        cajon_wb1 = 8
        cajon_wb2 = 16
        cajon_eb1 = 32
        cajon_eb2 = 64
    def __init__(self, _io, _parent=None, _root=None):
        self._io = _io
        self._parent = _parent
        self._root = _root if _root else self
        self._read()

    def _read(self):
        self.int1 = self._io.read_s4le()
        self.bool1 = common.Common.Boolean(self._io)
        self.int2 = self._io.read_s4le()
        self.int3 = self._io.read_s4le()
        self.int4 = self._io.read_s4le()
        if self.int1 >= 2:
            self.int4_2 = self._io.read_s4le()

        self.num_entries = self._io.read_s4le()
        self.entries = []
        for i in range(self.num_entries):
            self.entries.append(Traffic.TrafficEntry(self._io, self, self._root))


    class Class139(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.num = self._io.read_s4le()
            self.bool1 = common.Common.Boolean(self._io)
            if self.bool1.is_true:
                self.name = common.Common.R8string(self._io)

            self.caste = KaitaiStream.resolve_enum(Traffic.TrainCaste, self._io.read_u1())
            self.special_restrictions = KaitaiStream.resolve_enum(Traffic.TrainSpecialRestrictions, self._io.read_u1())
            self.bool2 = common.Common.Boolean(self._io)
            self.bool3 = common.Common.Boolean(self._io)
            self.bool4 = common.Common.Boolean(self._io)
            self.num_entries1 = self._io.read_s4le()
            self.entries1 = []
            for i in range(self.num_entries1):
                self.entries1.append(common.Common.R8string(self._io))

            self.num_railroads = self._io.read_s4le()
            self.railroads = []
            for i in range(self.num_railroads):
                self.railroads.append(common.Common.R8string(self._io))

            self.num_engines = self._io.read_s4le()
            self.engines = []
            for i in range(self.num_engines):
                self.engines.append(common.Common.R8string(self._io))

            self.num_entries4 = self._io.read_s4le()
            self.entries4 = []
            for i in range(self.num_entries4):
                self.entries4.append(common.Common.R8string(self._io))

            if self.num > 1:
                self.conditional_block = Traffic.ConditionalBlock(self._io, self, self._root)



    class Class339(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.reserved = self._io.read_s4le()
            self.train_class = KaitaiStream.resolve_enum(Traffic.TrainClass, self._io.read_u1())
            self.int1 = self._io.read_s4le()
            self.num_entries = self._io.read_s4le()
            if self.train_class == Traffic.TrainClass.saved_train:
                self.entries_case1 = []
                for i in range(self.num_entries):
                    self.entries_case1.append(Traffic.SavedTrain(self._io, self, self._root))


            if self.train_class != Traffic.TrainClass.saved_train:
                self.entries_case2 = []
                for i in range(self.num_entries):
                    self.entries_case2.append(Traffic.Class139(self._io, self, self._root))




    class ConditionalBlock(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.num_cars = self._io.read_s4le()
            self.cars = []
            for i in range(self.num_cars):
                self.cars.append(common.Common.R8string(self._io))

            self.num_entries6 = self._io.read_s4le()
            self.entries6 = []
            for i in range(self.num_entries6):
                self.entries6.append(common.Common.R8string(self._io))

            if self._parent.num > 2:
                self.bool5 = common.Common.Boolean(self._io)



    class SavedTrain(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.reserved = self._io.read_s4le()
            self.string1 = common.Common.CsString(self._io)
            self.string2 = common.Common.CsString(self._io)
            self.int1 = self._io.read_s4le()


    class TrafficEntry(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.reserved = self._io.read_s4le()
            self.name = common.Common.R8string(self._io)
            self.int1 = self._io.read_s4le()
            self.num_entries = self._io.read_s4le()
            self.entries = []
            for i in range(self.num_entries):
                self.entries.append(Traffic.Class339(self._io, self, self._root))




