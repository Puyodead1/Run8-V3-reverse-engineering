from kaitaistruct import KaitaiStruct


class DecodeRun8String(KaitaiStruct):
    def __init__(self, len_buf):
        super().__init__(len_buf)
        self.len = len_buf

    def decode(self, data: bytes) -> str:
        size = self.len // 2
        array = bytearray(size)
        num = 0
        for i in range(size):
            array[i] |= data[num] << 4
            num += 1
            array[i] |= data[num] >> 4
            num += 1
        return array.decode("utf8")
