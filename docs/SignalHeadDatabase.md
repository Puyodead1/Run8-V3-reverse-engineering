# SignalHeadDatabase (.r8)

## Header

| Offset | Type      | Explaination            |
| ------ | --------- | ----------------------- |
| 0      | Int32     | Reserved                |
| 4      | Int32     | Int32 count             |
| ...    | Int32[]   | Unknown Array of Int32s |
| ...    | Int32     | Class665 Count          |
| ...    | Int32     | Signal Index            |
| ...    | Bool      | Unknown                 |
| ...    | R8String  | Name                    |
| ...    | Vector3   | Position                |
| ...    | Float32   | Rotation Degrees Y      |
| ...    | TileIndex | TileXZ                  |
| ...    | Int32     | Unknown                 |
| ...    | Bool      | Unknown                 |
| ...    | Bool      | Unknown                 |
| ...    | Bool      | Unknown                 |
| ...    | Bool      | Unknown                 |
| ...    | Bool      | Unknown                 |
| ...    | Bool      | Unknown                 |

## Class665

| Offset | Type     | Explaination                                |
| ------ | -------- | ------------------------------------------- |
| 0      | Int32    | Reserved                                    |
| 4      | R8String | Track Name                                  |
| ...    | Int32    | Unknown                                     |
| ...    | Int32    | Int32 Count                                 |
| ...    | Int32[]  | Array of Block Detector IDs?                |
| ...    | Int32    | Int32 Count                                 |
| ...    | Int32[]  | Unknown Array of Int32s                     |
| ...    | Int32    | Class669 Count                              |
| ...    | Int32[]  | Array of Class669                           |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Int32    | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Bool     | Unknown                                     |
| ...    | Byte     | Unknown Enum, related to signal instruction |

## Class669

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Reserved     |
| 4      | Int32 | Unknown      |
| 8      | Bool  | Unknown      |
