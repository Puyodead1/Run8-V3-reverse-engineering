meta:
  id: run8_common
  title: Common
  application: Run8 Train Simulator V3
  endian: le
types:
  c_string:
    seq:
      - id: len
        type: b8
        doc: Length of the string as a 7 bit encoded int
      - id: value
        type: str
        encoding: UTF-8
        size: len
        doc: The string
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