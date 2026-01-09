# Tunnel Configuration (TunnelConfiguration.r8)

## Header

| Offset | Type      | Explaination  |
| ------ | --------- | ------------- |
| 0      | Int32     | Reserved      |
| 4      | Int32     | Struct1 Count |
| 8      | Struct1[] | Struct1 Array |

## Struct1

| Offset | Type     | Explaination             |
| ------ | -------- | ------------------------ |
| 0      | Int32    | Reserved                 |
| 4      | R8String | Unknown                  |
| ...    | Int32    | Unknown                  |
| ...    | Int32    | Unknown Int32 List Count |
| ...    | Int32[]  | Unknown Int32 List       |
| ...    | Struct2  | Unknown                  |
| ...    | Struct2  | Unknown                  |

## Struct2

| Offset | Type      | Explaination |
| ------ | --------- | ------------ |
| 0      | TileIndex | TileXZ       |
| 8      | Vector3   | Position     |
