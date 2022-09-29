using System;
using System.IO;
using System.Text;

namespace IndustryConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 Industry Configuration Parser");

            if(args.Length == 0)
            {
                Console.WriteLine("no file specified.");
                return;
            }

            string path = args[0];

            if(!File.Exists(path)) {
                Console.WriteLine("File does not exist");
                return;
            }


            using(FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    binaryReader.ReadInt32(); // header - reserved
                    int industryCount = binaryReader.ReadInt32(); // header - number of entries
                    Console.WriteLine("Industry Count: {0}", industryCount);

                    for(int i = 0; i < industryCount; i++)
                    {
                        binaryReader.ReadInt32(); // industry - reserved
                        string industryName = ReadString(binaryReader); // industry - industry name
                        Console.WriteLine("     Industry {0} Name: {1}", i, industryName);
                        string localFreightCode = ReadString(binaryReader); // industry - local freight code
                        Console.WriteLine("     Industry {0} Freight Code: {1}", i, localFreightCode);
                        string industryTag = ReadString(binaryReader); // industry - industry tag
                        Console.WriteLine("     Industry {0} Tag: {1}", i, industryTag);
                        bool bool0 = binaryReader.ReadBoolean(); // industry - unknown bool
                        Console.WriteLine("     Industry {0} Bool0: {1}", i, bool0);

                        int trackCount = binaryReader.ReadInt32(); // industry - track count
                        Console.WriteLine("     Industry {0} Track Count: {1}", i, trackCount);
                        for (int j = 0; j < trackCount; j++)
                        {
                            binaryReader.ReadInt32(); // track - reserved
                            int prefix = binaryReader.ReadInt32(); // track - Prefix
                            int section = binaryReader.ReadInt32(); // track - Section
                            int node = binaryReader.ReadInt32(); // track - Node
                            Console.WriteLine("         Industry {0} Track {1}: Prefix={2}, Section={3}, Node={4}", i, j, prefix, section, node);
                        }

                        int carCount = binaryReader.ReadInt32(); // industry - car count
                        Console.WriteLine("      Industry {0} Car Count: {1}", i, carCount);
                        for (int k = 0; k < carCount; k++)
                        {
                            int num = binaryReader.ReadInt32(); // car - unknown int
                            byte carType = binaryReader.ReadByte(); // car - car type
                            bool bool1 = binaryReader.ReadBoolean(); // car - unknown boolean
                            int int0 = binaryReader.ReadInt32(); // car - unknown int
                            int int1 = binaryReader.ReadInt32(); // car - unknown int
                            int num2 = binaryReader.ReadInt32(); // car unknown unt
                            Console.WriteLine("         Industry {0} Car {1}: num={2}, Type={3}, bool1={4}, int0={5}, int1={6}, num2={7}", i, k, num, carType, bool1, int0, int1, num2);

                            for(int l = 0; l < num2; l++)
                            {
                                string string0 = ReadString(binaryReader);
                                Console.WriteLine("         Industry {0} Car {1}: l={2}, string0={3}", i, k, l, string0);
                            }

                            if(num >= 2)
                            {
                                int num3 = binaryReader.ReadInt32(); // car - unknown int
                                Console.WriteLine("         Industry {0} Car {1}: num3={2}", i, k, num3);
                                for (int m = 0; m < num3; m++)
                                {
                                    string string0 = ReadString(binaryReader);
                                    Console.WriteLine("         Industry {0} Car {1}: m={2}, string0={3}", i, k, m, string0);
                                }
                            }
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
