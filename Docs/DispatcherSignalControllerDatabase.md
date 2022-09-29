# Dispatcher Signal Controller Database (DispatcherSignalControllerDatabase.r8)

Contains a list of dispatcher signal controllers, whatever those are.

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Int32 | Number of entries |
| 8      | Entry | Entries           |

## Entry

| Offset | Type                 | Explaination                                                             |
| ------ | -------------------- | ------------------------------------------------------------------------ |
| 0      | Int32                | Unknown n                                                                |
| 4      | Vector2              | Vector2 for DispatcherBlockLight below                                   |
| 12     | DispatcherBlockLight | See [DispatcherBlockLight Entry](/DispatcherBlockLightDatabase.md#Entry) |
| ...    | Int32                | Number of Unknown1 Entries                                               |
| ...    | Unknown1             | Unknown1 Entries                                                         |
| ...    | Unknown2             | Sub1 if n == 2                                                           |

## Unknown1 Entry

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Int32 | Unknown      |

## Sub1

| Offset | Type   | Explaination |
| ------ | ------ | ------------ |
| 0      | String | Unknown      |

## String

| Offset | Type  | Explaination        |
| ------ | ----- | ------------------- |
| 0      | Int32 | Size of string data |
| 4      | Bytes | String data         |

## Vector2

| Offset | Type  | Explaination |
| ------ | ----- | ------------ |
| 0      | Float | X            |
| 4      | Float | Y            |

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
