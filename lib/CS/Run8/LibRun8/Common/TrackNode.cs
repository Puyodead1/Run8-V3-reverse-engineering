namespace LibRun8.Common
{
    public class TrackNode
    {
        public TileIndex TileIndex { get; set; }
        public Vector3 Position { get; set; }
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

            TileIndex = new TileIndex(reader.ReadInt32(), reader.ReadInt32());
            Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
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
