using LibRun8.Common;
using LibRun8.Util;


namespace LibRun8.Formats
{
    public class SignalHeadDatabase : FileFormat
    {
        public List<SignalHead> SignalHeads { get; set; } = new List<SignalHead>();
        public static SignalHeadDatabase Read(string path)
        {
            SignalHeadDatabase item = new SignalHeadDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    int signalHeadCount = reader.ReadInt32();
                    for (int i = 0; i < signalHeadCount; i++)
                    {
                        item.SignalHeads.Add(SignalHead.Read(reader));
                    }
                }
            }

            return item;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class SignalHead
        {
            public List<int> list_0 { get; set; } = new List<int>();
            public List<Class665> list_1 { get; set; } = new List<Class665>();
            public int signalIndex { get; set; }
            public bool bool_0 { get; set; }
            public string nameType { get; set; }
            public Vector3 position { get; set; }
            public float rotationDegY { get; set; }
            public TileIndex tileXZ { get; set; }
            public int int_1 { get; set; }
            public bool bool_1 { get; set; }
            public bool bool_2 { get; set; }
            public bool bool_3 { get; set; }
            public bool bool_4 { get; set; }
            public bool bool_5 { get; set; }
            public bool bool_6 { get; set; }


            public static SignalHead Read(BinaryReader reader)
            {
                SignalHead signalHead = new SignalHead();
                reader.ReadInt32(); // reserved
                int num = reader.ReadInt32();
                for (int i = 0; i < num; i++)
                {
                    signalHead.list_0.Add(reader.ReadInt32());
                }
                int num2 = reader.ReadInt32();
                for (int j = 0; j < num2; j++)
                {
                    Class665 @class = Class665.Read(reader);
                    signalHead.list_1.Add(@class);
                }
                signalHead.signalIndex = reader.ReadInt32();
                signalHead.bool_0 = reader.ReadBoolean();
                signalHead.nameType = reader.ReadR8String();
                signalHead.position = reader.ReadVector3();
                signalHead.rotationDegY = reader.ReadSingle();
                signalHead.tileXZ = reader.ReadTileIndex();
                signalHead.int_1 = reader.ReadInt32();
                signalHead.bool_1 = reader.ReadBoolean();
                signalHead.bool_2 = reader.ReadBoolean();
                signalHead.bool_3 = reader.ReadBoolean();
                signalHead.bool_6 = reader.ReadBoolean();
                signalHead.bool_4 = reader.ReadBoolean();
                signalHead.bool_5 = reader.ReadBoolean();

                return signalHead;
            }
        }
    }
}
