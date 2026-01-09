# Defect Detector List (DefectDetectorList.r8)

Contains a list of defect detectors.

## Header

| Offset | Type             | Explaination           |
| ------ | ---------------- | ---------------------- |
| 0      | Int32            | Reserved               |
| 4      | Int32            | Defect Detectors Count |
| 8      | DefectDetector[] | Defect Detectors       |

## DefectDetector

| Offset | Type      | Explaination              |
| ------ | --------- | ------------------------- |
| 0      | Int32     | Version                   |
| 4      | Int32     | Milepost                  |
| 8      | Int32     | Milepost Decimal          |
| 16     | TileIndex | Tile Index                |
| 28     | Vector3   | Position                  |
| ...    | Byte      | Has AEI (If version=2)    |
| 29     | Byte      | SquawkOnDefectOnly        |
| 30     | Byte      | DraggingEquipment         |
| 31     | Byte      | SquawkTemperature         |
| 32     | Byte      | SquawkTrainSpeed          |
| 33     | Byte      | Hotbox                    |
| 34     | Byte      | HiWide (Legacy, not used) |
| 35     | R8String  | WaveBankName              |
| ...    | R8String  | SoundBankName             |
| ...    | Int32     | Track Number              |
