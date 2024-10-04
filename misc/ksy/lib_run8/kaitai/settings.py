# This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild
# type: ignore

import kaitaistruct
from kaitaistruct import KaitaiStruct, KaitaiStream, BytesIO
from lib_run8.kaitai import common


if getattr(kaitaistruct, 'API_VERSION', (0, 9)) < (0, 11):
    raise Exception("Incompatible Kaitai Struct Python API: 0.11 or later is required, but you have %s" % (kaitaistruct.__version__))

class Settings(KaitaiStruct):
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
        self.bool0 = self._io.read_s1()
        self.bool1 = self._io.read_s1()
        self.player_name = common.Common.String(self._io)
        self.float4 = self._io.read_f4le()
        self.sea_sick_intensity = self._io.read_f4le()
        self.train_draw_range = self._io.read_f4le()
        self.yard_light_range = self._io.read_f4le()
        self.tile_draw_range = self._io.read_s4le()
        self.bool2 = self._io.read_s1()
        self.vegetation_density = self._io.read_s4le()
        self.bool3 = self._io.read_s1()
        self.enable_car_spawners = self._io.read_s1()
        self.fade_time = self._io.read_f8le()
        self.new_messages_on_top = self._io.read_s1()
        self.bool6 = self._io.read_s1()
        self.radio_volume = self._io.read_f4le()
        self.bool7 = self._io.read_s1()
        self.use_24_hour_time = self._io.read_s1()
        self.use_dst = self._io.read_s1()
        self.chatbox_open_sound = self._io.read_s1()
        self.realistic_train_handling = self._io.read_s1()
        self.text_message_color = common.Common.Color(self._io)
        self.system_message_color = common.Common.Color(self._io)
        self.render_distant_terrain = self._io.read_s1()
        self.render_weather = self._io.read_s1()
        self.cloud_density = self._io.read_f4le()
        self.reserved2 = self._io.read_s1()
        self.bool14 = self._io.read_s1()
        self.flange_squeal = self._io.read_s1()
        self.bool16 = self._io.read_s1()
        self.smoke_effects = self._io.read_s1()
        self.avatar_tags = self._io.read_s1()
        self.max_fps = self._io.read_s8le()
        self.vsync = self._io.read_s1()
        self.bool20 = self._io.read_s1()
        self.camera_crosshairs = self._io.read_s1()
        self.num_list0 = self._io.read_s4le()
        self.list0 = []
        for i in range(self.num_list0):
            self.list0.append(Settings.Class787(self._io, self, self._root))

        self.current_avatar = self._io.read_s1()
        self.thin_vegetation = self._io.read_s1()
        self.rent_a_conductor = self._io.read_s1()
        self.conductor_current_avatar = self._io.read_s1()
        self.generate_conductor_name = self._io.read_s1()
        self.conductor_name = common.Common.String(self._io)
        self.mouse_look = self._io.read_s4le()
        self.mouse_move = self._io.read_f4le()
        self.int3 = self._io.read_s4le()
        self.bool25 = self._io.read_s1()
        self.float11 = self._io.read_f4le()
        self.float12 = self._io.read_f4le()
        self.autosave_train_interval = self._io.read_s4le()
        self.autosave_train = self._io.read_s1()
        self.int5 = self._io.read_s4le()
        self.bool27 = self._io.read_s1()
        self.autosave_world_interval = self._io.read_s4le()
        self.int7 = self._io.read_s4le()
        self.autosave_world = self._io.read_s1()
        self.invert_camera_y = self._io.read_s1()
        self.raildriver_led_readout = self._io.read_s1()
        self.use_raildriver = self._io.read_s1()
        self.network_password = common.Common.String(self._io)
        self.network_port = self._io.read_s4le()
        self.max_clients = self._io.read_s4le()
        self.dispatch_password = common.Common.String(self._io)
        self.ai_password = common.Common.String(self._io)
        self.consist_editor_password = common.Common.String(self._io)
        self.bool31 = self._io.read_s1()
        self.host_delete_lost_client_trains = self._io.read_s1()
        self.network_time_sync_on = self._io.read_s1()
        self.num_list1 = self._io.read_s4le()
        self.list1 = []
        for i in range(self.num_list1):
            self.list1.append(Settings.Class522(self._io, self, self._root))

        self.region = common.Common.String(self._io)
        self.scenario_filter = self._io.read_s1()
        self.scenario_name = common.Common.String(self._io)
        self.tdc_screen_scale = self._io.read_f8le()
        self.double2 = self._io.read_f8le()
        self.client_port = self._io.read_s4le()
        self.slow_speed_for_unit = self._io.read_s1()
        self.slow_speed_mph = self._io.read_f4le()
        self.small_window_x = self._io.read_s4le()
        self.small_window_y = self._io.read_s4le()
        self.float14 = self._io.read_f4le()
        self.monitor_0_toggle = self._io.read_s1()
        self.monitor_1_toggle = self._io.read_s1()
        self.monitor_2_toggle = self._io.read_s1()
        self.num_list2 = self._io.read_s4le()
        self.list2 = []
        for i in range(self.num_list2):
            self.list2.append(Settings.Class537(self._io, self, self._root))

        self.string9 = common.Common.CsString(self._io)
        self.bool38 = self._io.read_s1()
        self.basic_mrao = self._io.read_s1()
        self.airbrake_cheat = self._io.read_s1()
        self.client_use_host_horns = self._io.read_s1()
        self.ai_signal_call = self._io.read_s4le()
        self.det_audio_in_cab_only = self._io.read_s1()
        self.use_shadows = self._io.read_s1()
        self.shadow_vehicles = self._io.read_s1()
        self.shadow_scenery = self._io.read_s1()
        self.shadow_signal_heads = self._io.read_s1()
        self.shadow_trains = self._io.read_s1()
        self.shadow_switch_stands = self._io.read_s1()
        self.shadow_quality = self._io.read_s4le()
        self.presentation_mode = self._io.read_s1()
        self.shadow_terrain = self._io.read_s1()
        self.shadow_update = self._io.read_f8le()
        self.shadow_sample = self._io.read_s4le()
        self.shadow_procedural_vegetation = self._io.read_s1()
        self.cab_volume = self._io.read_f4le()
        self.train_roll = self._io.read_f4le()
        self.host_allow_clients_to_renumber = self._io.read_s1()
        self.stringline_derailments = self._io.read_s1()
        self.switch_derailments = self._io.read_s1()
        self.tipover_derailments = self._io.read_s1()
        self.allow_slippery_rails = self._io.read_s1()
        self.parallel_updates = self._io.read_s4le()
        self.use_dof = self._io.read_s1()
        self.realistic_alerter = self._io.read_s1()
        self.master_volume = self._io.read_f4le()
        self.mouse_level_drag = self._io.read_f4le()
        self.highlight_mouse_drag = self._io.read_s1()
        self.use_64bit_rt = self._io.read_s1()
        self.bool60 = self._io.read_s1()
        self.use_vignette = self._io.read_s1()
        self.float19 = self._io.read_f4le()
        self.tone_mapping = self._io.read_s1()
        self.render_precipitation = self._io.read_s1()
        self.use_v2_braking = self._io.read_s1()
        self.parking_brake_icon = self._io.read_s1()
        self.bool67 = self._io.read_s1()
        self.realistic_independent_brake = self._io.read_s1()
        self.allow_dynamiters = self._io.read_s1()
        self.raindrop_mode = self._io.read_s1()
        self.reflection_mode = self._io.read_s1()
        self.realistic_dpu = self._io.read_s1()
        self.host_message = common.Common.CsString(self._io)
        self.render_static_raindrops = self._io.read_s1()
        self.cold_wx_airbrake_effects = self._io.read_s1()
        self.int17 = self._io.read_s4le()
        self.int18 = self._io.read_s4le()
        self.use_custom_device = self._io.read_s1()
        self.udp = self._io.read_s4le()
        self.use_colorband_reduction = self._io.read_s1()
        self.antialiasing_mode = self._io.read_s1()
        self.msg_alert_sound = self._io.read_s1()
        self.use_normal_mapping = self._io.read_s1()
        self.shadow_mitigation = self._io.read_f4le()
        self.loco_failures = self._io.read_f4le()
        self.tag_text_size = self._io.read_f4le()
        self.ds_symbol_mode = self._io.read_s1()

    class Class522(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.server_name = common.Common.String(self._io)
            self.server_address = common.Common.String(self._io)
            self.password = common.Common.String(self._io)
            self.port = self._io.read_s4le()


    class Class537(KaitaiStruct):
        def __init__(self, _io, _parent=None, _root=None):
            self._io = _io
            self._parent = _parent
            self._root = _root
            self._read()

        def _read(self):
            self.string0 = common.Common.String(self._io)
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
            self.string0 = common.Common.String(self._io)
            self.num_list1 = self._io.read_s4le()
            self.list1 = []
            for i in range(self.num_list1):
                self.list1.append(self._io.read_s4le())




