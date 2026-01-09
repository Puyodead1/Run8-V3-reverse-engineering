# Run8 Key Settings (Run8KeySettings.r8)

## Header

| Offset | Type         | Explaination  |
| ------ | ------------ | ------------- |
| 0      | Int32        | Reserved      |
| 4      | Int32        | Setting Count |
| 8      | KeySetting[] | KeySettings   |

## KeySetting

| Offset | Type     | Explaination                                                                                                                     |
| ------ | -------- | -------------------------------------------------------------------------------------------------------------------------------- |
| 0      | Int32    | Reserved                                                                                                                         |
| 4      | R8String | Name                                                                                                                             |
| ...    | Byte     | ERailDriverButton                                                                                                                |
| ...    | Int32    | Key Count                                                                                                                        |
| ...    | Int32[]  | Key List (EKey) [See SharpDX Enumerations.cs](https://github.com/sharpdx/SharpDX/blob/master/Source/SharpDX.DirectInput/Generated/Enumerations.cs#L1562) |

## ERailDriverButton
TODO: figure out what the buttons actually are

| Key | Value    |
| --- | -------- |
| 0   | NotSet   |
| 1   | None     |
| 2   | Button0  |
| 3   | Button1  |
| 4   | Button2  |
| 5   | Button3  |
| 6   | Button4  |
| 7   | Button5  |
| 8   | Button6  |
| 9   | Button7  |
| 10  | Button8  |
| 11  | Button9  |
| 12  | Button10 |
| 13  | Button11 |
| 14  | Button12 |
| 15  | Button13 |
| 16  | Button14 |
| 17  | Button15 |
| 18  | Button16 |
| 19  | Button17 |
| 20  | Button18 |
| 21  | Button19 |
| 22  | Button20 |
| 23  | Button21 |
| 24  | Button22 |
| 25  | Button23 |
| 26  | Button24 |
| 27  | Button25 |
| 28  | Button26 |
| 29  | Button27 |
| 30  | Button28 |
| 31  | Button29 |
| 32  | Button30 |
| 33  | Button31 |
| 34  | Button32 |
| 35  | Button33 |
| 36  | Button34 |
| 37  | Button35 |
| 38  | Button36 |
| 39  | Button37 |
| 40  | Button38 |
| 41  | Button39 |
| 42  | Button40 |
| 43  | Button41 |
| 44  | Button42 |
| 45  | Button43 |
