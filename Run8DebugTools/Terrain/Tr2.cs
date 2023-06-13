using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrain
{
    public static class Tr2
    {
        public static void Read(TerrainTileChunk terrainTile)
        {
            using (FileStream fileStream = new FileStream(terrainTile.filePath.Replace(".tr2", ".tr4"), FileMode.Open, FileAccess.Read))
            {
                using (DeflateStream deflateStream = new DeflateStream(fileStream, CompressionMode.Decompress))
                {
                    using (BinaryReader binaryReader = new BinaryReader(deflateStream))
                    {
                        Utils.ReadTextures(binaryReader, terrainTile);



                        //if (writeObj) Write(class846, objPath);
                    }
                }
            }
        }

        internal static void ReadTile(BinaryReader binaryReader, TerrainTileChunk terrainTile)
        {
            terrainTile.terrainTile2.int_0 = 99;
            terrainTile.terrainTile2.tiles = new TerrainTile[25, 25];
            int num = 0;
            for (byte x = 0; x < 25; x += 1)
            {
                for (byte y = 0; y < 25; y += 1)
                {
                    terrainTile.terrainTile2.tiles[(int)x, (int)y] = new TerrainTile();
                    terrainTile.terrainTile2.tiles[(int)x, (int)y].short_0 = (short)binaryReader.ReadInt32();
                    terrainTile.terrainTile2.tiles[(int)x, (int)y].vertexPositionNormalTexturePositions = new float[(int)terrainTile.terrainTile2.tiles[(int)x, (int)y].short_0, (int)terrainTile.terrainTile2.tiles[(int)x, (int)y].short_0];
                    terrainTile.terrainTile2.tiles[(int)x, (int)y].x = x;
                    terrainTile.terrainTile2.tiles[(int)x, (int)y].y = y;
                    for (int i = 0; i < (int)terrainTile.terrainTile2.tiles[(int)x, (int)y].short_0; i++)
                    {
                        for (int j = 0; j < (int)terrainTile.terrainTile2.tiles[(int)x, (int)y].short_0; j++)
                        {
                            terrainTile.terrainTile2.tiles[(int)x, (int)y].vertexPositionNormalTexturePositions[i, j] = binaryReader.ReadSingle();
                            if (terrainTile.terrainTile2.tiles[(int)x, (int)y].vertexPositionNormalTexturePositions[i, j] <= -2f + 0.2f)
                            {
                                num++;
                            }
                        }
                    }
                }
            }
            terrainTile.terrainTile2.bool_0 = true && num > 100;
            try
            {
                terrainTile.terrainTile2.float_2 = binaryReader.ReadSingle();
                terrainTile.terrainTile2.float_3 = binaryReader.ReadSingle();
                terrainTile.terrainTile2.float_4 = binaryReader.ReadSingle();
                terrainTile.terrainTile2.float_5 = binaryReader.ReadSingle();
                terrainTile.terrainTile2.texture6 = binaryReader.ReadString();
            }
            catch (Exception)
            {
            }

            Utils.AdjustTile(terrainTile.terrainTile2);
        }
    }
}
