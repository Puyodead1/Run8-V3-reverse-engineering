using LibRun8.Common;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace LibRun8.Utils
{
    /// <summary>
    /// Provides some conspicuously absent string and type functionality to 
    /// <seealso cref="BinaryReader"/>
    /// </summary>
    static class BinaryReaderExtensions
    {
        /// <summary>
        /// Reads a class or a struct from the reader
        /// </summary>
        /// <typeparam name="T">The type to read</typeparam>
        /// <param name="reader">The reader</param>
        /// <returns>An instance of <typeparamref name="T"/> as read from the stream</returns>
        public static T ReadType<T>(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(Marshal.SizeOf(typeof(T)));

            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T result = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return result;
        }
        /// <summary>
        /// Reads a C style null terminated ASCII string
        /// </summary>
        /// <param name="reader">The binary reader</param>
        /// <returns>A string as read from the stream</returns>
        public static string ReadSZString(this BinaryReader reader)
        {
            var result = new StringBuilder();
            while (true)
            {
                byte b = reader.ReadByte();
                if (0 == b)
                    break;
                result.Append((char)b);
            }
            return result.ToString();
        }
        /// <summary>
        /// Reads a fixed size ASCII string
        /// </summary>
        /// <param name="reader">The binary reader</param>
        /// <param name="count">The number of characters</param>
        /// <returns>A string as read from the stream</returns>
        public static string ReadFixedString(this BinaryReader reader, int count)
        {
            return Encoding.ASCII.GetString(reader.ReadBytes(count));
        }

        public static Vector2 ReadVector2(this BinaryReader reader)
        {
            float X = reader.ReadSingle();
            float Y = reader.ReadSingle();
            return new Vector2(X, Y);
        }

        public static Vector3 ReadVector3(this BinaryReader reader)
        {
            float X = reader.ReadSingle();
            float Y = reader.ReadSingle();
            float Z = reader.ReadSingle();
            return new Vector3(X, Y, Z);
        }

        public static TileIndex ReadTileIndex(this BinaryReader reader)
        {
            int X = reader.ReadInt32();
            int Z = reader.ReadInt32();

            return new TileIndex(X, Z);
        }

        public static string ReadR8String(this BinaryReader reader)
        {
            int size = reader.ReadInt32();
            return R8String.DecodeBytes(reader.ReadBytes(size));
        }

        public static Rectangle ReadRectangle(this BinaryReader reader)
        {
            return new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }
    }
}
