meta:
  id: avatar
  title: Avatar File
  application: Run8 Train Simulator V3
  file-extension: rn8
  endian: le
  imports:
    - common
seq:
  - id: vertex_count
    type: s4
    doc: d:Vertex count
  - id: vertices
    type: vertex_struct
    repeat: expr
    repeat-expr: num_vertices
  - id: texture_count
    type: s4
    doc: d:Texture count
  - id: textures
    type: common::cs_string
    repeat: expr
    repeat-expr: num_textures
  - id: is_ushort_index_buffer
    type: common::boolean
    doc: d:Whether the index buffer is ushort or not
  - id: num_index_buffer
    type: s4
    doc: d:Indice count
  - id: ushort_index_buffer
    type: u2
    if: is_ushort_index_buffer.is_true
    doc: d:Only if the index buffer is ushort
    repeat: expr
    repeat-expr: num_index_buffer
  - id: index_buffer
    type: s4
    if: is_ushort_index_buffer.is_false
    doc: d:Only if the index buffer is not ushort
    repeat: expr
    repeat-expr: num_index_buffer
  - id: num_unknown_structs
    type: s4
  - id: unknown_structs
    type: unknown_struct
    repeat: expr
    repeat-expr: num_unknown_struct_altered
  - id: num_skeleton_hierarchy
    type: s4
    doc: d:Skeleton bone count
  - id: skeleton_hierarchy
    type: s4
    repeat: expr
    repeat-expr: num_skeleton_hierarchy
  - id: num_bone_indices
    type: s4
    doc: d:Bone Indices
  - id: bone_indices
    type: bone_index_struct
    repeat: expr
    repeat-expr: num_bone_indices
  - id: num_bind_poses
    type: s4
    doc: d:Bind pose count
  - id: bind_poses
    type: common::matrix4
    repeat: expr
    repeat-expr: num_bind_poses
  - id: num_inverse_bind_poses
    type: s4
    doc: d:Inverse bind pose count
  - id: inverse_bind_poses
    type: common::matrix4
    repeat: expr
    repeat-expr: num_inverse_bind_poses
  - id: num_animations
    type: s4
    doc: d:Animation count; This seems always be 16
  - id: animations
    type: animation_clip
    repeat: expr
    repeat-expr: num_animations
instances:
  num_vertices:
    value: vertex_count / 7
  num_textures:
    value: texture_count + 6
  num_unknown_struct_altered:
    value: num_unknown_structs - 9
types:
  vertex_struct:
    seq:
      - id: reserved1
        type: f4
      - id: position_x
        type: f4
        doc: The x position of the vertex
      - id: normal_y
        type: f4
        doc: The y normal of the vertex
      - id: position_z
        type: f4
        doc: The z position of the vertex
      - id: uv_x
        type: f4
        doc: The x uv of the vertex
      - id: normal_x
        type: f4
        doc: The x normal of the vertex
      - id: reserved2
        type: f4
        doc: A value that is never used in the game
      - id: normal_z
        type: f4
        doc: The z normal of the vertex
      - id: uv_y
        type: f4
        doc: The y uv of the vertex
      - id: position_y
        type: f4
        doc: The y position of the vertex
      - id: blend_index_w
        type: u1
        doc: The w blend index of the vertex
      - id: blend_weight_z
        type: f4
        doc: The z blend weight of the vertex
      - id: blend_index_x
        type: u1
        doc: The x blend index of the vertex
      - id: blend_weight_y
        type: f4
        doc: The y blend weight of the vertex
      - id: blend_index_y
        type: u1
        doc: The y blend index of the vertex
      - id: blend_weight_w
        type: f4
        doc: The w blend weight of the vertex
      - id: blend_idex_z
        type: u1
        doc: The z blend index of the vertex
      - id: blend_weight_x
        type: f4
        doc: The x blend weight of the vertex
  unknown_struct:
    seq:
      - id: reserved
        type: s4
      - id: texture_index
        type: s4
        doc: Index of the texture
      - id: num_index_buffer
        type: s4
        doc: The size of the index buffer
      - id: start_index_location
        type: s4
        doc: Start index of the index buffer
      - id: base_vertex_location
        type: s4
        doc: Start index of the vertex buffer
  bone_index_struct:
    seq:
      - id: key
        type: common::cs_string
        doc: The bone name
      - id: bone_index
        type: s4
        doc: The bone index
  animation_clip:
    seq:
      - id: key
        type: common::cs_string
        doc: The animation clip name
      - id: duration
        type: f8
        doc: The duration of the animation clip
      - id: num_keyframes
        type: s4
        doc: Number of keyframes
      - id: keyframes
        type: animation_keyframe
        repeat: expr
        repeat-expr: num_keyframes
        doc: The keyframes
  animation_keyframe:
    seq:
      - id: bone_index
        type: s4
        doc: The bone index
      - id: time
        type: f8
        doc: The time of the keyframe
      - id: transform
        type: common::matrix4
        doc: The transform of the keyframe
