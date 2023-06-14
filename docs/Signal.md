# Signal (.sig)

Contains information related to a signal

## Header

| Offset | Type         | Explaination                                                  |
| ------ | ------------ | ------------------------------------------------------------- |
| 0      | String       | Model Name                                                    |
| ...    | Int32        | Unknown                                                       |
| ...    | Byte         | Unknown Boolean (might be related to signal instruction stop) |
| ...    | Byte         | Unknown Boolean                                               |
| ...    | Int32        | SignalEntry1 Count                                            |
| ...    | SignalEntry1 | SignalEntry1s                                                 |

## SignalEntry1

| Offset | Type         | Explaination               |
| ------ | ------------ | -------------------------- |
| 0      | Vector3      | Unknown                    |
| 12     | Vector3      | Unknown                    |
| 24     | Float        | Unknown                    |
| 28     | Float        | Unknown                    |
| 32     | Int32        | SignalEntry2 Count         |
| 36     | SignalEntry2 | SignalEntry2s              |

## SignalEntry2

| Offset | Type    | Explaination |
| ------ | ------- | ------------ |
| 0      | Vector3 | Unknown      |
