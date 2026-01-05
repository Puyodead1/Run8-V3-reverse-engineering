# RN8 (\*.rn8)

3D Model file

Coordinate system in game is RH Y-up, -Z-forward<br/>
Coordinate system in file is: RH Z-up, +Y-forward

The first 4 bytes are used to determine the "type" of the model:

-   `-969697` - Models with multiple objects such as locomotives
-   `-969696` - Appears to be test files
-   Everything else, reset stream. Can only contain a single object.

## Root Structure

If the type is not `-969696` or `-969697`, the stream should be reset to 0 and read as a single Model Object

| Type           | ID             | Description       |
| -------------- | -------------- | ----------------- |
| Int32          | type           |                   |
| Int32          | header_basic   | If type = -969696 |
| Int32          | header_complex | If type = -969697 |
| model_object[] | objects        |                   |

## Types

### header_basic

| Type  | ID           | Description |
| ----- | ------------ | ----------- |
| Int32 | object_count |             |

### header_complex

| Type                         | ID           | Description |
| ---------------------------- | ------------ | ----------- |
| Int32                        | object_count |             |
| [Vector3](common.md#vector3) | vector3_0    |             |

### model_object

| Type         | ID                 | Description                  |
| ------------ | ------------------ | ---------------------------- |
| complex_body | complex_model_body | If type = -969696 or -969697 |
| Int32        | vertex_count       | Divide by 7                  |
| vertex[]     | vertices           |                              |
| Int32        | texture_count      | Add 6                        |
| texture[]    | textures           |                              |
| Int8         | is_ushort          |                              |
| Int32        | index_count        |                              |
| Int32[]      | indices            |                              |
| Int32        | submesh_count      | Subtract 9                   |
| submesh[]    | submeshes          |                              |

### complex_body

| Type                             | ID                      | Description |
| -------------------------------- | ----------------------- | ----------- |
| [C# String](common.md#cs_string) | name                    |             |
| [C# String](common.md#cs_string) | parent_name             |             |
| [Vector3](common.md#vector3)     | translation_vector      |             |
| [Vector3](common.md#vector3)     | relative_parent_offset  |             |
| [Vector3](common.md#vector3)     | local_rotation_offset   |             |
| [Vector3](common.md#vector3)     | unused1                 |             |
| [Vector3](common.md#vector3)     | unused2                 |             |
| [Vector3](common.md#vector3)     | unused3                 |             |
| [Vector3](common.md#vector3)     | local_rest_translation  |             |
| [Vector3](common.md#vector3)     | local_geometry_rotation |             |
| [Vector3](common.md#vector3)     | unused4                 |             |
| Int32                            | translation_frame_count |             |
| [Matrix4x4](common.md#matrix4)[] | translation_matrices    |             |
| Int32                            | rotation_frame_count    |             |
| [Matrix4x4](common.md#matrix4)[] | rotation                |             |

### vertex

| Type  | ID         | Description      |
| ----- | ---------- | ---------------- |
| Float | reserved_1 |                  |
| Float | pos_x      | Multiply by 63.7 |
| Float | normal_y   | Divide by -1.732 |
| Float | pos_z      | Divide by 16     |
| Float | uv_x       | Divide by 4.8    |
| Float | normal_x   | Divide by 10.962 |
| Float | reserved_2 |                  |
| Float | normal_z   | Divide by 11.432 |
| Float | uv_y       | Divide by 9.6    |
| Float | pos_y      | Multiply by 6    |

### texture

| Type                             | ID        | Description |
| -------------------------------- | --------- | ----------- |
| [C# String](common.md#cs_string) | file_name |             |

### submesh

| Type  | ID                   | Description |
| ----- | -------------------- | ----------- |
| Float | reserved_2           |             |
| Int32 | texture_index        |             |
| Int32 | index_count          |             |
| Int32 | start_index_location |             |
| Int32 | base_vertex_location |             |
