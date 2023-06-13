using System.Text;

namespace Run8Utils
{
    public static class Run8Utils
    {
        public static string DecodeString(byte[] bytes)
        {
            byte[] result = new byte[bytes.Length / 2];
            int num = 0;
            for (int i = 0; i < result.Length; i++)
            {
                result[i] |= (byte)(bytes[num++] << 4);
                result[i] |= (byte)(bytes[num++] >> 4);
            }

            return Encoding.UTF8.GetString(result);
        }

        public static byte[] EncodeString(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] array = new byte[bytes.Length * 2];
            int num = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                array[num++] = (byte)(bytes[i] >> 4);
                array[num++] = (byte)(bytes[i] << 4);
            }
            return array;
        }

        public static string ReadString(BinaryReader binaryReader)
        {
            int size = binaryReader.ReadInt32(); // string - size/length
            return DecodeString(binaryReader.ReadBytes(size)); // string - Read the specified size of bytes and decode them
        }
    }
}
