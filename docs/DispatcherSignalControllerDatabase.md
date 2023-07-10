# Dispatcher Signal Controller Database (DispatcherSignalControllerDatabase.r8)

List of Dispatcher Signal Controllers (whatever those are? something to do with dispatcher screen)

## Header

| Offset | Type                         | Explaination                  |
| ------ | ---------------------------- | ----------------------------- |
| 0      | Int32                        | Reserved                      |
| 4      | Int32                        | Number of Controllers	        |
| 8      | DispatcherSignalController[] | Dispatcher Signal Controllers |

## DispatcherSignalController

| Offset | Type                 | Explaination                                                             |
| ------ | -------------------- | ------------------------------------------------------------------------ |
| 0      | Int32                | Unknown n                                                                |
| 4      | Vector2              | ScreenXY<sup>1</sup>                                                     |
| 12     | DispatcherLight      | See [DispatcherLight](/Common.md#DispatcherLight)                        |
| ...    | Int32                | Number of Signals			                                               |
| ...    | Int32[]              | Signal Head IDs                                                          |
| ...    | String               | Name? If n == 2                                                          |
- <sup>1</sup>: This overrides the ScreenXY in DispatcherLightBlock