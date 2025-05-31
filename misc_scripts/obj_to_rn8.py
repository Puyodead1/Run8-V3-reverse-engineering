from io import BytesIO

from binwriter import BinaryWriter

positions = []
normals = []
uvs = []
vertices = []

# First pass: read data
with open("./test.obj", "r") as f:
    for line in f.readlines():
        line = line.strip()
        if not line or line.startswith("#"):
            continue

        parts = line.split()
        typ = parts[0]

        if typ == "v":
            # Read positions - note these are larger values than your working model
            x, y, z = map(float, parts[1:])
            # Scale down the positions to match working model scale
            positions.append((x, y, z))
        elif typ == "vt":
            u, v = map(float, parts[1:])
            uvs.append((u, v))
        elif typ == "vn":
            x, y, z = map(float, parts[1:])
            normals.append((x, y, z))
        elif typ == "f":
            # Process face indices
            for vert in parts[1:]:
                v_idx, t_idx, n_idx = map(lambda x: int(x) - 1, vert.split("/"))
                vertex = {"position": positions[v_idx], "normal": normals[n_idx], "uv": uvs[t_idx]}
                vertices.append(vertex)

# Now write the file
buf = BytesIO()
writer = BinaryWriter(buf)

# Write vertex count
writer.write_int32(len(vertices) * 7)

# Write each vertex
for i, vertex in enumerate(vertices):
    pos = vertex["position"]
    norm = vertex["normal"]
    uv = vertex["uv"]

    writer.write_float(0.0)  # unused
    writer.write_float(pos[0])  # pos x - already at correct scale
    writer.write_float(norm[1])  # normal y
    writer.write_float(pos[2])  # pos z
    writer.write_float(uv[0] * 4.8)  # uv x
    writer.write_float(norm[0])  # normal x
    writer.write_float(0.0)  # unused
    writer.write_float(norm[2])  # normal z
    writer.write_float(uv[1] * 9.6)  # uv y
    writer.write_float(pos[1])  # pos y

# Write texture count (-6 for testing)
writer.write_int32(-6)

# Write index buffer format (32-bit)
writer.write_bool(False)

# Write indices - just sequential since we expanded vertices
index_count = len(vertices)
writer.write_int32(index_count)
for i in range(index_count):
    writer.write_int32(i)

# Write definition count
writer.write_int32(9)

with open("./R8_Caboose_c509_SP01.rn8", "wb") as f:
    f.write(buf.getvalue())
