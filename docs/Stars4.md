# Stars4 (stars4.r8)

Stars4 file seems to contain file paths to other r8 database files, along with some random strings which are all identified by their array index.

## Header

| Offset | Type     | Explaination         |
| ------ | -------- | -------------------- |
| 0      | Int32    | Reserved             |
| 4      | Int32    | Number of Strings    |
| 8      | R8String | Run8 Encoded Strings |