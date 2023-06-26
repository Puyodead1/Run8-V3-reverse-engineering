import struct
import types
from io import BytesIO


class BinaryStream(object):
    _types = {
        int : struct.Struct("<i"),
        float : struct.Struct("<d"),
        bool : struct.Struct("<?"),
    }
    _formats = {
        int : "i",
        float : "d",
        bool : "?",
    }


class BinaryWriter(BinaryStream):
    def __init__(self):
        self.stream = BytesIO()

    def clear(self):
        self.stream = BytesIO()

    def __iadd__(self, value):
        try:
            self.stream.write(self._types[type(value)].pack(value))
        except KeyError:
            if type(value) == bytes:
                self.add_string(value)
            elif type(value) == str:
                self.add_string(value.encode("ascii"))
            elif type(value) == tuple:
                for v in value:
                    self += v
            elif type(value) == list:
                self.add_list(value)
            elif type(value) == dict:
                self.add_dict(value)
            else:
                if hasattr(value, 'dump'):
                    self.add_string(value.dump())
                else:
                    raise
        return self

    def add(self, format, value):
        self.stream.write(struct.pack(format, value))

    def add_7bit_int(self, value):
        temp = value
        bytess = ""
        while temp >= 128:
           bytess += chr(0x000000FF & (temp | 0x80))
           temp >>= 7
        bytess += chr(temp)
        self.stream.write(bytess)

    def add_string(self, value):
        self.add_7bit_int(len(value))
        self.stream.write(value)

    def add_double(self, value):
        self.stream.write(struct.pack("<d", value))

    def add_float(self, value):
        self.stream.write(struct.pack("<f", value))

    def extend(self, value):
        self.stream.write(value)

    def add_dict(self, value):
        self += len(value)
        for k,v in list(value.items()):
            self += k    
            self += v    

    def add_list(self, value):
        self += len(value)
        if value:
            if type(value[0]) in self._types:
                self.stream.write(struct.pack("<" + (self._formats[type(value[0])] * len(value)), *value))
            else:
                for s in value:
                    self += s
                
    def serial(self):
        self.stream.seek(0)
        return self.stream.read()
        

class BinaryReader(BinaryStream):
    _sizes = {
        int : 4,
        float : 8,
        bool : 1,
    }

    def __init__(self, serial):
        self.stream = serial
        self.index = 0

    def read(self, type):
        try:
            value = self._types[type].unpack_from(self.stream, self.index)[0]
            self.index += self._sizes[type]
        except KeyError:
            if type == str:
                size = self.read_7bit_int()
                value = self.stream[self.index:self.index+size] 
                self.index += size
            elif hasattr(type, 'load'):
                value = type.load(self.read(str))
            else: raise
        return value        

    def next(self, count):
        return self.stream[self.index:self.index+count]

    def pull(self, count):
        s = self.stream[self.index:self.index+count]
        self.index += count
        return s

    def remainder(self):
        v = self.stream[self.index:]
        self.index = len(self.stream)
        return v
    
    def peek(self, type):
        index = self.index
        value = self.read(type)
        self.index = index
        return value

    def peek_list(self, type):
        index = self.index
        value = self.read_list(type)
        self.index = index
        return value

    def peek_dict(self, k, v):
        index = self.index
        value = self.read_dict(k, v)
        self.index = index
        return value

    def read_double(self):
        return self.read(float)

    def read_float(self):
        return struct.unpack("f", self.pull(4))[0] 
    
    def read_byte(self):
        return struct.unpack("b", self.pull(1))[0]
    
    def read_7bit_int(self):
        value = 0
        shift = 0
        while True:
            val = ord(self.pull(1))
            if val & 128 == 0: break
            value |= (val & 0x7F) << shift
            shift += 7
        return value | (val << shift)

    def read_list(self, type):
        size = self.read(int)
        if type in self._formats:
            value = list(struct.unpack_from("<" + (self._formats[type]*size), self.stream, self.index))
            self.index += (size * self._sizes[type])
        elif type == bytes:
            value = [self.read(str) for i in range(size)]
        elif hasattr(type, 'load'):
            value = [type.load(self.read(str)) for i in range(size)]
        else:
            raise TypeError
        return value

    def read_dict(self, k, v):
        size = self.read(int)
        D = {}
        for i in range(size):
            key = self.read(k)
            value = self.read(v)
            D[key] = value
        return D
            
