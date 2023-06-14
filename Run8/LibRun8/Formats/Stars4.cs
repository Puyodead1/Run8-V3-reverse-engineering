namespace LibRun8.Formats
{
    public class Stars4 : FileFormat
    {
        public string[] entries { get; set; }

        public static Stars4 Read(string path)
        {
            Stars4 stars4 = new Stars4();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    binaryReader.ReadInt32();
                    int entryCount = binaryReader.ReadInt32();
                    stars4.entries = new string[entryCount];

                    for(int i = 0; i < stars4.entries.Length; i++)
                    {
                        stars4.entries[i] = Utils.String.Read(binaryReader);
                    }
                }
            }

            return stars4;
        }

        public override void Write()
        {
            throw new NotImplementedException();
        }
    }
}
