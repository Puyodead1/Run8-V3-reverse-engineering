namespace LibRun8.Util
{
    public class Texture
    {
        public static void Decrypt(FileInfo file)
        {
            if (!file.Exists)
                throw new FileNotFoundException("File not found: " + file);

            if (!file.FullName.EndsWith(".tx8"))
                throw new Exception("File is not a .tx8 file: " + file);

            Console.WriteLine("Decrypting texture '" + file.Name + "'...");

            using (FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] checksum = new byte[32];
                byte[] data = new byte[fs.Length - 32L];

                fs.Read(data, 0, (int)fs.Length - 32);
                fs.Read(checksum, 0, 32);

                DecryptData(data);
                DecryptData(checksum);

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
                string ddsPath = file.FullName.Replace(".tx8", ".dds");
                using (FileStream fs2 = new FileStream(ddsPath, FileMode.Create, FileAccess.Write))
                {
                    fs2.Write(data, 0, data.Length);
                }

                Console.WriteLine("Decrypted texture saved to: " + ddsPath);
            }
        }

        public static void Encrypt(FileInfo file)
        {
            if (!file.Exists)
                throw new FileNotFoundException("File not found: " + file);

            if (!file.FullName.EndsWith(".dds"))
                throw new Exception("Only DDS files are supported for encryption!");

            Console.WriteLine("Encrypting texture '" + file.Name + "'...");

            using (FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] data = new byte[fs.Length];

                fs.Read(data, 0, (int)fs.Length);

                // calculate the checksum of the data
                byte[] checksum = Utils.ComputeMD5ForBytes(data);

                EncryptData(data);
                EncryptData(checksum);

                // concat the data and the checksum
                byte[] encryptedData = new byte[data.Length + checksum.Length];
                Array.Copy(data, 0, encryptedData, 0, data.Length);
                Array.Copy(checksum, 0, encryptedData, data.Length, checksum.Length);
                
                // write the encrypted data to a new file, replace the extension .dds with .tx8
                string tx8Path = file.FullName.Replace(".dds", ".tx8");
                using (FileStream fs2 = new FileStream(tx8Path, FileMode.Create, FileAccess.Write))
                {
                    fs2.Write(encryptedData, 0, encryptedData.Length);
                }

                Console.WriteLine("Encrypted texture saved to: " + tx8Path);
            }
        }

        internal static void DecryptData(byte[] arr)
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

        internal static void EncryptData(byte[] arr)
        {
            if (arr != null && arr.Length != 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = WrapToByteRange((arr[i] - 96));
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
