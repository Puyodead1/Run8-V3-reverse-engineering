using LibRun8.Common;

namespace LibRun8.Formats
{
    public class BlockDetectorDatabase : FileFormat
    {
        public List<BlockDetector> Detectors { get; set; } = new List<BlockDetector>();
        public static BlockDetectorDatabase Read(string path)
        {
            BlockDetectorDatabase blockDetectorDatabase = new BlockDetectorDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    var numOfDetectors = reader.ReadInt32();
                    for (int i = 0; i < numOfDetectors; i++)
                    {
                        BlockDetector blockDetector = BlockDetector.Read(reader);
                        blockDetectorDatabase.Detectors.Add(blockDetector);
                    }
                }
            }

            return blockDetectorDatabase;
        }

        public override void Write()
        {
            throw new NotImplementedException();
        }

        public class BlockDetector
        {
            public int Index { get; set; }
            public List<int> Tracks { get; set; } = new List<int>();
            public TileIndex TileXZ { get; set; }
            public Vector3 PositionXYZ { get; set; }

            public static BlockDetector Read(BinaryReader reader)
            {
                BlockDetector blockDetector = new BlockDetector();

                reader.ReadInt32(); // reserved
                blockDetector.Index = reader.ReadInt32();
                var numOfTracks = reader.ReadInt32();
                for (int i = 0; i < numOfTracks; i++)
                {
                    blockDetector.Tracks.Add(reader.ReadInt32());
                }
                blockDetector.TileXZ = TileIndex.Read(reader);
                blockDetector.PositionXYZ = Vector3.Read(reader);

                return blockDetector;
            }
        }
    }
}
