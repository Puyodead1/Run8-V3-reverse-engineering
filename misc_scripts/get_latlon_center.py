def calculate_bounds_center(north, south, east, west):
    center_lat = (north + south) / 2
    center_lng = (east + west) / 2
    return center_lat, center_lng


def calculate_size_bounds(north, south, east, west):
    lat_size = north - south
    lng_size = east - west
    return lat_size, lng_size


north = 40.46984100341797
east = -78.58444213867188
south = 40.460601806640625
west = -78.59439849853516

print(calculate_bounds_center(north, south, east, west))
print(calculate_size_bounds(north, south, east, west))
