using Collada141;
using LibRun8.Collada;
using LibRun8.Common;
using LibRun8.Formats;
using Newtonsoft.Json;
using System.Numerics;

namespace RN8ToDae
{
    internal class Program
    {
        public static JsonSerializerSettings options = new JsonSerializerSettings { Formatting = Newtonsoft.Json.Formatting.Indented };


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

            //foreach (ModelObject obj in model.Objects)
            //{
            //    obj.Vertices.Clear();
            //    obj.Indices = new int[] { };
            //}
            //string jsonString = JsonConvert.SerializeObject(model, options);
            //File.WriteAllText("Model.json", jsonString);

            COLLADA dae = new COLLADA();

            List<geometry> geometries = new List<geometry>();
            List<SceneNode> sceneNodes = new List<SceneNode>();

            List<DanglingSceneNode> danglingSceneNodes = new List<DanglingSceneNode>();

            Console.WriteLine("There are {0} objects", model.ObjectCount);

            // TODO: properly handle object definitions
            for (int i = 0; i < model.Objects.Count; i++)
            {
                ModelObject obj = model.Objects[i];


                string id = obj.Name;
                if (string.IsNullOrEmpty(id))
                {
                    if (model.ObjectCount == 1) id = inputFileWithoutExt;
                    else id = "o_" + i;
                }

                id = id.Replace(" ", "_");

                Console.WriteLine("Model {0} has {1} definitions", id, obj.ObjectDefinitions.Count);

                string parent = obj.ParentName;
                if (!string.IsNullOrEmpty(parent)) parent = parent.Replace(" ", "_");

                List<SceneNode> defNodes = new List<SceneNode>();

                for (int j = 0; j < obj.ObjectDefinitions.Count; j++)
                {
                    string thisId = id;
                    ModelObjectDefinition definition = obj.ObjectDefinitions[j];

                    if (obj.ObjectDefinitions.Count > 1)
                        thisId = string.Format("{0}_{1}", id, j);
                    

                    var startVertex = definition.BaseVertexLocation;
                    var startIndex = definition.StartIndexLocation;
                    var indexCount = definition.IndexCountPerInstance;

                    ColladaGeometry geoBuilder = new ColladaGeometry(thisId, thisId);

                    double[] positions = obj.Vertices.Skip(startVertex)
                        .SelectMany(v => new double[] { v.Position.X, -v.Position.Z, v.Position.Y })
                        .ToArray();

                    double[] normals = obj.Vertices.Skip(startVertex)
                        .SelectMany(v => new double[] { v.Normal.X, -v.Normal.Z, v.Normal.Y })
                        .ToArray();

                    double[] uvs = obj.Vertices.Skip(startVertex)
                        .SelectMany(v => new double[] { v.TextureCoordinate.X, -v.TextureCoordinate.Y })
                        .ToArray();

                    int[] indices = obj.Indices.ToList().GetRange(startIndex, indexCount).SelectMany(x => new int[] { x, x, x }).ToArray();

                    // create the sources
                    source pos_source = ColladaUtils.CreateSource(geoBuilder.Id, ColladaArrayType.Positions, positions);
                    source normals_source = ColladaUtils.CreateSource(geoBuilder.Id, ColladaArrayType.Normals, normals);
                    source uvs_source = ColladaUtils.CreateSource(geoBuilder.Id, ColladaArrayType.UVs, uvs, 2);

                    // add the sources to the geometry
                    geoBuilder.AddSources(new source[] { pos_source });
                    geoBuilder.AddSources(new source[] { normals_source });
                    geoBuilder.AddSources(new source[] { uvs_source });

                    // add triangle input offsets
                    geoBuilder.AddTriangleInput(ColladaSemantic.Vertex);
                    geoBuilder.AddTriangleInput(ColladaSemantic.Normal);
                    geoBuilder.AddTriangleInput(ColladaSemantic.UV);

                    // add triangle
                    geoBuilder.AddTriangles(indices);

                    geometries.Add(geoBuilder.ToGeometry());

                    instance_geometry nodeGeo = new instance_geometry()
                    {
                        url = string.Format("#{0}", geoBuilder.Id),
                    };

                    LibRun8.Common.Vector3 offset;
                    if (obj.class252_0 != null)
                    {
                        if (j >= obj.class252_0.vector3_0.Length) 
                            offset = obj.class252_0.vector3_0.Last();
                        else
                            offset = obj.class252_0.vector3_0[j];

                    }
                    else
                    {
                        offset = obj.Position;
                    }

                    Matrix transformMatrix = Matrix.Identity;
                    transformMatrix.M14 = offset.X;
                    transformMatrix.M24 = offset.Z;
                    transformMatrix.M34 = offset.Y;

                    double[] transform = transformMatrix.ToArray().Select(x => (double)x).ToArray();

                    matrix nodeMatrix = new matrix
                    {
                        sid = "transform",
                        Values = transform
                    };

                    node defNode = new node()
                    {
                        id = thisId,
                        name = thisId,
                        Items = new object[] { nodeMatrix },
                        ItemsElementName = new ItemsChoiceType2[] { ItemsChoiceType2.matrix },
                        instance_geometry = new instance_geometry[] { nodeGeo }
                    };

                    defNodes.Add(new SceneNode(thisId, defNode));
                }

                SceneNode sceneNode;

                if(defNodes.Count > 1)
                {
                    node emptyNode = new node
                    {
                        id = id,
                        name = id,
                    };

                    sceneNode = new SceneNode(id, emptyNode);
                    sceneNode.children.AddRange(defNodes);
                }
                else
                {
                    sceneNode = defNodes.First();
                }

                if (string.IsNullOrEmpty(parent) && !sceneNodes.Any(x => x.id == id))
                {
                    sceneNodes.Add(sceneNode);
                }
                else if (!string.IsNullOrEmpty(parent))
                {
                    SceneNode? parentNode = ColladaUtils.FindParent(sceneNodes, parent);
                    if (parentNode == null)
                    {
                        Console.WriteLine(string.Format("Failed to find parent {0} for {1}", parent, id));
                        continue;
                    }

                    parentNode.children.Add(sceneNode);
                }
            }

            library_geometries lgeo = new library_geometries()
            {
                geometry = geometries.ToArray()
            };

            visual_scene vscene = new visual_scene()
            {
                id = "Scene",
                name = "Scene",
                node = ColladaUtils.ConvertSceneNodes(sceneNodes)
            };

            library_visual_scenes lscene = new library_visual_scenes() { visual_scene = new visual_scene[] { vscene } };

            COLLADAScene scene_obj = new COLLADAScene()
            {
                instance_visual_scene = new InstanceWithExtra()
                {
                    url = "#Scene"
                }
            };

            dae.Items = new object[] { lgeo, lscene };
            dae.scene = scene_obj;

            dae.Save(inputFileWithoutExt + ".dae");

            Console.WriteLine("Conversion of " + inputFileName + ": OK");
            return 0;
        }

        public static void ResolveParents(List<DanglingSceneNode> danglingNodes, List<SceneNode> sceneNodes)
        {
            int maxTries = 3;
            int attempts = 0;

            while (danglingNodes.Count > 0 && attempts < maxTries)
            {
                List<DanglingSceneNode> toRemove = new List<DanglingSceneNode>();

                foreach (var danglingNode in danglingNodes)
                {
                    SceneNode? parentNode = ColladaUtils.FindParent(sceneNodes, danglingNode.parent);

                    if (parentNode != null)
                    {
                        Console.WriteLine("+ Parent {0} resolved for {1} (attempt {2})", danglingNode.parent, danglingNode.id, attempts);
                        toRemove.Add(danglingNode);
                    } 
                    else
                    {
                        Console.WriteLine("- Could not find parent {0} for {1} (attempt {2})", danglingNode.parent, danglingNode.id, attempts);
                    }
                }

                foreach (var node in toRemove)
                {
                    danglingNodes.Remove(node);
                }

                attempts++;
            }
        }
    }

    struct DanglingSceneNode
    {
        public string id;
        public string parent;
        public node node;
        public int tries;
    }
}
