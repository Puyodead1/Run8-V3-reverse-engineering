# Dark Signal Database (DarkSignalDatabase.r8)

Contains a list of signals and their information

## Header

| Offset | Type        | Explanation  |
| ------ | ----------- | ------------ |
| 0      | Int32       | Reserved     |
| 4      | Int32       | Entry Count  |
| 8      | DarkEntry[] | Entries      |

## DarkEntry

| Offset | Type        | Explanation                              |
| ------ | ----------- | ---------------------------------------- |
| 0      | Int32       | Reserved                                 |
| 4      | Int32       | Section Count (usually 1, sometimes 2)   |
| 8      | Int32[]     | Section IDs (Section Count entries)      |

After Section IDs, continue reading sequentially:

| Offset | Type      | Explanation                              |
| ------ | --------- | ---------------------------------------- |
| 0      | Int32     | Signal Count                             |
| 4      | Signal[]  | Signals (Signal Count entries)           |

## Signal

| Offset | Type  | Explanation        |
| ------ | ----- | ------------------ |
| 0      | Int32 | Route ID           |
| 4      | Int32 | Signal Head Index  |

## Section ID Encoding

Section IDs are packed into a single Int32. The first 3 digits are the route ID
and the remaining digits are the section index.

| Example  | Route ID | Section Index |
| -------- | -------- | ------------- |
| 10087    | 100      | 87            |
| 100202   | 100      | 202           |
