# Comm Tower Database (CommTowerDatabase.r8)

Contains a list of Comm Towers

## Header

| Offset | Type      | Explaination      |
| ------ | --------- | ----------------- |
| 0      | Int32     | Reserved          |
| 4      | Int32     | Number of CommTowers |
| 8      | CommTower[] | CommTowers      |

## CommTower

| Offset | Type      | Explaination        |
| ------ | --------- | ------------------- |
| 0      | Int32     | Reserved            |
| 4      | TileIndex | TileXZ              |
| 12     | Vector3   | Position            |
| 24     | R8String  | Tower ID/Name       |
| ...    | Byte      | Channel             |
| ...    | R8String  | Dial Code           |
| ...    | R8String  | Emergency Dial Code |
| ...    | Float     | Range Meters        |
| ...    | R8String  | Dispatch Tone Cue Name |