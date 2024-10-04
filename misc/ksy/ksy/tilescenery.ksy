meta:
  id: tile_scenery
  title: Run8 Tile Scenery File
  application: Run8 Train Simulator
  file-extension: rn8
  endian: le
  imports:
    - common
seq:
  - id: num_assets
    type: s4
  - id: assets
    type: asset
    repeat: expr
    repeat-expr: num_assets
types:
  asset:
    seq:
      - id: num_decals
        type: s4
      - id: decals
        type: decal
        repeat: expr
        repeat-expr: num_decals
      - id: disregard_bounding_test
        type: b1
      - id: model_name
        type: common::cs_string
      - id: position
        type: common::vector3
      - id: rotation
        type: common::vector3
      - id: scale
        type: common::vector3
      - id: tile_xz
        type: common::tilexz
  decal:
    seq:
      - id: color_r
        type: f4
      - id: color_g
        type: f4
      - id: color_b
        type: f4
      - id: num_digits
        type: s4
      - id: digits
        type: digit
        repeat: expr
        repeat-expr: num_digits
      - id: offset
        type: common::vector3
      - id: rotation_deg
        type: common::vector3
      - id: size
        type: f4
      - id: texture_name
        type: common::cs_string
  digit:
    seq:
      - id: digit
        type: s4
