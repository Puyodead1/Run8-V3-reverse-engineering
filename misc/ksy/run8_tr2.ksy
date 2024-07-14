meta:
  id: run8_tr2
  title: Run8 TR2 Terrain Tile
  application: Run8 Train Simulator
  file-extension: tr2
  endian: le
  imports:
    - run8_common
seq:
  - id: texture_1
    type: run8_common::cs_string
  - id: texture_2
    type: run8_common::cs_string
  - id: texture_3
    type: run8_common::cs_string
  - id: texture_4
    type: run8_common::cs_string
  - id: chunks
    type: chunk_row
    repeat: expr
    repeat-expr: 25
  - id: lon_east
    type: f4
  - id: lon_west
    type: f4
  - id: lat_north
    type: f4
  - id: lat_south
    type: f4
types:
  chunk_row:
    seq:
      - id: chunks
        type: chunk
        repeat: expr
        repeat-expr: 25
  chunk:
    seq:
      - id: chunk_size
        type: u4
      - id: elevations_row
        type: elevation_col
        repeat: expr
        repeat-expr: chunk_size
  elevation_col:
    seq:
      - id: elevation
        type: f4
        repeat: expr
        repeat-expr: _parent.chunk_size