using LibRun8.Common;
using LibRun8.Utils;
using System.Collections.Generic;

namespace LibRun8.Formats
{
    public class HumpControllerList : FileFormat
    {
        public List<HumpController> Controllers { get; set; } = new List<HumpController>();

        public static HumpControllerList Read(string path)
        {
            HumpControllerList list = new HumpControllerList();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    int entryCount = reader.ReadInt32();
                    for (int i = 0; i < entryCount; i++)
                    {
                        HumpController controller = HumpController.Read(reader);
                        list.Controllers.Add(controller);
                    }
                }
            }

            return list;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class HumpController
        {
            public string Name { get; set; }
            public TileIndex TileXZ { get; set; }
            public Vector3 Position { get; set; }
            public List<TrackPath> trackPaths { get; set; } = new List<TrackPath>();

            public static HumpController Read(BinaryReader reader)
            {
                HumpController controller = new HumpController();
                reader.ReadInt32(); // reserved
                controller.Name = reader.ReadR8String();
                controller.TileXZ = TileIndex.Read(reader);
                controller.Position = Vector3.Read(reader);
                reader.ReadR8String(); // ????

                int entryCount = reader.ReadInt32();
                for (int i = 0; i < entryCount; i++)
                {
                    TrackPath trackPath = TrackPath.Read(reader);
                    controller.trackPaths.Add(trackPath);
                }

                return controller;
            }

            public class TrackPath
            {
                public string TrackName { get; set; }
                public List<SwitchConnection> SwitchList { get; set; } = new List<SwitchConnection>();

                public static TrackPath Read(BinaryReader reader)
                {
                    TrackPath trackPath = new TrackPath();
                    reader.ReadInt32(); // reserved
                    trackPath.TrackName = reader.ReadR8String();

                    int entryCount = reader.ReadInt32();
                    for (int i = 0; i < entryCount; i++)
                    {
                        SwitchConnection switchConnection = SwitchConnection.Read(reader);
                        if(trackPath.SwitchList.Find((SwitchConnection x) => x.TrackSectionIndex == switchConnection.TrackSectionIndex) == null)
                        {
                            trackPath.SwitchList.Add(switchConnection);
                        }
                    }

                    return trackPath;
                }

                public class SwitchConnection
                {
                    public int TrackSectionIndex { get; set; }
                    public bool IsReversed { get; set; }

                    public static SwitchConnection Read(BinaryReader reader)
                    {
                        SwitchConnection switchConnection = new SwitchConnection();
                        reader.ReadInt32(); // reserved
                        switchConnection.TrackSectionIndex = reader.ReadInt32();
                        switchConnection.IsReversed = reader.ReadBoolean();

                        return switchConnection;
                    }
                }
            }
        }
    }
}
