using LibRun8.Common;
using LibRun8.Utils;

namespace LibRun8.Formats
{
    public class RoadDatabase : FileFormat
    {
        public List<RoadSection> Sections { get; set; }
        public static RoadDatabase Read(string path)
        {
            RoadDatabase item = new RoadDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    item.Sections = new List<RoadSection>();

                    reader.ReadInt32(); // reserved
                    int numOfSections = reader.ReadInt32();
                    for (int i = 0; i < numOfSections; i++)
                    {
                        RoadSection section = RoadSection.Read(reader);
                        //foreach(RoadNode node in section.nodes)
                        //{
                        //    node.section = section;
                        //}

                        item.Sections.Add(section);
                    }
                }
            }

            return item;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class RoadSection
        {
            public List<RoadNode> Nodes { get; set; }
            public int Int0 { get; set; }
            public int Int1 { get; set; } = -1;
            public float ArcLengthMeters { get; set; } = 10f;
            public RoadExtrusionType Type { get; set; }
            public int NumLanesPerSide { get; set; }
            public float LaneCenterOffsetMeters { get; set; }
            public float LaneSpacingMeters { get; set; }

            public static RoadSection Read(BinaryReader reader)
            {
                RoadSection section = new RoadSection();
                section.Nodes = new List<RoadNode>();

                section.Int1 = reader.ReadInt32();
                int numOfNodes = reader.ReadInt32();
                for (int i = 0; i < numOfNodes; i++)
                {
                    RoadNode node = RoadNode.Read(reader);
                    section.Nodes.Add(node);
                }
                section.Int0 = reader.ReadInt32();
                reader.ReadSingle(); // reserved
                section.Type = (RoadExtrusionType)reader.ReadByte();
                section.NumLanesPerSide = reader.ReadInt32();
                section.LaneCenterOffsetMeters = reader.ReadInt32();
                section.LaneSpacingMeters = reader.ReadInt32();

                return section;
            }
        }

        public class RoadNode
        {
            public TileIndex TileXZ { get; set; }
            public Vector3 PositionXYZ { get; set; }
            public Vector3 TangentXYZ { get; set; }
            public int Index { get; set; }
            public float Float0 { get; set; }
            public int CurveSign { get; set; }
            public float Float1 { get; set; }
            public float ArcLengthMeters { get; set; }
            public int NumSegments { get; set; }
            public int Int3 { get; set; }
            public float MaxSpeedMPH { get; set; } = 55f;
            public static RoadNode Read(BinaryReader reader)
            {
                RoadNode node = new RoadNode();
                reader.ReadInt32(); // reserved
                node.TileXZ = reader.ReadTileIndex();
                node.PositionXYZ = reader.ReadVector3();
                node.TangentXYZ = reader.ReadVector3();
                reader.ReadVector3(); // reserved
                node.Index = reader.ReadInt32();
                node.Float0 = reader.ReadSingle();
                node.CurveSign = reader.ReadInt32();
                node.Float1 = reader.ReadSingle();
                node.ArcLengthMeters = reader.ReadSingle();
                node.NumSegments = reader.ReadInt32();
                node.Int3 = reader.ReadInt32();
                node.MaxSpeedMPH = reader.ReadSingle();

                return node;
            }
        }

        public enum RoadExtrusionType
        {
            const_0,
            const_1,
            const_2,
            const_3,
            const_4,
            const_5,
            const_6,
            const_7,
            const_8,
            const_9,
            const_10,
            const_11,
            const_12,
            const_13
        }
    }
}
