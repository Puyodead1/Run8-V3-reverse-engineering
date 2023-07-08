namespace LibRun8.Formats
{
    public class Stars4 : FileFormat
    {
        public string[] Entries { get; set; }

        public static Stars4 Read(string path)
        {
            Stars4 stars4 = new Stars4();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32();
                    int entryCount = reader.ReadInt32();
                    stars4.Entries = new string[entryCount];

                    for(int i = 0; i < stars4.Entries.Length; i++)
                    {
                        stars4.Entries[i] = Utils.String.Read(reader);
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
