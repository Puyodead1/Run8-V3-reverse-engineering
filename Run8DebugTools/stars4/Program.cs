using System;
using System.Collections.Generic;
using System.IO;
using static Run8Utils.Run8Utils;

namespace Stars4
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

            string newPath = Path.Join(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + "_new.rn8");

            Stars4 stars4 = Read(path);
            for (int i = 0; i < stars4.strings.Count; i++)
            {
                Console.WriteLine("Entry {0}: {1}", i, stars4.strings[i]);
            }

            stars4.strings.Add("EastPuyoville");

            Write(newPath, stars4);
        }

        static Stars4 Read(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    Stars4 stars4 = new Stars4();
                    binaryReader.ReadInt32(); // header - reserved
                    int entryCount = binaryReader.ReadInt32(); // header - number of entries
                    Console.WriteLine("Number of entries: {0}", entryCount);

                    for (int i = 0; i < entryCount; i++)
                    {
                        string entry = ReadString(binaryReader); // entry - string 
                        stars4.strings.Add(entry);
                    }

                    return stars4;
                }
            }
        }

        static void Write(string path, Stars4 stars4)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(stars4.strings.Count);

                    for(int i = 0; i < stars4.strings.Count; i++)
                    {
                        string entry = stars4.strings[i];
                        Console.WriteLine("Writing entry {0}: {1}", i, entry);
                        binaryWriter.Write(EncodeString(entry));
                    }
                }
            }
        }
    }

    class Stars4
    {

        public Stars4()
        {
            this.strings = new List<string>();
        }

        public List<string> strings;
    }
}
