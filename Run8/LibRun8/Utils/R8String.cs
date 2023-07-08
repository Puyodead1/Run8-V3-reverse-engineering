using System.Text;

namespace LibRun8.Utils
{
    public class R8String
    {
        public static string DecodeBytes(byte[] bytes)
        {
            byte[] array = new byte[bytes.Length / 2];
            int num = 0;
            for (int i = 0; i < array.Length; i++)
            {
                byte[] array2 = array;
                int num2 = i;
                array2[num2] |= (byte)(bytes[num++] << 4);
                byte[] array3 = array;
                int num3 = i;
                array3[num3] |= (byte)(bytes[num++] >> 4);
            }
            return Encoding.UTF8.GetString(array);
        }

        public static byte[] EncodeBytes(string val)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(val);
            byte[] array = new byte[bytes.Length * 2];
            int num = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                array[num++] = (byte)(bytes[i] >> 4);
                array[num++] = (byte)(bytes[i] << 4);
            }
            return array;
        }
    }
}
