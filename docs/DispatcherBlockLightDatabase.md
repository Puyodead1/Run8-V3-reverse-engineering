# Dispatcher Block Light Database (DispatcherBlockLightDatabase.r8)

Something to do with block lights?

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Int32 | Number of Entries |
| 8      | Entry | Entries           |

## Entry

| Offset | Type      | Explaination       |
| ------ | --------- | ------------------ |
| 0      | Int32     | Unknown n          |
| 4      | Rectangle | Rectangle          |
| 20     | Vector2   | Unknown            |
| 28     | Int32     | Number of Unknown1 |
| 32     | ...       | Unknown1           |
| 36     | ...       | Unknown2 if n == 2 |

## Unknown1

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Unknown      |

## Unknown2

| Offset | Type   | Explaination |
| ------ | ------ | ------------ |
| 0      | String | Unknown      |

## Rectangle

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | X            |
| 4      | Int32 | Y            |
| 8      | Int32 | Width        |
| 12     | Int32 | Height       |

## Vector2

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Float | X            |
| 4      | Float | Y            |

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
