meta:
  id: settings
  title: Run8 Settings
  application: Run8 Train Simulator
  file-extension: r8
  endian: le
  imports:
    - common
seq:
  - id: reserved
    type: s4
  - id: float0
    type: f4
  - id: float1
    type: f4
  - id: float2
    type: f4
  - id: headlight_intensity
    type: f4
  - id: bool0
    type: s1
  - id: bool1
    type: s1
  - id: player_name
    type: common::string
  - id: float4
    type: f4
  - id: sea_sick_intensity
    type: f4
  - id: train_draw_range
    type: f4
  - id: yard_light_range
    type: f4
  - id: tile_draw_range
    type: s4
  - id: bool2
    type: s1
  - id: vegetation_density
    type: s4
  - id: bool3
    type: s1
  - id: enable_car_spawners
    type: s1
  - id: fade_time
    type: f8
  - id: new_messages_on_top
    type: s1
  - id: bool6
    type: s1
  - id: radio_volume
    type: f4
  - id: bool7
    type: s1
  - id: use_24_hour_time
    type: s1
  - id: use_dst
    type: s1
  - id: chatbox_open_sound
    type: s1
  - id: realistic_train_handling
    type: s1
  - id: text_message_color
    type: common::color
  - id: system_message_color
    type: common::color
  - id: render_distant_terrain
    type: s1
  - id: render_weather
    type: s1
  - id: cloud_density
    type: f4
    doc: clamped to 0.6-1.0
  - id: reserved2
    type: s1
  - id: bool14
    type: s1
  - id: flange_squeal
    type: s1
  - id: bool16
    type: s1
  - id: smoke_effects
    type: s1
  - id: avatar_tags
    type: s1
  - id: max_fps
    type: s8
  - id: vsync
    type: s1
  - id: bool20
    type: s1
  - id: camera_crosshairs
    type: s1
  - id: num_list0
    type: s4
  - id: list0
    type: class787
    repeat: expr
    repeat-expr: num_list0
  - id: current_avatar
    type: s1
  - id: thin_vegetation
    type: s1
  - id: rent_a_conductor
    type: s1
  - id: conductor_current_avatar
    type: s1
  - id: generate_conductor_name
    type: s1
  - id: conductor_name
    type: common::string
  - id: mouse_look
    type: s4
  - id: mouse_move
    type: f4
  - id: int3
    type: s4
  - id: bool25
    type: s1
  - id: float11
    type: f4
  - id: float12
    type: f4
  - id: autosave_train_interval
    type: s4
  - id: autosave_train
    type: s1
  - id: int5
    type: s4
  - id: bool27
    type: s1
  - id: autosave_world_interval
    type: s4
  - id: int7
    type: s4
  - id: autosave_world
    type: s1
  - id: invert_camera_y
    type: s1
  - id: raildriver_led_readout
    type: s1
  - id: use_raildriver
    type: s1
  - id: network_password
    type: common::string
  - id: network_port
    type: s4
  - id: max_clients
    type: s4
  - id: dispatch_password
    type: common::string
  - id: ai_password
    type: common::string
  - id: consist_editor_password
    type: common::string
  - id: bool31
    type: s1
  - id: host_delete_lost_client_trains
    type: s1
  - id: network_time_sync_on
    type: s1
  - id: num_list1
    type: s4
  - id: list1
    type: class522
    repeat: expr
    repeat-expr: num_list1
  - id: region
    type: common::string
  - id: scenario_filter
    type: s1
  - id: scenario_name
    type: common::string
  - id: tdc_screen_scale
    type: f8
  - id: double2
    type: f8
  - id: client_port
    type: s4
  - id: slow_speed_for_unit
    type: s1
  - id: slow_speed_mph
    type: f4
  - id: small_window_x
    type: s4
  - id: small_window_y
    type: s4
  - id: float14
    type: f4
  - id: monitor_0_toggle
    type: s1
  - id: monitor_1_toggle
    type: s1
  - id: monitor_2_toggle
    type: s1
  - id: num_list2
    type: s4
  - id: list2
    type: class537
    repeat: expr
    repeat-expr: num_list2
  - id: string9
    type: common::cs_string
  - id: bool38
    type: s1
  - id: basic_mrao
    type: s1
  - id: airbrake_cheat
    type: s1
  - id: client_use_host_horns
    type: s1
  - id: ai_signal_call
    type: s4
  - id: det_audio_in_cab_only
    type: s1
  - id: use_shadows
    type: s1
  - id: shadow_vehicles
    type: s1
  - id: shadow_scenery
    type: s1
  - id: shadow_signal_heads
    type: s1
  - id: shadow_trains
    type: s1
  - id: shadow_switch_stands
    type: s1
  - id: shadow_quality
    type: s4
  - id: presentation_mode
    type: s1
  - id: shadow_terrain
    type: s1
  - id: shadow_update
    type: f8
  - id: shadow_sample
    type: s4
  - id: shadow_procedural_vegetation
    type: s1
  - id: cab_volume
    type: f4
  - id: train_roll
    type: f4
  - id: host_allow_clients_to_renumber
    type: s1
  - id: stringline_derailments
    type: s1
  - id: switch_derailments
    type: s1
  - id: tipover_derailments
    type: s1
  - id: allow_slippery_rails
    type: s1
  - id: parallel_updates
    type: s4
  - id: use_dof
    type: s1
  - id: realistic_alerter
    type: s1
  - id: master_volume
    type: f4
  - id: mouse_level_drag
    type: f4
  - id: highlight_mouse_drag
    type: s1
  - id: use_64bit_rt
    type: s1
  - id: bool60
    type: s1
  - id: use_vignette
    type: s1
  - id: float19
    type: f4
  - id: tone_mapping
    type: s1
  - id: render_precipitation
    type: s1
  - id: use_v2_braking
    type: s1
  - id: parking_brake_icon
    type: s1
  - id: bool67
    type: s1
  - id: realistic_independent_brake
    type: s1
  - id: allow_dynamiters
    type: s1
  - id: raindrop_mode
    type: s1
  - id: reflection_mode
    type: s1
  - id: realistic_dpu
    type: s1
  - id: host_message
    type: common::cs_string
  - id: render_static_raindrops
    type: s1
  - id: cold_wx_airbrake_effects
    type: s1
  - id: int17
    type: s4
  - id: int18
    type: s4
  - id: use_custom_device
    type: s1
  - id: udp
    type: s4
  - id: use_colorband_reduction
    type: s1
  - id: antialiasing_mode
    type: s1
  - id: msg_alert_sound
    type: s1
  - id: use_normal_mapping
    type: s1
  - id: shadow_mitigation
    type: f4
  - id: loco_failures
    type: f4
  - id: tag_text_size
    type: f4
  - id: ds_symbol_mode
    type: s1

types:
  class787:
    seq:
      - id: reserved
        type: s4
      - id: string0
        type: common::string
      - id: num_list1
        type: s4
      - id: list1
        type: s4
        repeat: expr
        repeat-expr: num_list1
  class522:
    seq:
      - id: server_name
        type: common::string
      - id: server_address
        type: common::string
      - id: password
        type: common::string
      - id: port
        type: s4
  class537:
    seq:
      - id: string0
        type: common::string
      - id: int0
        type: s4
      - id: vector30
        type: common::vector3
