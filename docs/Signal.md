# Signal (.sig)

Contains information related to a signal

## Header

| Offset | Type     | Explaination                                                  |
| ------ | -------- | ------------------------------------------------------------- |
| 0      | String   | Signal Name                                                   |
| ...    | Int32    | Unknown                                                       |
| ...    | Byte     | Unknown Boolean (might be related to signal instruction stop) |
| ...    | Byte     | Unknown Boolean                                               |
| ...    | Int32    | Number of unknown1                                            |
| ...    | Unknown1 | Unknown1 Entries                                              |

## Unknown1

| Offset | Type     | Explaination       |
| ------ | -------- | ------------------ |
| 0      | Vector3  | Unknown            |
| 12     | Vector3  | Unknown            |
| 24     | Float    | Unknown            |
| 28     | Float    | Unknown            |
| 32     | Int32    | Number of unknown2 |
| 36     | Unknown2 | Unknown2 Entries   |

## Unknown2

| Offset | Type    | Explaination |
| ------ | ------- | ------------ |
| 0      | Vector3 | Unknown      |

## Vector3

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Float | X            |
| 4      | Float | Y            |
| 8      | Float | Z            |
