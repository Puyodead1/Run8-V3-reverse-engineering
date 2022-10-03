using System;
using System.IO;
using System.Text;

namespace HornBellConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 HornBellConfiguration Parser");

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
                    binaryReader.ReadInt32(); // header - reserved
                    int num0 = binaryReader.ReadInt32(); // header - number of horns
                    Console.WriteLine("Number of Horns: {0}", num0);

                    for (int i = 0; i < num0; i++)
                    {
                        string key = ReadString(binaryReader); // horn - xml filename
                        string value = binaryReader.ReadString(); // horn - horn name

                        Console.WriteLine("Horn {0}: Filename={1}, Horn Name={2}", i, key, value);
                    }

                    int num1 = binaryReader.ReadInt32(); // header - number of bells
                    Console.WriteLine("Number of Bells: {0}", num0);

                    for (int i = 0; i < num1; i++)
                    {
                        string key = ReadString(binaryReader); // bell - locomotive xml filename
                        string value = binaryReader.ReadString(); // bell - bell name

                        Console.WriteLine("Bell {0}: Filename={1}, Bell Name={2}", i, key, value);
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
