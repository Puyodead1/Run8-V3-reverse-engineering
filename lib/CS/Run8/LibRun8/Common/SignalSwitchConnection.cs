namespace LibRun8.Common
{
    public class SignalSwitchConnection
    {
        public int SwitchIndex { get; set; }
        public bool ClearIfThrownNormal { get; set; }

        public static SignalSwitchConnection Read(BinaryReader reader)
        {
            SignalSwitchConnection self = new SignalSwitchConnection();
            reader.ReadInt32(); // reserved
            self.SwitchIndex = reader.ReadInt32();
            self.ClearIfThrownNormal = reader.ReadBoolean();
            return self;
        }
    }
}
