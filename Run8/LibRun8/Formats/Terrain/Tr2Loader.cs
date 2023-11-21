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
            using (FileStream fileStream = new FileStream(terrainTileLoadData.filePath, FileMode.Open, FileAccess.Read))
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
            terrainTileLoadData.tile.detailLevel = 99;
            terrainTileLoadData.tile.chunkData = new Chunk[25, 25];
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

                    terrainTileLoadData.tile.chunkData[x, z] = chunk;
                }
            }

            //unknown0 = region == "SouthernCA" && num > 100;

            try
            {
                terrainTileLoadData.tile.lonEast = reader.ReadSingle();
                terrainTileLoadData.tile.lonWest = reader.ReadSingle();
                terrainTileLoadData.tile.latNorth = reader.ReadSingle();
                terrainTileLoadData.tile.latSouth = reader.ReadSingle();
                terrainTileLoadData.tile.procVeg = reader.ReadString();
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
