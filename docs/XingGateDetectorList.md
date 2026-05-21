# Xing Gate Detector List (XingDetectorList.r8)

## Header

| Offset | Type            | Explanation    |
| ------ | --------------- | -------------- |
| 0      | Int32           | Reserved       |
| 4      | Int32           | Entry Count    |
| 8      | XingDetector[]  | Entries        |

## XingDetector

Total size per entry: **48 + (Linked ID Count × 4)** bytes.

| Offset | Type     | Explanation                                        |
| ------ | -------- | -------------------------------------------------- |
| 0      | Int32    | Type (always 2)                                    |
| 4      | Int32    | Index (0-based, sequential)                        |
| 8      | Byte     | Padding (always 0)                                 |
| 9      | Float32  | Detection Distance (detection range in world units) |
| 13     | Float32  | Sensor Width (typically 10.0)                      |
| 17     | Float32  | Sensor Height (typically 14.0)                     |
| 21     | Int32    | Tile X                                             |
| 25     | Int32    | Tile Y                                             |
| 29     | Float32  | Position X                                         |
| 33     | Float32  | Position Y                                         |
| 37     | Float32  | Position Z                                         |
| 41     | Int32    | Extra Encoded (= Linked ID Count × 256)            |
| 45     | Byte     | Byte 45 (usually 0, occasionally 3)                |
| 46     | UInt16   | Int16 46 (= 1 when no linked IDs, varies otherwise)|
| 48     | Int32[]  | Linked IDs (Linked ID Count entries, 4 bytes each) |
