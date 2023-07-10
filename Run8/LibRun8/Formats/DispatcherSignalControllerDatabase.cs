using LibRun8.Common;
using LibRun8.Utils;

namespace LibRun8.Formats
{
    public class DispatcherSignalControllerDatabase : FileFormat
    {
        public List<DispatcherSignalController> Controllers { get; private set; } = new List<DispatcherSignalController>();
        public static DispatcherSignalControllerDatabase Read(string path)
        {
            DispatcherSignalControllerDatabase item = new DispatcherSignalControllerDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    int numOfControllers = reader.ReadInt32();
                    for (int i = 0; i < numOfControllers; i++)
                    {
                        item.Controllers.Add(DispatcherSignalController.Read(reader));
                    }
                }
            }

            return item;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class DispatcherSignalController
        {
            public DispatcherLight Button { get; set; }
            public List<int> Signals { get; set; } = new List<int>();
            public string String0 { get; set; } = string.Empty;
            public static DispatcherSignalController Read(BinaryReader reader)
            {
                DispatcherSignalController controller = new DispatcherSignalController();

                int num = reader.ReadInt32();
                Vector2 screenXY = reader.ReadVector2();
                controller.Button = DispatcherLight.Read(reader);
                controller.Button.ScreenXY = screenXY;

                int numOfSignals = reader.ReadInt32();
                for (int i = 0; i < numOfSignals; i++)
                {
                    controller.Signals.Add(reader.ReadInt32());
                }

                if(num == 2)
                {
                    controller.String0 = reader.ReadString();
                }

                return controller;
            }
        }
    }
}
