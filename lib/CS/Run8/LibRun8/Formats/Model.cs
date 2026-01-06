using LibRun8.Common;
using SharpGLTF.Geometry;
using SharpGLTF.Geometry.VertexTypes;
using SharpGLTF.Materials;
using SharpGLTF.Memory;
using SharpGLTF.Schema2;
using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

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

        public void WriteGLTF(string path)
        {
            var model = ModelRoot.CreateModel();
            var scene = model.UseScene("Scene");

            Dictionary<string, Material> materialMap = new();
            List<(string id, Mesh mesh, System.Numerics.Matrix4x4 transform, string? parent)> defNodes = new();
            Dictionary<string, (Vector3 position, string? parent)> allObjects = new();

            for (int i = 0; i < this.Objects.Count; i++)
            {
                ModelObject obj = this.Objects[i];

                string id = obj.Name;
                if (string.IsNullOrEmpty(id))
                    id = this.ObjectCount == 1 ? this.fileName : $"o_{i}";

                id = id.Replace(" ", "_");
                string parentId = string.IsNullOrEmpty(obj.ParentName) ? null : obj.ParentName.Replace(" ", "_");

                if (obj.ObjectDefinitions.Count == 0 || obj.ObjectDefinitions.Count > 1)
                {
                    Vector3 objectPosition = (obj.ObjectDefinitions.Count > 1 && obj.class252_0 != null && obj.class252_0.vector3_0.Length > 0)
                        ? obj.class252_0.vector3_0[0]
                        : obj.Position;
                    allObjects[id] = (objectPosition, parentId);
                }

                foreach (var tex in obj.Textures)
                {
                    if (!materialMap.ContainsKey(tex))
                    {
                        var mat = model.CreateMaterial(tex);
                        materialMap[tex] = mat;
                    }
                }

                for (int j = 0; j < obj.ObjectDefinitions.Count; j++)
                {
                    ModelObjectDefinition def = obj.ObjectDefinitions[j];

                    string thisId = (obj.ObjectDefinitions.Count > 1) ? $"{id}_{j}" : id;

                    int startVertex = def.BaseVertexLocation;
                    int startIndex = def.StartIndexLocation;
                    int indexCount = def.IndexCountPerInstance;

                    var positions = obj.Vertices.Skip(startVertex)
                        .Take(obj.Vertices.Count - startVertex)
                        .Select(v => new Vector3(v.Position.X, v.Position.Y, v.Position.Z))
                        .ToArray();

                    var normals = obj.Vertices.Skip(startVertex)
                        .Take(obj.Vertices.Count - startVertex)
                        .Select(v => new Vector3(v.Normal.X, v.Normal.Y, v.Normal.Z))
                        .ToArray();

                    var uvs = obj.Vertices.Skip(startVertex)
                        .Take(obj.Vertices.Count - startVertex)
                        .Select(v => new Vector2(v.TextureCoordinate.X, v.TextureCoordinate.Y))
                        .ToArray();

                    var indices = obj.Indices.Skip(startIndex).Take(indexCount).ToArray();

                    for (int k = 0; k < indices.Length; k += 3)
                    {
                        (indices[k + 1], indices[k + 2]) = (indices[k + 2], indices[k + 1]);
                    }

                    var mesh = model.CreateMesh(thisId);
                    var prim = mesh.CreatePrimitive();

                    if (def.texture2D_0 != null)
                    {
                        Material? gltfMat = materialMap[def.texture2D_0];
                        if (gltfMat != null)
                        {
                            prim.WithMaterial(gltfMat);
                        }
                    }

                    {
                        int bytes = positions.Length * 12;
                        var view = model.CreateBufferView(bytes, 0, BufferMode.ARRAY_BUFFER);
                        MemoryMarshal.AsBytes(positions.AsSpan()).CopyTo(view.Content);
                        var acc = model.CreateAccessor();
                        acc.SetVertexData(view, 0, positions.Length, AttributeFormat.Float3);
                        prim.SetVertexAccessor("POSITION", acc);
                    }

                    {
                        int bytes = normals.Length * 12;
                        var view = model.CreateBufferView(bytes, 0, BufferMode.ARRAY_BUFFER);
                        MemoryMarshal.AsBytes(normals.AsSpan()).CopyTo(view.Content);
                        var acc = model.CreateAccessor();
                        acc.SetVertexData(view, 0, normals.Length, AttributeFormat.Float3);
                        prim.SetVertexAccessor("NORMAL", acc);
                    }

                    {
                        int bytes = uvs.Length * 8;
                        var view = model.CreateBufferView(bytes, 0, BufferMode.ARRAY_BUFFER);
                        MemoryMarshal.AsBytes(uvs.AsSpan()).CopyTo(view.Content);
                        var acc = model.CreateAccessor();
                        acc.SetVertexData(view, 0, uvs.Length, AttributeFormat.Float2);
                        prim.SetVertexAccessor("TEXCOORD_0", acc);
                    }

                    {
                        int bytes = indices.Length * sizeof(int);
                        var view = model.CreateBufferView(bytes, 0, BufferMode.ELEMENT_ARRAY_BUFFER);
                        MemoryMarshal.AsBytes(indices.AsSpan()).CopyTo(view.Content);
                        var acc = model.CreateAccessor();
                        acc.SetIndexData(view, 0, indices.Length, IndexEncodingType.UNSIGNED_INT);
                        prim.SetIndexAccessor(acc);
                    }

                    Vector3 offset;
                    string meshParent;

                    if (obj.ObjectDefinitions.Count > 1)
                    {
                        offset = Vector3.Zero;
                        meshParent = id;
                    }
                    else
                    {
                        offset = (obj.class252_0 != null && j < obj.class252_0.vector3_0.Length)
                            ? obj.class252_0.vector3_0[j]
                            : obj.Position;
                        meshParent = parentId;
                    }

                    System.Numerics.Matrix4x4 transform = System.Numerics.Matrix4x4.CreateTranslation(offset.X, offset.Y, -offset.Z);
                    defNodes.Add((thisId, mesh, transform, meshParent));
                }
            }

            Dictionary<string, Node> nodeMap = new();
            var processed = new HashSet<string>();

            void ProcessNode(string id, bool createEmpty = false)
            {
                if (processed.Contains(id)) return;

                var nodeInfo = defNodes.FirstOrDefault(n => n.id == id);
                bool hasMesh = nodeInfo != default;

                string parent = null;
                Vector3 position = Vector3.Zero;

                if (hasMesh)
                {
                    parent = nodeInfo.parent;
                }
                else if (allObjects.ContainsKey(id))
                {
                    (position, parent) = allObjects[id];
                }
                else
                {
                    return;
                }

                if (!string.IsNullOrEmpty(parent))
                {
                    ProcessNode(parent, createEmpty: true);
                }

                if (string.IsNullOrEmpty(parent) || !nodeMap.ContainsKey(parent))
                {
                    var node = scene.CreateNode(id);
                    if (hasMesh)
                    {
                        node.WithMesh(nodeInfo.mesh);
                        node.LocalMatrix = nodeInfo.transform;
                    }
                    else
                    {
                        var transform = System.Numerics.Matrix4x4.CreateTranslation(position.X, position.Y, -position.Z);
                        node.LocalMatrix = transform;
                    }
                    nodeMap[id] = node;
                }
                else
                {
                    var parentNode = nodeMap[parent];
                    var childNode = parentNode.CreateNode(id);

                    if (hasMesh)
                    {
                        childNode.WithMesh(nodeInfo.mesh);
                        childNode.LocalMatrix = nodeInfo.transform;
                    }
                    else
                    {
                        var transform = System.Numerics.Matrix4x4.CreateTranslation(position.X, position.Y, -position.Z);
                        childNode.LocalMatrix = transform;
                    }
                    nodeMap[id] = childNode;
                }

                processed.Add(id);
            }

            var parentIds = defNodes.Select(n => n.parent).Where(p => !string.IsNullOrEmpty(p)).Distinct();
            foreach (var (id, _, transform, parent) in defNodes)
            {
                var translation = new Vector3(transform.M41, transform.M42, transform.M43);
                Console.WriteLine($"defNode: '{id}' parent='{parent ?? "NULL"}' offset=({translation.X}, {translation.Y}, {translation.Z})");
            }
            foreach (var parentId in parentIds)
            {
                if (!defNodes.Any(n => n.id == parentId))
                {
                    ProcessNode(parentId, createEmpty: true);
                }
            }


            foreach (var (id, _, _, _) in defNodes)
            {
                ProcessNode(id);
            }

            model.SaveGLB(path);
        }

        public void ReadGLTF(string path)
        {
            var model = ModelRoot.Load(path);
            var scene = model.DefaultScene;

            this.Objects = new List<ModelObject>();

            Dictionary<string, ModelObject> objectMap = new();

            void ProcessNode(Node node, string parentName = null)
            {
                string nodeName = node.Name;
                if (string.IsNullOrEmpty(nodeName))
                    nodeName = $"node_{node.LogicalIndex}";

                var mesh = node.Mesh;
                bool hasMesh = mesh != null;

                var localMatrix = node.LocalMatrix;
                var translation = localMatrix.Translation;
                Vector3 position = new Vector3(translation.X, translation.Y, -translation.Z);

                if (hasMesh)
                {
                    foreach (var primitive in mesh.Primitives)
                    {
                        ModelObject obj = new ModelObject
                        {
                            Name = nodeName,
                            ParentName = parentName ?? "",
                            Position = position,
                            TranslationVector = Vector3.Zero,
                            Vertices = new List<VertexStruct>(),
                            Indices = Array.Empty<uint>(),
                            ObjectDefinitions = new List<ModelObjectDefinition>(),
                            Textures = Array.Empty<string>()
                        };

                        var positions = primitive.GetVertexAccessor("POSITION")?.AsVector3Array();
                        var normals = primitive.GetVertexAccessor("NORMAL")?.AsVector3Array();
                        var uvs = primitive.GetVertexAccessor("TEXCOORD_0")?.AsVector2Array();
                        var indices = primitive.GetIndices();

                        if (positions != null)
                        {
                            for (int i = 0; i < positions.Count; i++)
                            {
                                var pos = positions[i];
                                var normal = normals != null && i < normals.Count ? normals[i] : System.Numerics.Vector3.Zero;
                                var uv = uvs != null && i < uvs.Count ? uvs[i] : System.Numerics.Vector2.Zero;

                                obj.Vertices.Add(new VertexStruct
                                {
                                    Position = new Vector3(pos.X, pos.Y, pos.Z),
                                    Normal = new Vector3(normal.X, normal.Y, normal.Z),
                                    TextureCoordinate = new Vector2(uv.X, uv.Y),
                                    Binormal = Vector3.Zero,
                                    Tangent = Vector3.Zero
                                });
                            }
                        }

                        if (indices != null)
                        {
                            var indexList = indices.ToList();
                            for (int i = 0; i < indexList.Count; i += 3)
                            {
                                if (i + 2 < indexList.Count)
                                {
                                    (indexList[i + 1], indexList[i + 2]) = (indexList[i + 2], indexList[i + 1]);
                                }
                            }
                            obj.Indices = indexList.ToArray();
                        }

                        var material = primitive.Material;
                        if (material != null)
                        {
                            obj.Textures = new[] { material.Name ?? "default" };
                        }

                        ModelObjectDefinition def = new ModelObjectDefinition
                        {
                            IndexCountPerInstance = obj.Indices.Length,
                            BaseVertexLocation = 0,
                            StartIndexLocation = 0,
                            texture2D_0 = obj.Textures.Length > 0 ? obj.Textures[0] : null
                        };
                        obj.ObjectDefinitions.Add(def);

                        this.Objects.Add(obj);
                        objectMap[nodeName] = obj;
                    }
                }
                else
                {
                    ModelObject emptyObj = new ModelObject
                    {
                        Name = nodeName,
                        ParentName = parentName ?? "",
                        Position = position,
                        TranslationVector = Vector3.Zero,
                        Vertices = new List<VertexStruct>(),
                        Indices = Array.Empty<uint>(),
                        ObjectDefinitions = new List<ModelObjectDefinition>(),
                        Textures = Array.Empty<string>()
                    };

                    this.Objects.Add(emptyObj);
                    objectMap[nodeName] = emptyObj;
                }

                foreach (var child in node.VisualChildren)
                {
                    ProcessNode(child, nodeName);
                }
            }

            foreach (var rootNode in scene.VisualChildren)
            {
                ProcessNode(rootNode);
            }

            this.ObjectCount = this.Objects.Count;
        }
    }
}
