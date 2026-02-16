# AI Signal Database (AISignalDatabase.r8)

Contains a list of signals and their information

## Header

| Offset | Type       | Explaination   |
| ------ | ---------- | -------------- |
| 0      | Int32      | Reserved       |
| 4      | Int32      | AISignal Count |
| 8      | AISignal[] | AISignals      |

## AISignal

| Offset | Type     | Explaination            |
| ------ | -------- | ----------------------- |
| 0      | Int32    | Reserved                |
| 4      | Int32    | Signal Index Count      |
| 8      | Int32[]  | Signal Indices          |
| ...    | Bool     | Unknown/Unused?         |
| ...    | Int32    | Unknown                 |
| ...    | Int32    | Stopping Distance Feet? |
| ...    | Bool     | Unknown Boolean n       |
| ...    | RouteTrackReference | Unknown2 (if n)         |
| ...    | Bool     | Unknown Boolean m       |
| ...    | RouteTrackReference | Unknown2 (if m)         |

## RouteTrackReference

| Offset | Type  | Explaination  |
| ------ | ----- | ------------- |
| 0      | Int32 | Route ID      |
| 4      | Int32 | Track ID      |
