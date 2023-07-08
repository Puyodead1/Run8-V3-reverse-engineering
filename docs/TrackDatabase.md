# Track Database (TrackDatabase.r8)

Contain a list of track locations as well as other track related information.

## Header

| Offset | Type         | Explaination        |
| ------ | ------------ | ------------------- |
| 0      | Int32        | Reserved            |
| 4      | Int32        | Number of Sections  |
| 8      | TrackSection[] | Sections            |

## Track Section

| Offset | Type     | Explaination                    |
| ------ | -------- | ------------------------------- |
| 0      | Int32    | Reserved                        |
| 4      | Int32    | Number of Nodes                 |
| 8      | TrackNode[]| Nodes                           |
| ...    | Int32    | Index                           |
| ...    | Byte     | Switch Lever Position           |
| ...    | Int32    | Number of Section Indices       |
| ...    | Int32[]  | Next Section Indices            |
| ...    | Byte     | Track Type                      |
| ...    | Double   | Retarder MPH                    |
| ...    | Byte     | Is Occupied                     |
| ...    | Byte     | Switch Stand Left Side          |
| ...    | Int32    | Switch Stand Type               |
| ...    | Byte     | Is CTC Switch                   |

## Track Node

| Offset | Type      | Explaination                    |
| ------ | --------- | ------------------------------- |
| 0      | Int32     | Reserved                        |
| 4      | TileIndex | Tile Index                      |
| 12     | Vector3   | Position                        |
| 24     | Vector3   | Tangent Degrees                 |
| 32     | Vector3   | End Position                    |
| 44     | Int32     | Index                           |
| 48     | Byte      | Is Switch Node                  |
| 49     | Byte      | Is Reverse Path                 |
| 50     | Float     | Curvature Degrees               |
| 54     | Int32     | Curve Sign                      |
| 58     | Float     | Radius Meters                   |
| 62     | Float     | Arc Length Meters               |
| 66     | Int32     | Num Segments                    |
| 70     | Int32     | Section this Node belongs to    |
| 74     | Byte      | Is Selected                     |