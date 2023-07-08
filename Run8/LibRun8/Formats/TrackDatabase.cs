using LibRun8.Common;

namespace LibRun8.Formats
{
    public class TrackDatabase : FileFormat
    {
        public TrackSection[] Sections { get; set; }

        public static TrackDatabase Read(string path)
        {
            TrackDatabase database = new TrackDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    int sectionCount = reader.ReadInt32();
                    database.Sections = new TrackSection[sectionCount];

                    for(int i = 0; i < sectionCount; i++)
                    {
                        TrackSection section = new TrackSection();
                        section.Read(reader);
                        database.Sections[i] = section;
                    }
                }
            }

            return database;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class TrackSection
        {
            public TrackNode[] Nodes { get; set; }
            public int Index { get; set; }
            public bool SwitchLeverPosition { get; set; }
            public int[] NextSectionIndex { get; set; }
            public byte TrackType { get; set; } // not 100% sure if this is correct
            public double RetarderMPH { get; set; }
            public bool IsOccupied { get; set; }
            public bool SwitchStandLeftSide { get; set; }
            public int SwitchStandType { get; set; }
            public bool IsCTCSwitch { get; set; }
            //public bool IsTurntable { get; set; } = false;
            //public bool IsTransferTable { get; set; } = false;

            public void Read(BinaryReader reader)
            {
                reader.ReadInt32(); // reserved

                int numNodes = reader.ReadInt32();
                Nodes = new TrackNode[numNodes];

                for(int i = 0; i < numNodes; i++)
                {
                    Nodes[i] = new TrackNode();
                    Nodes[i].Read(reader);
                }

                Index = reader.ReadInt32();
                SwitchLeverPosition = reader.ReadBoolean();

                int numSectionIndices = reader.ReadInt32();
                NextSectionIndex = new int[numSectionIndices];
                for(int i = 0; i <  numSectionIndices; i++)
                {
                    NextSectionIndex[i] = reader.ReadInt32();
                }

                TrackType = reader.ReadByte();
                RetarderMPH = reader.ReadDouble();
                IsOccupied = reader.ReadBoolean();
                SwitchStandLeftSide = reader.ReadBoolean();
                SwitchStandType = reader.ReadInt32();
                IsCTCSwitch = reader.ReadBoolean();
            }
        }

        public class TrackNode
        {
            public TileIndex TileXZ { get; set; }
            public Vector3 PositionXYZ { get; set; }
            public Vector3 TangentDegXYZ { get; set; }
            public Vector3 EndPositionXYZ { get; set; }
            public int NodeIndex { get; set; }
            public bool IsSwitchNode { get; set; }
            public bool IsReversePath { get; set; }
            public float CurvatureDeg { get; set; }
            public int CurveSign { get; set; }
            public float RadiusMeters { get; set; }
            public float ArcLengthMeters { get; set; }
            public int NumSegments { get; set; }
            public int BelongsToTrackIndex { get; set; }
            public bool IsSelected { get; set; } // could also be SoundTrigger

            public void Read(BinaryReader reader)
            {
                reader.ReadInt32(); // reserved

                TileXZ = new TileIndex(reader.ReadInt32(), reader.ReadInt32());
                PositionXYZ = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                TangentDegXYZ = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                EndPositionXYZ = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                NodeIndex = reader.ReadInt32();
                IsSwitchNode = reader.ReadBoolean();
                IsReversePath = reader.ReadBoolean();
                CurvatureDeg = reader.ReadSingle();
                CurveSign = reader.ReadInt32();
                RadiusMeters = reader.ReadSingle();
                ArcLengthMeters = reader.ReadSingle();
                NumSegments = reader.ReadInt32();
                BelongsToTrackIndex = reader.ReadInt32();
                IsSelected = reader.ReadBoolean();
            }
        }
    }
}
