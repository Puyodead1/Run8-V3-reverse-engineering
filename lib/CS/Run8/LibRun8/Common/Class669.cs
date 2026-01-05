namespace LibRun8.Common
{
    public class Class669
    {
        public int int_0 { get; set; }
        public bool bool_0 { get; set; }

        public static Class669 Read(BinaryReader reader)
        {
            Class669 class669 = new Class669();
            reader.ReadInt32(); // reserved
            class669.int_0 = reader.ReadInt32();
            class669.bool_0 = reader.ReadBoolean();
            return class669;
        }
    }
}
