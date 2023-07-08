using LibRun8.Common;
using LibRun8.Utils;

namespace LibRun8.Formats
{
    public class CarSpewerDatabase : FileFormat
    {
        public List<CarSpewer> Spewers { get; set; } = new List<CarSpewer>();
        public static CarSpewerDatabase Read(string path)
        {
            CarSpewerDatabase carSpewerDatabase = new CarSpewerDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    var numOfSpewers = reader.ReadInt32();
                    for (int i = 0; i < numOfSpewers; i++)
                    {
                        carSpewerDatabase.Spewers.Add(CarSpewer.Read(reader));
                    }
                }
            }

            return carSpewerDatabase;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class CarSpewer
        {
            public int Int0 { get; set; } // Doesn't appear to ever be used
            public int RoadNodeIndex { get; set; }
            public int MaxNumCars { get; set; }
            public double MinTimeBetwixtSpew { get; set; }
            public double MaxTimeBetwixtSpew { get; set; }
            public float MaxSpeed { get; set; }
            public List<CarSpewStartPoint> CarSpewStartPoints { get; set; } = new List<CarSpewStartPoint>();

            public static CarSpewer Read(BinaryReader reader)
            {
                CarSpewer carSpewer = new CarSpewer();

                var num = reader.ReadInt32();
                carSpewer.Int0 = reader.ReadInt32();
                carSpewer.RoadNodeIndex = reader.ReadInt32();
                carSpewer.MaxNumCars = reader.ReadInt32();
                carSpewer.MinTimeBetwixtSpew = reader.ReadDouble();
                carSpewer.MaxTimeBetwixtSpew = reader.ReadDouble();
                if (num == 2)
                {
                    carSpewer.MaxSpeed = reader.ReadSingle();
                }
                var numOfSpewStartPoints = reader.ReadInt32();
                for (int i = 0; i < numOfSpewStartPoints; i++)
                {
                    carSpewer.CarSpewStartPoints.Add(CarSpewStartPoint.Read(reader));
                }

                return carSpewer;
            }
        }

        public class CarSpewStartPoint
        {
            public Vector3 PosXYZ { get; set; }
            public TileIndex TileXZ { get; set; }
            public float Heading { get; set; } = -999f;

            public static CarSpewStartPoint Read(BinaryReader reader)
            {
                CarSpewStartPoint carSpewStartPoint = new CarSpewStartPoint();

                reader.ReadInt32();
                carSpewStartPoint.PosXYZ = reader.ReadVector3();
                carSpewStartPoint.TileXZ = reader.ReadTileIndex();
                carSpewStartPoint.Heading = reader.ReadSingle();

                return carSpewStartPoint;
            }
        }
    }
}
