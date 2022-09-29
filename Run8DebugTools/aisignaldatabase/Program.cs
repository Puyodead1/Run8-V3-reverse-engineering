using System;
using System.IO;

namespace AISignalDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 AISignalDatabase Parser");

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
                    Console.WriteLine("AISignalDatabase Entry Count: {0}", num);

                    for(int i = 0; i < num; i++)
                    {
                        int int0 = binaryReader.ReadInt32(); // unknown1 - unknown
                        Console.WriteLine("     AISignalDatabase Entry {0}, Unknown1 int0={1}", i, int0);
                    }

                    bool bool0 = binaryReader.ReadBoolean(); // header - unknown bool
                    int int1 = binaryReader.ReadInt32(); // header - unknown int
                    int int2 = binaryReader.ReadInt32(); // header - unknown int
                    bool bool1 = binaryReader.ReadBoolean(); // header - unknown bool
                    if(bool1)
                    {
                        int int3 = binaryReader.ReadInt32(); // header - unknown int
                        int int4 = binaryReader.ReadInt32(); // header - unknown int
                        Console.WriteLine("AISignalDatabase: int3={0}, int4={1}", int3, int4);
                    }
                    bool bool2 = binaryReader.ReadBoolean(); // header - unknown bool
                    if (bool2)
                    {
                        int int5 = binaryReader.ReadInt32(); // header - unknown int
                        int int6 = binaryReader.ReadInt32(); // header - unknown int
                        Console.WriteLine("AISignalDatabase: int5={0}, int6={1}", int5, int6);
                    }

                    Console.WriteLine("AISignalDatabase: bool0={0}, int1={1}, int2={2}, bool1={3}, bool2={4}", bool0, int1, int2, bool1, bool2);
                }
            }
        }
    }
}
