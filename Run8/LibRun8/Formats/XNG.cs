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
        public GateRotationType gateRotationType { get; set; }
        public string gateModelName { get; set; }
        public string standModelName { get; set; }
        public float activeDegrees { get; set; }
        public float inactiveDegrees { get; set; }
        public float rotationDegreesPerSecond { get; set; }
        public Vector3 gateOffset { get; set; }
        public LightLoader[] lights { get; set; }

        public static XNG Read(string path)
        {
            XNG xng = new XNG();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    xng.gateRotationType = (GateRotationType)binaryReader.ReadByte();
                    xng.gateModelName = binaryReader.ReadString();
                    xng.standModelName = binaryReader.ReadString();
                    xng.activeDegrees = binaryReader.ReadSingle();
                    xng.inactiveDegrees = binaryReader.ReadSingle();
                    xng.rotationDegreesPerSecond = binaryReader.ReadSingle();
                    xng.gateOffset = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());

                    int lightCount = binaryReader.ReadInt32();
                    xng.lights = new LightLoader[lightCount];

                    for(int i = 0; i < lightCount; i++)
                    {
                        LightLoader light = new LightLoader();
                        light.lightRange = binaryReader.ReadSingle();
                        light.lightIntensity = binaryReader.ReadSingle();
                        light.lightOffsetXYZ = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        light.color = new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), 1f);
                        light.lightDirectionDeg = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        light.lightGlareRadiusMeters = binaryReader.ReadSingle();
                        light.isLimitedYardLight = binaryReader.ReadBoolean();

                        int glareCount = binaryReader.ReadInt32();
                        light.glareList = new Vector3[glareCount];

                        for(int j = 0;  j < glareCount; j++)
                        {
                            light.glareList[j] = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        }
                        xng.lights[i] = light;
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
            public Vector3 lightOffset { get; set; }
            // this is technically a vec4
            public Vector4 color { get; set; }
            public float lightGlareRadiusMeters { get; set; }
            public float lightRange { get; set; }
            public Vector3[] glareList { get; set; }
        }
    }
}
