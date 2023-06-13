# Horn Bell Configuration (HornBellConfiguration.r8)

Mapping of horn and bell names to xml file configurations

## Header

| Offset | Type  | Explaination    |
| ------ | ----- | --------------- |
| 0      | Int32 | Reserved        |
| 4      | Int32 | Number of Horns |
| 8      | Horn  | Horns           |
| 12     | Int32 | Number of bells |
| 16     | Bell  | Bells           |

## Horn

| Offset | Type   | Explaination                                    |
| ------ | ------ | ----------------------------------------------- |
| 0      | String | Key: Locomotive XML filename                    |
| ...    | String | Value (regular string, no custom bs): Horn name |

## Bell

| Offset | Type   | Explaination                                    |
| ------ | ------ | ----------------------------------------------- |
| 0      | String | Key: Locomotive XML Filename                    |
| ...    | String | Value (regular string, no custom bs): Bell name |

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
