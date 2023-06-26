meta:
  id: run8_avatar
  title: Run8 V3 Avatar File
  application: Run8 Train Simulator V3
  file-extension: rn8
  endian: le
  imports:
    - run8_common
seq:
  - id: vertex_count
    type: s4
    doc: The number of verticies in the mesh
  - id: vertices
    type: vertex_struct
    repeat: expr
    repeat-expr: num_vertices
    doc: The verticies in the mesh
  - id: texture_count
    type: s4
    doc: The number of textures in the mesh
  - id: textures
    type: run8_common::c_string
    repeat: expr
    repeat-expr: num_textures
    doc: The textures in the mesh
  - id: is_ushort_index_buffer
    type: b1
    doc: Whether the index buffer is ushort or not
  - id: num_index_buffer
    type: s4
    doc: The size of the index buffer
  - id: ushort_index_buffer
    type: u2
    if: is_ushort_index_buffer == true
    repeat: expr
    repeat-expr: num_index_buffer
    doc: The index buffer
  - id: index_buffer
    type: s4
    if: is_ushort_index_buffer == false
    repeat: expr
    repeat-expr: num_index_buffer
    doc: The index buffer
  - id: num_unknown_structs
    type: s4
    doc: The number of unknown structs
  - id: unknown_structs
    type: unknown_struct
    repeat: expr
    repeat-expr: num_unknown_struct_altered
    doc: The unknown structs
  - id: num_skeleton_hierarchy
    type: s4
    doc: The number of bones in the skeleton
  - id: skeleton_hierarchy
    type: s4
    repeat: expr
    repeat-expr: num_skeleton_hierarchy
    doc: The bones in the skeleton
  - id: num_bone_indices
    type: s4
    doc: The number of bone indices
  - id: bone_indices
    type: bone_index_struct
    repeat: expr
    repeat-expr: num_bone_indices
    doc: The bone indices
  - id: num_bind_poses
    type: s4
    doc: The number of bind poses
  - id: bind_poses
    type: run8_common::matrix4
    repeat: expr
    repeat-expr: num_bind_poses
    doc: The bind poses
  - id: num_inverse_bind_poses
    type: s4
    doc: The number of inverse bind poses
  - id: inverse_bind_poses
    type: run8_common::matrix4
    repeat: expr
    repeat-expr: num_inverse_bind_poses
    doc: The inverse bind poses
  - id: num_animations
    type: s4
    doc: The number of animations. This seems always be 16
  - id: animations
    type: animation_clip
    repeat: expr
    repeat-expr: num_animations
    doc: The animations
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
        doc: A value that is never used in the game
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
        doc: A value that is never used in the game
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
        type: run8_common::c_string
        doc: The bone name
      - id: bone_index
        type: s4
        doc: The bone index
  animation_clip:
    seq:
      - id: key
        type: run8_common::c_string
        doc: The animation clip name
      - id: duration
        type: f8
        doc: The duration of the animation clip
      - id: num_keyframes
        type: s4
        doc: The number of keyframes
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
        type: run8_common::matrix4
        doc: The transform of the keyframe
        