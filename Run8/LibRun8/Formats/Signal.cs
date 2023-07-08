using LibRun8.Common;

namespace LibRun8.Formats
{
    public class Signal : FileFormat
    {
        public string ModelName { get; set; }
        public int LeastRestrictiveSignalState { get; set; }
        public bool B0 { get; set; }
        public bool IsDwarf { get; set; }
        public SignalLight[] SignalLights { get; set; }

        public static Signal Read(string path)
        {
            Signal signal = new Signal();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    signal.ModelName = reader.ReadString();
                    signal.LeastRestrictiveSignalState = reader.ReadInt32();
                    signal.B0 = reader.ReadBoolean();
                    signal.IsDwarf = reader.ReadBoolean();

                    int signalCount = reader.ReadInt32();
                    signal.SignalLights = new SignalLight[signalCount];

                    for(int i = 0; i < signalCount; i++)
                    {
                        SignalLight light = new SignalLight();
                        light.LightOffset = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.Color = new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), 1f);
                        light.LightGlareRadiusMeters = reader.ReadSingle();
                        light.LightRange = reader.ReadSingle();

                        int VectorCount = reader.ReadInt32();
                        light.GlareList = new Vector3[VectorCount];

                        for(int j = 0; j < VectorCount;  j++)
                        {
                            light.GlareList[j] = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        }

                        signal.SignalLights[i] = light;
                    }
                }
            }

            return signal;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class SignalLight
        {
            public Vector3 LightOffset { get; set; }
            public Vector3 lightDirectionDeg { get; set; }
            public Vector3[] GlareList { get; set; }
            public Vector4 Color { get; set; }
            public float LightGlareRadiusMeters { get; set; } = 0.35f;
            public float LightRange { get; set; }
            public float LightIntensity { get; set; }
            public bool IsLimitedYardLight { get; set; }
        }
    }
}
