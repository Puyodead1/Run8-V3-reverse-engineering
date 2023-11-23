using LibRun8.Common;
using LibRun8.Utils;
using System.IO.Compression;

namespace LibRun8.Formats.Terrain
{
    public class Tr4Loader
    {
        public static void LoadTile(TerrainTileLoadData terrainTileLoadData, bool bool0)
        {
            using (FileStream fileStream = new FileStream(terrainTileLoadData.FilePath, FileMode.Open, FileAccess.Read))
            {
                using (DeflateStream deflateStream = new DeflateStream(fileStream, CompressionMode.Decompress))
                {
                    using (BinaryReader binaryReader = new BinaryReader(deflateStream))
                    {
                        LoadTexturesAndTerrain(binaryReader, terrainTileLoadData, bool0);
                        LoadSceneryItems(binaryReader, terrainTileLoadData);
                        LoadVegetation(binaryReader, terrainTileLoadData);
                        LoadNothing(binaryReader, terrainTileLoadData);
                        LoadWeightMap(binaryReader, terrainTileLoadData);
                    }
                }
            }
        }

        private static void LoadTexturesAndTerrain(BinaryReader binaryReader, TerrainTileLoadData terrainTileLoadData, bool bool0)
        {
            TileUtil.LoadTextures(binaryReader, terrainTileLoadData, bool0);
            Tr2Loader.LoadChunks(binaryReader, terrainTileLoadData);
            //terrainTileLoadData.tile.CopyToVertexBuffers();
        }

        private static void LoadSceneryItems(BinaryReader binaryReader, TerrainTileLoadData terrainTileLoadData)
        {
            int count = binaryReader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                SceneryAssetLoader sceneryAssetLoader = new SceneryAssetLoader
                {
                    DecalLoadList = new List<DecalLoader>()
                };

                int decalCount = binaryReader.ReadInt32();

                for (int j = 0; j < decalCount; j++)
                {
                    DecalLoader decalLoader = new DecalLoader
                    {
                        ColorRGB = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
                    };

                    int digitCount = binaryReader.ReadInt32();
                    for(int k = 0; k < digitCount; k++)
                    {
                        decalLoader.Digits.Add(binaryReader.ReadInt32());
                    }

                    decalLoader.OffsetXYZ = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                    decalLoader.RotationDegXYZ = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                    decalLoader.Size = binaryReader.ReadSingle();
                    decalLoader.TextureName = binaryReader.ReadString();
                    sceneryAssetLoader.DecalLoadList.Add(decalLoader);
                }

                sceneryAssetLoader.DisregardBoundingTest = binaryReader.ReadBoolean();
                sceneryAssetLoader.ModelName = binaryReader.ReadString();
                sceneryAssetLoader.PositionXYZ = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                sceneryAssetLoader.RotationXYZ = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                sceneryAssetLoader.Scale = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                sceneryAssetLoader.TileXZ = new TileIndex(binaryReader.ReadInt32(), binaryReader.ReadInt32());
                terrainTileLoadData.Tile.LoadList.Add(sceneryAssetLoader);
            }

            // and then it goes and loads shit
        }

        private static void LoadVegetation(BinaryReader binaryReader, TerrainTileLoadData terrainTileLoadData)
        {
            ProceduralVegetation proceduralVegetation = new ProceduralVegetation();

            List<Vector4> plants = new List<Vector4>();
            int plantCount = binaryReader.ReadInt32();
            // The game will only load a max of 100000 plants
            for (int i = 0; i < plantCount; i++)
            {
                Vector4 vector = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadByte(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                // the game will also check the plant is inside the tile
                plants.Add(vector);
            }

            proceduralVegetation.PlantCount = plants.Count;
            proceduralVegetation.Plants = plants; // the game actually passes this to VegetationLoader
            proceduralVegetation.TileXZ = terrainTileLoadData.Tile.TileXZ;
            terrainTileLoadData.Tile.Plants = proceduralVegetation;
        }
   
        private static void LoadNothing(BinaryReader binaryReader, TerrainTileLoadData terrainTileLoadData)
        {
            // fuck if I know
            int num = binaryReader.ReadInt32();
            Console.WriteLine($"Nothing: {num}");
        }

        private static void LoadWeightMap(BinaryReader binaryReader, TerrainTileLoadData terrainTileLoadData)
        {
            MemoryStream memoryStream = new MemoryStream();
            for(; ; )
            {
                int num = binaryReader.BaseStream.ReadByte();
                if(num == -1)
                {
                    break;
                }
                memoryStream.WriteByte((byte)num);
            }

            // creates a texture from that
            terrainTileLoadData.Tile.WeightMap = memoryStream.ToArray();
            memoryStream.Close();
        }
    }
}
