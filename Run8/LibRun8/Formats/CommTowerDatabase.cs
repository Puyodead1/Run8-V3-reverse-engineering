using LibRun8.Common;
using LibRun8.Utils;
using System.IO;

namespace LibRun8.Formats
{
    public class CommTowerDatabase : FileFormat
    {
        public List<CommTower> CommTowers { get; set; } = new List<CommTower>();
        public static CommTowerDatabase Read(string path)
        {
            CommTowerDatabase commTowerDatabase = new CommTowerDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32();
                    int numOfTowers = reader.ReadInt32();
                    for (int i = 0; i < numOfTowers; i++)
                    {
                        commTowerDatabase.CommTowers.Add(CommTower.Read(reader));
                    }
                }
            }

            return commTowerDatabase;
        }

        public override void Write(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    writer.Write(1); // reserved
                    if (CommTowers == null)
                    {
                        writer.Write(0);
                        return;
                    }
                    writer.Write(CommTowers.Count);
                    CommTowers.ForEach(x => x.Write(writer));
                }
            }
        }

        public class CommTower
        {
            public TileIndex TileXZ { get; set; }
            public Vector3 Position { get; set; }
            public string TowerID { get; set; }
            public byte Channel { get; set; }
            public string DispDMTFCode { get; set; }
            public string EmgDMTFCode { get; set; }
            public float RangeMeters { get; set; }
            public string DispToneCueName { get; set; }

            public static CommTower Read(BinaryReader reader)
            {
                CommTower commTower = new CommTower();

                reader.ReadInt32(); // reserved
                commTower.TileXZ = reader.ReadTileIndex();
                commTower.Position = reader.ReadVector3();
                commTower.TowerID = reader.ReadR8String();
                commTower.Channel = reader.ReadByte();
                commTower.DispDMTFCode = reader.ReadR8String();
                commTower.EmgDMTFCode = reader.ReadR8String();
                commTower.RangeMeters = reader.ReadSingle();
                commTower.DispToneCueName = reader.ReadR8String();

                return commTower;
            }

            public void Write(BinaryWriter writer)
            {
                writer.Write(1); // reserved
                writer.WriteTileIndex(TileXZ);
                writer.WriteVector3(Position);
                writer.WriteR8String(TowerID);
                writer.Write(Channel);
                writer.WriteR8String(DispDMTFCode);
                writer.WriteR8String(EmgDMTFCode);
                writer.Write(RangeMeters);
                writer.WriteR8String(DispToneCueName);
            }
        }
    }
}
