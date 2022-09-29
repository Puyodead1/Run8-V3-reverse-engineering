using System;
using System.IO;
using System.Text;

namespace MilepostDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 MilepostDatabase Parser");

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
                    int num = binaryReader.ReadInt32(); // header - number of entries
                    Console.WriteLine("Number of entries: {0}", num);

                    for(int i = 0; i < num; i++)
                    {
                        binaryReader.ReadInt32(); // milepost - reserved
                        string string0 = ReadString(binaryReader); // milepost - unknown
                        string string1 = ReadString(binaryReader); // milepost - unknown
                        int tileIndexX = binaryReader.ReadInt32(); // milepost - tile index x
                        int tileIndexY = binaryReader.ReadInt32(); // milepost - tile index y
                        float vector3X = binaryReader.ReadSingle(); // milepost - unknown vector3 x
                        float vector3Y = binaryReader.ReadSingle(); // milepost - unknown vector3 y
                        float vector3Z = binaryReader.ReadSingle(); // milepost - unknown vector3 z

                        Console.WriteLine("Milepost {0}: string0={1}, string1={2}, tileX={3}, tileY={4}, vecX={5}, vecY={6}, vecZ={6}", i, string0, string1, tileIndexX, tileIndexY, vector3X, vector3Y, vector3Z);
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
