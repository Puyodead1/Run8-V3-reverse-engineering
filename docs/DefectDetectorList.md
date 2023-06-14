# Defect Detector List (DefectDetectorList.r8)

Contains a list of defect detectors.

## Header

| Offset | Type     | Explaination        |
| ------ | -------- | ------------------- |
| 0      | Int32    | Reserved            |
| 4      | Int32    | Detector Count      |
| 8      | Detector | Detectors           |

## Detector

| Offset | Type  | Explaination                   |
| ------ | ----- | ------------------------------ |
| 0      | Int32 | Unknown n                      |
| 4      | Int32 | Sub1 if n == 1, Sub2 if n == 2 |

## Sub1

| Offset | Type      | Explaination                                                   |
| ------ | --------- | -------------------------------------------------------------- |
| 0      | Int32     | ID 1, Seems to be displayed like an ID in the format `id1.id2` |
| 4      | Int32     | ID 1, Seems to be displayed like an ID in the format `id1.id2` |
| 8      | TileIndex | TileIndex                                                      |
| 16     | Vector3   | Location                                                       |
| 28     | Byte      | Unknown Boolean                                                |
| 29     | Byte      | Unknown Boolean                                                |
| 30     | Byte      | Unknown Boolean                                                |
| 31     | Byte      | Unknown Boolean                                                |
| 32     | Byte      | Unknown Boolean                                                |
| 33     | Byte      | Unknown Boolean                                                |
| 34     | R8String  | Detector .xwb file name                                        |
| ...    | R8String  | Detector .xsb file name                                        |
| ...    | Int32     | Track Number?                                                  |

## Sub2

| Offset | Type      | Explaination                                                   |
| ------ | --------- | -------------------------------------------------------------- |
| 0      | Int32     | ID 1, Seems to be displayed like an ID in the format `id1.id2` |
| 4      | Int32     | ID 1, Seems to be displayed like an ID in the format `id1.id2` |
| 8      | TileIndex | TileIndex                                                      |
| 16     | Vector3   | Location                                                       |
| 28     | Byte      | Unknown Boolean                                                |
| 29     | Byte      | Unknown Boolean                                                |
| 30     | Byte      | Unknown Boolean                                                |
| 31     | Byte      | Unknown Boolean                                                |
| 32     | Byte      | Unknown Boolean                                                |
| 33     | Byte      | Unknown Boolean                                                |
| 34     | Byte      | Unknown Boolean                                                |
| 35     | R8String  | Unknown                                                        |
| ...    | R8String  | Unknown                                                        |
| ...    | Int32     | Track Number?                                                  |