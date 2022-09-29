# AI Track Speed (AITrackSpeed.r8)

Seems to contain information related to track speeds

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Int32 | Number of entries |
| 8      | Entry | Entries           |

## Entry

| Offset | Type     | Explaination       |
| ------ | -------- | ------------------ |
| 0      | Int32    | Reserved           |
| 4      | Int32    | Unknown            |
| 8      | Int32    | Number of Unknown1 |
| 12     | Unknown1 | Unknown1 Entries   |

## Unknown1

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Reserved     |
| 4      | Int32 | Unknown      |
| 8      | Int32 | Unknown      |
| 12     | Int32 | Unknown      |
