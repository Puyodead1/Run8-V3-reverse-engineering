doc: "Common Types\n\n
  ## Encoding Strings\n
  ```c#\n
  string s = \"1ST COAST RECYCLING\";\n
  byte[] bytes = Encoding.UTF8.GetBytes(s);\n
  byte[] encoded = new byte[bytes.Length * 2];\n
  int num = 0;\n
  for (int i = 0; i < bytes.Length; i++)\n
  {\n
  \tencoded[num++] = (byte)(bytes[i] >> 4);\n
  \tencoded[num++] = (byte)(bytes[i] << 4);\n
  }\n
  ```\n\n
  ## Decoding Strings\n
  ```c#\n
  byte[] encoded = <string data>;\n
  byte[] decodedBytes = new byte[encoded.Length / 2];\n
  int num = 0;\n
  for (int i = 0; i < decodedBytes.Length; i++)\n
  {\n
  \tdecodedBytes[i] |= (byte)(encoded[num++] << 4);\n
  \tdecodedBytes[i] |= (byte)(encoded[num++] >> 4);\n
  }\n\n
  string decodedString = Encoding.UTF8.GetString(decodedBytes);\n
  ```"
meta:
  id: common
  title: Common
  application: Run8 Train Simulator V3
  endian: le
  ks-opaque-types: true
types:
  r8string:
    doc: Run8 specific string format
    seq:
      - id: len_value
        type: s4
        doc: d:Length of the encoded string, x2 len of decoded string
      - id: value
        size: len_value
        process: lib_run8.string_utils.decode_run8_string(len_value)
        doc: d:Decoded string
  cs_string:
    doc: C# style string
    seq:
      - id: len
        type: u1
        doc: d:Length of the string as a 7 bit encoded int
      - id: value
        type: str
        encoding: UTF-8
        size: len
  vector2:
    seq:
      - id: x
        type: f4
      - id: y
        type: f4
  vector3:
    seq:
      - id: x
        type: f4
      - id: y
        type: f4
      - id: z
        type: f4
  tilexz:
    seq:
      - id: x
        type: s4
      - id: z
        type: s4
  matrix4:
    seq:
      - id: m11
        type: f4
      - id: m12
        type: f4
      - id: m13
        type: f4
      - id: m14
        type: f4
      - id: m21
        type: f4
      - id: m22
        type: f4
      - id: m23
        type: f4
      - id: m24
        type: f4
      - id: m31
        type: f4
      - id: m32
        type: f4
      - id: m33
        type: f4
      - id: m34
        type: f4
      - id: m41
        type: f4
      - id: m42
        type: f4
      - id: m43
        type: f4
      - id: m44
        type: f4
  color:
    seq:
      - id: a
        type: u1
      - id: r
        type: u1
      - id: g
        type: u1
      - id: b
        type: u1
  boolean:
    doc: This is just a bullshit stub
    seq:
      - id: value
        type: u1
    instances:
      is_true:
        value: value != 0
      is_false:
        value: value == 0
