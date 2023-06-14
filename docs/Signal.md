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

## SignalEntry1

| Offset | Type           | Explaination                |
| ------ | -------------- | --------------------------- |
| 0      | Vector3        | Light Offset                |
| 12     | Vector3        | Color*                      |
| 24     | Float          | Light Glare Radius (meters) |
| 28     | Float          | Light Range                 |
| 32     | Int32          | GlareList Count             |
| 36     | GlareListEntry | GlareList Entries           |

* Technically this is a Vector4, but only X, Y and Z come are read, W is constant as 1f

## GlareListEntry

| Offset | Type    | Explaination |
| ------ | ------- | ------------ |
| 0      | Vector3 | Unknown      |

