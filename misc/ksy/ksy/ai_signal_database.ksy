doc: Contains a list of AI Signals
meta:
  id: ai_signal_database
  title: AISignalDatabase (AISignalDatabase.r8)
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
    type: ai_signal
    repeat: expr
    repeat-expr: num_entries

types:
  ai_signal:
    seq:
      - id: reserved
        type: s4
      - id: num_entries
        type: s4
      - id: entries
        type: s4
        repeat: expr
        repeat-expr: num_entries
      - id: bool1
        type: common::boolean
      - id: int1
        type: s4
      - id: int2
        type: s4
      - id: bool2
        type: common::boolean
      - id: class_341_1
        type: class_341
        if: bool2.is_true
        doc: c:Only if bool2 is true
      - id: bool3
        type: common::boolean
      - id: class_341_2
        type: class_341
        if: bool3.is_true
        doc: c:Only if bool3 is true
  class_341:
    seq:
      - id: int1
        type: s4
      - id: int2
        type: s4
