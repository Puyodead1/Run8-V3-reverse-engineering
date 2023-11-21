import struct
from io import SEEK_CUR
from typing import BinaryIO

ENDIAN_PREFIXES = ("@", "<", ">", "=", "!")


class BinaryWriter:
    def __init__(self, buf: BinaryIO, endian: str = "<") -> None:
        self.buf = buf
        self.endian = endian

    def align(self) -> None:
        old = self.tell()
        new = (old + 3) & -4
        if new > old:
            self.seek(new - old, SEEK_CUR)

    def write(self, *args) -> int:
        return self.buf.write(*args)

    def seek(self, *args) -> int:
        return self.buf.seek(*args)

    def tell(self) -> int:
        return self.buf.tell()

    def write_string(self, value: str, encoding: str = "utf-8") -> int:
        """
        Writes a 7 bit prefixed string to the buffer.
        """
        encoded_string = value.encode(encoding)
        size = self.write_7bit_encoded_int(len(encoded_string))
        size += self.write(encoded_string)

        return size

    def write_cstring(self, value: str) -> int:
        return self.write_string(value + "\0")

    def write_7bit_encoded_int(self, value: int) -> int:
        while value >= 128:
            self.write(bytes([((value & 0x7F) | 0x80)]))
            value >>= 7
        return self.write(bytes([value]))

    def write_bool(self, value: bool) -> int:
        return self.write(struct.pack(self.endian + "b", int(value)))

    def write_byte(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "b", value))

    def write_ubyte(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "B", value))

    def write_int16(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "h", value))

    def write_uint16(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "H", value))

    def write_int32(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "i", value))

    def write_uint32(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "I", value))

    def write_int64(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "q", value))

    def write_uint64(self, value: int) -> int:
        return self.write(struct.pack(self.endian + "Q", value))

    def write_float(self, value: float) -> int:
        return self.write(struct.pack(self.endian + "f", value))

    def write_double(self, value: float) -> int:
        return self.write(struct.pack(self.endian + "d", value))

    def write_struct(self, format: str, *values) -> int:
        if not format.startswith(ENDIAN_PREFIXES):
            format = self.endian + format
        data = struct.pack(format, *values)
        return self.write(data)

    # Aliases
    def write_int(self, value: int) -> int:
        return self.write_int32(value)

    def write_uint(self, value: int) -> int:
        return self.write_uint32(value)
