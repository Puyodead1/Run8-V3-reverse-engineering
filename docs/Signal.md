# Signal (.sig)

## Header

| Offset | Type          | Explaination                |
| ------ | ------------- | --------------------------- |
| 0      | String        | Model Name                  |
| ...    | Int32         | leastRestrictiveSignalState |
| ...    | Bool          | isAdvanceDiverging          |
| ...    | Byte          | isDwarf                     |
| ...    | Int32         | Light Count                 |
| ...    | SignalLight[] | Signal Lights               |

## SignalLight

| Offset | Type      | Explaination                |
| ------ | --------- | --------------------------- |
| 0      | Vector3   | Light Offset                |
| 12     | Vector4   | Color\*                     |
| 24     | Float     | Light Glare Radius (meters) |
| 28     | Float     | Light Range                 |
| 32     | Int32     | Glare Count                 |
| 36     | Vector3[] | Glares                      |

\* W is constant at 1f
