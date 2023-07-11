using LibRun8.Common;
using LibRun8.Utils;

namespace LibRun8.Formats
{
    public class DispatcherSwitchIconDatabase : FileFormat
    {
        public List<DispatcherButton> Buttons { get; set; } = new List<DispatcherButton>();
        public static DispatcherSwitchIconDatabase Read(string path)
        {
            DispatcherSwitchIconDatabase item = new DispatcherSwitchIconDatabase();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    reader.ReadInt32(); // reserved
                    int numOfIcons = reader.ReadInt32();
                    for (int i = 0; i < numOfIcons; i++)
                    {
                        item.Buttons.Add(DispatcherButton.Read(reader));
                    }
                }
            }

            return item;
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public class DispatcherButton
        {
            public Rectangle Button { get; set; }
            public Vector2 ScreenXY { get; set; }
            public int RoutePrefix { get; set; }
            public int Index { get; set; }
            public List<int> SwitchControllers { get; set; } = new List<int>();
            public string String0 { get; set; } = string.Empty;

            public static DispatcherButton Read(BinaryReader reader)
            {
                DispatcherButton dispatcherButton = new DispatcherButton();

                int num = reader.ReadInt32();
                dispatcherButton.Button = reader.ReadRectangle();
                dispatcherButton.ScreenXY = reader.ReadVector2();
                dispatcherButton.RoutePrefix = reader.ReadInt32();
                dispatcherButton.Index = reader.ReadInt32();
                int numOfSwitchControllers = reader.ReadInt32();

                for (int i = 0;  i < numOfSwitchControllers; i++)
                {
                    dispatcherButton.SwitchControllers.Add(reader.ReadInt32());
                }

                if (num == 2)
                {
                    dispatcherButton.String0 = reader.ReadString();
                }

                return dispatcherButton;
            }
        }
    }
}
