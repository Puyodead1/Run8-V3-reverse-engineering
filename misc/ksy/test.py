import pprint

from kaitaistruct import KaitaiStream

from lib_run8.kaitai.key_settings import KeySettings
from lib_run8.kaitai.settings import Settings
from lib_run8.kaitai.stars4 import Stars4
from lib_run8.kaitai.tile_scenery import TileScenery
from lib_run8.kaitai.tr2 import Tr2

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
#     for i, v in enumerate(data.strings):
#         print(i, v.value)
#     stream.close()


# with open("./samples/V3/Run8KeySettings.r8", "rb") as f:
#     stream = KaitaiStream(f)
#     data = KeySettings(stream)
#     for i in data.settings:
#         print(i)
#     stream.close()

with open("./samples/V3/Run8Settings.r8", "rb") as f:
    stream = KaitaiStream(f)
    data = Settings(stream)
    print(data.host_message.value)
    stream.close()
