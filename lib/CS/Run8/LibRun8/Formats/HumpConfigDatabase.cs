using LibRun8.Util;

namespace LibRun8.Formats
{
    public class HumpConfigDatabase : FileFormat
    {
        public List<HumpConfigEntry> Humps { get; set; } = new();

        public static HumpConfigDatabase Read(string path)
        {
            HumpConfigDatabase item = new HumpConfigDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    int humpCount = reader.ReadInt32();
                    for (int i = 0; i < humpCount; i++)
                    {
                        HumpConfigEntry humpConfigEntryConfig = HumpConfigEntry.Read(reader);
                        item.Humps.Add(humpConfigEntryConfig);
                    }
                }
            }

            return item;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class HumpConfigEntry
        {
            public string Name { get; set; }
            public List<HumpConfig> Configs { get; set; } = new();

            public static HumpConfigEntry Read(BinaryReader reader)
            {
                HumpConfigEntry humpConfigEntry = new HumpConfigEntry();
                reader.ReadInt32(); // reserved
                humpConfigEntry.Name = reader.ReadR8String();

                int configCount = reader.ReadInt32();
                for (int i = 0; i < configCount; i++)
                {
                    HumpConfig config = HumpConfig.Read(reader);
                    humpConfigEntry.Configs.Add(config);
                }

                return humpConfigEntry;
            }

            public class HumpConfig
            {
                public string ConfigName { get; set; }
                public List<HumpConfigTrack> Tracks { get; set; } = new();

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
                        HumpConfigTrack configTrack = HumpConfigTrack.Read(reader);
                        config.Tracks.Add(configTrack);
                    }

                    return config;
                }

                public class HumpConfigTrack
                {
                    public string TrackName { get; set; }
                    public List<string> Tags { get; set; } = new();

                    public static HumpConfigTrack Read(BinaryReader reader)
                    {
                        HumpConfigTrack configTrack = new HumpConfigTrack();
                        reader.ReadInt32(); // reserved
                        configTrack.TrackName = reader.ReadR8String();

                        int tagCount = reader.ReadInt32();
                        for (int i = 0; i < tagCount; i++)
                        {
                            configTrack.Tags.Add(reader.ReadR8String());
                        }

                        return configTrack;
                    }
                }
            }
        }
    }
}
