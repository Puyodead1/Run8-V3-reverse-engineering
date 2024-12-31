# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild
# type: ignore

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO
from lib_run8.kaitai import common
from enum import IntEnum


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 11):
    raise Exception("Incompatible Kaitai Struct Python API: 0.11 or later is required, but you have %s" % (kaitaistruct.__version__))

class Settings(KaitaiStruct):
    """Stores game settings. A lot of this shit appears to be unused, or is for developer crap that is stripped in release versions."""

    class AntialiasingMode(IntEnum):
        none = 0
        fxaa = 1
        smaa_x1 = 2

    class RaildriverLedMode(IntEnum):
        speed = 0
        coupler = 1
        distance = 2
        throttle = 3
        speed_limit = 4
        none = 5

    class RaindropMode(IntEnum):
        disabled = 0
        performance = 1
        quality = 2

    class ReflectionMode(IntEnum):
        low = 1
        high = 2
    def __init__(self, _io, _parent=None, _root=None):
        self._io = _io
        self._parent = _parent
        self._root = _root if _root else self
        self._read()

    def _read(self):
        self.reserved = self._io.read_s4le()
        self.float0 = self._io.read_f4le()
        self.float1 = self._io.read_f4le()
        self.float2 = self._io.read_f4le()
        self.headlight_intensity = self._io.read_f4le()
        self.bool0 = common.Common.Boolean(self._io)
        self.bool1 = common.Common.Boolean(self._io)
        self.player_name = common.Common.R8string(self._io)
        self.float4 = self._io.read_f4le()
        self.sea_sick_intensity = self._io.read_f4le()
        self.train_draw_range = self._io.read_f4le()
        self.yard_light_range = self._io.read_f4le()
        self.tile_draw_range = self._io.read_s4le()
        self.bool2 = common.Common.Boolean(self._io)
        self.vegetation_density = self._io.read_s4le()
        self.bool3 = common.Common.Boolean(self._io)
        self.enable_car_spawners = common.Common.Boolean(self._io)
        self.fade_time = self._io.read_f8le()
        self.new_messages_on_top = common.Common.Boolean(self._io)
        self.bool6 = common.Common.Boolean(self._io)
        self.radio_volume = self._io.read_f4le()
        self.bool7 = common.Common.Boolean(self._io)
        self.use_24_hour_time = common.Common.Boolean(self._io)
        self.use_dst = common.Common.Boolean(self._io)
        self.chatbox_open_sound = common.Common.Boolean(self._io)
        self.realistic_train_handling = common.Common.Boolean(self._io)
        self.text_message_color = common.Common.Color(self._io)
        self.system_message_color = common.Common.Color(self._io)
        self.render_distant_terrain = common.Common.Boolean(self._io)
        self.render_weather = common.Common.Boolean(self._io)
        self.cloud_density = self._io.read_f4le()
        self.reserved2 = self._io.read_s1()
        self.bool14 = common.Common.Boolean(self._io)
        self.flange_squeal = common.Common.Boolean(self._io)
        self.bool16 = common.Common.Boolean(self._io)
        self.smoke_effects = common.Common.Boolean(self._io)
        self.avatar_tags = common.Common.Boolean(self._io)
        self.max_fps = self._io.read_s8le()
        self.vsync = common.Common.Boolean(self._io)
        self.bool20 = common.Common.Boolean(self._io)
        self.camera_crosshairs = common.Common.Boolean(self._io)
        self.num_list0 = self._io.read_s4le()
        self.list0 = []
        for i in range(self.num_list0):
            self.list0.append(Settings.Class787(self._io, self, self._root))

        self.current_avatar = self._io.read_s1()
        self.thin_vegetation = common.Common.Boolean(self._io)
        self.rent_a_conductor = common.Common.Boolean(self._io)
        self.conductor_current_avatar = self._io.read_s1()
        self.generate_conductor_name = common.Common.Boolean(self._io)
        self.conductor_name = common.Common.R8string(self._io)
        self.mouse_look = self._io.read_s4le()
        self.mouse_move = self._io.read_f4le()
        self.int3 = self._io.read_s4le()
        self.bool25 = common.Common.Boolean(self._io)
        self.float11 = self._io.read_f4le()
        self.float12 = self._io.read_f4le()
        self.autosave_train_interval = self._io.read_s4le()
        self.autosave_train = common.Common.Boolean(self._io)
        self.int5 = self._io.read_s4le()
        self.bool27 = common.Common.Boolean(self._io)
        self.autosave_world_interval = self._io.read_s4le()
        self.int7 = self._io.read_s4le()
        self.autosave_world = common.Common.Boolean(self._io)
        self.invert_camera_y = common.Common.Boolean(self._io)
        self.raildriver_led_readout = KaitaiStream.resolve_enum(Settings.RaildriverLedMode, self._io.read_s1())
        self.use_raildriver = common.Common.Boolean(self._io)
        self.network_password = common.Common.R8string(self._io)
        self.network_port = self._io.read_s4le()
        self.max_clients = self._io.read_s4le()
        self.dispatch_password = common.Common.R8string(self._io)
        self.ai_password = common.Common.R8string(self._io)
        self.consist_editor_password = common.Common.R8string(self._io)
        self.bool31 = common.Common.Boolean(self._io)
        self.host_delete_lost_client_trains = common.Common.Boolean(self._io)
        self.network_time_sync_on = common.Common.Boolean(self._io)
        self.num_list1 = self._io.read_s4le()
        self.list1 = []
        for i in range(self.num_list1):
            self.list1.append(Settings.Class522(self._io, self, self._root))

        self.region = common.Common.R8string(self._io)
        self.scenario_filter = self._io.read_s1()
        self.scenario_name = common.Common.R8string(self._io)
        self.tdc_screen_scale = self._io.read_f8le()
        self.double2 = self._io.read_f8le()
        self.client_port = self._io.read_s4le()
        self.slow_speed_for_unit = common.Common.Boolean(self._io)
        self.slow_speed_mph = self._io.read_f4le()
        self.small_window_x = self._io.read_s4le()
        self.small_window_y = self._io.read_s4le()
        self.float14 = self._io.read_f4le()
        self.monitor_0_toggle = common.Common.Boolean(self._io)
        self.monitor_1_toggle = common.Common.Boolean(self._io)
        self.monitor_2_toggle = common.Common.Boolean(self._io)
        self.num_list2 = self._io.read_s4le()
        self.list2 = []
        for i in range(self.num_list2):
            self.list2.append(Settings.Class537(self._io, self, self._root))

        self.string9 = common.Common.CsString(self._io)
        self.bool38 = common.Common.Boolean(self._io)
        self.basic_mrao = common.Common.Boolean(self._io)
        self.airbrake_cheat_flags = self._io.read_s1()
        self.client_use_host_horns = common.Common.Boolean(self._io)
        self.ai_signal_call = self._io.read_s4le()
        self.det_audio_in_cab_only = common.Common.Boolean(self._io)
        self.use_shadows = common.Common.Boolean(self._io)
        self.shadow_vehicles = common.Common.Boolean(self._io)
        self.shadow_scenery = common.Common.Boolean(self._io)
        self.shadow_signal_heads = common.Common.Boolean(self._io)
        self.shadow_trains = common.Common.Boolean(self._io)
        self.shadow_switch_stands = common.Common.Boolean(self._io)
        self.shadow_quality = self._io.read_s4le()
        self.presentation_mode_one = common.Common.Boolean(self._io)
        self.shadow_terrain = common.Common.Boolean(self._io)
        self.shadow_update = self._io.read_f8le()
        self.shadow_sample = self._io.read_s4le()
        self.shadow_procedural_vegetation = common.Common.Boolean(self._io)
        self.cab_volume = self._io.read_f4le()
        self.train_roll = self._io.read_f4le()
        self.host_allow_clients_to_renumber = common.Common.Boolean(self._io)
        self.stringline_derailments = common.Common.Boolean(self._io)
        self.switch_derailments = common.Common.Boolean(self._io)
        self.tipover_derailments = common.Common.Boolean(self._io)
        self.allow_slippery_rails = common.Common.Boolean(self._io)
        self.parallel_updates = self._io.read_s4le()
        self.use_dof = common.Common.Boolean(self._io)
        self.realistic_alerter_time = common.Common.Boolean(self._io)
        self.master_volume = self._io.read_f4le()
        self.mouse_level_drag = self._io.read_f4le()
        self.highlight_mouse_drag = common.Common.Boolean(self._io)
        self.use_64bit_rt = common.Common.Boolean(self._io)
        self.bool60 = common.Common.Boolean(self._io)
        self.use_vignette = common.Common.Boolean(self._io)
        self.float19 = self._io.read_f4le()
        self.tone_mapping_flags = self._io.read_s1()
        self.render_precipitation = common.Common.Boolean(self._io)
        self.use_v2_braking = common.Common.Boolean(self._io)
        self.parking_brake_icon = common.Common.Boolean(self._io)
        self.bool67 = common.Common.Boolean(self._io)
        self.realistic_independent_brake = common.Common.Boolean(self._io)
        self.allow_dynamiters = common.Common.Boolean(self._io)
        self.raindrop_mode = KaitaiStream.resolve_enum(Settings.RaindropMode, self._io.read_s1())
        self.reflection_mode = KaitaiStream.resolve_enum(Settings.ReflectionMode, self._io.read_s1())
        self.realistic_dpu = common.Common.Boolean(self._io)
        self.host_message = common.Common.CsString(self._io)
        self.render_static_raindrops = common.Common.Boolean(self._io)
        self.cold_wx_airbrake_effects = common.Common.Boolean(self._io)
        self.int17 = self._io.read_s4le()
        self.int18 = self._io.read_s4le()
        self.use_custom_device = common.Common.Boolean(self._io)
        self.udp = self._io.read_s4le()
        self.use_colorband_reduction = common.Common.Boolean(self._io)
        self.antialiasing_mode = KaitaiStream.resolve_enum(Settings.AntialiasingMode, self._io.read_s1())
        self.msg_alert_sound = common.Common.Boolean(self._io)
        self.use_normal_mapping = common.Common.Boolean(self._io)
        self.shadow_mitigation = self._io.read_f4le()
        self.loco_failures = self._io.read_f4le()
        self.tag_text_size = self._io.read_f4le()
        self.ds_symbol_mode = common.Common.Boolean(self._io)

    class Class522(KaitaiStruct):
        """Probably represents a client."""
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.server_name = common.Common.R8string(self._io)
            self.server_address = common.Common.R8string(self._io)
            self.password = common.Common.R8string(self._io)
            self.port = self._io.read_s4le()


    class Class537(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.string0 = common.Common.R8string(self._io)
            self.int0 = self._io.read_s4le()
            self.vector30 = common.Common.Vector3(self._io)


    class Class787(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.reserved = self._io.read_s4le()
            self.string0 = common.Common.R8string(self._io)
            self.num_list1 = self._io.read_s4le()
            self.list1 = []
            for i in range(self.num_list1):
                self.list1.append(self._io.read_s4le())




