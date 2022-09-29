# AI Signal Database (AISignalDatabase.r8)

Contains a list of signals and their information

## Header

| Offset | Type     | Explaination      |
| ------ | -------- | ----------------- |
| 0      | Int32    | Reserved          |
| 4      | Int32    | Number of entries |
| 8      | AISignal | AI Signal Entries |

## AISignal

| Offset | Type     | Explaination      |
| ------ | -------- | ----------------- |
| 0      | Int32    | Reserved          |
| 4      | Int32    | Unknown           |
| 8      | Unknown1 | Unknown1          |
| ...    | Byte     | Unknown Boolean   |
| ...    | Int32    | Unknown           |
| ...    | Int32    | Unknown           |
| ...    | Byte     | Unknown Boolean n |
| ...    | Unknown2 | Unknown2 (if n)   |
| ...    | Byte     | Unknown Boolean m |
| ...    | Unknown2 | Unknown2 (if m)   |

## Unknown1

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Unknown      |

## Unknown2

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Unknown      |
| 4      | Int32 | Unknown      |
