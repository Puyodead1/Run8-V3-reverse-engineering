# DefectDetectorList.r8

Contains a list of defect detectors.

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Int32 | Number of Entries |
| 8      | Entry | Entries           |

## Entry

| Offset | Type  | Explaination                   |
| ------ | ----- | ------------------------------ |
| 0      | Int32 | Unknown n                      |
| 4      | Int32 | Sub1 if n == 1, Sub2 if n == 2 |

## Sub1

| Offset | Type      | Explaination                                                   |
| ------ | --------- | -------------------------------------------------------------- |
| 0      | Int32     | ID 1, Seems to be displayed like an ID in the format `id1.id2` |
| 4      | Int32     | ID 1, Seems to be displayed like an ID in the format `id1.id2` |
| 8      | TileIndex | TileIndex                                                      |
| 16     | Vector3   | Location                                                       |
| 28     | Byte      | Unknown Boolean                                                |
| 29     | Byte      | Unknown Boolean                                                |
| 30     | Byte      | Unknown Boolean                                                |
| 31     | Byte      | Unknown Boolean                                                |
| 32     | Byte      | Unknown Boolean                                                |
| 33     | Byte      | Unknown Boolean                                                |
| 34     | String    | Detector .xwb file name                                        |
| ...    | String    | Detector .xsb file name                                        |
| ...    | Int32     | Track Number?                                                  |

## Sub2

| Offset | Type      | Explaination                                                   |
| ------ | --------- | -------------------------------------------------------------- |
| 0      | Int32     | ID 1, Seems to be displayed like an ID in the format `id1.id2` |
| 4      | Int32     | ID 1, Seems to be displayed like an ID in the format `id1.id2` |
| 8      | TileIndex | TileIndex                                                      |
| 16     | Vector3   | Location                                                       |
| 28     | Byte      | Unknown Boolean                                                |
| 29     | Byte      | Unknown Boolean                                                |
| 30     | Byte      | Unknown Boolean                                                |
| 31     | Byte      | Unknown Boolean                                                |
| 32     | Byte      | Unknown Boolean                                                |
| 33     | Byte      | Unknown Boolean                                                |
| 34     | Byte      | Unknown Boolean                                                |
| 35     | String    | Unknown                                                        |
| ...    | String    | Unknown                                                        |
| ...    | Int32     | Track Number?                                                  |

## Tile Index

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | X            |
| 4      | Int32 | Y            |

## Vector3

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Float | X            |
| 4      | Float | Y            |
| 8      | Float | Z            |

## String

| Offset | Type  | Explaination        |
| ------ | ----- | ------------------- |
| 0      | Int32 | Size of string data |
| 4      | Bytes | String data         |

## Encoding Strings

```c#
string s = "1ST COAST RECYCLING";
byte[] bytes = Encoding.UTF8.GetBytes(s);
byte[] encoded = new byte[bytes.Length * 2];
int num = 0;
for (int i = 0; i < bytes.Length; i++)
{
	encoded[num++] = (byte)(bytes[i] >> 4);
	encoded[num++] = (byte)(bytes[i] << 4);
}
```

## Decoding Strings

```c#
byte[] encoded = <string data>;
byte[] decodedBytes = new byte[encoded.Length / 2];
int num = 0;
for (int i = 0; i < decodedBytes.Length; i++)
{
	decodedBytes[i] |= (byte)(encoded[num++] << 4);
	decodedBytes[i] |= (byte)(encoded[num++] >> 4);
}

string decodedString = Encoding.UTF8.GetString(decodedBytes);
```
