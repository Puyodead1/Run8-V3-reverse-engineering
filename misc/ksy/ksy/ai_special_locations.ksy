doc: Contains a list of AI Special Locations
meta:
  id: ai_special_locations
  title: AISpecialLocations (AISpecialLocations.r8)
  application: Run8 Train Simulator
  file-extension: r8
  endian: le
  imports:
    - common
seq:
  - id: reserved
    type: s4
  - id: num_entries
    type: s4
  - id: entries
    type: ai_special_location
    repeat: expr
    repeat-expr: num_entries

types:
  ai_special_location:
    seq:
      - id: reserved
        type: s4
      - id: name
        type: common::r8string
      - id: type
        type: u1
        enum: ai_special_location_type
      - id: int1
        type: s4
      - id: int2
        type: s4
      - id: int3
        type: s4
      - id: float1
        type: f4
      - id: int4
        type: s4
      - id: bool1
        type: common::boolean

enums:
  ai_special_location_type:
    0: spawn_point
    1: crew_change
    2: crew_change_and_hold
    3: passenger
    4: passenger_crew_change
    5: passenger_crew_change_and_hold
    6: reliquish
    7: passenger_reliquish
