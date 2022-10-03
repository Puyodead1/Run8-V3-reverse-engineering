# Dispatcher Switch Icon Database (DispatcherSwitchIconDatabase.r8)

Contains a list of dispatcher switch icons.

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Int32 | Number of entries |
| 8      | Entry | Entries           |

## Entry

| Offset | Type      | Explaination               |
| ------ | --------- | -------------------------- |
| 0      | Int32     | Unknown n                  |
| 4      | Rectangle | Unknown                    |
| 12     | Vector2   | Unknown                    |
| 20     | Int32     | Unknown                    |
| 24     | Int32     | Unknown                    |
| 28     | Int32     | Number of Unknown1 Entries |
| 32     | Unknown1  | Unknown1 Entries           |
| ...    | Unknown2  | Sub1 if n == 2             |

## Unknown1 Entry

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Unknown      |

## Sub1

| Offset | Type   | Explaination |
| ------ | ------ | ------------ |
| 0      | String | Unknown      |

## Vector2

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Float | X            |
| 4      | Float | Y            |

## Rectangle

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | X            |
| 4      | Int32 | Y            |
| 8      | Int32 | Width        |
| 12     | Int32 | Height       |

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
