doc: Contains a list of strings, including file paths. Strings are referenced by their index.
meta:
  id: stars4
  title: Stars4 (stars4.r8)
  application: Run8 Train Simulator
  file-extension: rn8
  endian: le
  imports:
    - common
seq:
  - id: reserved
    type: s4
  - id: num_strings
    type: s4
  - id: strings
    type: common::r8string
    repeat: expr
    repeat-expr: num_strings
