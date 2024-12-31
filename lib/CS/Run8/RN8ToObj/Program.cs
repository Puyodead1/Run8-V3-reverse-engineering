using LibRun8.Formats;

namespace RN8ToDae
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No file specified");
                return 1;
            }

            string inputFilePath = args[0];
            string inputFileName, inputFileExt, inputFileWithoutExt;

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("File does not exist: " + inputFilePath);
                return 1;
            }

            inputFileName = Path.GetFileName(inputFilePath);
            inputFileExt = Path.GetExtension(inputFilePath);
            inputFileWithoutExt = Path.GetFileNameWithoutExtension(inputFilePath);

            if (inputFileExt.ToLower() != ".rn8")
            {
                Console.WriteLine("File is not a .rn8 file: " + inputFilePath);
                return 1;
            }

            try
            {
                Model model = Model.Read(inputFilePath);
                model.WriteCollada(inputFileWithoutExt + ".dae");
                Console.WriteLine("Conversion of " + inputFileName + ": OK");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Conversion of " + inputFileName + ": FAILED");
                Console.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}
