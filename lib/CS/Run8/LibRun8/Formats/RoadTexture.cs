using LibRun8.Util;

namespace LibRun8.Formats
{
    public class RoadTexture : FileFormat
    {
        public Dictionary<int, string> NameToIndexMap { get; set; } = new Dictionary<int, string>();
        public Dictionary<string, int> IndexToNameMap { get; set; } = new Dictionary<string, int>();

        public static RoadTexture Read(string path)
        {
            RoadTexture self = new RoadTexture();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    int entryCount = reader.ReadInt32();
                    for (int i = 0; i < entryCount; i++)
                    {
                        string str = reader.ReadR8String();
                        self.NameToIndexMap[i] = str;
                        self.IndexToNameMap[str] = i;
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
