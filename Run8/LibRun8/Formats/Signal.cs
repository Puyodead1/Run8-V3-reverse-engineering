using SharpDX;
using SharpDX.Mathematics;

namespace LibRun8.Formats
{
    public class Signal : FileFormat
    {
        public string modelName { get; set; }
        public int i0 { get; set; }
        public bool b0 { get; set; }
        public bool b1 { get; set; }
        public SignalEntry[] signals;

        public static Signal Read(string path)
        {
            Signal signal = new Signal();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    signal.modelName = binaryReader.ReadString();
                    signal.i0 = binaryReader.ReadInt32();
                    signal.b0 = binaryReader.ReadBoolean();
                    signal.b1 = binaryReader.ReadBoolean();

                    int signalCount = binaryReader.ReadInt32();
                    signal.signals = new SignalEntry[signalCount];

                    for(int i = 0; i < signalCount; i++)
                    {
                        SignalEntry entry = new SignalEntry();
                        entry.vector0 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        entry.vector1 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        entry.f0 = binaryReader.ReadSingle();
                        entry.f1 = binaryReader.ReadSingle();

                        int VectorCount = binaryReader.ReadInt32();
                        entry.vectors = new Vector3[VectorCount];

                        for(int j = 0; j < VectorCount;  j++)
                        {
                            entry.vectors[j] = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        }

                        signal.signals[i] = entry;
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
            public Vector3[] vectors { get; set; }
        }
    }
}
