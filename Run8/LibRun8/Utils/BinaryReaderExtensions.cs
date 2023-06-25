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
    }
}
