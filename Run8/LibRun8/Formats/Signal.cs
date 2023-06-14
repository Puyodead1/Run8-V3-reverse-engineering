using LibRun8.Common;

namespace LibRun8.Formats
{
    public class Signal : FileFormat
    {
        public string modelName { get; set; }
        public int leastRestrictiveSignalState { get; set; }
        public bool b0 { get; set; }
        public bool isDwarf { get; set; }
        public SignalLight[] lights { get; set; }

        public static Signal Read(string path)
        {
            Signal signal = new Signal();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    signal.modelName = binaryReader.ReadString();
                    signal.leastRestrictiveSignalState = binaryReader.ReadInt32();
                    signal.b0 = binaryReader.ReadBoolean();
                    signal.isDwarf = binaryReader.ReadBoolean();

                    int signalCount = binaryReader.ReadInt32();
                    signal.lights = new SignalLight[signalCount];

                    for(int i = 0; i < signalCount; i++)
                    {
                        SignalLight light = new SignalLight();
                        light.lightOffset = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        light.color = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), 1f);
                        light.lightGlareRadiusMeters = binaryReader.ReadSingle();
                        light.lightRange = binaryReader.ReadSingle();

                        int VectorCount = binaryReader.ReadInt32();
                        light.glareList = new Vector3[VectorCount];

                        for(int j = 0; j < VectorCount;  j++)
                        {
                            light.glareList[j] = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        }

                        signal.lights[i] = light;
                    }
                }
            }

            return signal;
        }

        public override void Write()
        {
            throw new NotImplementedException();
        }

        public class SignalLight
        {
            public Vector3 lightOffset { get; set; }
            // this is technically a vec4
            public Vector4 color { get; set; }
            public float lightGlareRadiusMeters { get; set; }
            public float lightRange { get; set; }
            public Vector3[] glareList { get; set; }
        }
    }
}
