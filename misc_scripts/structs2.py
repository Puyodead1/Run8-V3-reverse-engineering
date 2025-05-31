from construct import Array, Float32l, GreedyBytes, Int8sl, Int8ul, Int32ul, Struct, this


def encode_7bit_int(length):
    result = bytearray()
    while length >= 0x80:
        result.append((length & 0x7F) | 0x80)
        length >>= 7
    result.append(length)
    return bytes(result)


def build_cs_string(text):
    encoded_text = text.encode("utf8")
    length_prefix = encode_7bit_int(len(encoded_text))
    return length_prefix + encoded_text


Chunk = Struct(
    "hixel_count" / Int32ul,
    "hixels" / Array(this.hixel_count, Array(this.hixel_count, Float32l)),
)

Vector3 = Struct(
    "x" / Float32l,
    "y" / Float32l,
    "z" / Float32l,
)

Vector4 = Struct(
    "x" / Float32l,
    "y" / Float32l,
    "z" / Float32l,
    "w" / Float32l,
)

TileIndex = Struct(
    "x" / Int32ul,
    "y" / Int32ul,
)

Decal = Struct(
    "r" / Float32l,
    "g" / Float32l,
    "b" / Float32l,
    "digit_count" / Int32ul,
    "digits" / Array(this.digit_count, Int32ul),
    "offset" / Vector3,
    "rotation" / Vector3,
    "size" / Float32l,
    "texture_name" / GreedyBytes,
)

SceneryAsset = Struct(
    "decal_count" / Int32ul,
    "decals" / Array(this.decal_count, Decal),
    "disregard_bounding_test" / Int8ul,
    "model_name" / GreedyBytes,
    "position" / Vector3,
    "rotation" / Vector3,
    "scale" / Vector3,
    "tile_index" / TileIndex,
)

TR4 = Struct(
    "texture_1" / GreedyBytes,
    "texture_2" / GreedyBytes,
    "texture_3" / GreedyBytes,
    "texture_4" / GreedyBytes,
    "chunks" / Array(25, Array(25, Chunk)),
    "east" / Float32l,
    "west" / Float32l,
    "north" / Float32l,
    "south" / Float32l,
    "veg_texture" / GreedyBytes,
    "scenery_asset_count" / Int32ul,
    "scenery_assets" / Array(this.scenery_asset_count, SceneryAsset),
    "plant_count" / Int32ul,
    "plants" / Array(this.plant_count, Vector4),
    "reserved" / Int32ul,
    "weight_map" / Int8sl,
)

TR2 = Struct(
    "texture_1" / GreedyBytes,
    "texture_2" / GreedyBytes,
    "texture_3" / GreedyBytes,
    "texture_4" / GreedyBytes,
    "chunks" / Array(25, Array(25, Chunk)),
    "east" / Float32l,
    "west" / Float32l,
    "north" / Float32l,
    "south" / Float32l,
    "veg_texture" / GreedyBytes,
)
