# SignalHeadDatabase (.r8)

## Header

| Offset | Type         | Explaination      |
| ------ | ------------ | ----------------- |
| 0      | Int32        | Reserved          |
| 4      | Int32        | Signal Head Count |
| ...    | SignalHead[] | Signal Heads      |

## SignalHead

| Offset | Type      | Explaination                   |
| ------ | --------- | ------------------------------ |
| 0      | Int32     | Reserved                       |
| 4      | Int32     | Signal Indices Count           |
| ...    | Int32[]   | Signal Indices                 |
| ...    | Int32     | Route Count                    |
| ...    | Int32     | Signal Index                   |
| ...    | Bool      | Is Absolute                    |
| ...    | R8String  | Model Name                     |
| ...    | Vector3   | Position                       |
| ...    | Float     | Rotation Degrees Y             |
| ...    | TileIndex | TileXZ                         |
| ...    | Int32     | Least Restrictive Signal State |
| ...    | Bool      | Is Advance Diverging           |
| ...    | Bool      | Unknown                        |
| ...    | Bool      | Unknown                        |
| ...    | Bool      | Unknown                        |
| ...    | Bool      | Unknown                        |
| ...    | Bool      | Is Dwarf                       |

## Route

| Offset | Type                    | Explaination                                |
| ------ | ----------------------- | ------------------------------------------- |
| 0      | Int32                   | Version                                     |
| 4      | R8String                | Route Name                                  |
| ...    | Int32                   | Route Max MPH                               |
| ...    | Int32                   | Block Detector Count                        |
| ...    | Int32[]                 | Block Detector Indices                      |
| ...    | Int32                   | Previous Signal Count                       |
| ...    | Int32[]                 | Previous Signal Indices                     |
| ...    | Int32                   | Signal Switch Connector Count               |
| ...    | SignalSwitchConnector[] | Signal Switch Connectors                    |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Int32                   | ReadFromDatabasePrefix                      |
| ...    | Bool                    | Unknown<sup>1</sup>                         |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Bool                    | Unknown                                     |
| ...    | Byte                    | Unknown Enum, related to signal instruction |

-   <sup>1</sup>: Only when `version` is 2

## SignalSwitchConnector

| Offset | Type  | Explaination           |
| ------ | ----- | ---------------------- |
| 0      | Int32 | Reserved               |
| 4      | Int32 | Switch Index           |
| 8      | Bool  | Clear If Thrown Normal |
