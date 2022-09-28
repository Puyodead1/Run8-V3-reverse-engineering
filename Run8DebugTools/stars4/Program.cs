using System;
using System.IO;
using System.Text;

namespace star4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 Stars4 Parser");

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
                    Console.WriteLine("Number of entries: {0}", num);

                    for (int i = 0; i < num; i++)
                    {
                        string string0 = ReadString(binaryReader); // entry - string 0

                        Console.WriteLine("Entry {0}: string0={1}", i, string0);
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
