using LibRun8.Util;

namespace LibRun8.Common
{
    public class SignalHead
    {
        public List<int> SignalIndices { get; set; } = new List<int>();
        public List<Route> Routes { get; set; } = new List<Route>();
        public int SignalIndex { get; set; }
        public bool IsAbsolute { get; set; }
        public string ModelName { get; set; }
        public Vector3 Position { get; set; }
        public float RotationDegY { get; set; }
        public TileIndex TileXZ { get; set; }
        public int LeastRestrictiveSignalState { get; set; }
        public bool IsAdvanceDiverging { get; set; }
        public bool bool_2 { get; set; }
        public bool bool_3 { get; set; }
        public bool bool_4 { get; set; }
        public bool bool_5 { get; set; }
        public bool IsDwarf { get; set; }


        public static SignalHead Read(BinaryReader reader)
        {
            SignalHead self = new SignalHead();
            reader.ReadInt32(); // reserved
            int numSignals = reader.ReadInt32();
            for (int i = 0; i < numSignals; i++)
            {
                self.SignalIndices.Add(reader.ReadInt32());
            }

            int numRoutes = reader.ReadInt32();
            for (int j = 0; j < numRoutes; j++)
            {
                Route route = Route.Read(reader);
                self.Routes.Add(route);
            }

            self.SignalIndex = reader.ReadInt32();
            self.IsAbsolute = reader.ReadBoolean();
            self.ModelName = reader.ReadR8String();
            self.Position = reader.ReadVector3();
            self.RotationDegY = reader.ReadSingle();
            self.TileXZ = reader.ReadTileIndex();
            self.LeastRestrictiveSignalState = reader.ReadInt32();
            self.IsAdvanceDiverging = reader.ReadBoolean();
            self.bool_2 = reader.ReadBoolean();
            self.bool_3 = reader.ReadBoolean();
            self.bool_4 = reader.ReadBoolean();
            self.bool_5 = reader.ReadBoolean();
            self.IsDwarf = reader.ReadBoolean();

            return self;
        }
    }
}
