using LibRun8.Common;

namespace LibRun8.Util
{
    /// <summary>
    /// Provides some conspicuously absent string and type functionality to 
    /// <seealso cref="BinaryWriter"/>
    /// </summary>
    static class BinaryWriterExtensions
    {

        public static void WriteVector3(this BinaryWriter writer, Vector3 vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }

        public static void WriteTileIndex(this BinaryWriter writer, TileIndex TileXZ)
        {
            writer.Write(TileXZ.X);
            writer.Write(TileXZ.Z);
        }

        public static void WriteR8String(this BinaryWriter writer, string val)
        {
            if (val == null)
            {
                val = string.Empty;
            }
            byte[] array = R8String.EncodeBytes(val);
            writer.Write(array.Length);
            writer.Write(array);
        }
    }
}
