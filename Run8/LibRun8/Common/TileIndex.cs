namespace LibRun8.Common
{
    public class TileIndex
    {
        public int X {  get; set; }
        public int Y { get; set; }

        public TileIndex()
        {
            X = 0;
            Y = 0;
        }

        public TileIndex(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static TileIndex Read(BinaryReader reader)
        {
            TileIndex tileXZ = new TileIndex();

            tileXZ.X = reader.ReadInt32();
            tileXZ.Y = reader.ReadInt32();

            return tileXZ;
        }
    }
}
