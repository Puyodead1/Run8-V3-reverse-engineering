using LibRun8.Util;
using System.CommandLine;

namespace TextureTools
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("Run8 Texture Tools");
            var decryptCommand = new Command("decrypt", "Convert tx8 to dds");
            var encryptCommand = new Command("encrypt", "Convert dds to tx8");

            var fileArgument = new Argument<FileInfo>("file", "File to convert");
            decryptCommand.Add(fileArgument);
            encryptCommand.Add(fileArgument);

            rootCommand.Add(decryptCommand);
            rootCommand.Add(encryptCommand);

            decryptCommand.SetHandler((file) =>
            {
                try
                {
                    Decrypt(file);
                } catch(Exception e)
                {
                    Console.WriteLine("Decryption of file '" + file.Name + "' failed: " + e.Message);
                }
            },
            fileArgument);

            encryptCommand.SetHandler((file) =>
            {
                try
                {
                    Encrypt(file);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Encryption of file '" + file.Name + "' failed: " + e.Message);
                }
            },
            fileArgument);

            return await rootCommand.InvokeAsync(args);
        }

        private static void Decrypt(FileInfo file)
        {
            Texture.Decrypt(file);
        }

        private static void Encrypt(FileInfo file)
        {
            Texture.Encrypt(file);
        }
    }
}
