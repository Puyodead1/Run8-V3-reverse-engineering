doc: Contains a list of AI Track Speeds
meta:
  id: ai_track_speed_database
  title: AITrackSpeedDatabase (AITrackSpeedDatabase.r8)
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
    type: ai_track_speed
    repeat: expr
    repeat-expr: num_entries

types:
  ai_track_speed:
    seq:
      - id: reserved
        type: s4
      - id: int1
        type: s4
      - id: num_entries
        type: s4
      - id: entries
        type: class_322
        repeat: expr
        repeat-expr: num_entries
  class_322:
    seq:
      - id: reserved
        type: s4
      - id: int1
        type: s4
      - id: int2
        type: s4
      - id: int3
        type: s4
