using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRun8.Utils
{
    public class Texture
    {
        public static void DecryptTexture(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found: " + filePath);
                return;
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] checksum = new byte[32];
                byte[] data = new byte[fs.Length - 32L];
                fs.Read(data, 0, (int)fs.Length - 32);
                fs.Read(checksum, 0, 32);

                Decrypt(data);
                Decrypt(checksum);

                // calculate the checksum of the decrypted data
                byte[] calculatedChecksum = Utils.ComputeMD5ForBytes(data);

                // compare the checksums
                if (!Utils.CompareByteArrays(checksum, calculatedChecksum))
                {
                    Console.Error.WriteLine("Checksums do not match!");
                    return;
                } 
                else
                {
                    Console.WriteLine("Checksums match!");
                }

                // write the decrypted data to a new file, replace the extension .tx8 with .dds
                string ddsPath = filePath.Replace(".tx8", ".dds");
                using (FileStream fs2 = new FileStream(ddsPath, FileMode.Create, FileAccess.Write))
                {
                    fs2.Write(data, 0, data.Length);
                }

                Console.WriteLine("Decrypted texture saved to: " + ddsPath);
            }
        }

        internal static void Decrypt(byte[] arr)
        {
            if (arr != null && arr.Length != 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = WrapToByteRange((arr[i] + 96));
                }
                return;
            }
        }

        internal static byte WrapToByteRange(int value)
        {
            if (value > 255)
            {
                return (byte)(value - 256);
            }
            if (value < 0)
            {
                return (byte)(value + 256);
            }
            return (byte)value;
        }
    }
}
