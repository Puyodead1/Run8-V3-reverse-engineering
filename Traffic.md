# Traffic.r8

Seems to contain information about AI traffic such as cars, etc

## Header

| Offset | Type  | Explaination      |
| ------ | ----- | ----------------- |
| 0      | Int32 | Reserved          |
| 4      | Byte  | Unknown Boolean   |
| 5      | Int32 | Unknown           |
| 9      | Int32 | Unknown           |
| 13     | Int32 | Unknown           |
| 17     | Int32 | Number of entries |
| 21     | Entry | Entries           |

## Entry

| Offset | Type     | Explaination       |
| ------ | -------- | ------------------ |
| 0      | Int32    | Reserved           |
| 4      | String   | Route Name 	     |
| 8      | Int32    | Unknown            |
| 12     | Int32    | Number of Unknown1 |
| 16     | Unknown1 | Unknown1 Entries   |

## Unknown1

| Offset | Type       | Explaination                                              |
| ------ | ---------- | --------------------------------------------------------- |
| 0      | Int32      | Reserved                                                  |
| 4      | TrainClass | Train Class                                               |
| 5      | Int32      | Unknown                                                   |
| 9      | Int32      | Number of Unknown2                                        |
| 13     | Unknown2   | Unknown2 Entries (only read if train class is SavedTrain) |
| ...    | Int32      | Number of Unknown3                                        |
| ...    | Unknown3   | Unknown3 Entries                                          |

## Unknown2

| Offset | Type   | Explaination |
| ------ | ------ | ------------ |
| 0      | Int32  | Reserved     |
| 4      | String | Unknown      |
| ...    | String | Unknown      |
| ...    | Int32  | Unknown      |

## Unknown3

| Offset | Type       | Explaination                              |
| ------ | ---------- | ----------------------------------------- |
| 0      | Int32      | Unknown n                                 |
| 4      | Byte       | Unknown Boolean                           |
| 5      | String     | Train Tag (only read if above bool is true) |
| ...    | Byte		  | Train Caste                               |
| ...    | Byte		  | Train Special Restrictions   			  |
| ...    | Byte       | Unknown Boolean                           |
| ...    | Byte       | Unknown Boolean                           |
| ...    | Byte       | Unknown Boolean                           |
| ...    | Int32      | Number of unknown Strings                 |
| ...    | String     | Unknown Strings                           |
| ...    | Int32      | Number of unknown Strings                 |
| ...    | String     | Unknown Strings                           |
| ...    | Int32      | Number of unknown Strings                 |
| ...    | String     | Unknown Strings                           |
| ...    | Int32      | Number of unknown Strings                 |
| ...    | String     | Unknown Strings                           |
| ...    | Sub1       | Sub1 (if n > 1)                           |

### Sub1

| Offset | Type   | Explaination              |
| ------ | ------ | ------------------------- |
| 0      | Int32  | Number of unknown Strings |
| 4      | String | Unknown Strings           |
| ...    | Int32  | Number of unknown Strings |
| ...    | String | Unknown Strings           |
| ...    | Sub2   | Sub2 (if n > 2)           |

### Sub2

| Offset | Type | Explaination    |
| ------ | ---- | --------------- |
| 0      | Byte | Unknown Boolean |

## TrainClass

| Key | Value                  |
| --- | ---------------------- |
| 0   | None                   |
| 1   | Passenger              |
| 2   | Baretables             |
| 3   | ContainerDomestic      |
| 4   | ContainerInternational |
| 5   | ContainerMixed         |
| 6   | Intermodal             |
| 7   | MixedIntermodal        |
| 8   | FreightMixed           |
| 9   | UnitAutorack           |
| 10  | UnitCoal               |
| 11  | UnitOil                |
| 12  | UnitGrain              |
| 13  | UnitReefer             |
| 14  | PowerMove              |
| 15  | UnitBethgonCoal        |
| 16  | UnitCoilSteel          |
| 255 | SavedTrain             |

## TrainCaste

| Key | Value          |
| --- | -------------- |
| 0   | UtterPeon      |
| 1   | Low            |
| 2   | Medium         |
| 3   | High           |
| 4   | KingOfTheRails |

## TrainSpecialRestrictions

| Key | Value       | Attribute  |
| --- | ----------- | ---------- |
| 0   | None        | None       |
| 2   | TehachapiWB | SouthernCA |
| 4   | TehachapiEB | SouthernCA |
| 8   | CajonWB1    | SouthernCA |
| 16  | CajonWB2    | SouthernCA |
| 32  | CajonEB1    | SouthernCA |
| 64  | CajonEB2    | SouthernCA |

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
