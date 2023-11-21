class Vector2:
    def __init__(self, x: float, y: float) -> None:
        self.x = x
        self.y = y

    def __str__(self) -> str:
        return f"Vector2({self.x}, {self.y})"


class Vector3:
    def __init__(self, x: float, y: float, z: float) -> None:
        self.x = x
        self.y = y
        self.z = z

    def __str__(self) -> str:
        return f"Vector3({self.x}, {self.y}, {self.z})"


class Vector4:
    def __init__(self, x: float, y: float, z: float, w: float) -> None:
        self.x = x
        self.y = y
        self.z = z
        self.w = w

    def __str__(self) -> str:
        return f"Vector4({self.x}, {self.y}, {self.z}, {self.w})"
