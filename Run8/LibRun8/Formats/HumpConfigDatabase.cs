using LibRun8.Utils;

namespace LibRun8.Formats
{
    public class HumpConfigDatabase : FileFormat
    {
        public List<Hump> Humps { get; set; } = new List<Hump>();

        public static HumpConfigDatabase Read(string path)
        {
            HumpConfigDatabase item = new HumpConfigDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    int entryCount = reader.ReadInt32();
                    for (int i = 0; i < entryCount; i++)
                    {
                        Hump humpConfig = Hump.Read(reader);
                        item.Humps.Add(humpConfig);
                    }
                }
            }

            return item;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class Hump
        {
            public string Name { get; set; }
            public List<HumpConfig> Configs { get; set; } = new List<HumpConfig>();

            public static Hump Read(BinaryReader reader)
            {
                Hump hump = new Hump();
                reader.ReadInt32(); // reserved
                hump.Name = reader.ReadR8String();

                int entryCount = reader.ReadInt32();
                for (int i = 0; i < entryCount; i++)
                {
                    HumpConfig config = HumpConfig.Read(reader);
                    hump.Configs.Add(config);
                }

                return hump;
            }

            public class HumpConfig
            {
                public string ConfigName { get; set; }
                public List<HumpTrack> Tracks { get; set; } = new List<HumpTrack>();

                public static HumpConfig Read(BinaryReader reader)
                {
                    HumpConfig config = new HumpConfig();
                    reader.ReadInt32(); // reserved
                    if(reader.ReadBoolean())
                    {
                        config.ConfigName = reader.ReadR8String();
                    }

                    int entryCount = reader.ReadInt32();
                    for (int i = 0; i < entryCount; i++)
                    {
                        HumpTrack track = HumpTrack.Read(reader);
                        config.Tracks.Add(track);
                    }

                    return config;
                }

                public class HumpTrack
                {
                    public string TrackName { get; set; }
                    public List<string> Tags { get; set; } = new List<string>();

                    public static HumpTrack Read(BinaryReader reader)
                    {
                        HumpTrack track = new HumpTrack();
                        reader.ReadInt32(); // reserved
                        track.TrackName = reader.ReadR8String();

                        int entryCount = reader.ReadInt32();
                        for (int i = 0; i < entryCount; i++)
                        {
                            track.Tags.Add(reader.ReadR8String());
                        }

                        return track;
                    }
                }
            }
        }
    }
}
