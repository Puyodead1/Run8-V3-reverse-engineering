using LibRun8.Common;
using LibRun8.Utils;

namespace LibRun8.Formats
{
    public class DefectDetectorList : FileFormat
    {
        public List<DefectDetector> DefectDetectors { get; set; } = new List<DefectDetector>();
        public static DefectDetectorList Read(string path)
        {
            DefectDetectorList defectDetectorList = new DefectDetectorList();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    int numOfDetectors = reader.ReadInt32();
                    for (int i = 0;  i < numOfDetectors; i++)
                    {
                        defectDetectorList.DefectDetectors.Add(DefectDetector.Read(reader));
                    }
                }
            }

            return defectDetectorList;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class DefectDetector
        {
            public int Milepost { get; set; }
            public int MilepostDecimal { get; set; }
            public TileIndex TileXZ { get; set; }
            public Vector3 PositionXYZ { get; set; }
            public bool IsAEI { get; set; }
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
                int num = reader.ReadInt32();
                if (num == 1)
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

                if (num == 2)
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
}
