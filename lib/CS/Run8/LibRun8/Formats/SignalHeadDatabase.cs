using LibRun8.Common;


namespace LibRun8.Formats
{
    public class SignalHeadDatabase : FileFormat
    {
        public List<SignalHead> SignalHeads { get; set; } = new List<SignalHead>();
        public static SignalHeadDatabase Read(string path)
        {
            SignalHeadDatabase self = new SignalHeadDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    int signalHeadCount = reader.ReadInt32();
                    for (int i = 0; i < signalHeadCount; i++)
                    {
                        self.SignalHeads.Add(SignalHead.Read(reader));
                    }
                }
            }

            return self;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

       
    }
}
