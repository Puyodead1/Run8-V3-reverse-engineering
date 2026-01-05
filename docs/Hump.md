# Hump (Hump.r8)

Contains hump configurations for a region

## Header

| Offset | Type  | Explaination    |
| ------ | ----- | --------------- |
| 0      | Int32 | Reserved        |
| 4      | Int32 | Number of Humps |
| 8      | Hump  | Hump            |

## Hump

| Offset | Type       | Explaination          |
| ------ | ---------- | --------------------- |
| 0      | Int32      | Reserved              |
| 4      | R8String   | Hump Name             |
| ...    | Int32      | Number of HumpConfigs |
| ...    | HumpConfig | Configs               |

## HumpConfig

| Offset | Type      | Explaination            |
| ------ | --------- | ----------------------- |
| 0      | Int32     | Reserved                |
| 4      | Boolean   | HasName                 |
| 5      | R8String  | Config Name<sup>1</sup> |
| ...    | Int32     | Number of Tracks        |
| ...    | HumpTrack | Tracks                  |

-   <sup>1</sup>: Only read if HasName is True

## HumpTrack

| Offset | Type       | Explaination   |
| ------ | ---------- | -------------- |
| 0      | Int32      | Reserved       |
| 4      | R8String   | Track Name     |
| ...    | Int32      | Number of Tags |
| ...    | R8String[] | Tags           |
