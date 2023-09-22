# XNG

Crossing Gates

## Header

| Offset | Type   | Explaination                  |
| ------ | ------ | ----------------------------- |
| 0      | Byte   | Gate Rotation Type            |
| ...    | String | Gate Model Name               |
| ...    | String | Stand Model Name              |
| ...    | Float  | Active Degrees                |
| ...    | Float  | Inactive Degrees              |
| ...    | Float  | Rotation Degrees Per Second\* |

\* This gets multiplied by a random float between the range of `-4f` and `3f`

## GateRotationType

| Key | Value       |
| --- | ----------- |
| 0   | Vertical    |
| 1   | Horizontal  |
| 2   | XBuckNoBell |

Some methods to look into in the future:

-   0x06002B51
