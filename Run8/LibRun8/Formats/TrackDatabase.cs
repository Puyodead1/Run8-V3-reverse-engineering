using LibRun8.Common;
using SharpDX;

namespace LibRun8.Formats
{
    public class TrackDatabase : FileFormat
    {
        public TrackSection[] sections { get; set; }

        public static TrackDatabase Read(string path)
        {
            TrackDatabase database = new TrackDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    binaryReader.ReadInt32(); // reserved
                    int sectionCount = binaryReader.ReadInt32();
                    database.sections = new TrackSection[sectionCount];

                    for(int i = 0; i < sectionCount; i++)
                    {
                        TrackSection section = new TrackSection();
                        section.Read(binaryReader);
                        database.sections[i] = section;
                    }
                }
            }

            return database;
        }

        public override void Write()
        {
            throw new NotImplementedException();
        }

        public class TrackSection
        {
            public TrackNode[] nodes { get; set; }
            public int id { get; set; }
            public bool bool0 { get; set; }
            public int[] sectionIds { get; set; }
            public byte byte0 { get; set; }
            public double double0 { get; set; }
            public bool bool1 { get; set; }
            public bool bool2 { get; set; }
            public int int1 { get; set; }
            public bool bool3 { get; set; }
            public bool isTurntable { get; set; } = false;
            public bool isTransferTable { get; set; } = false;

            public void Read(BinaryReader binaryReader)
            {
                binaryReader.ReadInt32(); // reserved

                int nodeCount = binaryReader.ReadInt32();
                nodes = new TrackNode[nodeCount];

                for(int i = 0; i < nodeCount; i++)
                {
                    nodes[i] = new TrackNode();
                    nodes[i].Read(binaryReader);
                }

                id = binaryReader.ReadInt32();
                bool0 = binaryReader.ReadBoolean();

                int idCount = binaryReader.ReadInt32();
                sectionIds = new int[idCount];
                for(int i = 0; i <  idCount; i++)
                {
                    sectionIds[i] = binaryReader.ReadInt32();
                }

                byte0 = binaryReader.ReadByte();
                double0 = binaryReader.ReadDouble();
                bool1 = binaryReader.ReadBoolean();
                bool2 = binaryReader.ReadBoolean();
                id = binaryReader.ReadInt32();
                bool3 = binaryReader.ReadBoolean();
            }
        }

        public class TrackNode
        {
            public TileIndex tileIndex { get; set; }
            public Vector3 vector0 { get; set; }
            public Vector3 vector1 { get; set; }
            public Vector3 vector2 { get; set; }
            public int id { get; set; }
            public bool bool0 { get; set; }
            public bool bool1 { get; set; }
            public float float0 { get; set; }
            public int int1 { get; set; }
            public float float1 { get; set; }
            public float float2 { get; set; }
            public int int2 { get; set; }
            public int sectionId { get; set; }
            public bool bool2 { get; set; }

            public void Read(BinaryReader binaryReader)
            {
                binaryReader.ReadInt32(); // reserved

                tileIndex = new TileIndex(binaryReader.ReadInt32(), binaryReader.ReadInt32());
                vector0 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                vector1 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                vector2 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());

                id = binaryReader.ReadInt32();
                bool0 = binaryReader.ReadBoolean();
                bool1 = binaryReader.ReadBoolean();
                float0 = binaryReader.ReadSingle();
                int1 = binaryReader.ReadInt32();
                float1 = binaryReader.ReadSingle();
                float2 = binaryReader.ReadSingle();
                int2 = binaryReader.ReadInt32();
                sectionId = binaryReader.ReadInt32();
                bool2 = binaryReader.ReadBoolean();
            }
        }
    }
}
