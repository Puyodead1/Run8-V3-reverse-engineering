# Dispatcher Switch Icon Database (DispatcherSwitchIconDatabase.r8)

Contains a list of Dispatcher Switch Icons

## Header

| Offset | Type               | Explaination    |
| ------ | ------------------ | --------------- |
| 0      | Int32              | Reserved        |
| 4      | Int32              | Number of Icons |
| 8      | DispatcherButton[] | Icons           |

## DispatcherButton

| Offset | Type      | Explaination                |
| ------ | --------- | --------------------------- |
| 0      | Int32     | Unknown n                   |
| 4      | Rectangle | Button                      |
| 16     | Vector2   | ScreenXY                    |
| ...    | Int32     | Route Prefix                |
| ...    | Int32     | Index                       |
| ...    | Int32     | Number of SignalControllers |
| ...    | Int32[]   | Signal Controller IDs       |
| ...    | String    | Name? If n == 2             |
