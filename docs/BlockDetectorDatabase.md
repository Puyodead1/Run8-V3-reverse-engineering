# BlockDetectorDatabase (BlockDetectorDatabase.r8)

Contains information for Block Detectors.

## Header

| Offset | Type            | Explaination         |
| ------ | --------------- | -------------------- |
| 0      | Int32           | Reserved             |
| 4      | Int32           | Block Detector Count |
| 8      | BlockDetector[] | Block Detectors      |

## BlockDetector

| Offset | Type      | Explaination     |
| ------ | --------- | ---------------- |
| 0      | Int32     | Reserved         |
| 4      | Int32     | Index            |
| 8      | Int32     | Number of Tracks |
| 12     | Int32[]   | Track Indices    |
| ...    | TileIndex | Tile XZ          |
| ...    | Vector3   | Position         |
