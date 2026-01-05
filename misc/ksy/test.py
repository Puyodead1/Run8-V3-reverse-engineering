import json

from kaitaistruct import KaitaiStream
from lib_run8.kaitai.ai_signal_database import AiSignalDatabase
from lib_run8.kaitai.ai_special_locations import AiSpecialLocations
from lib_run8.kaitai.ai_track_speed_database import AiTrackSpeedDatabase
from lib_run8.kaitai.key_settings import KeySettings
from lib_run8.kaitai.model import Run8Model
from lib_run8.kaitai.service_area_database import ServiceAreaDatabase
from lib_run8.kaitai.settings import Settings
from lib_run8.kaitai.stars4 import Stars4
from lib_run8.kaitai.terrain_tr2 import TerrainTr2
from lib_run8.kaitai.tile_scenery import TileScenery
from lib_run8.kaitai.traffic import Traffic
from lib_run8.visualizer import KaitaiStructVisualizer

# f = open("./samples/V2/00000_00037.rn8", "rb")
# stream = KaitaiStream(f)
# data = TileScenery(stream)
# for asset in data.assets:
#     print(asset.model_name.value)
# stream.close()


# f = open("./samples/V2/00000_00035.tr2", "rb")
# stream = KaitaiStream(f)
# data = Tr2(stream)
# print(data.texture_1.value)
# stream.close()


# with open("./samples/V3/stars4.rn8", "rb") as f:
#     stream = KaitaiStream(f)
#     data = Stars4(stream)
#     tree = KaitaiStructVisualizer.obj_to_h(data)
#     for i in range(len(tree["strings"])):
#         print(i)
#         o = tree["strings"][i]
#         o["index"] = i
#     with open("./samples/output/stars4.json", "w") as f:
#         f.write(json.dumps(tree, indent=4))
#     print(json.dumps(tree, indent=4))
#     stream.close()


# with open("./samples/V3/Run8KeySettings.r8", "rb") as f:
#     stream = KaitaiStream(f)
#     data = KeySettings(stream)
#     tree = KaitaiStructVisualizer.obj_to_h(data)
#     with open("./samples/output/Run8KeySettings.json", "w") as f:
#         f.write(json.dumps(tree, indent=4))
#     print(json.dumps(tree, indent=4))
#     stream.close()

# with open("./samples/V3/Run8Settings.r8", "rb") as f:
#     stream = KaitaiStream(f)
#     data = Settings(stream)
#     tree = KaitaiStructVisualizer.obj_to_h(data)
#     # with open("./samples/output/Run8Settings.json", "w") as f:
#     #     f.write(json.dumps(tree, indent=4))
#     print(json.dumps(tree, indent=4))
#     stream.close()


# with open("./samples/V3/ServiceAreaDatabase.r8", "rb") as f:
#     stream = KaitaiStream(f)
#     data = ServiceAreaDatabase(stream)
#     tree = KaitaiStructVisualizer.obj_to_h(data)
#     # with open("./samples/output/ServiceAreaDatabase.json", "w") as f:
#     #     f.write(json.dumps(tree, indent=4))
#     print(json.dumps(tree, indent=4))
#     stream.close()


# with open("./samples/V3/AISignalDatabase.r8", "rb") as f:
#     stream = KaitaiStream(f)
#     data = AiSignalDatabase(stream)
#     tree = KaitaiStructVisualizer.obj_to_h(data)
#     # with open("./samples/output/ServiceAreaDatabase.json", "w") as f:
#     #     f.write(json.dumps(tree, indent=4))
#     print(json.dumps(tree, indent=4))
#     stream.close()

# with open("./samples/V3/AITrackSpeedDatabase.r8", "rb") as f:
#     stream = KaitaiStream(f)
#     data = AiTrackSpeedDatabase(stream)
#     tree = KaitaiStructVisualizer.obj_to_h(data)
#     # with open("./samples/output/ServiceAreaDatabase.json", "w") as f:
#     #     f.write(json.dumps(tree, indent=4))
#     print(json.dumps(tree, indent=4))
#     stream.close()


# with open("./samples/V3/AISpecialLocations.r8", "rb") as f:
#     stream = KaitaiStream(f)
#     data = AiSpecialLocations(stream)
#     tree = KaitaiStructVisualizer.obj_to_h(data)
#     # with open("./samples/output/ServiceAreaDatabase.json", "w") as f:
#     #     f.write(json.dumps(tree, indent=4))
#     print(json.dumps(tree, indent=4))
#     stream.close()


# with open("./samples/V3/Traffic.r8", "rb") as f:
#     stream = KaitaiStream(f)
#     data = Traffic(stream)
#     tree = KaitaiStructVisualizer.obj_to_h(data)
#     # with open("./samples/output/ServiceAreaDatabase.json", "w") as f:
#     #     f.write(json.dumps(tree, indent=4))
#     print(json.dumps(tree, indent=4))
#     stream.close()


with open("./samples/V3/R8_MP15DC_GMTX01.rn8", "rb") as f:
    stream = KaitaiStream(f)
    data = Run8Model(stream)
    tree = KaitaiStructVisualizer.obj_to_h(data)
    # with open("./samples/output/ServiceAreaDatabase.json", "w") as f:
    #     f.write(json.dumps(tree, indent=4))
    print(json.dumps(tree, indent=4))
    stream.close()
