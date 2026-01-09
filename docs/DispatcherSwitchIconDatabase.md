# Dispatcher Switch Icon Database (DispatcherSwitchIconDatabase.r8)

Contains a list of Dispatcher Switch Icons

## Header

| Offset | Type                   | Explaination |
| ------ | ---------------------- | ------------ |
| 0      | Int32                  | Reserved     |
| 4      | Int32                  | Icon Count   |
| 8      | DispatcherSwitchIcon[] | Icons        |

## DispatcherSwitchIcon

| Offset | Type      | Explaination                  |
| ------ | --------- | ----------------------------- |
| 0      | Int32     | Version                       |
| 4      | Rectangle | Button                        |
| 16     | Vector2   | ScreenXY                      |
| ...    | Int32     | Route Prefix                  |
| ...    | Int32     | Index                         |
| ...    | Int32     | Signal Controller Index Count |
| ...    | Int32[]   | Signal Controller Indices     |
| ...    | String    | Image Name (If version == 2)  |
