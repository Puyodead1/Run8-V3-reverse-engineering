using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrain
{
    public static class Ter
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
    }
}
