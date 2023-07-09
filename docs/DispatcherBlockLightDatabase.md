# Dispatcher Block Light Database (DispatcherBlockLightDatabase.r8)

List of Dispatcher Lights (whatever those are? probably something with the lights in the dispatcher screen)

## Header

| Offset | Type            | Explaination              |
| ------ | --------------- | ------------------------- |
| 0      | Int32           | Reserved                  |
| 4      | Int32           | Number of Dispatch Lights |
| 8      | DispatchLight[] | Dispatch Lights           |

## DispatchLight

| Offset | Type      | Explaination       |
| ------ | --------- | ------------------ |
| 0      | Int32     | Unknown n          |
| 4      | Rectangle | Button Rectangle   |
| 20     | Vector2   | Screen XY          |
| 28     | Int32[]   | Number of Indices  |
| ...    | String    | Name? if n == 2<sup>1</sup> |

- <sup>1</sup>: Always seems to be `block01`, although the actual default is an empty string