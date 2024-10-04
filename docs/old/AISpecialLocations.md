# AI Special Locations (AISpecialLocations.r8)

Seems to contain information about "special" track locations.

## Header

| Offset | Type              | Explaination            |
| ------ | ----------------- | ----------------------- |
| 0      | Int32             | Reserved                |
| 4      | Int32             | AISpecialLocation Count |
| 8      | AISpecialLocation | AISpecialLocation       |

## AISpecialLocation

| Offset | Type     | Explaination        |
| ------ | -------- | ------------------- |
| 0      | Int32    | Reserved            |
| 4      | R8String | Location Name       |
| ...    | Byte     | SpecialLocationType |
| ...    | Int32    | Unknown             |
| ...    | Int32    | Unknown             |
| ...    | Int32    | Unknown             |
| ...    | Float    | Unknown             |
| ...    | Int32    | Unknown             |
| ...    | Byte     | Unknown Boolean     |

## SpecialLocationType

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
