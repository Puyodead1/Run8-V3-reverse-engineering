from dist.run8_tilescenery import Run8Tilescenery
from dist.run8_tr2 import Run8Tr2
from kaitaistruct import KaitaiStream

# f = open("./samples/V2/00000_00037.rn8", "rb")
# stream = KaitaiStream(f)
# data = Run8Tilescenery(stream)
# for asset in data.assets:
#     print(asset.model_name.value)
# stream.close()


f = open("./samples/V2/00000_00035.tr2", "rb")
stream = KaitaiStream(f)
data = Run8Tr2(stream)
print(data.texture_1.value)
stream.close()
