# Dispatcher Signal Controller Database (DispatcherSignalControllerDatabase.r8)

List of Dispatcher Signal Controllers

## Header

| Offset | Type                         | Explaination                  |
| ------ | ---------------------------- | ----------------------------- |
| 0      | Int32                        | Reserved                      |
| 4      | Int32                        | Controller Count              |
| 8      | DispatcherSignalController[] | Dispatcher Signal Controllers |

## DispatcherSignalController

| Offset | Type                 | Explaination                                                |
| ------ | -------------------- | ----------------------------------------------------------- |
| 0      | Int32                | Version                                                     |
| 4      | Vector2              | Block Light ScreenXY<sup>1</sup>                            |
| 12     | DispatcherBlockLight | See [DispatcherBlockLight](/Common.md#DispatcherBlockLight) |
| ...    | Int32                | Signal Head Index Count                                     |
| ...    | Int32[]              | Signal Head Indices                                         |
| ...    | String               | Image Name (If version == 2)                                |

-   <sup>1</sup>: This is used for the ScreenXY in DispatcherBlockLight
