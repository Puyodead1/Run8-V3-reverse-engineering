# Dispatcher Block Light Database (DispatcherBlockLightDatabase.r8)

Something to do with block lights?

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Int32 | Entry Count		 |
| 8      | Entry | Entries           |

## Entry

| Offset | Type      | Explaination       |
| ------ | --------- | ------------------ |
| 0      | Int32     | Unknown n          |
| 4      | Rectangle | Rectangle          |
| 20     | Vector2   | Unknown            |
| 28     | Int32     | Unknown1 Count	  |
| 32     | ...       | Unknown1s          |
| 36     | ...       | Unknown2 if n == 2 |

## Unknown1

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Unknown      |

## Unknown2

| Offset | Type     | Explaination |
| ------ | -------- | ------------ |
| 0      | R8String | Unknown      |