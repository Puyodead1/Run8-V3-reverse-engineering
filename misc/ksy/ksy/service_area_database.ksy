meta:
  id: service_area_database
  title: ServiceAreaDatabase (ServiceAreaDatabase.r8)
  application: Run8 Train Simulator
  file-extension: r8
  endian: le
  imports:
    - common
seq:
  - id: reserved
    type: s4
  - id: num_service_areas
    type: s4
  - id: service_areas
    type: service_area
    repeat: expr
    repeat-expr: num_service_areas

types:
  service_area:
    seq:
      - id: num
        type: s4
      - id: path1
        type: path1
        if: num == 1
        doc: c:Only if num == 1
      - id: path2
        type: path2
        if: num == 2
        doc: c:Only if num == 2
  path1:
    seq:
      - id: tile_xz
        type: common::tilexz
      - id: position
        type: common::vector3
      - id: float0
        type: f4
      - id: bool0
        type: common::boolean
      - id: bool1
        type: common::boolean
      - id: bool2
        type: common::boolean
      - id: bool3
        type: common::boolean
  path2:
    seq:
      - id: tile_xz
        type: common::tilexz
      - id: position
        type: common::vector3
      - id: float0
        type: f4
      - id: class646
        type: class646
  class646:
    seq:
      - id: enum50_0
        type: s1
      - id: enum60_0
        type: s1
      - id: double1
        type: f8
      - id: double2
        type: f8
