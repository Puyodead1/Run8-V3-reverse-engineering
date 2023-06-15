# Signal (.sig)

Contains information related to a signal

## Header

| Offset | Type         | Explaination                                                  |
| ------ | ------------ | ------------------------------------------------------------- |
| 0      | String       | Model Name                                                    |
| ...    | Int32        | leastRestrictiveSignalState                                   |
| ...    | Byte         | Unknown Boolean (might be related to signal instruction stop) |
| ...    | Byte         | isDwarf                                                       |
| ...    | Int32        | SignalEntry1 Count                                            |
| ...    | SignalEntry1 | SignalEntry1s                                                 |

## SignalLight

| Offset | Type           | Explaination                |
| ------ | -------------- | --------------------------- |
| 0      | Vector3        | Light Offset                |
| 12     | Vector4        | Color*                      |
| 24     | Float          | Light Glare Radius (meters) |
| 28     | Float          | Light Range                 |
| 32     | Int32          | Glare Count                 |
| 36     | Vector3[]      | Glares                      |

\* W is constant at 1f

