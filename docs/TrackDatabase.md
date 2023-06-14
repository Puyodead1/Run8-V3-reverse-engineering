# Track Database (TrackDatabase.r8)

Seems to contain a list of track locations as well as other track related information.

## Header

| Offset | Type         | Explaination        |
| ------ | ------------ | ------------------- |
| 0      | Int32        | Reserved            |
| 4      | Int32        | Track Section Count |
| 8      | TrackSection | Track Sections      |

## Track Section

| Offset | Type     | Explaination                    |
| ------ | -------- | ------------------------------- |
| 0      | Int32    | Reserved                        |
| 4      | Int32    | Track Node Count                |
| 8      | TrackNode| Track Nodes                     |
| ...    | Int32    | ID                              |
| ...    | Byte     | Unknown Boolean                 |
| ...    | Int32    | Section ID Count                |
| ...    | Int32    | Array of other Section IDs      |
| ...    | Byte     | Unknown Byte (Enum) (Always 3?) |
| ...    | Double   | Unknown                         |
| ...    | Byte     | Unknown Boolean                 |
| ...    | Byte     | Unknown Boolean                 |
| ...    | Int32    | Unknown                         |
| ...    | Byte     | Unknown Boolean                 |

## Track Node

| Offset | Type      | Explaination                    |
| ------ | --------- | ------------------------------- |
| 0      | Int32     | Reserved                        |
| 4      | TileIndex | Tile Index                      |
| 12     | Vector3   | Unknown                         |
| 24     | Vector3   | Unknown                         |
| 32     | Vector3   | Unknown                         |
| 44     | Int32     | ID                              |
| 48     | Byte      | Unknown Boolean                 |
| 49     | Byte      | Unknown Boolean                 |
| 50     | Float     | Unknown                         |
| 54     | Int32     | Unknown                         |
| 58     | Float     | Unknown                         |
| 62     | Float     | Unknown                         |
| 66     | Int32     | Unknown                         |
| 70     | Int32     | Section ID this node belongs to |
| 74     | Byte      | Unknown Boolean                 |