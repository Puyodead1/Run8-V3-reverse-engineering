# Service Area Database (ServiceAreaDatabase.r8)

## Header

| Offset | Type          | Explaination       |
| ------ | ------------- | ------------------ |
| 0      | Int32         | Reserved           |
| 4      | Int32         | Service Area Count |
| 8      | ServiceArea[] | Service Areas      |

## ServiceArea

| Offset | Type      | Explaination      |
| ------ | --------- | ----------------- |
| 0      | Int32     | Version           |
| 4      | TileIndex | TileXZ            |
| 12     | Vector3   | Position          |
| 24     | Float     | Radius Meters     |
| ...    | Sub1      | When version == 1 |
| ...    | Struct1   | When version == 2 |

## Sub1

| Offset | Type | Explaination |
| ------ | ---- | ------------ |
| 29     | Bool | Unknown      |
| 30     | Bool | Unknown      |
| 31     | Bool | Unknown      |
| 32     | Bool | Unknown      |

## Struct1

| Offset | Type   | Explaination |
| ------ | ------ | ------------ |
| 0      | Byte   | Unknown      |
| 1      | Byte   | Unknown      |
| 2      | Double | Unknown      |
| 6      | Double | Unknown      |
