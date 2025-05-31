doc: Traffic Data
meta:
  id: traffic
  title: Traffic (Traffic.r8)
  application: Run8 Train Simulator
  file-extension: r8
  endian: le
  imports:
    - common
seq:
  - id: int1
    type: s4
  - id: bool1
    type: common::boolean
  - id: int2
    type: s4
  - id: int3
    type: s4
  - id: int4
    type: s4
  - id: int4_2
    type: s4
    if: int1 >= 2
    doc: c:If int1 is 2 or greater, this field is present.
  - id: num_entries
    type: s4
  - id: entries
    type: traffic_entry
    repeat: expr
    repeat-expr: num_entries

types:
  traffic_entry:
    seq:
      - id: reserved
        type: s4
      - id: name
        type: common::r8string
      - id: int1
        type: s4
      - id: num_entries
        type: s4
      - id: entries
        type: class_339
        repeat: expr
        repeat-expr: num_entries
  class_339:
    seq:
      - id: reserved
        type: s4
      - id: train_class
        type: u1
        enum: train_class
      - id: int1
        type: s4
      - id: num_entries
        type: s4
      - id: entries_case1
        type: saved_train
        repeat: expr
        repeat-expr: num_entries
        if: train_class == train_class::saved_train
        doc: c:If train_class is saved_train
      - id: entries_case2
        type: class_139
        repeat: expr
        repeat-expr: num_entries
        if: train_class != train_class::saved_train
        doc: c:If train_class is not saved_train
  saved_train:
    seq:
      - id: reserved
        type: s4
      - id: string1
        type: common::cs_string
      - id: string2
        type: common::cs_string
      - id: int1
        type: s4
  class_139:
    seq:
      - id: num
        type: s4
      - id: bool1
        type: common::boolean
      - id: name
        type: common::r8string
        if: bool1.is_true
        doc: c:If bool1 is true, this field is present.
      - id: caste
        type: u1
        enum: train_caste
      - id: special_restrictions
        type: u1
        enum: train_special_restrictions
      - id: bool2
        type: common::boolean
      - id: bool3
        type: common::boolean
      - id: bool4
        type: common::boolean
      - id: num_entries1
        type: s4
      - id: entries1
        type: common::r8string
        repeat: expr
        repeat-expr: num_entries1
      - id: num_railroads
        type: s4
      - id: railroads
        type: common::r8string
        repeat: expr
        repeat-expr: num_railroads
      - id: num_engines
        type: s4
      - id: engines
        type: common::r8string
        repeat: expr
        repeat-expr: num_engines
      - id: num_entries4
        type: s4
      - id: entries4
        type: common::r8string
        repeat: expr
        repeat-expr: num_entries4
      - id: conditional_block
        type: conditional_block
        if: num > 1
  conditional_block:
    seq:
      - id: num_cars
        type: s4
      - id: cars
        type: common::r8string
        repeat: expr
        repeat-expr: num_cars
      - id: num_entries6
        type: s4
      - id: entries6
        type: common::r8string
        repeat: expr
        repeat-expr: num_entries6
      - id: bool5
        type: common::boolean
        if: _parent.num > 2
        doc: c:If _parent.num is greater than 2, this field is present.

enums:
  train_class:
    0: none
    1: passenger
    2: baretables
    3: container_domestic
    4: container_international
    5: container_mixed
    6: intermodal
    7: mixed_intermodal
    8: freight_mixed
    9: unit_autorack
    10: unit_coal
    11: unit_oil
    12: unit_grain
    13: unit_reefer
    14: power_move
    15: unit_bethgon_coal
    16: unit_coil_steel
    17: unit_aggregate
    255: saved_train
  train_caste:
    0: utter_peon
    1: low
    2: medium
    3: high
    4: king_of_the_rails
  train_special_restrictions:
    0: none
    2: tehachapi_wb
    4: tehachapi_eb
    8: cajon_wb1
    16: cajon_wb2
    32: cajon_eb1
    64: cajon_eb2
