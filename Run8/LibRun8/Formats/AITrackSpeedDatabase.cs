/**
 * Low Accuracy
 */

namespace LibRun8.Formats
{
    public class AITrackSpeedDatabase : FileFormat
    {
        public List<TrackSpeed> TrackSpeeds { get; set; } = new List<TrackSpeed>();
        public static AITrackSpeedDatabase Read(string path)
        {
            AITrackSpeedDatabase aiTrackSpeedDatabase = new AITrackSpeedDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    var count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        aiTrackSpeedDatabase.TrackSpeeds.Add(TrackSpeed.Read(reader));
                    }
                }
            }

            return aiTrackSpeedDatabase;
        }

        public override void Write()
        {
            throw new NotImplementedException();
        }

        public class TrackSpeedEntry
        {
            public int Int0 { get; set; }
            public int Int1 { get; set; } = 15;
            public int Int2 { get; set; } = 15;
            public static TrackSpeedEntry Read(BinaryReader reader)
            {
                TrackSpeedEntry class317 = new TrackSpeedEntry();

                reader.ReadInt32(); // reserved
                class317.Int0 = reader.ReadInt32();
                class317.Int1 = reader.ReadInt32();
                class317.Int2 = reader.ReadInt32();

                return class317;
            }
        }

        public class TrackSpeed
        {
            public List<TrackSpeedEntry> Speeds { get; set; } = new List<TrackSpeedEntry>();
            public int Int0 { get; set; }
            public static TrackSpeed Read(BinaryReader reader)
            {
                TrackSpeed trackSpeed = new TrackSpeed();

                reader.ReadInt32(); // reserved
                trackSpeed.Int0 = reader.ReadInt32();
                var count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    trackSpeed.Speeds.Add(TrackSpeedEntry.Read(reader));
                }

                return trackSpeed;
            }
        }
    }
}
