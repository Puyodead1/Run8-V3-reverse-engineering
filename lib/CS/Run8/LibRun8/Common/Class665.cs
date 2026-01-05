using LibRun8.Util;

namespace LibRun8.Common
{
    public class Class665
    {
        public string trackName { get; set; } = "No TrackName";
        public int int_0 { get; set; } = -1;
        public List<int> blockDetectorIDS { get; set; } = new List<int>();
        public List<int> list_2 { get; set; } = new List<int>();
        public List<Class669> list_3 { get; set; } = new List<Class669>();
        public bool bool_0 { get; set; }
        public bool bool_1 { get; set; }
        public bool bool_2 { get; set; }
        public bool bool_3 { get; set; }
        public bool bool_4{ get; set; }
        public int int_2 { get; set; } = -1;
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
        public Enum61 enum61 { get; set; }

        public static Class665 Read(BinaryReader reader)
        {
            Class665 class665 = new Class665();
            reader.ReadInt32(); // reserved
            class665.trackName = reader.ReadR8String();
            class665.int_0 = reader.ReadInt32();

            int count = reader.ReadInt32();
            class665.blockDetectorIDS.Clear();
            for (int i = 0; i < count; i++)
            {
                class665.blockDetectorIDS.Add(reader.ReadInt32());
            }

            int count2 = reader.ReadInt32();
            class665.list_2.Clear();
            for (int i = 0; i < count2; i++)
            {
                class665.list_2.Add(reader.ReadInt32());
            }

            int count3 = reader.ReadInt32();
            class665.list_3.Clear();
            for (int i = 0; i < count3; i++)
            {
                Class669 @class = Class669.Read(reader);
                class665.list_3.Add(@class);
            }

            class665.bool_0 = reader.ReadBoolean();
            class665.bool_1 = reader.ReadBoolean();
            class665.bool_2 = reader.ReadBoolean();
            class665.bool_3 = reader.ReadBoolean();
            class665.bool_4 = reader.ReadBoolean();
            class665.int_2 = reader.ReadInt32();
            class665.bool_5 = reader.ReadBoolean();
            class665.bool_6 = reader.ReadBoolean();
            class665.bool_7 = reader.ReadBoolean();
            class665.bool_8 = reader.ReadBoolean();
            class665.bool_9 = reader.ReadBoolean();
            class665.bool_10 = reader.ReadBoolean();
            class665.bool_11 = reader.ReadBoolean();
            class665.bool_12 = reader.ReadBoolean();
            class665.bool_13 = reader.ReadBoolean();
            class665.bool_14 = reader.ReadBoolean();
            class665.bool_15 = reader.ReadBoolean();
            class665.bool_16 = reader.ReadBoolean();
            class665.bool_17 = reader.ReadBoolean();
            class665.enum61 = (Enum61)reader.ReadInt32();
            return class665;
        }
    }
}
