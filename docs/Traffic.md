# Traffic (Traffic.r8)

Seems to contain information about AI traffic such as cars, etc

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Byte  | Unknown Boolean   |
| 5      | Int32 | Unknown           |
| 9      | Int32 | Unknown           |
| 13     | Int32 | Unknown           |
| 17     | Int32 | Entry Count		 |
| 21     | Entry | Entries           |

## Entry

| Offset | Type     | Explaination       |
| ------ | -------- | ------------------ |
| 0      | Int32    | Reserved           |
| 4      | R8String | Route Name         |
| 8      | Int32    | Unknown            |
| 12     | Int32    | Unknown1 Count	 |
| 16     | Unknown1 | Unknown1 Entries   |

## Unknown1

| Offset | Type       | Explaination                                              |
| ------ | ---------- | --------------------------------------------------------- |
| 0      | Int32      | Reserved                                                  |
| 4      | TrainClass | Train Class                                               |
| 5      | Int32      | Unknown                                                   |
| 9      | Int32      | Unknown2 Count	                                          |
| 13     | Unknown2   | Unknown2 Entries (only read if train class is SavedTrain) |
| ...    | Int32      | Unknown3 Count                                            |
| ...    | Unknown3   | Unknown3 Entries                                          |

## Unknown2

| Offset | Type     | Explaination |
| ------ | -------- | ------------ |
| 0      | Int32    | Reserved     |
| 4      | R8String | Unknown      |
| ...    | R8String | Unknown      |
| ...    | Int32    | Unknown      |

## Unknown3

| Offset | Type       | Explaination                                                             |
| ------ | ---------- | ------------------------------------------------------------------------ |
| 0      | Int32      | Unknown n                                                                |
| 4      | Byte       | Unknown Boolean                                                          |
| 5      | R8String   | Train Tag (only read if above bool is true)                              |
| ...    | Byte       | Train Caste                                                              |
| ...    | Byte       | Train Special Restrictions                                               |
| ...    | Byte       | Unknown Boolean                                                          |
| ...    | Byte       | Unknown Boolean                                                          |
| ...    | Byte       | Unknown Boolean                                                          |
| ...    | Int32      | Unknown Strings Count	                                                 |
| ...    | R8String   | Unknown Strings                                                          |
| ...    | Int32      | Unknown Strings Count                                                    |
| ...    | R8String[] | Unknown Strings (looks like company strings, ex up, csx, etc)            |
| ...    | Int32      | Unknown Strings Count                                                    |
| ...    | R8String[] | Unknown Strings (looks like a list of locomotives, ex ES44, SD40-2, etc) |
| ...    | Int32      | Unknown Strings Count                                                    |
| ...    | R8String[] | Unknown Strings                                                          |
| ...    | Sub1       | Sub1 (if n > 1)                                                          |

### Sub1

| Offset | Type       | Explaination                                              |
| ------ | ---------- | --------------------------------------------------------- |
| 0      | Int32      | Unknown Strings Count                                     |
| 4      | R8String[] | Unknown Strings (looks like xml file names for some cars) |
| ...    | Int32      | Unknown Strings Count                                     |
| ...    | R8String[] | Unknown Strings                                           |
| ...    | Sub2       | Sub2 (if n > 2)                                           |

### Sub2

| Offset | Type | Explaination    |
| ------ | ---- | --------------- |
| 0      | Byte | Unknown Boolean |

## TrainClass

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

## TrainCaste

| Key | Value          |
| --- | -------------- |
| 0   | UtterPeon      |
| 1   | Low            |
| 2   | Medium         |
| 3   | High           |
| 4   | KingOfTheRails |

## TrainSpecialRestrictions

| Key | Value       | Attribute  |
| --- | ----------- | ---------- |
| 0   | None        | None       |
| 2   | TehachapiWB | SouthernCA |
| 4   | TehachapiEB | SouthernCA |
| 8   | CajonWB1    | SouthernCA |
| 16  | CajonWB2    | SouthernCA |
| 32  | CajonEB1    | SouthernCA |
| 64  | CajonEB2    | SouthernCA |
