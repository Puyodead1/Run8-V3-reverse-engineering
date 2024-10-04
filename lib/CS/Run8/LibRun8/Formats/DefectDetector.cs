using LibRun8.Common;
using LibRun8.Util;

namespace LibRun8.Formats
{
    public class DefectDetector
    {
        public int Version { get; set; } = 2;
        public int Milepost { get; set; }
        public int MilepostDecimal { get; set; }
        public TileIndex TileXZ { get; set; }
        public Vector3 PositionXYZ { get; set; }
        public bool? IsAEI { get; set; }
        public bool SquawkOnDefectOnly { get; set; }
        public bool DraggingEquipment { get; set; }
        public bool SquawkTemperature { get; set; }
        public bool SquawkTrainSpeed { get; set; }
        public bool Hotbox { get; set; }
        public bool HiWide { get; set; } // legacy, not used
        public string WaveBankName { get; set; }
        public string SoundBankName { get; set; }
        public int TrackNumber { get; set; }

        public static DefectDetector Read(BinaryReader reader)
        {
            DefectDetector defectDetector = new DefectDetector();
            defectDetector.Version = reader.ReadInt32();
            if (defectDetector.Version == 1)
            {
                defectDetector.Milepost = reader.ReadInt32();
                defectDetector.MilepostDecimal = reader.ReadInt32();
                defectDetector.TileXZ = reader.ReadTileIndex();
                defectDetector.PositionXYZ = reader.ReadVector3();
                defectDetector.SquawkOnDefectOnly = reader.ReadBoolean();
                defectDetector.DraggingEquipment = reader.ReadBoolean();
                defectDetector.SquawkTemperature = reader.ReadBoolean();
                defectDetector.SquawkTrainSpeed = reader.ReadBoolean();
                defectDetector.Hotbox = reader.ReadBoolean();
                defectDetector.HiWide = reader.ReadBoolean();
                defectDetector.WaveBankName = reader.ReadR8String();
                defectDetector.SoundBankName = reader.ReadR8String();
                defectDetector.TrackNumber = reader.ReadInt32();
            }

            if (defectDetector.Version == 2)
            {
                defectDetector.Milepost = reader.ReadInt32();
                defectDetector.MilepostDecimal = reader.ReadInt32();
                defectDetector.TileXZ = reader.ReadTileIndex();
                defectDetector.PositionXYZ = reader.ReadVector3();
                defectDetector.IsAEI = reader.ReadBoolean();
                defectDetector.SquawkOnDefectOnly = reader.ReadBoolean();
                defectDetector.DraggingEquipment = reader.ReadBoolean();
                defectDetector.SquawkTemperature = reader.ReadBoolean();
                defectDetector.SquawkTrainSpeed = reader.ReadBoolean();
                defectDetector.Hotbox = reader.ReadBoolean();
                defectDetector.HiWide = reader.ReadBoolean();
                defectDetector.WaveBankName = reader.ReadR8String();
                defectDetector.SoundBankName = reader.ReadR8String();
                defectDetector.TrackNumber = reader.ReadInt32();
            }

            return defectDetector;
        }
    }
}
