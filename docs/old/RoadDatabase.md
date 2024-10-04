# Road Database (RoadDatabase.r8)

Contains road information.

## Header

| Offset | Type        | Explaination       |
| ------ | ----------- | ------------------ |
| 0      | Int32       | Reserved           |
| 4      | Int32       | Number of Sections |
| 8      | RoadSection | Sections           |

## RoadSection

| Offset | Type     | Explaination              |
| ------ | -------- | ------------------------- |
| 0      | Int32    | Unknown Int               |
| 4      | Int32    | Number of Nodes           |
| 8      | RoadNode | Nodes                     |
| ...    | Int32    | Unknown Int               |
| ...    | Float    | Reserved                  |
| ...    | Byte     | RoadExtrusionType         |
| ...    | Int32    | Num Lanes Per Side        |
| ...    | Int32    | Lane Center Offset Meters |
| ...    | Int32    | Lane Spacing Meters       |

## RoadNode

| Offset | Type      | Explaination      |
| ------ | --------- | ----------------- |
| 0      | Int32     | Reserved          |
| 4      | TileIndex | TileXZ            |
| 12     | Vector3   | PositionXYZ       |
| 24     | Vector3   | TangentXYZ        |
| 36     | Vector3   | Reserved          |
| 48     | Int32     | Index             |
| 52     | Float     | Unknown           |
| 56     | Int32     | Curve Sign        |
| 60     | Float     | Unknown           |
| 64     | Float     | Arc Length Meters |
| 68     | Int32     | Num of Segments   |
| 72     | Int32     | Unknown           |
| 76     | Float     | Max Speed MPH     |

## RoadExtrusionType

| Key | Value   |
| --- | ------- |
| 0   | Unknown |
| 1   | Unknown |
| 2   | Unknown |
| 3   | Unknown |
| 4   | Unknown |
| 5   | Unknown |
| 6   | Unknown |
| 7   | Unknown |
| 8   | Unknown |
| 9   | Unknown |
| 10  | Unknown |
| 11  | Unknown |
| 12  | Unknown |
| 13  | Unknown |
