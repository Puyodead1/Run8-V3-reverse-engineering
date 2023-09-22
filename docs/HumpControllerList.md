# Hump Controller List (HumpControllerList.r8)

Contains hump controllers

## Header

| Offset | Type           | Explaination          |
| ------ | -------------- | --------------------- |
| 0      | Int32          | Reserved              |
| 4      | Int32          | Number of Controllers |
| 8      | HumpController | Controller            |

## HumpController

| Offset | Type      | Explaination        |
| ------ | --------- | ------------------- |
| 0      | Int32     | Reserved            |
| 4      | R8String  | Hump Name           |
| ...    | TileIndex | TileXZ              |
| ...    | Vector3   | Position            |
| ...    | R8String  | Unused              |
| ...    | Int32     | Number of TrackPath |
| ...    | TrackPath | TrackPaths          |

## TrackPath

| Offset | Type             | Explaination               |
| ------ | ---------------- | -------------------------- |
| 0      | Int32            | Reserved                   |
| 4      | String           | Track Name                 |
| ...    | Int32            | Number of SwitchConnection |
| ...    | SwitchConnection | SwitchConnections          |

## SwitchConnection

| Offset | Type    | Explaination        |
| ------ | ------- | ------------------- |
| 0      | Int32   | Reserved            |
| 4      | Int32   | Track Section Index |
| 8      | Boolean | Is Reversed         |
