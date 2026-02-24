# Industry Configuration (.ind)

Stores information about industries

## Header

| Offset | Type       | Explaination   |
| ------ | ---------- | -------------- |
| 0      | Int32      | Reserved       |
| 4      | Int32      | Industry Count |
| 8      | Industry[] | Industries     |

## Industry

| Offset | Type            | Explaination         |
| ------ | --------------- | -------------------- |
| 0      | Int32           | Reserved             |
| 4      | R8String        | Industry Name        |
| ...    | R8String        | Local Freight Code   |
| ...    | R8String        | Industry Tag         |
| ...    | Bool            | Process Cars         |
| ...    | Int32           | Industry Track Count |
| ...    | IndustryTrack[] | Industry Tracks      |
| ...    | Int32           | Car Count            |
| ...    | IndustryCar[]   | Industry Cars        |

## IndustryTrack

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Reserved     |
| 4      | Int32 | Prefix       |
| 8      | Int32 | Section      |
| 12     | Int32 | Node         |

## IndustryCar

| Offset | Type       | Explaination                      |
| ------ | ---------- | --------------------------------- |
| 0      | Int32      | Version                           |
| 4      | Byte       | Car Type                          |
| 8      | Bool       | Produces Loads                    |
| 12     | Int32      | Hours                             |
| 16     | Int32      | Capacity                          |
| 20     | Int32      | Tag Count                         |
| 24     | R8String[] | Tags                              |
| ...    | Int32      | Filtered Car Count (version >= 2) |
| ...    | R8String[] | Filtered cars XML (version >= 2)  |

## ECarType

| Key | Value                |
| --- | -------------------- |
| 0   | All                  |
| 1   | AcidTank             |
| 2   | AmtrackAutorack      |
| 3   | Autorack             |
| 4   | BallastHopper        |
| 5   | Box                  |
| 6   | Caboose              |
| 7   | Centerbeam           |
| 8   | CoveredHopper        |
| 9   | TwoBayHoper          |
| 10  | Gondola              |
| 11  | OpenHopper           |
| 12  | Passenger            |
| 13  | Piggyback            |
| 14  | Reefer               |
| 15  | Tank                 |
| 16  | Well                 |
| 17  | BethGon              |
| 18  | PlasticPellet_Hopper |
| 19  | Woodchip_Hopper      |
| 20  | Box_AutoParts        |
| 21  | Bulkhead_Flat        |
| 22  | Shoving_Platform     |
| 23  | CoilCar              |
| 24  | AggregateHopper      |
| 25  | GeometryCar          |
| 26  | MOW                  |
| 27  | Loco                 |
