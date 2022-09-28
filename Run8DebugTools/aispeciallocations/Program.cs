using System;
using System.IO;
using System.Text;

namespace aispeciallocations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 AISpecialLocations Parser");

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
                    Console.WriteLine("AISpecialLocations entry count: {0}", num);

                    for (int i = 0; i < num; i++)
                    {
                        binaryReader.ReadInt32(); // entry - reserved
                        string locationName = ReadString(binaryReader); // entry - unknown
                        byte locationType = binaryReader.ReadByte(); // entry - unknown
                        int int0 = binaryReader.ReadInt32(); // entry - unknown
                        int int1 = binaryReader.ReadInt32(); // entry - unknown
                        int int2 = binaryReader.ReadInt32(); // entry - unknown
                        float float0 = binaryReader.ReadSingle(); // entry - unknown
                        int int3 = binaryReader.ReadInt32(); // entry - unknown
                        bool bool0 = binaryReader.ReadBoolean(); // entry - unknown

                        Console.WriteLine("     AISpecialLocation Entry {0}: location name={1}, locationType={2}, int0={3}, int1={4}, int2={5}, float0={6}, int3={7}, bool0={8}", i, locationName, locationType, int0, int1, int2, float0, int3, bool0);
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
