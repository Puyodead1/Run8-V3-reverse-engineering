import typing
from System import MarshalByRefObject

class ObjectHandle(MarshalByRefObject):
    def __init__(self, o: typing.Any) -> None: ...
    def Unwrap(self) -> typing.Any: ...

