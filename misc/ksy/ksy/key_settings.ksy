doc: Stores game keybind settings
meta:
  id: key_settings
  title: Key Settings (Run8KeySettings.r8)
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
        type: common::r8string
      - id: enum70
        type: s1
      - id: num_keys
        type: s4
      - id: keys
        type: s4
        repeat: expr
        repeat-expr: num_keys
