# Avatar

There are currently 3 avatars:

-   Brian
-   Chris
-   Pablo

Avatar files are 3D model files with the `rn8` file extension. Models are in cm

## Header

| Offset | Type              | Explaination                   |
| ------ | ----------------- | ------------------------------ |
| 0      | Int32             | VertexStruct Count<sup>1</sup> |
| 4      | VertexStruct[]    | VertexStructs                  |
| ...    | Int32             | Texture Name Count<sup>2</sup> |
| ...    | String[]          | Texture Names                  |
| ...    | Bool              | IsUshortBuffer                 |
| ...    | Int32             | Index Buffer Size              |
| ...    | Int32[]           | Index Buffer                   |
| ...    | Int32             | Unknown1 Count<sup>3</sup>     |
| ...    | UnknownStruct1    | UnknownStruct1s<sup>4</sup>    |
| ...    | Int32             | Skeleton Hierarchy Count       |
| ...    | Int32[]           | Skeleton Hierarchy             |
| ...    | Int32             | Bone Index Count               |
| ...    | BoneIndexStruct[] | Bone Indicies                  |
| ...    | Int32             | Bind Pose Count                |
| ...    | Matrix4x4[]       | Bind Poses                     |
| ...    | Int32             | Inverse Bind Pose Count        |
| ...    | Matrix4x4[]       | Inverse Bind Poses             |
| ...    | Int32             | Animation Clip Count           |
| ...    | AnimationClip[]   | Animation Clips                |

-   <sup>1</sup>: This number gets divided by 7
-   <sup>2</sup>: 6 is added to the number
-   <sup>3</sup>: 9 is subtracted from the number
-   <sup>4</sup>: These are only read when the count is not 0, otherwise only a single entry is created

## UnknownStruct1

| Offset | Type  | Explaination              | Value If Single   |
| ------ | ----- | ------------------------- | ----------------- |
| 0      | Int32 | Reserved                  |                   |
| 4      | Int32 | Texture Index<sup>1</sup> |                   |
| 8      | Int32 | Index Count               | Index Buffer Size |
| 12     | Int32 | Start Index Location      | 0                 |
| 16     | Int32 | Base Vertex Location      | 0                 |

-   <sup>1</sup>: Gets the texture at the index and appends `_mrao`

## BoneIndexStruct

| Offset | Type   | Explaination |
| ------ | ------ | ------------ |
| 0      | String | Key          |
| ...    | Int32  | Bone Index   |

## AnimationClip

| Offset | Type       | Explaination   |
| ------ | ---------- | -------------- |
| 0      | String     | Key            |
| ...    | Double     | Duration (ms)  |
| ...    | Int32      | Keyframe Count |
| ...    | Keyframe[] | Keyframes      |

## Keyframe

| Offset | Type      | Explaination |
| ------ | --------- | ------------ |
| 0      | Int32     | Bone         |
| 4      | Double    | Time (ms)    |
| 12     | Matrix4x4 | Transform    |
