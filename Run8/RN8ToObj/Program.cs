using LibRun8.Common;
using LibRun8.Formats;
using System.Security.Claims;
using System.Text;

namespace RN8ToObj
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

            //try
            //{
            //    Model model = Model.Read(inputFilePath);
            //    model.WriteObj(inputFileWithoutExt + ".obj");
            //    Console.WriteLine("Conversion of " + inputFileName + ": OK");
            //    return 0;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Conversion of " + inputFileName + ": FAILED");
            //    Console.WriteLine(ex.Message);
            //    return 1;
            //}

            Model model = Model.Read(inputFilePath);
            Console.WriteLine(model.Objects.Count);

            for (int i = 0; i < model.Objects.Count; i++)
            {
                StringBuilder sb = new StringBuilder();
                ModelObject obj = model.Objects[i];
                // write the vertex buffer
                //sb.AppendLine("o " + obj.string_0);

                for (int j = 0; j < obj.ObjectDefinitions.Count; j++)
                {
                    sb.AppendLine("o " + obj.Name + "_" + j);

                    ModelObjectDefinition def = obj.ObjectDefinitions[j];

                    var indexCount = def.IndexCountPerInstance;
                    var startIndexLocation = def.StartIndexLocation;
                    var baseVertexLocation = def.BaseVertexLocation;

                    for (int k = 0; k < indexCount; k++)
                    {
                        var index = obj.Indices[startIndexLocation + k];
                        var vertex = obj.Vertices[index + baseVertexLocation];

                        sb.AppendLine("v " + vertex.Position.X + " " + vertex.Position.Y + " " + vertex.Position.Z);
                    }

                    for (int k = 0; k < indexCount; k += 3)
                    {
                        var index1 = obj.Indices[startIndexLocation + k];
                        var index2 = obj.Indices[startIndexLocation + k + 1];
                        var index3 = obj.Indices[startIndexLocation + k + 2];

                        sb.AppendLine("f " + (index1 + 1) + " " + (index2 + 1) + " " + (index3 + 1));
                    }
                }

                File.WriteAllText(obj.Name + ".obj", sb.ToString());
            }

            Console.WriteLine("Conversion of " + inputFileName + ": OK");
            return 0;
        }
    }
}
