# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild
# type: ignore

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO
import lib_run8.string_utils


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 11):
    raise Exception("Incompatible Kaitai Struct Python API: 0.11 or later is required, but you have %s" % (kaitaistruct.__version__))

class Common(KaitaiStruct):
    """Common Types
    
     ## Encoding Strings
     ```c#
     string s = "1ST COAST RECYCLING";
     byte[] bytes = Encoding.UTF8.GetBytes(s);
     byte[] encoded = new byte[bytes.Length * 2];
     int num = 0;
     for (int i = 0; i < bytes.Length; i++)
     {
     	encoded[num++] = (byte)(bytes[i] >> 4);
     	encoded[num++] = (byte)(bytes[i] << 4);
     }
     ```
    
     ## Decoding Strings
     ```c#
     byte[] encoded = <string data>;
     byte[] decodedBytes = new byte[encoded.Length / 2];
     int num = 0;
     for (int i = 0; i < decodedBytes.Length; i++)
     {
     	decodedBytes[i] |= (byte)(encoded[num++] << 4);
     	decodedBytes[i] |= (byte)(encoded[num++] >> 4);
     }
    
     string decodedString = Encoding.UTF8.GetString(decodedBytes);
     ```."""
    def __init__(self, _io, _parent=None, _root=None):
        self._io = _io
        self._parent = _parent
        self._root = _root if _root else self
        self._read()

    def _read(self):
        pass

    class Boolean(KaitaiStruct):
        """This is just a bullshit stub."""
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.value = self._io.read_u1()

        @property
        def is_false(self):
            if hasattr(self, '_m_is_false'):
                return self._m_is_false

            self._m_is_false = self.value == 0
            return getattr(self, '_m_is_false', None)

        @property
        def is_true(self):
            if hasattr(self, '_m_is_true'):
                return self._m_is_true

            self._m_is_true = self.value != 0
            return getattr(self, '_m_is_true', None)


    class Color(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.a = self._io.read_u1()
            self.r = self._io.read_u1()
            self.g = self._io.read_u1()
            self.b = self._io.read_u1()


    class CsString(KaitaiStruct):
        """C# style string."""
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.len = self._io.read_u1()
            self.value = (self._io.read_bytes(self.len)).decode(u"UTF-8")


    class Matrix4(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
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


    class R8string(KaitaiStruct):
        """Run8 specific string format."""
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.len_value = self._io.read_s4le()
            self._raw_value = self._io.read_bytes(self.len_value)
            _process = lib_run8.string_utils.DecodeRun8String(self.len_value)
            self.value = _process.decode(self._raw_value)


    class Tilexz(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.x = self._io.read_s4le()
            self.z = self._io.read_s4le()


    class Vector2(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.x = self._io.read_f4le()
            self.y = self._io.read_f4le()


    class Vector3(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.x = self._io.read_f4le()
            self.y = self._io.read_f4le()
            self.z = self._io.read_f4le()



