doc: Stores game settings. A lot of this shit appears to be unused, or is for developer crap that is stripped in release versions.
meta:
  id: settings
  title: Settings (Run8Settings.r8)
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
    type: common::boolean
  - id: bool1
    type: common::boolean
  - id: player_name
    type: common::r8string
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
    type: common::boolean
  - id: vegetation_density
    type: s4
  - id: bool3
    type: common::boolean
  - id: enable_car_spawners
    type: common::boolean
  - id: fade_time
    type: f8
  - id: new_messages_on_top
    type: common::boolean
  - id: bool6
    type: common::boolean
  - id: radio_volume
    type: f4
  - id: bool7
    type: common::boolean
  - id: use_24_hour_time
    type: common::boolean
  - id: use_dst
    type: common::boolean
  - id: chatbox_open_sound
    type: common::boolean
    doc: d:Play sound when chatbox is opened
  - id: realistic_train_handling
    type: common::boolean
  - id: text_message_color
    type: common::color
  - id: system_message_color
    type: common::color
  - id: render_distant_terrain
    type: common::boolean
  - id: render_weather
    type: common::boolean
  - id: cloud_density
    type: f4
    doc: d:Clamped to 0.6-1.0
  - id: reserved2
    type: s1
  - id: bool14
    type: common::boolean
  - id: flange_squeal
    type: common::boolean
    doc: d:Enable Flange Squeal Sound
  - id: bool16
    type: common::boolean
  - id: smoke_effects
    type: common::boolean
  - id: avatar_tags
    type: common::boolean
  - id: max_fps
    type: s8
  - id: vsync
    type: common::boolean
  - id: bool20
    type: common::boolean
  - id: camera_crosshairs
    type: common::boolean
  - id: num_list0
    type: s4
  - id: list0
    type: class787
    repeat: expr
    repeat-expr: num_list0
  - id: current_avatar
    type: s1
  - id: thin_vegetation
    type: common::boolean
  - id: rent_a_conductor
    type: common::boolean
  - id: conductor_current_avatar
    type: s1
  - id: generate_conductor_name
    type: common::boolean
  - id: conductor_name
    type: common::r8string
  - id: mouse_look
    type: s4
  - id: mouse_move
    type: f4
  - id: int3
    type: s4
  - id: bool25
    type: common::boolean
  - id: float11
    type: f4
  - id: float12
    type: f4
  - id: autosave_train_interval
    type: s4
    doc: d:in minutes
  - id: autosave_train
    type: common::boolean
  - id: int5
    type: s4
  - id: bool27
    type: common::boolean
  - id: autosave_world_interval
    type: s4
    doc: d:in minutes
  - id: int7
    type: s4
  - id: autosave_world
    type: common::boolean
  - id: invert_camera_y
    type: common::boolean
  - id: raildriver_led_readout
    type: s1
    enum: raildriver_led_mode
    doc: n:RailDriver LED Mode
  - id: use_raildriver
    type: common::boolean
  - id: network_password
    type: common::r8string
  - id: network_port
    type: s4
  - id: max_clients
    type: s4
  - id: dispatch_password
    type: common::r8string
  - id: ai_password
    type: common::r8string
  - id: consist_editor_password
    type: common::r8string
  - id: bool31
    type: common::boolean
  - id: host_delete_lost_client_trains
    type: common::boolean
  - id: network_time_sync_on
    type: common::boolean
  - id: num_list1
    type: s4
  - id: list1
    type: class522
    repeat: expr
    repeat-expr: num_list1
  - id: region
    type: common::r8string
  - id: scenario_filter
    type: s1
  - id: scenario_name
    type: common::r8string
  - id: tdc_screen_scale
    type: f8
  - id: double2
    type: f8
  - id: client_port
    type: s4
  - id: slow_speed_for_unit
    type: common::boolean
  - id: slow_speed_mph
    type: f4
  - id: small_window_x
    type: s4
  - id: small_window_y
    type: s4
  - id: float14
    type: f4
  - id: monitor_0_toggle
    type: common::boolean
  - id: monitor_1_toggle
    type: common::boolean
  - id: monitor_2_toggle
    type: common::boolean
  - id: num_list2
    type: s4
  - id: list2
    type: class537
    repeat: expr
    repeat-expr: num_list2
  - id: string9
    type: common::cs_string
  - id: bool38
    type: common::boolean
  - id: basic_mrao
    type: common::boolean
  - id: airbrake_cheat_flags
    type: s1
  - id: client_use_host_horns
    type: common::boolean
  - id: ai_signal_call
    type: s4
  - id: det_audio_in_cab_only
    type: common::boolean
  - id: use_shadows
    type: common::boolean
  - id: shadow_vehicles
    type: common::boolean
  - id: shadow_scenery
    type: common::boolean
  - id: shadow_signal_heads
    type: common::boolean
  - id: shadow_trains
    type: common::boolean
  - id: shadow_switch_stands
    type: common::boolean
  - id: shadow_quality
    type: s4
  - id: presentation_mode_one
    type: common::boolean
    doc: d:True if the presentation mode is set to One
  - id: shadow_terrain
    type: common::boolean
  - id: shadow_update
    type: f8
  - id: shadow_sample
    type: s4
  - id: shadow_procedural_vegetation
    type: common::boolean
  - id: cab_volume
    type: f4
  - id: train_roll
    type: f4
  - id: host_allow_clients_to_renumber
    type: common::boolean
  - id: stringline_derailments
    type: common::boolean
  - id: switch_derailments
    type: common::boolean
  - id: tipover_derailments
    type: common::boolean
  - id: allow_slippery_rails
    type: common::boolean
  - id: parallel_updates
    type: s4
  - id: use_dof
    type: common::boolean
  - id: realistic_alerter_time
    type: common::boolean
  - id: master_volume
    type: f4
  - id: mouse_level_drag
    type: f4
  - id: highlight_mouse_drag
    type: common::boolean
  - id: use_64bit_rt
    type: common::boolean
  - id: bool60
    type: common::boolean
  - id: use_vignette
    type: common::boolean
  - id: float19
    type: f4
  - id: tone_mapping_flags
    type: s1
  - id: render_precipitation
    type: common::boolean
  - id: use_v2_braking
    type: common::boolean
  - id: parking_brake_icon
    type: common::boolean
  - id: bool67
    type: common::boolean
  - id: realistic_independent_brake
    type: common::boolean
  - id: allow_dynamiters
    type: common::boolean
  - id: raindrop_mode
    type: s1
    enum: raindrop_mode
  - id: reflection_mode
    type: s1
    enum: reflection_mode
  - id: realistic_dpu
    type: common::boolean
  - id: host_message
    type: common::cs_string
  - id: render_static_raindrops
    type: common::boolean
  - id: cold_wx_airbrake_effects
    type: common::boolean
  - id: int17
    type: s4
  - id: int18
    type: s4
  - id: use_custom_device
    type: common::boolean
  - id: udp
    type: s4
  - id: use_colorband_reduction
    type: common::boolean
  - id: antialiasing_mode
    type: s1
    enum: antialiasing_mode
  - id: msg_alert_sound
    type: common::boolean
  - id: use_normal_mapping
    type: common::boolean
  - id: shadow_mitigation
    type: f4
  - id: loco_failures
    type: f4
  - id: tag_text_size
    type: f4
  - id: ds_symbol_mode
    type: common::boolean

types:
  class787:
    seq:
      - id: reserved
        type: s4
      - id: string0
        type: common::r8string
      - id: num_list1
        type: s4
      - id: list1
        type: s4
        repeat: expr
        repeat-expr: num_list1
  class522:
    doc: Probably represents a client
    seq:
      - id: server_name
        type: common::r8string
      - id: server_address
        type: common::r8string
      - id: password
        type: common::r8string
      - id: port
        type: s4
  class537:
    seq:
      - id: string0
        type: common::r8string
      - id: int0
        type: s4
      - id: vector30
        type: common::vector3
enums:
  raildriver_led_mode:
    0: speed
    1: coupler
    2: distance
    3: throttle
    4: speed_limit
    5: none
  antialiasing_mode:
    0: none
    1: fxaa
    2: smaa_x1
  raindrop_mode:
    0: disabled
    1: performance
    2: quality
  reflection_mode:
    1: low
    2: high
