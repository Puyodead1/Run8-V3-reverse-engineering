namespace LibRun8.Common
{
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

            for (int i = 0; i < numNodes; i++)
            {
                Nodes[i] = new TrackNode();
                Nodes[i].Read(reader);
            }

            Index = reader.ReadInt32();
            SwitchLeverPosition = reader.ReadBoolean();

            int numSectionIndices = reader.ReadInt32();
            NextSectionIndex = new int[numSectionIndices];
            for (int i = 0; i < numSectionIndices; i++)
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
}
