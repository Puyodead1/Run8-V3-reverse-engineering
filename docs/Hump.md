# Humps (Hump.r8)

Configuration for route humps

## Header

| Offset | Type   | Explaination |
| ------ | ------ | ------------ |
| 0      | Int32  | Reserved     |
| 4      | Int32  | Hump Count   |
| 8      | Hump[] | The Humps    |

## Hump

| Offset | Type         | Explaination |
| ------ | ------------ | ------------ |
| 0      | Int32        | Reserved     |
| 4      | R8String     | Hump Name    |
| ...    | Int32        | Config Count |
| ...    | HumpConfig[] | Configs      |

## HumpConfig

| Offset | Type        | Explaination            |
| ------ | ----------- | ----------------------- |
| 0      | Int32       | Reserved                |
| 4      | Boolean     | HasName                 |
| 5      | R8String    | Config Name<sup>1</sup> |
| ...    | Int32       | Track Count             |
| ...    | HumpTrack[] | Tracks                  |

-   <sup>1</sup>: Only read if HasName is True

## HumpTrack

| Offset | Type       | Explaination |
| ------ | ---------- | ------------ |
| 0      | Int32      | Reserved     |
| 4      | R8String   | Track Name   |
| ...    | Int32      | Tag Count    |
| ...    | R8String[] | Tags         |
