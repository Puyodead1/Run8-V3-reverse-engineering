from enum import Enum, Flag
from typing import BinaryIO, Union

from binreader import BinaryReader


class SerializeFlags(Flag):
    Normal = 0
    Dynamic = 1
    Nullable = 2

class EffectShaderType(Enum):
    Vertex = 0
    Hull = 1
    Domain = 2
    Geometry = 3
    Pixel = 4
    Compute = 5

class EffectCompilerFlags(Flag):
    Debug = 1
    SkipValidation = 2
    SkipOptimization = 4
    PackMatrixRowMajor = 8
    PackMatrixColumnMajor = 16
    PartialPrecision = 32
    AvoidFlowControl = 512
    PreferFlowControl = 1024
    EnableStrictness = 2048
    EnableBackwardsCompatibility = 4096
    IeeeStrictness = 8192
    OptimizationLevel0 = 16384
    OptimizationLevel1 = 0
    OptimizationLevel2 = 49152
    OptimizationLevel3 = 32768
    WarningsAreErrors = 262144
    Empty = 0

class FeatureLevel(Enum):
    Level_9_1 = 37120
    Level_9_2 = 37376
    Level_9_3 = 37632
    Level_10_0 = 40960
    Level_10_1 = 41216
    Level_11_0 = 45056

class EffectParameterClass(Enum):
    Scalar = 0
    Vector = 1
    MatrixRows = 2
    MatrixColumns = 3
    Object = 4
    Struct = 5
    InterfaceClass = 6
    InterfacePointer = 7

class EffectParameterType(Enum):
    Void = 0
    Bool = 1
    Int = 2
    Float = 3
    String = 4
    Texture = 5
    Texture1D = 6
    Texture2D = 7
    Texture3D = 8
    TextureCube = 9
    Sampler = 10
    Sampler1D = 11
    Sampler2D = 12
    Sampler3D = 13
    SamplerCube = 14
    Pixelshader = 15
    Vertexshader = 16
    Pixelfragment = 17
    Vertexfragment = 18
    UInt = 19
    UInt8 = 20
    Geometryshader = 21
    Rasterizer = 22
    Depthstencil = 23
    Blend = 24
    Buffer = 25
    ConstantBuffer = 26
    TextureBuffer = 27
    Texture1DArray = 28
    Texture2DArray = 29
    Rendertargetview = 30
    Depthstencilview = 31
    Texture2DMultisampled = 32
    Texture2DMultisampledArray = 33
    TextureCubeArray = 34
    Hullshader = 35
    Domainshader = 36
    InterfacePointer = 37
    Computeshader = 38
    Double = 39
    RWTexture1D = 40
    RWTexture1DArray = 41
    RWTexture2D = 42
    RWTexture2DArray = 43
    RWTexture3D = 44
    RWBuffer = 45
    ByteAddressBuffer = 46
    RWByteAddressBuffer = 47
    StructuredBuffer = 48
    RWStructuredBuffer = 49
    AppendStructuredBuffer = 50
    ConsumeStructuredBuffer = 51

class WrappedBinaryReader(BinaryReader):
    def __init__(self, buf: BinaryIO, endian: str = "<") -> None:
        super().__init__(buf, endian)

    def is_serialize_null(self, flags: SerializeFlags = SerializeFlags.Nullable) -> bool:
        if (flags & SerializeFlags.Nullable) != SerializeFlags.Normal:
            # read a byte
            aByte = self.read_byte()

            # 1 stores a reference, 2 gets this reference
            if aByte == 1:
                print("is_serialize_null: 1")
            elif aByte == 2:
                raise Exception("length 2 is not supported")

            # if 0, string is null
            return aByte == 0
        
        return False
    
    def read_7bit_encoded_int(self) -> int:
        value = 0
        shift = 0
        while True:
            byte_value = ord(self.read(1))
            value |= (byte_value & 0x7F) << shift
            shift += 7
            if (byte_value & 0x80) == 0:
                break
        return value
    
    def serialize_string(self, flags=SerializeFlags.Normal) -> Union[str, None]:
        if self.is_serialize_null(flags=flags):
            return None

        # read the size
        size = self.read_7bit_encoded_int()
        # read the string
        return self.read(size).decode("utf-8")
        
    def serialize_bytes(self, flags=SerializeFlags.Normal) -> bytes:
        if self.is_serialize_null(flags=flags):
            return None
        
        # read the size
        size = self.read_7bit_encoded_int()
        # read the bytes
        return self.read(size)
    
    def serialize_class_array(self, class_type: type) -> object:
        if self.is_serialize_null(flags=SerializeFlags.Normal):
            return None
        
        # read the number of classes
        count = self.read_7bit_encoded_int()
        return [class_type(self) for i in range(count)]

class FourCC:
    @classmethod
    def calculate(self, code):
        """
        Create a FourCC code from a four-character string.
        """
        if len(code) != 4:
            raise ValueError("FourCC code must be a four-character string.")
        
        fourcc = 0
        for i, char in enumerate(code):
            fourcc += ord(char) << (8 * i)
        
        return fourcc

class Chunk:
    id: int
    index_end: int
    reader: WrappedBinaryReader

    def __init__(self, id: str, reader: WrappedBinaryReader):
        self.id = FourCC.calculate(id)
        self.reader = reader

        # read the magic from the file
        magic = reader.read_int32()
        if magic != self.id:
            raise ValueError("Invalid Chunk Magic.")
        
        # read the chunk end index
        self.index_end = reader.read_int32()

class TKFX(Chunk):
    version: int

    def __init__(self, reader: WrappedBinaryReader):
        super().__init__("TKFX", reader)
        self.version = reader.read_int32()

        # hard coded version check
        if self.version != 257:
            raise ValueError("Invalid TKFX version.")

class SHDRShaderSignatureSemantic:
    name: str
    index: int
    register: int
    system_value_type: int
    component_type: int
    usage_mask: int
    read_write_mask: int
    stream: int

    def __init__(self, reader: WrappedBinaryReader):
        self.name = reader.serialize_string()
        self.index = reader.read_byte()
        self.register = reader.read_byte()
        self.system_value_type = reader.read_byte()
        self.component_type = reader.read_byte()
        self.usage_mask = reader.read_byte()
        self.read_write_mask = reader.read_byte()
        self.stream = reader.read_byte()

    def __repr__(self) -> str:
        return f"SHDRShaderSignatureSemantic(name={self.name}, index={self.index}, register={self.register}, system_value_type={self.system_value_type}, component_type={self.component_type}, usage_mask={self.usage_mask}, read_write_mask={self.read_write_mask}, stream={self.stream})"

class SHDRShaderSignature:
    semantics: list[SHDRShaderSignatureSemantic]
    bytecode: bytes
    hashcode: int

    def __init__(self, reader: WrappedBinaryReader) -> None:
        self.semantics = reader.serialize_class_array(SHDRShaderSignatureSemantic)
        # read bytecode
        self.bytecode = reader.serialize_bytes(flags=SerializeFlags.Nullable)
        # read hashcode
        self.hashcode = reader.read_int32()

    def __repr__(self) -> str:
        return f"SHDRShaderSignature(semantics={self.semantics}, bytecode={self.bytecode}, hashcode={self.hashcode})"

class Base:
    name: str
    parameter_class: EffectParameterClass
    parameter_type: EffectParameterType

    def __init__(self, reader: WrappedBinaryReader) -> None:
        self.name = reader.serialize_string()
        self.parameter_class = EffectParameterClass(reader.read_byte())
        self.parameter_type = EffectParameterType(reader.read_byte())

    def __repr__(self) -> str:
        return f"Base(name={self.name}, parameter_class={self.parameter_class}, parameter_type={self.parameter_type})"
    
class ValueTypeParameter(Base):
    offset: int
    count: int
    size: int
    row_count: int
    column_count: int
    default_value: bytes

    def __init__(self, reader: WrappedBinaryReader) -> None:
        super().__init__(reader)
        self.offset = reader.read_7bit_encoded_int()
        self.count = reader.read_7bit_encoded_int()
        self.size = reader.read_7bit_encoded_int()
        self.row_count = reader.read_byte()
        self.column_count = reader.read_byte()
        self.default_value = reader.serialize_bytes(flags=SerializeFlags.Nullable)

    def __repr__(self) -> str:
        return f"ValueTypeParameter(name={self.name}, class={self.parameter_class}, type={self.parameter_type}, offset={self.offset}, count={self.count}, size={self.size}, row_count={self.row_count}, column_count={self.column_count}, default_value={self.default_value})"

class ConstantBuffer:
    name: str
    size: int
    parameters: list[ValueTypeParameter]

    def __init__(self, reader: WrappedBinaryReader) -> None:
        self.name = reader.serialize_string()
        self.size = reader.read_7bit_encoded_int()

        # read an array of parameters
        self.parameters = reader.serialize_class_array(ValueTypeParameter)

    def __repr__(self) -> str:
        return f"ConstantBuffer(name={self.name}, size={self.size}, parameters={self.parameters})"

class ResourceParameter(Base):
    slot: int
    count: int

    def __init__(self, reader: WrappedBinaryReader) -> None:
        super().__init__(reader)
        self.slot = reader.read_byte()
        self.count = reader.read_byte()

    def __repr__(self) -> str:
        return f"ResourceParameter(name={self.name}, class={self.parameter_class}, type={self.parameter_type}, slot={self.slot}, count={self.count})"

class SHDRShader:
    name: str
    shader_type: EffectShaderType
    compiler_flags: EffectCompilerFlags
    feature_level: FeatureLevel
    bytecode: bytes
    hashcode: int
    input_signature: SHDRShaderSignature
    output_signature: SHDRShaderSignature
    constant_buffers: list[ConstantBuffer]
    resource_parameters: list[ResourceParameter]

    def __init__(self, reader: WrappedBinaryReader) -> None:
        self.name = reader.serialize_string(flags=SerializeFlags.Nullable)
        # read the shader type
        self.shader_type = EffectShaderType(reader.read_byte())
        # read compiler flags
        self.compiler_flags = EffectCompilerFlags(reader.read_int32())
        # read feature level
        self.feature_level = FeatureLevel(reader.read_int32())
        # read bytecode
        self.bytecode = reader.serialize_bytes()
        # read hashcode
        self.hashcode = reader.read_int32()
        # read input signature
        self.input_signature = SHDRShaderSignature(reader)
        # read output signature
        self.output_signature = SHDRShaderSignature(reader)
        # read constant buffers
        self.constant_buffers = reader.serialize_class_array(ConstantBuffer)
        # read resource parameters
        self.resource_parameters = reader.serialize_class_array(ResourceParameter)

    def __repr__(self) -> str:
        return f"SHDRShader(name={self.name}, shader_type={self.shader_type}, compiler_flags={self.compiler_flags}, feature_level={self.feature_level}, bytecode={self.bytecode}, hashcode={self.hashcode}, input_signature={self.input_signature}, output_signature={self.output_signature}, constant_buffers={self.constant_buffers}, resource_parameters={self.resource_parameters})"

class SHDR(Chunk):
    def __init__(self, reader: WrappedBinaryReader):
        super().__init__("SHDR", reader)

        # read an array of shaders
        num_shaders = self.reader.read_7bit_encoded_int()
        print("Shader Count: " + str(num_shaders))
        for i in range(num_shaders):
            shader = SHDRShader(self.reader)
            print(shader)


with open("C:\\Run8Studios\\Run8 Train Simulator V3\\Content\\Shaders\\Avatar.tkb", "rb") as f:
    reader = WrappedBinaryReader(f)
    tkfx = TKFX(reader)
    shdr = SHDR(reader)