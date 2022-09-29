using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Traffic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 Traffic Parser");

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
                    bool bool0 = binaryReader.ReadBoolean(); // header - unknown
                    int int0 = binaryReader.ReadInt32(); // header - unknown
                    int int1 = binaryReader.ReadInt32(); // header - unknown
                    int int2 = binaryReader.ReadInt32(); // header - unknown
                    Console.WriteLine("Traffic: bool0={0}, int0={1}, int1={2}, int2={3}", bool0, int0, int1, int2);

                    int num0 = binaryReader.ReadInt32(); // header - number of unknown1 entries
                    Console.WriteLine("Traffic number of unknown1: {0}", num0);
                    for (int i = 0; i < num0; i++)
                    {
                        binaryReader.ReadInt32(); // unknown1 - reserved
                        string string0 = ReadString(binaryReader); // unknown1 - unknown, looks like the route name (ex. Amsterdam SSDG East)
                        int int3 = binaryReader.ReadInt32(); // unknown1 - unknown
                        Console.WriteLine("     Traffic unknown1 {0}: string0={1}, int3={2}", i, string0, int3);

                        int num1 = binaryReader.ReadInt32(); // unknown1 - number of unknown2 entries
                        Console.WriteLine("     Traffic unknown1 {0} number of unknown2: {0}", i, num1);
                        for (int j = 0; j < num1; j++)
                        {
                            binaryReader.ReadInt32(); // unknown2 - reserved
                            byte trainClass = binaryReader.ReadByte(); // unknown2 - train class
                            int int4 = binaryReader.ReadInt32(); // unknown2 - unknown
                            Console.WriteLine("         Traffic unknown1 {0} unknown2 {1}: trainClass={2}, int4={3}", i, j, trainClass, int4);

                            int num2 = binaryReader.ReadInt32(); // unknown2 - number of unknown3 entries
                            Console.WriteLine("         Traffic unknown1 {0} unknown2 {1}, number of unknown3: {2}", i, j, num2);
                            if (trainClass == 0x000000FF) // SavedTrain
                            {
                                for (int k = 0; k < num2; k++)
                                {
                                    binaryReader.ReadInt32(); // unknown3 - reserved
                                    string string1 = ReadString(binaryReader); // unknown3 - unknown
                                    string string2 = ReadString(binaryReader); // unknown3 - unknown
                                    int int5 = binaryReader.ReadInt32(); // unknown3 - unknown

                                    Console.WriteLine("             Traffic unknown1 {0} unknown2 {1} unknown3 {2} (saved train): string1={3}, string2={4}, int5={5}", i, j, k, string1, string2, int5);
                                }
                                return;
                            }

                            for (int k = 0; k < num2; k++)
                            {
                                int num3 = binaryReader.ReadInt32(); // unknown4 - unknown
                                bool bool1 = binaryReader.ReadBoolean(); // unknown4 - unknown
                             
                                if (bool1)
                                {
                                    string string3 = ReadString(binaryReader); // unknown4 - unknown
                                    Console.WriteLine("                 Traffic unknown1 {0} unknown2 {1} unknown3 {2} unknown4: string3={3}", i, j, k, string3); // looks like the train tag
                                }

                                byte trainCaste = binaryReader.ReadByte(); // unknown4 - train caste
                                byte trainSpecialRestrictions = binaryReader.ReadByte(); // unknown4 - train caste
                                bool bool2 = binaryReader.ReadBoolean(); // unknown4 - unknown
                                bool bool3 = binaryReader.ReadBoolean(); // unknown4 - unknown
                                bool bool4 = binaryReader.ReadBoolean(); // unknown4 - unknown

                                Console.WriteLine("             Traffic unknown1 {0} unknown2 {1} unknown3 {2}: num3={3}, bool1={4}, trainCaste={5}, trainSpecialRestrictions={6}, bool2={7}, bool3={8}, bool4={9}", i, j, k, num3, bool1, trainCaste, trainSpecialRestrictions, bool2, bool3, bool4);

                                ReadStringArray(binaryReader, 3, string.Format("Traffic unknown1 {0} unknown2 {1} unknown3 {2} stringarray1", i, j, k)); // not sure, found one usage that was one entry of "Maple Leaf"
                                ReadStringArray(binaryReader, 3, string.Format("Traffic unknown1 {0} unknown2 {1} unknown3 {2} stringarray2", i, j, k)); // looks like companies (bnsf, up, csx, etc)
                                ReadStringArray(binaryReader, 3, string.Format("Traffic unknown1 {0} unknown2 {1} unknown3 {2} stringarray3", i, j, k)); // looks like locomotives (ES44, SD40-2, etc)
                                ReadStringArray(binaryReader, 3, string.Format("Traffic unknown1 {0} unknown2 {1} unknown3 {2} stringarray4", i, j, k)); 

                                if(num3 > 1)
                                {
                                    ReadStringArray(binaryReader, 4, string.Format("Traffic unknown1 {0} unknown2 {1} unknown3 {2} stringarray5", i, j, k)); // looks like this specifies xml files for some cars (ex R8_ChipHopper_GSC7000_CSXT01.xml)
                                    ReadStringArray(binaryReader, 4, string.Format("Traffic unknown1 {0} unknown2 {1} unknown3 {2} stringarray6", i, j, k));

                                    if(num3 > 2)
                                    {
                                        bool bool5 = binaryReader.ReadBoolean(); // unknown4 - unknown
                                        Console.WriteLine("             Traffic unknown1 {0} unknown2 {1} unknown3 {2}: bool5={3}", i, j, k, num3, bool5);
                                    }
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

        static void ReadStringArray(BinaryReader binaryReader, int indent, string prefix)
        {
            int num = binaryReader.ReadInt32(); // number of strings
            for (int i = 0; i < num; i++)
            {
                string string_ = ReadString(binaryReader);
                Console.WriteLine("{0}{1} string {2}: {3}", string.Concat(Enumerable.Repeat(" ", indent)), prefix, i, string_);
            }
        }
    }
}
