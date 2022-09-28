using System;
using System.IO;
using System.Text;

namespace sig
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 Signal Parser");

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
                    string string0 = binaryReader.ReadString();
                    int int0 = binaryReader.ReadInt32();
                    bool bool0 = binaryReader.ReadBoolean();
                    bool bool1 = binaryReader.ReadBoolean();

                    Console.WriteLine("Signal string0={0}, int0={1}, bool0={2}, bool1={3}", string0, int0, bool0, bool1);

                    int num = binaryReader.ReadInt32();
                    Console.WriteLine("Signal entry count: {0}", num);
                    for (int i = 0; i < num; i++)
                    {
                        float vector3_1X = binaryReader.ReadSingle();
                        float vector3_1Y = binaryReader.ReadSingle();
                        float vector3_1Z = binaryReader.ReadSingle();
                        float vector3_2X = binaryReader.ReadSingle();
                        float vector3_2Y = binaryReader.ReadSingle();
                        float vector3_2Z = binaryReader.ReadSingle();
                        float float0 = binaryReader.ReadSingle();
                        float float1 = binaryReader.ReadSingle();

                        Console.WriteLine("     Signal entry {0}: vector3_1X={1}, vector3_1Y={2}, vector3_1Z={3}, vector3_2X={4}, vector3_2Y={5}, vector3_2Z={6}, float0={7}, float1={8}", i, vector3_1X, vector3_1Y, vector3_1Z, vector3_2X, vector3_2Y, vector3_2Z, float0, float1);

                        int num2 = binaryReader.ReadInt32();
                        Console.WriteLine("     Signal entry {0} Unknown Vector Count: {1}", i, num2);
                        for (int j = 0; j < num2; j++)
                        {
                            float vector3_3X = binaryReader.ReadSingle();
                            float vector3_3Y = binaryReader.ReadSingle();
                            float vector3_3Z = binaryReader.ReadSingle();

                            Console.WriteLine("         Signal entry {0} Unknown Vector {1}: X={2},Y={3},Z={4}", i, j, vector3_3X, vector3_3Y, vector3_3Z);
                        }
                    }
                }
            }
        }
    }
}
