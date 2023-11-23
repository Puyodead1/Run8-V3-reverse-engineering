using LibRun8.Utils;

namespace LibRun8.Formats.Terrain
{
    public class Tr2Loader
    {
        public static void LoadTile(TerrainTileLoadData terrainTileLoadData)
        {
            LoadTile(terrainTileLoadData, false);
        }

        public static void LoadTile(TerrainTileLoadData terrainTileLoadData, bool bool0)
        {
            using (FileStream fileStream = new FileStream(terrainTileLoadData.FilePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    TileUtil.LoadTextures(binaryReader, terrainTileLoadData, bool0);
                    LoadChunks(binaryReader, terrainTileLoadData);
                }
            }
        }

        public static void LoadChunks(BinaryReader reader, TerrainTileLoadData terrainTileLoadData)
        {
            terrainTileLoadData.Tile.DetailLevel = 99;
            terrainTileLoadData.Tile.ChunkData = new Chunk[25, 25];
            //int num = 0;
            for (byte x = 0; x < 25; x++)
            {
                for (byte z = 0; z < 25; z++)
                {
                    Chunk chunk = new Chunk();
                    chunk.hixels = (short)reader.ReadInt32();
                    chunk.heightMap = new float[chunk.hixels, chunk.hixels];
                    chunk.cx = x;
                    chunk.cz = z;

                    for (int i = 0; i < chunk.hixels; i++)
                    {
                        for (int j = 0; j < chunk.hixels; j++)
                        {
                            float elevation = reader.ReadSingle();
                            chunk.heightMap[i, j] = elevation;
                            //if(elevation <= heightOffset + 0.2f)
                            //{
                            //    num++;
                            //}
                        }
                    }

                    terrainTileLoadData.Tile.ChunkData[x, z] = chunk;
                }
            }

            //unknown0 = region == "SouthernCA" && num > 100;

            try
            {
                terrainTileLoadData.Tile.LonEast = reader.ReadSingle();
                terrainTileLoadData.Tile.LonWest = reader.ReadSingle();
                terrainTileLoadData.Tile.LatNorth = reader.ReadSingle();
                terrainTileLoadData.Tile.LatSouth = reader.ReadSingle();
                terrainTileLoadData.Tile.ProcVeg = reader.ReadString();
            }
            catch { }

            //TileUtil.smethod_13(this);
        }

        public void Write(string path)
        {
            throw new NotImplementedException();
        }
    }
}
