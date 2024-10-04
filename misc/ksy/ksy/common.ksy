meta:
  id: common
  title: Common
  application: Run8 Train Simulator V3
  endian: le
  ks-opaque-types: true
types:
  string:
    seq:
      - id: len_value
        type: s4
      - id: value
        size: len_value
        process: lib_run8.string_utils.decode_run8_string(len_value)
  cs_string:
    seq:
      - id: len
        type: u1
        doc: Length of the string as a 7 bit encoded int
      - id: value
        type: str
        encoding: UTF-8
        size: len
        doc: The string
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
    seq:
      - id: value
        type: u1
    instances:
      is_true:
        value: value != 0
      is_false:
        value: value == 0
