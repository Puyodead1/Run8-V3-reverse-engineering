namespace LibRun8.Common
{
    public class TileIndex
    {
        public int X {  get; set; }
        public int Z { get; set; }

        public TileIndex()
        {
            X = 0;
            Z = 0;
        }

        public TileIndex(int x, int y)
        {
            X = x;
            Z = y;
        }

        public static TileIndex Read(BinaryReader reader)
        {
            TileIndex tileXZ = new TileIndex();

            tileXZ.X = reader.ReadInt32();
            tileXZ.Z = reader.ReadInt32();

            return tileXZ;
        }
    }
}
