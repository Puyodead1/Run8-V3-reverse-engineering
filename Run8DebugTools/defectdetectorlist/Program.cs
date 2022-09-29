using System;
using System.IO;
using System.Text;

namespace defectdetectorlist
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 DefectDetectorList Parser");

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
                        if(num1 == 1)
                        {
                            int int1 = binaryReader.ReadInt32(); // entry - unknown
                            int int2 = binaryReader.ReadInt32(); // entry - unknown
                            int tileIndexX = binaryReader.ReadInt32(); // entry - tile index x
                            int tileIndexY = binaryReader.ReadInt32(); // entry - tile index y

                            int vector3X = binaryReader.ReadInt32(); // entry - vector3 x
                            int vector3Y = binaryReader.ReadInt32(); // entry - vector3 y
                            int vector3Z = binaryReader.ReadInt32(); // entry - vector3 z

                            byte bool0 = binaryReader.ReadByte(); // entry - unknown
                            byte bool1 = binaryReader.ReadByte(); // entry - unknown
                            byte bool2 = binaryReader.ReadByte(); // entry - unknown
                            byte bool3 = binaryReader.ReadByte(); // entry - unknown
                            byte bool4 = binaryReader.ReadByte(); // entry - unknown
                            byte bool5 = binaryReader.ReadByte(); // entry - unknown

                            string string0 = ReadString(binaryReader); // entry - unknown
                            string string1 = ReadString(binaryReader); // entry - unknown

                            int int3 = binaryReader.ReadInt32(); // entry - track number

                            Console.WriteLine("     DefectDetector {0}: int1={1}, int2={2}, tileIndexX={3}, tileIndexY={4}, vector3X={5}, vector3Y={6}, vector3Z={7}, bool0={8}, bool1={9}, bool2={10}, bool3={11}, bool4={12}, bool5={13}, string0={14}, string1={15}, int3={16}", i, int1, int2, tileIndexX, tileIndexY, vector3X, vector3Y, vector3Z, bool0, bool1, bool2, bool3, bool4, bool5, string0, string1, int3);
                            return;
                        }

                        if(num1 == 2)
                        {
                            int int1 = binaryReader.ReadInt32(); // entry - unknown
                            int int2 = binaryReader.ReadInt32(); // entry - unknown
                            int tileIndexX = binaryReader.ReadInt32(); // entry - tile index x
                            int tileIndexY = binaryReader.ReadInt32(); // entry - tile index y

                            int vector3X = binaryReader.ReadInt32(); // entry - vector3 x
                            int vector3Y = binaryReader.ReadInt32(); // entry - vector3 y
                            int vector3Z = binaryReader.ReadInt32(); // entry - vector3 z

                            byte bool0 = binaryReader.ReadByte(); // entry - unknown
                            byte bool1 = binaryReader.ReadByte(); // entry - unknown
                            byte bool2 = binaryReader.ReadByte(); // entry - unknown
                            byte bool3 = binaryReader.ReadByte(); // entry - unknown
                            byte bool4 = binaryReader.ReadByte(); // entry - unknown
                            byte bool5 = binaryReader.ReadByte(); // entry - unknown
                            byte bool6 = binaryReader.ReadByte(); // entry - unknown

                            string string0 = ReadString(binaryReader); // entry - unknown
                            string string1 = ReadString(binaryReader); // entry - unknown

                            int int3 = binaryReader.ReadInt32(); // entry - track number

                            Console.WriteLine("     DefectDetector {0}: int1={1}, int2={2}, tileIndexX={3}, tileIndexY={4}, vector3X={5}, vector3Y={6}, vector3Z={7}, bool0={8}, bool1={9}, bool2={10}, bool3={11}, bool4={12}, bool5={13}, bool6={14}, string0={15}, string1={16}, int3={17}", i, int1, int2, tileIndexX, tileIndexY, vector3X, vector3Y, vector3Z, bool0, bool1, bool2, bool3, bool4, bool5, bool6, string0, string1, int3);
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
