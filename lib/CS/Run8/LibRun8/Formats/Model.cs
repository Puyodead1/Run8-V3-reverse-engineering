using Collada141;
using LibRun8.Collada;
using LibRun8.Common;

namespace LibRun8.Formats
{
    public class Model : FileFormat
    {
        public string? fileName;
        public int ObjectCount { get; set; } = 1;
        public Vector3 UnkVec3 { get; set; }  = Vector3.Zero;
        public Matrix UnkMatrix { get; set; } = Matrix.Identity;
        public bool IsAdvancedModel { get; set; } = false;
        public List<ModelObject> Objects { get; set; } = new List<ModelObject>();
        public float BoundingRadius { get; set; } = 0f;

        public static Model Read(Stream stream)
        {
            Model item = new Model();

            using (BinaryReader reader = new BinaryReader(stream))
            {
                // read the "Type"
                int type = reader.ReadInt32();

                if (type == -969696)
                {
                    item.ObjectCount = reader.ReadInt32();
                    item.IsAdvancedModel = true;
                }
                else if (type == -969697)
                {
                    item.ObjectCount = reader.ReadInt32();
                    item.UnkVec3 = Vector3.Read(reader);
                    item.IsAdvancedModel = true;
                }
                else
                {
                    reader.BaseStream.Position = 0;
                }

                item.Objects = new List<ModelObject>(item.ObjectCount);

                for (int i = 0; i < item.ObjectCount; i++)
                {
                    ModelObject obj = new ModelObject(reader, item);
                    item.Objects.Add(obj);
                }

                foreach (ModelObject obj in item.Objects)
                {
                    if (!string.IsNullOrEmpty(obj.ParentName))
                    {
                        obj.ParentObject = item.Objects.Find(x => x.Name.Contains(obj.ParentName));
                    }

                    string parentNameLower = obj.ParentName.ToLower();
                }
            }

            return item;
        }

        public static Model Read(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                Model model = Read(fileStream);
                model.fileName = Path.GetFileNameWithoutExtension(path);
                return model;
            }
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }

        public void WriteCollada(string path)
        {
            COLLADA dae = new COLLADA();

            List<geometry> geometries = new List<geometry>();
            List<SceneNode> sceneNodes = new List<SceneNode>();

            // TODO: properly handle object definitions
            for (int i = 0; i < this.Objects.Count; i++)
            {
                ModelObject obj = this.Objects[i];


                string id = obj.Name;
                if (string.IsNullOrEmpty(id))
                {
                    if (this.ObjectCount == 1) id = this.fileName;
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

                if (defNodes.Count > 1)
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

            dae.Save(path);
        }
    }
}
