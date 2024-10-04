# converted to python from ruby by chatgpt
# Original source: https://github.com/kaitai-io/kaitai_struct_visualizer/blob/master/lib/kaitai/struct/visualizer/obj_to_h.rb

from kaitaistruct import KaitaiStruct


class KaitaiStructVisualizer:
    @staticmethod
    def obj_to_h(obj):
        if isinstance(obj, (bool, int, float, type(None))):
            return obj
        elif isinstance(obj, str):
            try:
                obj.encode("ascii")
                return obj
            except UnicodeEncodeError:
                return " ".join(f"{ord(c):02X}" for c in obj)
        elif isinstance(obj, list):
            return [KaitaiStructVisualizer.obj_to_h(x) for x in obj]
        elif isinstance(obj, KaitaiStruct):
            root = {}
            for attr in dir(obj):
                if not attr.startswith("_") and not callable(getattr(obj, attr)):
                    value = getattr(obj, attr)
                    v = KaitaiStructVisualizer.obj_to_h(value)
                    if v is not None:
                        root[attr] = v
            return root
        else:
            print(type(obj).__name__)
            return f"OPAQUE ({type(obj).__name__})"
