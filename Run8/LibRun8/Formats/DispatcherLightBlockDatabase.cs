using LibRun8.Common;

namespace LibRun8.Formats
{
    public class DispatcherLightBlockDatabase : FileFormat
    {
        public List<DispatcherLight> DispatchLights { get; set; } = new List<DispatcherLight>();
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
                        dispatcherLightBlockDatabase.DispatchLights.Add(DispatcherLight.Read(reader));
                    }
                }
            }

            return dispatcherLightBlockDatabase;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }
    }
}
