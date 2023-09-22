# CarSpewerDatabase (CarSpewerDatabase.r8)

Contains information for Car Spewers (Spawners)

## Header

| Offset | Type        | Explaination      |
| ------ | ----------- | ----------------- |
| 0      | Int32       | Reserved          |
| 4      | Int32       | Number of Spewers |
| 8      | CarSpewer[] | Spewers           |

## Car Spewer

| Offset | Type              | Explaination               |
| ------ | ----------------- | -------------------------- |
| 0      | Int32             | Unknown (num)              |
| 4      | Int32             | Unknown (Not used)         |
| 8      | Int32             | Road Node Index            |
| 12     | Int32             | Max Cars                   |
| 16     | Int32             | Min Time between Spews     |
| 20     | Int32             | Max Time between Spews     |
| 24     | Float             | Max Speed (Only if num==2) |
| 28     | Int32             | Number of Spew Points      |
| 32     | CarSpewStartPoint | Spew Start Points          |

## Car Spew Start Point

| Offset | Type      | Explaination           |
| ------ | --------- | ---------------------- |
| 0      | Int32     | Reserved               |
| 4      | Vector3   | Position               |
| 12     | TileIndex | Tile Index             |
| 24     | Float     | Heading (Default -999) |
