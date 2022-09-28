# AISpecialLocations.r8

Seems to contain information about "special" track locations.

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Int32 | Number of entries |
| 8      | Entry | Entries           |

## Entry

| Offset | Type   | Explaination        |
| ------ | ------ | ------------------- |
| 0      | Int32  | Reserved            |
| 4      | String | Location Name       |
| ...    | Byte   | SpecialLocationType |
| ...    | Int32  | Unknown             |
| ...    | Int32  | Unknown             |
| ...    | Int32  | Unknown             |
| ...    | Float  | Unknown             |
| ...    | Int32  | Unknown             |
| ...    | Byte   | Unknown Boolean     |

## String

| Offset | Type  | Explaination        |
| ------ | ----- | ------------------- |
| 0      | Int32 | Size of string data |
| 4      | Bytes | String data         |

## SpecialLocationType

| Key | Value                      |
| --- | -------------------------- |
| 0   | SpawnPoint                 |
| 1   | CrewChange                 |
| 2   | CrewChangeAndHold          |
| 3   | Passenger                  |
| 4   | PassengerCrewChange        |
| 5   | PassengerCrewChangeAndHold |
| 6   | Relinquish                 |
| 7   | PassengerRelinquish        |

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
