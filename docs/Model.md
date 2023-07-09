# 3D Model (.rn8)

Contains 3D Models.

The first 4 bytes are used to determine the "type" of the model:
  - `-969697` - Appears to only be used for locomotives. Can contain multiple objects.
  - `-969696` - Not sure, seems to only be used for a few files in `Splendid Assets`, they all seem like tests. maybe its for unoptimized models? Can contain multiple objects.
  - Everything else, reset stream. Can only contain a single object.


## Header
If the type is not `-969696` or `-969697`, the stream should be reset to 0 and read as a single Model Object

| Offset | Type              | Explaination                        |
| ------ | ----------------- |------------------------------------ |
| 0      | Int32             | Type                                |
| 4      | Int32             | Number of Objects                   |
| 8      | TypeModelObject[] | Objects                             |


## Type Model Object

| Offset | Type          | Explaination                        |
| ------ | ------------- |------------------------------------ |
| 0      | String        | Name                                |
| ...    | String        | Parent Name                         |
| ...    | Vector3       |                                     |
| ...    | Vector3       |                                     |
| ...    | Quaternion    | Rotation Quaternion                 |
| ...    | ScalingMatrix | Unused Scaling Matrix               |
| ...    | Quaternion    | Unused Rotation Quaternion          |
| ...    | Quaternion    | Unused Rotation Quaternion          |
| ...    | Vector3       | Appears to be position              |
| ...    | Quaternion    | Rotation Quaternion                 |
| ...    | ScalingMatrix | Unused Scaling Matrix               |
| ...    | Int32         | Number of Matrices                  |
| ...    | Matrix4x4[]   |                                     |
| ...    | Int32         | Number of Matrices                  |
| ...    | Matrix4x4[]   |                                     |
| ...    | ModelObject   |                                     |

## Model Object

| Offset | Type         | Explaination                        |
| ------ | ------------ |------------------------------------ |
| 0      | Int32        | Number of Verticies<sup>1</sup>     |
| 4      | VertexStruct[] | Vertex Buffer                     |
| ...    | Int32        | Number of Textures<sup>2</sup>      |
| ...    | String[]     | Textures                            |
| ...    | bool         | Indicates if the index buffer should be created as ushort |
| ...    | Int32        | Number of Indices                   |
| ...    | Int32[]      | Index Buffer                        |
| ...    | Int32        | Number of Struct1s<sup>2</sup>      |
| ...    | Struct1[]    | Struct1s                            |

- <sup>1</sup>: Divide by 7
- <sup>2</sup>: Add 6
- <sup>3</sup>: Subtract 9

## Struct1

| Offset | Type           | Explaination                |
| ------ | -------------- | --------------------------- |
| 0      | Float          | Reserved                    |
| 4      | Int32          | Texture Index               |
| 8      | Int32          | Index Count                 |
| 12     | Int32          | Start Index Location        |
| 16     | Int32          | Base Vertex Location        |

 - Reserved is just a random float that never gets read

# ScalingMatrix

| Offset | Type           | Explaination                |
| ------ | -------------- | --------------------------- |
| 0      | Float          | X                           |
| 4      | Float          | Y                           |
| 8      | Float          | Z                           |