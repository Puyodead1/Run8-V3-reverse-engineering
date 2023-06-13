using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRun8.Utils
{
    public class String
    {
        public static string Deserialize(byte[] bytes)
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

        public static string Read(BinaryReader binaryReader)
        {
            int size = binaryReader.ReadInt32();
            return Deserialize(binaryReader.ReadBytes(size));
        }
    }
}
