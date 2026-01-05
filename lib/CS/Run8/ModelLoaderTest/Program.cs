using LibRun8.Common;
using System.Text;

namespace ModelLoaderTest
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

                //Model model = Model.Read(inputFilePath);
                //model.WriteObj(inputFileWithoutExt + ".obj");
                Class678 @class = new Class678();
                @class.method_1(inputFilePath, Vector3.Zero);

                Console.WriteLine(@class.ObjectList.Count);

                for(int i = 0; i < @class.ObjectList.Count; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    Class252 obj = @class.ObjectList[i];
                    // write the vertex buffer
                    //sb.AppendLine("o " + obj.trackName);
                    
                    for(int j = 0; j < obj.list_0.Count; j++)
                    {
                        sb.AppendLine("o " + obj.string_0 + "_" + j);

                        Class141 inst = obj.list_0[j];

                        var indexCount = inst.IndexCountPerInstance;
                        var startIndexLocation = inst.StartIndexLocation;
                        var baseVertexLocation = inst.BaseVertexLocation;

                        for (int k = 0; k < indexCount; k++)
                        {
                            var index = obj.isUshortIndexBuffer ? obj.IndexBuffer1[startIndexLocation + k] : obj.IndexBuffer2[startIndexLocation + k];
                            var vertex = obj.VertexBuffer[index + baseVertexLocation];

                            sb.AppendLine("v " + vertex.Position.X + " " + vertex.Position.Y + " " + vertex.Position.Z);
                        }

                        for (int k = 0; k < indexCount; k += 3)
                        {
                            var index1 = obj.isUshortIndexBuffer ? obj.IndexBuffer1[startIndexLocation + k] : obj.IndexBuffer2[startIndexLocation + k];
                            var index2 = obj.isUshortIndexBuffer ? obj.IndexBuffer1[startIndexLocation + k + 1] : obj.IndexBuffer2[startIndexLocation + k + 1];
                            var index3 = obj.isUshortIndexBuffer ? obj.IndexBuffer1[startIndexLocation + k + 2] : obj.IndexBuffer2[startIndexLocation + k + 2];

                            sb.AppendLine("f " + (index1 + 1) + " " + (index2 + 1) + " " + (index3 + 1));
                        }
                    }

                    File.WriteAllText(obj.string_0 + ".obj", sb.ToString());
                }

                Console.WriteLine("Conversion of " + inputFileName + ": OK");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Conversion of " + inputFileName + ": FAILED");
                Console.WriteLine(ex.Message);
                return 1;
            }

            return 0;
        }
    }
}
