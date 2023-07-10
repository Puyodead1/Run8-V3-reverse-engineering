using LibRun8.Utils;

namespace LibRun8.Common
{
    public class DispatcherLight
    {
        public Rectangle ButtonRectangle { get; set; }
        public List<int> Indices { get; set; } = new List<int>();
        public Vector2 ScreenXY { get; set; }
        public string String0 { get; set; } = string.Empty;

        public static DispatcherLight Read(BinaryReader reader)
        {
            DispatcherLight dispatcherBlockLight = new DispatcherLight();
            int num = reader.ReadInt32();
            dispatcherBlockLight.ButtonRectangle = reader.ReadRectangle();
            dispatcherBlockLight.ScreenXY = reader.ReadVector2();
            int numOfIndices = reader.ReadInt32();
            for (int i = 0; i < numOfIndices; i++)
            {
                dispatcherBlockLight.Indices.Add(reader.ReadInt32());
            }
            if (num == 2)
            {
                dispatcherBlockLight.String0 = reader.ReadString();
            }

            return dispatcherBlockLight;
        }
    }
}
