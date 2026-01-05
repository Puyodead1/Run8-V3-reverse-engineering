def float_to_byte_rgb(float_rgb):
    """
    Converts a float RGB tuple (0.0-1.0 range) to a byte RGB tuple (0-255 range).
    """
    # Multiply by 255 and clamp the values to ensure they are within the 0-255 range
    r = int(min(max(float_rgb[0] * 255, 0), 255))
    g = int(min(max(float_rgb[1] * 255, 0), 255))
    b = int(min(max(float_rgb[2] * 255, 0), 255))
    return (r, g, b)
    
float_color = (1, 0.70588, 0.23529)
byte_color = float_to_byte_rgb(float_color)
print(f"Float RGB: {float_color}")
print(f"Byte RGB: {byte_color}")