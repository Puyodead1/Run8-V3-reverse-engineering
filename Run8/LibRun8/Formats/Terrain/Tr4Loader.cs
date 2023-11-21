using LibRun8.Utils;
using System.IO.Compression;

namespace LibRun8.Formats.Terrain
{
    public class Tr4Loader
    {
        public static void LoadTile(TerrainTileLoadData terrainTileLoadData, bool bool0)
        {
            using (FileStream fileStream = new FileStream(terrainTileLoadData.filePath, FileMode.Open, FileAccess.Read))
            {
                using (DeflateStream deflateStream = new DeflateStream(fileStream, CompressionMode.Decompress))
                {
                    using (BinaryReader binaryReader = new BinaryReader(deflateStream))
                    {
                        LoadTexturesAndTerrain(binaryReader, terrainTileLoadData, bool0);
                        //LoadSceneryItems(binaryReader, terrainTileLoadData);
                        //LoadVegetation(binaryReader, terrainTileLoadData);
                        //LoadNothing(binaryReader, terrainTileLoadData);
                        //LoadWeightMap(binaryReader, terrainTileLoadData);
                    }
                }
            }
        }

        public static void LoadTexturesAndTerrain(BinaryReader binaryReader, TerrainTileLoadData terrainTileLoadData, bool bool0)
        {
            TileUtil.LoadTextures(binaryReader, terrainTileLoadData, bool0);
            Tr2Loader.LoadChunks(binaryReader, terrainTileLoadData);
            //terrainTileLoadData.tile.CopyToVertexBuffers();
        }
    }
}
