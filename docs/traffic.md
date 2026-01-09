# Traffic (Traffic.r8)

Seems to contain information about AI traffic such as cars, etc

## Header

| Offset | Type             | Explaination                 |
| ------ | ---------------- | ---------------------------- |
| 0      | Int32            | Version                      |
| 4      | Bool             | AI Spawn Enabled             |
| 5      | Int32            | Timeout                      |
| 9      | Int32            | Max Time                     |
| 13     | Int32            | Max Trains                   |
| 17     | Int32            | Min Trains (If version >= 2) |
| 21     | Int32            | Spawn Point Count            |
| 25     | SpawnPointData[] | Spawn Point Data             |

## SpawnPointData

| Offset | Type             | Explaination           |
| ------ | ---------------- | ---------------------- |
| 0      | Int32            | Reserved               |
| 4      | R8String         | Name                   |
| 8      | Int32            | Weight                 |
| 12     | Int32            | Train Class Count      |
| 16     | TrainClassData[] | TrainClassData Entries |

## TrainClassData

| Offset | Type              | Explaination                                       |
| ------ | ----------------- | -------------------------------------------------- |
| 0      | Int32             | Reserved                                           |
| 4      | Byte              | ETrain Class                                       |
| 5      | Int32             | Weight                                             |
| 9      | Int32             | Saved Train Count                                  |
| 13     | SavedTrain[]      | Saved Train Entries (if train class is SavedTrain) |
| ...    | Int32             | Train Symbol Data Count                            |
| ...    | TrainSymbolData[] | Train Symbol Data Entries                          |

## SavedTrain

| Offset | Type     | Explaination |
| ------ | -------- | ------------ |
| 0      | Int32    | Reserved     |
| 4      | R8String | Filename     |
| ...    | R8String | Train Symbol |
| ...    | Int32    | Weight       |

## TrainSymbolData

| Offset | Type       | Explaination                  |
| ------ | ---------- | ----------------------------- |
| 0      | Int32      | Version                       |
| 4      | Bool       | Has Train Symbol              |
| 5      | R8String   | Train Tag (if HasTrainSymbol) |
| ...    | Byte       | ETrainCaste                   |
| ...    | Byte       | ETrainSpecialRestrictions     |
| ...    | Byte       | Manifests Use Industry Config |
| ...    | Byte       | Loaded                        |
| ...    | Byte       | Allow Mid Train DPUs          |
| ...    | Int32      | Tag Count                     |
| ...    | R8String[] | Tags                          |
| ...    | Int32      | Unknown Strings Count         |
| ...    | R8String[] | Unknown Strings               |
| ...    | Int32      | Unknown Strings Count         |
| ...    | R8String[] | Unknown Strings               |
| ...    | Int32      | Unknown Strings Count         |
| ...    | R8String[] | Unknown Strings               |
| ...    | Sub1       | Sub1 (if version > 1)         |

### Sub1

| Offset | Type       | Explaination                |
| ------ | ---------- | --------------------------- |
| 0      | Int32      | Unknown Strings Count       |
| 4      | R8String[] | Unknown Strings             |
| ...    | Int32      | Unknown Strings Count       |
| ...    | R8String[] | Unknown Strings             |
| 0      | Bool       | Allow DPUs (If version > 2) |

## ETrainClass

| Key | Value                  |
| --- | ---------------------- |
| 0   | None                   |
| 1   | Passenger              |
| 2   | Baretables             |
| 3   | ContainerDomestic      |
| 4   | ContainerInternational |
| 5   | ContainerMixed         |
| 6   | Intermodal             |
| 7   | MixedIntermodal        |
| 8   | FreightMixed           |
| 9   | UnitAutorack           |
| 10  | UnitCoal               |
| 11  | UnitOil                |
| 12  | UnitGrain              |
| 13  | UnitReefer             |
| 14  | PowerMove              |
| 15  | UnitBethgonCoal        |
| 16  | UnitCoilSteel          |
| 255 | SavedTrain             |

## ETrainCaste

| Key | Value          |
| --- | -------------- |
| 0   | UtterPeon      |
| 1   | Low            |
| 2   | Medium         |
| 3   | High           |
| 4   | KingOfTheRails |

## ETrainSpecialRestrictions

| Key | Value       | Attribute  |
| --- | ----------- | ---------- |
| 0   | None        | None       |
| 2   | TehachapiWB | SouthernCA |
| 4   | TehachapiEB | SouthernCA |
| 8   | CajonWB1    | SouthernCA |
| 16  | CajonWB2    | SouthernCA |
| 32  | CajonEB1    | SouthernCA |
| 64  | CajonEB2    | SouthernCA |
