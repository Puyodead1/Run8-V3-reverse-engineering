meta:
  id: stars4
  title: Run8 Stars4
  application: Run8 Train Simulator
  file-extension: rn8
  endian: le
  imports:
    - common
seq:
  - id: reserved
    type: s4
  - id: count
    type: s4
  - id: strings
    type: common::string
    repeat: expr
    repeat-expr: count
