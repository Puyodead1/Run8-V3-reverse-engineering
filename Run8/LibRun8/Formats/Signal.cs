using SharpDX;
using SharpDX.Mathematics;

namespace LibRun8.Formats
{
    public class Signal : FileFormat
    {
        public string Name { get; set; }
        public int i0 { get; set; }
        public bool b0 { get; set; }
        public bool b1 { get; set; }
        public int EntryCount { get; set; }
        public SignalEntry[] Entries;

        public static Signal Read(string path)
        {
            Signal signal = new Signal();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    signal.Name = binaryReader.ReadString();
                    signal.i0 = binaryReader.ReadInt32();
                    signal.b0 = binaryReader.ReadBoolean();
                    signal.b1 = binaryReader.ReadBoolean();

                    signal.EntryCount = binaryReader.ReadInt32();
                    signal.Entries = new SignalEntry[signal.EntryCount];

                    for(int i = 0; i < signal.EntryCount; i++)
                    {
                        SignalEntry entry = new SignalEntry();
                        entry.vector0 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        entry.vector1 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        entry.f0 = binaryReader.ReadSingle();
                        entry.f1 = binaryReader.ReadSingle();

                        int VectorCount = binaryReader.ReadInt32();
                        entry.Vectors = new Vector3[VectorCount];

                        for(int j = 0; j < VectorCount;  j++)
                        {
                            entry.Vectors[j] = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        }

                        signal.Entries[i] = entry;
                    }
                }
            }

            return signal;
        }

        public override void Write()
        {
            throw new NotImplementedException();
        }

        public class SignalEntry
        {
            public Vector3 vector0 { get; set; }
            public Vector3 vector1 { get; set; }
            public float f0 { get; set; }
            public float f1 { get; set; }
            public Vector3[] Vectors { get; set; }
        }
    }
}
