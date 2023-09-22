# Defect Detector List (DefectDetectorList.r8)

Contains a list of defect detectors.

## Header

| Offset | Type             | Explaination               |
| ------ | ---------------- | -------------------------- |
| 0      | Int32            | Reserved                   |
| 4      | Int32            | Number of Defect Detectors |
| 8      | DefectDetector[] | Defect Detectors           |

## Defect Detector

| Offset | Type      | Explaination                  |
| ------ | --------- | ----------------------------- |
| 0      | Int32     | Unknown (n)                   |
| 4      | Int32     | Milepost                      |
| 8      | Int32     | Milepost Decimal              |
| 16     | TileIndex | Tile Index                    |
| 28     | Vector3   | Position                      |
| ...    | Byte      | IsAEI (IF n=2 otherwise skip) |
| 29     | Byte      | SquawkOnDefectOnly            |
| 30     | Byte      | DraggingEquipment             |
| 31     | Byte      | SquawkTemperature             |
| 32     | Byte      | SquawkTrainSpeed              |
| 33     | Byte      | Hotbox                        |
| 34     | Byte      | HiWide (Legacy, not used)     |
| 35     | R8String  | WaveBankName                  |
| ...    | R8String  | SoundBankName                 |
| ...    | Int32     | Track Number                  |
