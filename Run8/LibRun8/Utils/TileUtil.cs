using LibRun8.Common;
using LibRun8.Formats.Terrain;

namespace LibRun8.Util
{
    public class TileUtil
    {
        public static void smethod_13(TerrainTile tile)
        {
            float chunkXSizeMeters = 33.772842f;
            float chunkYSizeMeters = 41.043285f;
            List<int> indices = new List<int>();
            int vertCount = 0;
            for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
            {
                for (int z = 0; z < Chunk.CHUNK_SIZE; z++)
                {
                    int chunkVertIndex = 0;
                    var chunk = tile.ChunkData[x, z];
                    chunk.Vertices = new VertexStruct[(chunk.Hixels * chunk.Hixels)];
                    for (int cx = 0; cx < chunk.Hixels; cx++)
                    {
                        for (int cy = 0; cy < chunk.Hixels; cy++)
                        {
                            VertexStruct vertex = new VertexStruct();
                            Vector3 position = vertex.Position;
                            Vector2 uvCoords = vertex.TextureCoordinate;

                            position.X = (x * chunkXSizeMeters) + chunkXSizeMeters / (chunk.Hixels - 1) * cx;
                            position.Z = -(z * chunkYSizeMeters + chunkYSizeMeters / (chunk.Hixels - 1) * cy);
                            position.Y = chunk.HeightMap[cx, cy];
                            vertex.Position = position;

                            uvCoords.X = vertex.Position.X / 844.3211f;
                            uvCoords.Y = -vertex.Position.Z / 1026.0822f;
                            vertex.TextureCoordinate = uvCoords;

                            chunk.Vertices[chunkVertIndex] = vertex;
                            vertCount++;
                            chunkVertIndex++;
                        }
                    }
                    SetUpChunkIndices(chunk.Hixels, vertCount, indices);
                    CalculateNormals(chunk.Vertices);
                }
            }
            ChunkStitcher.StitchChucks(tile.ChunkData);
            List<VertexStruct> vertices = new List<VertexStruct>(vertCount);
            for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
            {
                for (int z = 0; z < Chunk.CHUNK_SIZE; z++)
                {
                    for (int num5 = 0; num5 < tile.ChunkData[x, z].Vertices.Length; num5++)
                    {
                        vertices.Add(tile.ChunkData[x, z].Vertices[num5]);
                    }
                    tile.ChunkData[x, z].Vertices = null;
                }
            }
            tile.AllVerticesTemp = vertices.ToArray();
            tile.IndexBuffer = indices.ToArray();
            SetTileCenter(tile, tile.AllVerticesTemp);
        }

        public static void SetUpChunkIndices(int numHixels, int vertCounter, List<int> indices)
        {
            int num = vertCounter - numHixels * numHixels;
            for (int x = 0; x < numHixels - 1; x++)
            {
                for (int z = 0; z < numHixels - 1; z++)
                {
                    int num2 = z + x * numHixels + num;
                    int num3 = z + 1 + x * numHixels + num;
                    int num4 = z + (x + 1) * numHixels + num;
                    int num5 = z + 1 + (x + 1) * numHixels + num;
                    indices.Add(num4);
                    indices.Add(num2);
                    indices.Add(num3);
                    indices.Add(num4);
                    indices.Add(num3);
                    indices.Add(num5);
                }
            }
        }

        public static void CalculateNormals(VertexStruct[] vertices)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].Normal = Vector3.Zero;
            }
            int num = (int)Math.Sqrt(vertices.Length);
            for (int j = 0; j < num - 1; j++)
            {
                for (int k = 0; k < num - 1; k++)
                {
                    Vector3 vector = vertices[j * num + k + 1].Position - vertices[j * num + k].Position;
                    Vector3 vector2 = Vector3.Cross(vertices[(j + 1) * num + k].Position - vertices[j * num + k].Position, vector);
                    int num2 = j * num + k + 1;
                    vertices[num2].Normal = vertices[num2].Normal + vector2;
                    int num3 = j * num + k;
                    vertices[num3].Normal = vertices[num3].Normal + vector2;
                    int num4 = (j + 1) * num + k;
                    vertices[num4].Normal = vertices[num4].Normal + vector2;
                    vector = vertices[(j + 1) * num + k].Position - vertices[(j + 1) * num + (k + 1)].Position;
                    vector2 = Vector3.Cross(vertices[j * num + k + 1].Position - vertices[(j + 1) * num + (k + 1)].Position, vector);
                    int num5 = (j + 1) * num + k;
                    vertices[num5].Normal = vertices[num5].Normal + vector2;
                    int num6 = (j + 1) * num + (k + 1);
                    vertices[num6].Normal = vertices[num6].Normal + vector2;
                    int num7 = j * num + k + 1;
                    vertices[num7].Normal = vertices[num7].Normal + vector2;
                }
            }
            for (int l = 0; l < vertices.Length; l++)
            {
                vertices[l].Normal = Vector3.Normalize(vertices[l].Normal);
            }
        }

        internal static void SetTileCenter(TerrainTile tile, VertexStruct[] vertices)
        {
            tile.CenterXZ = new Vector2(422.16055f, 513.0411f);
            tile.CenterY = (float)((double)(vertices[0].Position.Y + vertices[vertices.Length - 1].Position.Y) / 2.0);
        }

        public static void LoadTextures(BinaryReader reader, TerrainTileLoadData terrainTileLoadData, bool bool0)
        {
            terrainTileLoadData.Tile.Texture0Name = reader.ReadString();
            terrainTileLoadData.Tile.Texture1Name = reader.ReadString();
            terrainTileLoadData.Tile.Texture2Name = reader.ReadString();
            terrainTileLoadData.Tile.Texture3Name = reader.ReadString();
        }
    }
}
