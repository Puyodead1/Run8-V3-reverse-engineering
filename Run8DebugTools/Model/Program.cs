using System;
using System.IO;
using System.Text;

namespace Model
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 Model Parser");

            if (args.Length == 0)
            {
                Console.WriteLine("no file specified.");
                return;
            }

            string path = args[0];

            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist");
                return;
            }


            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    bool flag = false;
                    int num = 1;
                    int num2 = binaryReader.ReadInt32();
                    if(num2 == -969696)
                    {
                        num = binaryReader.ReadInt32();
                        flag = true;
                    }
                    else if(num2 == -969697)
                    {
                        num = binaryReader.ReadInt32();
                        float vec3_0_x = binaryReader.ReadSingle();
                        float vec3_0_y = binaryReader.ReadSingle();
                        float vec3_0_z = binaryReader.ReadSingle();

                        flag = true;
                    }
                }
            }
        }

        static string DecodeString(byte[] bytes)
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

        static string ReadString(BinaryReader binaryReader)
        {
            int size = binaryReader.ReadInt32(); // string - size/length
            return DecodeString(binaryReader.ReadBytes(size)); // string - Read the specified size of bytes and decode them
        }
    }
}
