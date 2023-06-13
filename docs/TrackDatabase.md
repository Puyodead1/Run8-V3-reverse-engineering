# Track Database (TrackDatabase.r8)

Seems to contain a list of track locations as well as other track related information.

## Header

| Offset | Type  | Explaination     |
| ------ | ----- | ---------------- |
| 0      | Int32 | Reserved         |
| 4      | Int32 | Number of Tracks |

## Track

| Offset | Type     | Explaination                    |
| ------ | -------- | ------------------------------- |
| 0      | Int32    | Reserved                        |
| 4      | Int32    | Number of Unknown               |
| 8      | Unknown  | Unknown                         |
| ...    | Byte     | Unknown Boolean                 |
| ...    | Unknown2 | Number of Unknown2              |
| ...    | Byte     | Unknown Byte (Enum) (Always 3?) |
| ...    | Double   | Unknown                         |
| ...    | Byte     | Unknown Boolean                 |
| ...    | Byte     | Unknown Boolean                 |
| ...    | Int32    | Unknown                         |
| ...    | Byte     | Unknown Boolean                 |

## Unknown

| Offset | Type      | Explaination    |
| ------ | --------- | --------------- |
| 0      | Int32     | Reserved        |
| 4      | TileIndex | Tile Index      |
| 12     | Vector3   | Unknown         |
| 24     | Vector3   | Unknown         |
| 32     | Vector3   | Unknown         |
| 44     | Int32     | Unknown         |
| 48     | Byte      | Unknown Boolean |
| 49     | Byte      | Unknown Boolean |
| 50     | Float     | Unknown         |
| 54     | Int32     | Unknown         |
| 58     | Float     | Unknown         |
| 62     | Float     | Unknown         |
| 66     | Int32     | Unknown         |
| 70     | Int32     | Unknown         |
| 74     | Byte      | Unknown Boolean |

## Unknown2

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Unknown      |

## Tile Index

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | X            |
| 4      | Int32 | Y            |

## Vector3

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Float | X            |
| 4      | Float | Y            |
| 8      | Float | Z            |
