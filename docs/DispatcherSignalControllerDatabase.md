# Dispatcher Signal Controller Database (DispatcherSignalControllerDatabase.r8)

Contains a list of dispatcher signal controllers, whatever those are.

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Int32 | Entry Count		 |
| 8      | Entry | Entries           |

## Entry

| Offset | Type                 | Explaination                                                             |
| ------ | -------------------- | ------------------------------------------------------------------------ |
| 0      | Int32                | Unknown n                                                                |
| 4      | Vector2              | Vector2 for DispatcherBlockLight below                                   |
| 12     | DispatcherBlockLight | See [DispatcherBlockLight Entry](/DispatcherBlockLightDatabase.md#Entry) |
| ...    | Int32                | Unknown1 Count			                                               |
| ...    | Unknown1             | Unknown1s                                                         |
| ...    | Unknown2             | Sub1 if n == 2                                                           |

## Unknown1 Entry

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Unknown      |

## Sub1

| Offset | Type     | Explaination |
| ------ | -------- | ------------ |
| 0      | R8String | Unknown      |
