using System;
using System.IO;
using System.Text;

namespace trackdatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 TrackDatabase Parser");

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
                    int trackCount = binaryReader.ReadInt32(); // header - number of tracks
                    Console.WriteLine("Track Count: {0}", trackCount);

                    for (int i = 0; i < trackCount; i++)
                    {
                        binaryReader.ReadInt32(); // track - reserved
                        int num = binaryReader.ReadInt32(); // track - number of unknown objects
                        Console.WriteLine("Track {0}: number of unknown={1}", i, num);

                        for (int j = 0; j < num; j++)
                        {
                            binaryReader.ReadInt32(); // unknown - reserved
                            int tileIndexX = binaryReader.ReadInt32(); // unknown - tile index x
                            int tileIndexY = binaryReader.ReadInt32(); // unknown - tile index y
                            Console.WriteLine("     Track {0} Unknown {1} - Tile Index: X={2}, Y={3}", i, j, tileIndexX, tileIndexY);

                            float vector1_x = binaryReader.ReadSingle(); // unknown - vector3 1 x
                            float vector1_y = binaryReader.ReadSingle(); // unknown - vector3 1 y
                            float vector1_z = binaryReader.ReadSingle(); // unknown - vector3 1 z
                            Console.WriteLine("     Track {0} Unknown {1} - Vector3 0: X={2}, Y={3}, Z={4}", i, j, vector1_x, vector1_y, vector1_z);

                            float vector2_x = binaryReader.ReadSingle(); // unknown - vector3 2 x
                            float vector2_y = binaryReader.ReadSingle(); // unknown - vector3 2 y
                            float vector2_z = binaryReader.ReadSingle(); // unknown - vector3 2 z
                            Console.WriteLine("     Track {0} Unknown {1} - Vector3 1: X={2}, Y={3}, Z={4}", i, j, vector2_x, vector2_y, vector2_z);

                            float vector3_x = binaryReader.ReadSingle(); // unknown - vector3 3 x
                            float vector3_y = binaryReader.ReadSingle(); // unknown - vector3 3 y
                            float vector3_z = binaryReader.ReadSingle(); // unknown - vector3 3 z
                            Console.WriteLine("     Track {0} Unknown {1} - Vector3 2: X={2}, Y={3}, Z={4}", i, j, vector3_x, vector3_y, vector3_z);

                            int int1 = binaryReader.ReadInt32(); // unknown - unknown
                            bool bool1 = binaryReader.ReadBoolean(); // unknown - unknown
                            bool bool2 = binaryReader.ReadBoolean(); // unknown - unknown
                            float float1 = binaryReader.ReadSingle(); // unknown - unknown
                            int int2 = binaryReader.ReadInt32(); // unknown - unknown
                            float float2 = binaryReader.ReadSingle(); // unknown - unknown
                            float float3 = binaryReader.ReadSingle(); // unknown - unknown
                            int int3 = binaryReader.ReadInt32(); // unknown - unknown
                            int int4 = binaryReader.ReadInt32(); // unknown - unknown
                            bool bool3 = binaryReader.ReadBoolean(); // unknown - unknown

                            Console.WriteLine("     Track {0} Unknown {1} - int0={2}, bool0={3}, bool1={4}, float2={5}, int1={6}, float0={7}, float1={8}, int2={9}, int3={10}, bool2={11}", i, j, int1, bool1, bool2, float1, int2, float2, float3, int3, int4, bool3);
                        }

                        int int0 = binaryReader.ReadInt32(); // track - unknown
                        bool bool4 = binaryReader.ReadBoolean(); // track - unknown
                        Console.WriteLine("Track {0}: bool0={1}, int0={2}", i, bool4, int0);
                        int int5 = binaryReader.ReadInt32(); // track - number of unknown ints
                        Console.WriteLine("Track {0}: num2={1}", i, int5);
                        for (int k = 0; k < int5; k++)
                        {
                            int int6 = binaryReader.ReadInt32(); // unknown2 - unknown
                            Console.WriteLine("     Track {0} Unknown2 {1} - int5={2}", i, k, int6);
                        }

                        byte byte1 = binaryReader.ReadByte(); // track - unknown enum
                        double double1 = binaryReader.ReadDouble(); // track - unknown
                        bool bool5 = binaryReader.ReadBoolean(); // track - unknown
                        bool bool6 = binaryReader.ReadBoolean(); // track - unknown
                        int int7 = binaryReader.ReadInt32(); // track - unknown
                        bool bool7 = binaryReader.ReadBoolean(); // track - unknown

                        Console.WriteLine("Track {0}: enum37={1}, double0={2}, bool1={3}, bool2={4}, int1={5}, bool3={6}", i, byte1, double1, bool5, bool6, int7, bool7);
                    }
                }
            }
        }
    }
}
