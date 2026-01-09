# Milepost Database (MilepostDatabase.r8)

Contains a list of mileposts and their information

## Header

| Offset | Type       | Explaination   |
| ------ | ---------- | -------------- |
| 0      | Int32      | Reserved       |
| 4      | Int32      | Milepost Count |
| 8      | Milepost[] | Mileposts      |

## Milepost

| Offset | Type      | Explaination |
| ------ | --------- | ------------ |
| 0      | Int32     | Reserved     |
| 4      | R8String  | Milepost     |
| ...    | R8String  | Station Name |
| ...    | TileIndex | Tile Index   |
| ...    | Vector3   | Location     |
