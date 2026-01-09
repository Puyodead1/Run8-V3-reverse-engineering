# AI Special Locations (AISpecialLocations.r8)

Seems to contain information about "special" track locations, used for spawning AI

## Header

| Offset | Type                | Explaination            |
| ------ | ------------------- | ----------------------- |
| 0      | Int32               | Reserved                |
| 4      | Int32               | AISpecialLocation Count |
| 8      | AISpecialLocation[] | AISpecialLocation       |

## AISpecialLocation

| Offset | Type     | Explaination          |
| ------ | -------- | --------------------- |
| 0      | Int32    | Reserved              |
| 4      | R8String | Name                  |
| ...    | Byte     | ESpecialLocationType  |
| ...    | Int32    | Route Prefix          |
| ...    | Int32    | Track Section Index   |
| ...    | Int32    | Track Node Index      |
| ...    | Float    | Node Position Meters? |
| ...    | Int32    | Unknown               |
| ...    | Bool     | Unknown               |

## ESpecialLocationType

| Key | Value                      |
| --- | -------------------------- |
| 0   | SpawnPoint                 |
| 1   | CrewChange                 |
| 2   | CrewChangeAndHold          |
| 3   | Passenger                  |
| 4   | PassengerCrewChange        |
| 5   | PassengerCrewChangeAndHold |
| 6   | Relinquish                 |
| 7   | PassengerRelinquish        |
