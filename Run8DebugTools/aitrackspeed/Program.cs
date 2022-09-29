using System;
using System.IO;

namespace AITrackSpeed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 AITrackSpeed Parser");

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
                    Console.WriteLine("AITrackSpeed entry count: {0}", num);

                    for(int i = 0; i < num; i++)
                    {
                        binaryReader.ReadInt32(); // entry - reserved
                        int int0 = binaryReader.ReadInt32(); // entry - unknown
                        int num1 = binaryReader.ReadInt32(); // entry - number of unknown1 entries

                        Console.WriteLine("     AITrackSpeed Entry {0}: int0={1}, unknown1 count: {2}", i, int0, num1);

                        for(int j = 0; j < num1; j++)
                        {
                            binaryReader.ReadInt32(); // entry - reserved
                            int int1 = binaryReader.ReadInt32(); // entry - unknown
                            int int2 = binaryReader.ReadInt32(); // entry - unknown
                            int int3 = binaryReader.ReadInt32(); // entry - unknown

                            Console.WriteLine("         AITrackSpeed Entry {0} Unknown1 {1}: int1={2}, int2={3}, int3={4}", i, j, int1, int2, int3);
                        }
                    }
                }
            }
        }
    }
}
