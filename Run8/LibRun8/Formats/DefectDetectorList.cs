using LibRun8.Util;

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
                    for (int i = 0; i < numOfDetectors; i++)
                    {
                        defectDetectorList.DefectDetectors.Add(DefectDetector.Read(reader));
                    }
                }
            }

            return defectDetectorList;
        }

        public override void Write(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {

                writer.Write(1); // reserved
                writer.Write(DefectDetectors.Count);
                foreach (DefectDetector defectDetector in DefectDetectors)
                {
                    writer.Write(defectDetector.Version);
                    writer.Write(defectDetector.Milepost);
                    writer.Write(defectDetector.MilepostDecimal);
                    writer.WriteTileIndex(defectDetector.TileXZ);
                    writer.WriteVector3(defectDetector.PositionXYZ);
                    if (defectDetector.Version == 2)
                    {
                        if(!defectDetector.IsAEI.HasValue)
                        {
                            throw new Exception("IsAEI must have a value when version is 2!");
                        }
                        writer.Write(defectDetector.IsAEI.Value);
                    }
                    writer.Write(defectDetector.SquawkOnDefectOnly);
                    writer.Write(defectDetector.DraggingEquipment);
                    writer.Write(defectDetector.SquawkTemperature);
                    writer.Write(defectDetector.SquawkTrainSpeed);
                    writer.Write(defectDetector.Hotbox);
                    writer.Write(defectDetector.HiWide);
                    writer.WriteR8String(defectDetector.WaveBankName);
                    writer.WriteR8String(defectDetector.SoundBankName);
                    writer.Write(defectDetector.TrackNumber);
                }
            }
        }
    }
}