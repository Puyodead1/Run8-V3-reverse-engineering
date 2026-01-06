using LibRun8.Common;

namespace LibRun8.Formats
{
    public class TrackDatabase : FileFormat
    {
        public TrackSection[] Sections { get; set; }

        public static TrackDatabase Read(string path)
        {
            TrackDatabase database = new TrackDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    int sectionCount = reader.ReadInt32();
                    database.Sections = new TrackSection[sectionCount];

                    for(int i = 0; i < sectionCount; i++)
                    {
                        TrackSection section = new TrackSection();
                        section.Read(reader);
                        database.Sections[i] = section;
                    }
                }
            }

            return database;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }
    }
}
