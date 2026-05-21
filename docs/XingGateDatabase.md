# Xing Gate Database (XingGateDatabase.r8)

## Header

| Offset | Type            | Explanation   |
| ------ | --------------- | ------------- |
| 0      | Int32           | Reserved      |
| 4      | Int32           | Entry Count   |
| 8      | XingGateEntry[] | Entries       |

## XingGateEntry

| Offset | Type     | Explanation  |
| ------ | -------- | ------------ |
| 0      | Int32    | Reserved     |
| 4      | R8String | Gate Type    |

After Gate Type, continue reading sequentially:

| Offset | Type    | Explanation                       |
| ------ | ------- | --------------------------------- |
| 0      | Float32 | Position X                        |
| 4      | Float32 | Position Y                        |
| 8      | Float32 | Position Z                        |
| 12     | Float32 | Rotation X (always 0.0)           |
| 16     | Float32 | Rotation Y (degrees, -360 to 318) |
| 20     | Float32 | Rotation Z (always 0.0)           |
| 24     | Int32   | Tile X                            |
| 28     | Int32   | Tile Y                            |
| 32     | Int32   | Padding (always 0)                |

## R8String

| Offset      | Type   | Explanation                                              |
| ----------- | ------ | -------------------------------------------------------- |
| 0           | Int32  | Byte Length (= char count × 2)                           |
| 4           | Bytes  | Characters, each as big-endian UInt16 (decode: raw >> 4) |
