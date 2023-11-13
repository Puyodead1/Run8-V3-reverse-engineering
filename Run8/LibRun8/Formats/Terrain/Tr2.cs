using LibRun8.Utils;
using System.IO;

namespace LibRun8.Formats.Terrain
{
    public class Tr2 : TerrainTile
    {
        public Tr2(string path, string region, float heightOffset, BinaryReader reader)
        {
            this.region = region;
            this.reader = reader;
            this.heightOffset = heightOffset;

            LoadTextures();
            LoadChunks();
        }

        private void LoadChunks()
        {
            detailLevel = 99;
            chunkData = new Chunk[25, 25];
            int num = 0;
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
                            if(elevation <= heightOffset + 0.2f)
                            {
                                num++;
                            }
                        }
                    }

                    chunkData[x, z] = chunk;
                }
            }

            unknown0 = region == "SouthernCA" && num > 100;

            try
            {
                lonEast = reader.ReadSingle();
                lonWest = reader.ReadSingle();
                latNorth = reader.ReadSingle();
                latSouth = reader.ReadSingle();
                unknown1 = reader.ReadString();
            }
            catch { }

            TileUtil.smethod_13(this);
        }

        public void Write(string path)
        {
            throw new NotImplementedException();
        }
    }
}
