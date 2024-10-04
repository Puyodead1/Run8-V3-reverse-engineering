meta:
  id: key_settings
  title: Run8 Key Settings
  application: Run8 Train Simulator
  file-extension: r8
  endian: le
  imports:
    - common
seq:
  - id: reserved
    type: s4
  - id: num_settings
    type: s4
  - id: settings
    type: key_setting
    repeat: expr
    repeat-expr: num_settings

types:
  key_setting:
    seq:
      - id: reserved
        type: s4
      - id: name
        type: common::string
      - id: enum70
        type: b1
      - id: num_keys
        type: s4
      - id: keys
        type: s4
        repeat: expr
        repeat-expr: num_keys
