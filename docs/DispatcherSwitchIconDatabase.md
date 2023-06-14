# Dispatcher Switch Icon Database (DispatcherSwitchIconDatabase.r8)

Contains a list of dispatcher switch icons.

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Int32 | Entry Count		 |
| 8      | Entry | Entries           |

## Entry

| Offset | Type      | Explaination               |
| ------ | --------- | -------------------------- |
| 0      | Int32     | Unknown n                  |
| 4      | Rectangle | Unknown                    |
| 12     | Vector2   | Unknown                    |
| 20     | Int32     | Unknown                    |
| 24     | Int32     | Unknown                    |
| 28     | Int32     | Unknown1 Count			  |
| 32     | Unknown1  | Unknown1s                  |
| ...    | Unknown2  | Sub1 if n == 2             |

## Unknown1 Entry

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Unknown      |

## Sub1

| Offset | Type   | Explaination |
| ------ | ------ | ------------ |
| 0      | String | Unknown      |