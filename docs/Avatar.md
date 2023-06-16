# Avatar

There are currently 3 avatars:
- Brian
- Chris
- Pablo

Avatar files are 3D model files with the `rn8` file extension.

## Header

| Offset | Type            | Explaination            |
| ------ | --------------- | ----------------------- |
| 0      | Int32           | VertexStruct Count\*    |
| 4      | VertexStruct[]  | VertexStructs           |
| ...    | Int32           | Texture Name Count      |
| ...    | String[]        | Texture Names           |
| ...    | Bool            | IsUshortBuffer          |
| ...    | Int32           | Index Buffer Size       |
| ...    | Int32[]         | Index Buffer            |
| ...    | Int32           | Unknown1 Count          |
| ...    | UnknownStruct1  | UnknownStruct1s\*\*     |
| ...    | Int32           | Skeleton Hiearchy Count |
| ...    | Int32[]         | Skeleton Hiearchy       |
| ...    | Int32           | Bone Index Count        |
| ...    | Int32[]         | Bone Indicies           |
| ...    | Int32           | Bind Pose Count         |
| ...    | Matrix4x4[]     | Bind Poses              |
| ...    | Int32           | Inverse Bind Pose Count |
| ...    | Matrix4x4[]     | Inverse Bind Poses      |
| ...    | Int32           | Animation Clip Count    |
| ...    | AnimationClip[] | Animation Clips         |

- \* This number gets divided by 7
- \*\* These are only read when the count is not 0, otherwise only a single entry is created


## UnknownStruct1

| Offset | Type           | Explaination         | Value If Single   |
| ------ | -------------- | -------------------- | ----------------- |
| 0      | Int32          | Reserved             |                   |
| 4      | Int32          | Texture Index\*      |                   |
| 8      | Int32          | Index Count          | Index Buffer Size |
| 12     | Int32          | Start Index Location | 0                 |
| 16     | Int32          | Base VertexLocation  | 0                 |

- \* Gets the texture at the index and appends `_mrao`

## AnimationClip

| Offset | Type           | Explaination         |
| ------ | -------------- | -------------------- |
| 0      | String         | Key                  |
| ...    | Double         | Duration (ms)        |
| ...    | Int32          | Keyframe Count       |
| ...    | Keyframe[]     | Keyframes            |

## Keyframe

| Offset | Type           | Explaination         |
| ------ | -------------- | -------------------- |
| 0      | Int32          | Bone                 |
| 4      | Double         | Time (ms)            |
| 12     | Matrix4x4      | Transform            |