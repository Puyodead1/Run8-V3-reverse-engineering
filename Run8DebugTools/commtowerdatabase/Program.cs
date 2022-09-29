using System;
using System.IO;
using System.Text;

namespace CommTowerDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 CommTowerDatabase Parser");

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

                    for(int i = 0; i < num0; i++)
                    {
                        binaryReader.ReadInt32(); // entry - reserved
                        int tileIndexX = binaryReader.ReadInt32(); // entry - tile index x
                        int tileIndexY = binaryReader.ReadInt32(); // entry - tile index y

                        float vector3_1x = binaryReader.ReadSingle(); // entry - vector3 x
                        float vector3_1y = binaryReader.ReadSingle(); // entry - vector3 y
                        float vector3_1z = binaryReader.ReadSingle(); // entry - vector3 z

                        string string0 = ReadString(binaryReader); // entry - tower name
                        byte byte0 = binaryReader.ReadByte(); // entry - unknown
                        string string1 = ReadString(binaryReader); // entry - dial code
                        string string2 = ReadString(binaryReader); // entry - emergency dial code?
                        float float0 = binaryReader.ReadSingle(); // entry - unknown
                        string string3 = ReadString(binaryReader); // entry - dispatch tone type

                        Console.WriteLine("     CommTower {0}: tileIndexX={1}, tileIndexY={2}, vector3X={3}, vector3Y={4}, vector3Z={5}, string0={6}, byte0={7}, string1={8}, string2={9}, float0={10}, string3={11}", i, tileIndexX, tileIndexY, vector3_1x, vector3_1y, vector3_1z, string0, byte0, string1, string2, float0, string3);
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
