/**
 * Low Accuracy
 */

namespace LibRun8.Formats
{
    public class AISignalDatabase : FileFormat
    {
        public List<AISignal> Signals { get; set; } = new List<AISignal>();

        public static AISignalDatabase Read(string path)
        {
            AISignalDatabase aiSignalDatabase = new AISignalDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved

                    var signalCount = reader.ReadInt32();
                    for (int i = 0; i < signalCount; i++)
                    {
                        AISignal aiSignal = AISignal.Read(reader);
                        aiSignalDatabase.Signals.Add(aiSignal);
                    }
                }
            }

            return aiSignalDatabase;
        }

        public override void Write()
        {
            throw new NotImplementedException();
        }

        public class AISignal
        {
            public List<int> SignalIndices {  get; set; } = new List<int>(); // maps signal head signal index -> signal
            public bool Bool0 { get; set; }
            public int Int0 { get; set; }
            public int Int1 { get; set; }
            public Class336 Class336_0 { get; set; }
            public Class336 Class336_1 { get; set; }

            public static AISignal Read(BinaryReader reader)
            {
                AISignal aiSignal = new AISignal();

                reader.ReadInt32(); // reserved
                var count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    aiSignal.SignalIndices.Add(reader.ReadInt32());
                }

                aiSignal.Bool0 = reader.ReadBoolean();
                aiSignal.Int0 = reader.ReadInt32();
                aiSignal.Int1 = reader.ReadInt32();

                if(reader.ReadBoolean())
                {
                    aiSignal.Class336_0 = Class336.Read(reader);
                }

                if(reader.ReadBoolean())
                {
                    aiSignal.Class336_1 = Class336.Read(reader);
                }

                return aiSignal;
            }
        }

        public class Class336
        {
            public int Int0 { get; set; }
            public int Int1 { get; set; }
            public static Class336 Read(BinaryReader reader)
            {
                Class336 @class = new Class336();
                @class.Int0 = reader.ReadInt32();
                @class.Int1 = reader.ReadInt32();

                return @class;
            }
        }
    }
}
