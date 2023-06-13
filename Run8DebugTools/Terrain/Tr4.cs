using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrain
{
    public static class Tr4
    {
        public static void Read(TerrainTileChunk terrainTile)
        {
            using (FileStream fileStream = new FileStream(terrainTile.filePath.Replace(".tr2", ".tr4"), FileMode.Open, FileAccess.Read))
            {
                using (DeflateStream deflateStream = new DeflateStream(fileStream, CompressionMode.Decompress))
                {
                    using (BinaryReader binaryReader = new BinaryReader(deflateStream))
                    {
                        smethod_5(binaryReader, terrainTile);



                        //if (writeObj) Write(class846, objPath);
                    }
                }
            }
        }

        private static void smethod_5(BinaryReader binaryReader, TerrainTileChunk terrainTile)
        {
            Utils.ReadTextures(binaryReader, terrainTile);
            Tr2.ReadTile(binaryReader, terrainTile);
        }
    }
}
