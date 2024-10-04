using LibRun8.Common;

namespace LibRun8.Formats
{
    public class TileScenery : FileFormat
    {
        public List<SceneryAssetLoader> LoadList = new List<SceneryAssetLoader>();

        public static TileScenery Read(string path)
        {
            TileScenery item = new TileScenery();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    // this is a copy of the LoadSceneryItems method from Tr4Loader.cs
                    int count = reader.ReadInt32();

                    for (int i = 0; i < count; i++)
                    {
                        SceneryAssetLoader sceneryAssetLoader = new SceneryAssetLoader
                        {
                            DecalLoadList = new List<DecalLoader>()
                        };

                        int decalCount = reader.ReadInt32();

                        for (int j = 0; j < decalCount; j++)
                        {
                            DecalLoader decalLoader = new DecalLoader
                            {
                                ColorRGB = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
                            };

                            int digitCount = reader.ReadInt32();
                            for (int k = 0; k < digitCount; k++)
                            {
                                decalLoader.Digits.Add(reader.ReadInt32());
                            }

                            decalLoader.OffsetXYZ = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                            decalLoader.RotationDegXYZ = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                            decalLoader.Size = reader.ReadSingle();
                            decalLoader.TextureName = reader.ReadString();
                            sceneryAssetLoader.DecalLoadList.Add(decalLoader);
                        }

                        sceneryAssetLoader.DisregardBoundingTest = reader.ReadBoolean();
                        sceneryAssetLoader.ModelName = reader.ReadString();
                        sceneryAssetLoader.PositionXYZ = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        sceneryAssetLoader.RotationXYZ = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        sceneryAssetLoader.Scale = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        sceneryAssetLoader.TileXZ = new TileIndex(reader.ReadInt32(), reader.ReadInt32());
                        item.LoadList.Add(sceneryAssetLoader);
                    }
                }
            }

            return item;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }
    }
}
