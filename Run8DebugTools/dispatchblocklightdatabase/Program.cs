using System;
using System.IO;
using System.Text;

namespace DispatchBlockLightDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 DispatchBlockLightDatabase Parser");

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
                    int num0 = binaryReader.ReadInt32(); // header - number of entries
                    Console.WriteLine("Number of entries: {0}", num0);

                    for (int i = 0; i < num0; i++)
                    {
                        int num1 = binaryReader.ReadInt32(); // entry - unknown

                        int recX = binaryReader.ReadInt32(); // entry - rectangle x
                        int recY = binaryReader.ReadInt32(); // entry - rectangle y
                        int recW = binaryReader.ReadInt32(); // entry - rectangle width
                        int recH = binaryReader.ReadInt32(); // entry - rectangle height

                        float vec2X = binaryReader.ReadSingle(); // entry - vector2 x
                        float vec2Y = binaryReader.ReadSingle(); // entry - vector2 y 

                        Console.WriteLine("     DispatchBlockLight {0}: num1={1}, recX={2}, recY={3}, recW={4}, recH={5}, vec2X={6}, vec2Y={7}", i, num1, recX, recY, recW, recH, vec2X, vec2Y);

                        int num2 = binaryReader.ReadInt32(); // entry - number of unknown1

                        Console.WriteLine("     DispatchBlockLight {0}: Number of unknown1 {1}", i, num2);

                        for (int j = 0; j < num2; j++)
                        {
                            int int2 = binaryReader.ReadInt32(); // unknown1 - unknown
                            Console.WriteLine("         DispatchBlockLight {0} Unknown1 {1}: int2={2}", i, j, int2);
                        }

                        if(num1 == 2)
                        {
                            string string0 = ReadString(binaryReader); // unknown2 - unknown
                            Console.WriteLine("         DispatchBlockLight {0} Unknown2: string0={2}", i, string0);
                        }
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
