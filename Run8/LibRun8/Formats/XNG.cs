using LibRun8.Common;

namespace LibRun8.Formats
{
    public enum GateRotationType
    {
        Vertical,
        Horizontal,
        XBuckNoBell
    }

    public class XNG : FileFormat
    {
        public GateRotationType GateRotationType { get; set; }
        public string GateModelName { get; set; }
        public string StandModelName { get; set; }
        public float ActiveDegrees { get; set; }
        public float InactiveDegrees { get; set; }
        public float RotationDegreesPerSecond { get; set; }
        public Vector3 GateOffset { get; set; }
        public LightLoader[] Lights { get; set; }

        public static XNG Read(string path)
        {
            XNG xng = new XNG();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    xng.GateRotationType = (GateRotationType)reader.ReadByte();
                    xng.GateModelName = reader.ReadString();
                    xng.StandModelName = reader.ReadString();
                    xng.ActiveDegrees = reader.ReadSingle();
                    xng.InactiveDegrees = reader.ReadSingle();
                    xng.RotationDegreesPerSecond = reader.ReadSingle();
                    xng.GateOffset = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                    int lightCount = reader.ReadInt32();
                    xng.Lights = new LightLoader[lightCount];

                    for(int i = 0; i < lightCount; i++)
                    {
                        LightLoader light = new LightLoader();
                        light.LightRange = reader.ReadSingle();
                        light.LightIntensity = reader.ReadSingle();
                        light.LightOffsetXYZ = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.Color = new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), 1f);
                        light.LightDirectionDeg = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.LightGlareRadiusMeters = reader.ReadSingle();
                        light.IsLimitedYardLight = reader.ReadBoolean();

                        int glareCount = reader.ReadInt32();
                        light.GlareList = new Vector3[glareCount];

                        for(int j = 0;  j < glareCount; j++)
                        {
                            light.GlareList[j] = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        }
                        xng.Lights[i] = light;
                    }
                }
            }

            return xng;
        }

        public override void Write()
        {
            throw new NotImplementedException();
        }

        public class SignalLight
        {
            public Vector3 LightOffset { get; set; }
            // this is technically a vec4
            public Vector4 Color { get; set; }
            public float LightGlareRadiusMeters { get; set; }
            public float LightRange { get; set; }
            public Vector3[] GlareList { get; set; }
        }
    }
}
