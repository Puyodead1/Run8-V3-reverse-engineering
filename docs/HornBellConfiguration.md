# Horn Bell Configuration (HornBellConfiguration.r8)

Mapping of horn and bell names to xml file configurations

## Header

| Offset | Type   | Explaination |
| ------ | ------ | ------------ |
| 0      | Int32  | Reserved     |
| 4      | Int32  | Horn Count   |
| 8      | Horn   | Horns        |
| 12     | Int32  | Bell Count   |
| 16     | Bell[] | Bells        |

## Horn

| Offset | Type     | Explaination                 |
| ------ | -------- | ---------------------------- |
| 0      | CSString | Key: Locomotive XML filename |
| ...    | CSString | Value: Horn name             |

## Bell

| Offset | Type     | Explaination                 |
| ------ | -------- | ---------------------------- |
| 0      | CSString | Key: Locomotive XML Filename |
| ...    | CSString | Value: Bell name             |
