# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild
# type: ignore

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO
from lib_run8.kaitai import common


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 11):
    raise Exception("Incompatible Kaitai Struct Python API: 0.11 or later is required, but you have %s" % (kaitaistruct.__version__))

class Avatar(KaitaiStruct):
    def __init__(self, _io, _parent=None, _root=None):
        self._io = _io
        self._parent = _parent
        self._root = _root if _root else self
        self._read()

    def _read(self):
        self.vertex_count = self._io.read_s4le()
        self.vertices = []
        for i in range(self.num_vertices):
            self.vertices.append(Avatar.VertexStruct(self._io, self, self._root))

        self.texture_count = self._io.read_s4le()
        self.textures = []
        for i in range(self.num_textures):
            self.textures.append(common.Common.CsString(self._io))

        self.is_ushort_index_buffer = self._io.read_bits_int_be(1) != 0
        self._io.align_to_byte()
        self.num_index_buffer = self._io.read_s4le()
        if self.is_ushort_index_buffer == True:
            self.ushort_index_buffer = []
            for i in range(self.num_index_buffer):
                self.ushort_index_buffer.append(self._io.read_u2le())


        if self.is_ushort_index_buffer == False:
            self.index_buffer = []
            for i in range(self.num_index_buffer):
                self.index_buffer.append(self._io.read_s4le())


        self.num_unknown_structs = self._io.read_s4le()
        self.unknown_structs = []
        for i in range(self.num_unknown_struct_altered):
            self.unknown_structs.append(Avatar.UnknownStruct(self._io, self, self._root))

        self.num_skeleton_hierarchy = self._io.read_s4le()
        self.skeleton_hierarchy = []
        for i in range(self.num_skeleton_hierarchy):
            self.skeleton_hierarchy.append(self._io.read_s4le())

        self.num_bone_indices = self._io.read_s4le()
        self.bone_indices = []
        for i in range(self.num_bone_indices):
            self.bone_indices.append(Avatar.BoneIndexStruct(self._io, self, self._root))

        self.num_bind_poses = self._io.read_s4le()
        self.bind_poses = []
        for i in range(self.num_bind_poses):
            self.bind_poses.append(common.Common.Matrix4(self._io))

        self.num_inverse_bind_poses = self._io.read_s4le()
        self.inverse_bind_poses = []
        for i in range(self.num_inverse_bind_poses):
            self.inverse_bind_poses.append(common.Common.Matrix4(self._io))

        self.num_animations = self._io.read_s4le()
        self.animations = []
        for i in range(self.num_animations):
            self.animations.append(Avatar.AnimationClip(self._io, self, self._root))


    class AnimationClip(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.key = common.Common.CsString(self._io)
            self.duration = self._io.read_f8le()
            self.num_keyframes = self._io.read_s4le()
            self.keyframes = []
            for i in range(self.num_keyframes):
                self.keyframes.append(Avatar.AnimationKeyframe(self._io, self, self._root))



    class AnimationKeyframe(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.bone_index = self._io.read_s4le()
            self.time = self._io.read_f8le()
            self.transform = common.Common.Matrix4(self._io)


    class BoneIndexStruct(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.key = common.Common.CsString(self._io)
            self.bone_index = self._io.read_s4le()


    class UnknownStruct(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.reserved = self._io.read_s4le()
            self.texture_index = self._io.read_s4le()
            self.num_index_buffer = self._io.read_s4le()
            self.start_index_location = self._io.read_s4le()
            self.base_vertex_location = self._io.read_s4le()


    class VertexStruct(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.reserved1 = self._io.read_f4le()
            self.position_x = self._io.read_f4le()
            self.normal_y = self._io.read_f4le()
            self.position_z = self._io.read_f4le()
            self.uv_x = self._io.read_f4le()
            self.normal_x = self._io.read_f4le()
            self.reserved2 = self._io.read_f4le()
            self.normal_z = self._io.read_f4le()
            self.uv_y = self._io.read_f4le()
            self.position_y = self._io.read_f4le()
            self.blend_index_w = self._io.read_u1()
            self.blend_weight_z = self._io.read_f4le()
            self.blend_index_x = self._io.read_u1()
            self.blend_weight_y = self._io.read_f4le()
            self.blend_index_y = self._io.read_u1()
            self.blend_weight_w = self._io.read_f4le()
            self.blend_idex_z = self._io.read_u1()
            self.blend_weight_x = self._io.read_f4le()


    @property
    def num_textures(self):
        if hasattr(self, '_m_num_textures'):
            return self._m_num_textures

        self._m_num_textures = self.texture_count + 6
        return getattr(self, '_m_num_textures', None)

    @property
    def num_unknown_struct_altered(self):
        if hasattr(self, '_m_num_unknown_struct_altered'):
            return self._m_num_unknown_struct_altered

        self._m_num_unknown_struct_altered = self.num_unknown_structs - 9
        return getattr(self, '_m_num_unknown_struct_altered', None)

    @property
    def num_vertices(self):
        if hasattr(self, '_m_num_vertices'):
            return self._m_num_vertices

        self._m_num_vertices = self.vertex_count // 7
        return getattr(self, '_m_num_vertices', None)


