# CommTowerDatabase.r8

Contains a list of Comm Towers

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Int32 | Number of Entries |
| 8      | Entry | Entries           |

## Entry

| Offset | Type      | Explaination         |
| ------ | --------- | -------------------- |
| 0      | Int32     | Reserved             |
| 4      | TileIndex | Tile Index           |
| 12     | Vector3   | Location?            |
| 24     | String    | Tower Name           |
| ...    | Byte      | Unknown              |
| ...    | String    | Dial Code            |
| ...    | String    | Emergency Dial Code? |
| ...    | Float     | Unknown              |
| ...    | String    | Dispatch Tone Type   |

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
