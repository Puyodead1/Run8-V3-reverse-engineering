from enum import IntFlag

from construct import (Bytes, Const, Enum, GreedyBytes, Int8sl, Int8ul,
                       Int16ul, Int32ul, Int64ul, PascalString, Struct, VarInt,
                       this)


# create Compiler Flags for deserialization
class CompilerFlags(IntFlag):
    NONE = 0
    DEBUG = 1
    SKIP_VALIDATION = 2
    SKIP_OPTIMIZATION = 4
    PACK_MATRIX_ROW_MAJOR = 8
    PACK_MATRIX_COLUMN_MAJOR = 16
    PARTIAL_PRECISION = 32
    AVOID_FLOW_CONTROL = 512
    PREFER_FLOW_CONTROL = 1024
    ENABLE_STRICTNESS = 2048
    ENABLE_BACKWARD_COMPATIBILITY = 4096
    IEEE_STRICTNESS = 8192
    OPTIMIZATION_LEVEL0 = 16384
    OPTIMIZATION_LEVEL1 = 0
    OPTIMIZATION_LEVEL2 = 49152
    OPTIMIZATION_LEVEL3 = 32768
    WARNINGS_ARE_ERRORS = 262144

STRING = Struct(
    "orh" / Int8sl,
    "length" / VarInt
)

SHADER_TYPE = Enum(
    Int8ul,
    VERTEX=0,
    NULL=1,
    DOMAIN=2,
    GEOMETRY=3,
    PIXEL=4,
    COMPUTE=5,
)

SHADER = Struct(
    "magic" / Const(b"SHDR"),
    "chunk_size" / Int32ul,
    "name" / STRING,
    "type" / SHADER_TYPE,
    "flags" / Int32ul,
    "level" / Int32ul,
    "bytecode_length" / VarInt,
    "bytecode" / Bytes(this.bytecode_length),
    "hashcode" / Int32ul,
    
)

TKB = Struct(
    "magic" / Const(b"TKFX"),
    "chunk_size" / Int32ul,
    "version" / Int32ul,
    "shader" / SHADER,
)

if __name__ == "__main__":
    with open("E:\\Run8Studios\\Run8 Train Simulator V3\\Content\\Shaders\\Avatar.tkb", 'rb') as f:
        data = f.read()
        parsed = TKB.parse(data)
        print(parsed)

        # get the flags forthe shader
        flags = [flag for flag in CompilerFlags if flag & parsed.shader.flags]
        print("Compiler Flags:")
        for flag in flags:
            print(f" - {flag.name} ({flag.value})")