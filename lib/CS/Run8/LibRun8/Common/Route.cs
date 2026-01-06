using LibRun8.Util;

namespace LibRun8.Common
{
    public class Route
    {
        public string RouteName { get; set; } = "No RouteName";
        public int RouteMaxMPH { get; set; } = -1;
        public List<int> BlockDetectorIndices { get; set; } = new List<int>();
        public List<int> PrevSignalIndices { get; set; } = new List<int>();
        public List<SignalSwitchConnection> SwitchList { get; set; } = new List<SignalSwitchConnection>();
        public bool bool_0 { get; set; }
        public bool bool_1 { get; set; }
        public bool bool_2 { get; set; }
        public bool bool_3 { get; set; }
        public bool bool_4{ get; set; }
        public int ReadFromDatabasePrefix { get; set; } = -1;
        public bool bool_5 { get; set; }
        public bool bool_6 { get; set; }
        public bool bool_7 { get; set; }
        public bool bool_8 { get; set; }
        public bool bool_9 { get; set; }
        public bool bool_10 { get; set; }
        public bool bool_11 { get; set; }
        public bool bool_12 { get; set; }
        public bool bool_13 { get; set; }
        public bool bool_14 { get; set; }
        public bool bool_15 { get; set; }
        public bool bool_16 { get; set; }
        public bool bool_17 { get; set; }
        public bool bool_18 { get; set; }
        public Enum61 enum61 { get; set; }

        public static Route Read(BinaryReader reader)
        {
            Route self = new Route();
            int version = reader.ReadInt32();
            self.RouteName = reader.ReadR8String();
            self.RouteMaxMPH = reader.ReadInt32();

            int blockDetectorCount = reader.ReadInt32();
            self.BlockDetectorIndices.Clear();
            for (int i = 0; i < blockDetectorCount; i++)
            {
                self.BlockDetectorIndices.Add(reader.ReadInt32());
            }

            int prevSignalCount = reader.ReadInt32();
            self.PrevSignalIndices.Clear();
            for (int i = 0; i < prevSignalCount; i++)
            {
                self.PrevSignalIndices.Add(reader.ReadInt32());
            }

            int switchCount = reader.ReadInt32();
            self.SwitchList.Clear();
            for (int i = 0; i < switchCount; i++)
            {
                SignalSwitchConnection sigSwitchConn = SignalSwitchConnection.Read(reader);
                self.SwitchList.Add(sigSwitchConn);
            }

            self.bool_0 = reader.ReadBoolean();
            self.bool_1 = reader.ReadBoolean();
            self.bool_2 = reader.ReadBoolean();
            self.bool_3 = reader.ReadBoolean();
            self.bool_4 = reader.ReadBoolean();
            self.ReadFromDatabasePrefix = reader.ReadInt32();
            if(version == 2)
            {
                self.bool_5 = reader.ReadBoolean();
            }
            self.bool_6 = reader.ReadBoolean();
            self.bool_7 = reader.ReadBoolean();
            self.bool_8 = reader.ReadBoolean();
            self.bool_9 = reader.ReadBoolean();
            self.bool_10 = reader.ReadBoolean();
            self.bool_11 = reader.ReadBoolean();
            self.bool_12 = reader.ReadBoolean();
            self.bool_13 = reader.ReadBoolean();
            self.bool_14 = reader.ReadBoolean();
            self.bool_15 = reader.ReadBoolean();
            self.bool_16 = reader.ReadBoolean();
            self.bool_17 = reader.ReadBoolean();
            self.bool_18 = reader.ReadBoolean();
            self.enum61 = (Enum61)reader.ReadByte();
            return self;
        }
    }
}
