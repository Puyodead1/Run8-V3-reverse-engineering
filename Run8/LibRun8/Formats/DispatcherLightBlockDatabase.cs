using LibRun8.Common;
using LibRun8.Utils;

namespace LibRun8.Formats
{
    public class DispatcherLightBlockDatabase : FileFormat
    {
        public List<DispatchLight> DispatchLights { get; set; } = new List<DispatchLight>();
        public static DispatcherLightBlockDatabase Read(string path)
        {
            DispatcherLightBlockDatabase dispatcherLightBlockDatabase = new DispatcherLightBlockDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    int numOfBlocks = reader.ReadInt32();
                    for (int i = 0; i < numOfBlocks; i++)
                    {
                        dispatcherLightBlockDatabase.DispatchLights.Add(DispatchLight.Read(reader));
                    }
                }
            }

            return dispatcherLightBlockDatabase;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class DispatchLight
        {
            public Rectangle ButtonRectangle { get; set; }
            public List<int> Indices { get; set; } = new List<int>();
            public Vector2 ScreenXY { get; set; }
            public string String0 { get; set; } = "";

            public static DispatchLight Read(BinaryReader reader)
            {
                DispatchLight dispatcherBlockLight = new DispatchLight();
                int num = reader.ReadInt32();
                dispatcherBlockLight.ButtonRectangle = reader.ReadRectangle();
                dispatcherBlockLight.ScreenXY = reader.ReadVector2();
                int numOfIndices = reader.ReadInt32();
                for (int i = 0;i < numOfIndices;i++)
                {
                    dispatcherBlockLight.Indices.Add(reader.ReadInt32());
                }
                if(num == 2)
                {
                    dispatcherBlockLight.String0 = reader.ReadString();
                }

                return dispatcherBlockLight;
            }
        }
    }
}
