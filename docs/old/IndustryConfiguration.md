# Industry Configuration (.ind)

Stores information about industries

## Header

| Offset | Type     | Explaination   |
| ------ | -------- | -------------- |
| 0      | Int32    | Reserved       |
| 4      | Int32    | Industry Count |
| 8      | Industry | Industries     |

## Industry

| Offset | Type     | Explaination         |
| ------ | -------- | -------------------- |
| 0      | Int32    | Reserved             |
| 4      | R8String | Industry Name        |
| ...    | R8String | Local Freight Code   |
| ...    | R8String | Industry Tag         |
| ...    | Byte     | Unknown Boolean      |
| ...    | Int32    | Industry Track Count |
| ...    | IndTrack | Industry Tracks      |

## Industry Track

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Unknown Int  |
| 4      | Int32 | Prefix       |
| 8      | Int32 | Section      |
| 12     | Int32 | Node         |

## Car

| Offset | Type  | Explaination    |
| ------ | ----- | --------------- |
| 0      | Int32 | Reserved        |
| 4      | Byte  | Car Type        |
| 8      | Byte  | Unknown Boolean |
| 12     | Int32 | Unknown Int     |
| 16     | Int32 | Unknown Int     |
| 20     | Int32 | Unknown Int     |
