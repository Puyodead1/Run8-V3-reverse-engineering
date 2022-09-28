# Industry Configuration (.ind)

Stores information about industries

## Header

| Offset | Type        | Explaination             |
| ------ | ----------- | ------------------------ |
| 0      | Int32       | Reserved                 |
| 4      | Int32       | Number of config entries |
| 8      | ConfigEntry | Config Entry             |

## Config Entry

| Offset | Type   | Explaination       |
| ------ | ------ | ------------------ |
| 0      | Int32  | Reserved           |
| 4      | String | Industry Name      |
| ...    | String | Local Freight Code |
| ...    | String | Industry Tag       |
| ...    | Byte   | Unknown Boolean    |
| ...    | Int32  | Number of tracks   |

## Track

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Unknown Int  |
| 4      | Int32 | Prefix       |
| 8      | Int32 | Section      |
| 12     | Int32 | Node         |

## Car

| Offset | Type  | Explaination    |
| ------ | ----- | --------------- |
| 0      | Int32 | Reserved        |
| 4      | Byte  | Car Type        |
| 8      | Byte  | Unknown Boolean |
| 12     | Int32 | Unknown Int     |
| 16     | Int32 | Unknown Int     |
| 20     | Int32 | Unknown Int     |

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
