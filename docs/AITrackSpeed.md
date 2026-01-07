# AI Track Speed (AITrackSpeed.r8)

Seems to contain information related to track speeds

## Header

| Offset | Type         | Explaination        |
| ------ | ------------ | ------------------- |
| 0      | Int32        | Reserved            |
| 4      | Int32        | Track Speed count   |
| 8      | TrackSpeed[] | Track Speed Entries |

## TrackSpeed

| Offset | Type    | Explaination     |
| ------ | ------- | ---------------- |
| 0      | Int32   | Reserved         |
| 4      | Int32   | Track Section ID |
| 8      | Int32   | Speed Count      |
| 12     | Speed[] | Speeds           |

## Speed

| Offset | Type  | Explaination          |
| ------ | ----- | --------------------- |
| 0      | Int32 | Reserved              |
| 4      | Int32 | Track Node Index      |
| 8      | Int32 | Passenger Speed Limit |
| 12     | Int32 | Freight Speed Limit   |
