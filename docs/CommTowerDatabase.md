# Comm Tower Database (CommTowerDatabase.r8)

Contains a list of Comm Towers

## Header

| Offset | Type      | Explaination      |
| ------ | --------- | ----------------- |
| 0      | Int32     | Reserved          |
| 4      | Int32     | CommTower Count	 |
| 8      | CommTower | CommTowers        |

## CommTower

| Offset | Type      | Explaination        |
| ------ | --------- | ------------------- |
| 0      | Int32     | Reserved            |
| 4      | TileIndex | Tile Index          |
| 12     | Vector3   | Location?           |
| 24     | R8String  | Tower Name          |
| ...    | Byte      | Unknown             |
| ...    | R8String  | Dial Code           |
| ...    | R8String  | Emergency Dial Code |
| ...    | Float     | Unknown             |
| ...    | R8String  | Dispatch Tone Type  |