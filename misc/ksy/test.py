import json
import pprint

from kaitaistruct import KaitaiStream

from lib_run8.kaitai.key_settings import KeySettings
from lib_run8.kaitai.settings import Settings
from lib_run8.kaitai.stars4 import Stars4
from lib_run8.kaitai.tile_scenery import TileScenery
from lib_run8.kaitai.tr2 import Tr2
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
#     with open("./samples/output/stars4.json", "w") as f:
#         f.write(json.dumps(tree, indent=4))
#     pprint.pp(tree)
#     stream.close()


# with open("./samples/V3/Run8KeySettings.r8", "rb") as f:
#     stream = KaitaiStream(f)
#     data = KeySettings(stream)
#     tree = KaitaiStructVisualizer.obj_to_h(data)
#     with open("./samples/output/Run8KeySettings.json", "w") as f:
#         f.write(json.dumps(tree, indent=4))
#     pprint.pp(tree)
#     stream.close()

with open("./samples/V3/Run8Settings.r8", "rb") as f:
    stream = KaitaiStream(f)
    data = Settings(stream)
    tree = KaitaiStructVisualizer.obj_to_h(data)
    # with open("./samples/output/Run8Settings.json", "w") as f:
    #     f.write(json.dumps(tree, indent=4))
    pprint.pp(tree)
    stream.close()
