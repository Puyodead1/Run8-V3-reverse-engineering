using Collada141;
using MoreLinq;
using MoreLinq.Experimental;
using System.Collections;
using System.ComponentModel;

namespace LibRun8.Collada
{
    public enum ColladaArrayType
    {
        [Description("positions")]
        Positions,
        [Description("normals")]
        Normals,
        [Description("uvs")]
        UVs
    }

    public enum ColladaSemantic
    {
        [Description("VERTEX")]
        Vertex,
        [Description("NORMAL")]
        Normal
    }


    public class ColladaUtils
    {
        public static string ConvertTrianglesToString(int[] points)
        {
            if (points.Length % 3 != 0)
                throw new ArgumentException("The number of points should be a multiple of 3.");

            return string.Join(" ", points);
        }

        public static string ConvertTrianglesToString(double[] points)
        {
            if (points.Length % 3 != 0)
                throw new ArgumentException("The number of points should be a multiple of 3.");

            return string.Join(" ", points);
        }

        public static string GetDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));

            return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
        }

        public static double[] IntToDoubleArray(int[] srcArray)
        {
            return srcArray.Select(x => (double)x).ToArray();
        }

        public static source CreateSource(string Id, ColladaArrayType arrayType, int[] value, int stride = 3)
        {
            string typeName = GetDescription(arrayType);

            float_array _float_array = new float_array();
            _float_array.id = string.Format("{0}-{1}-array", Id, typeName);
            _float_array.count = (ulong)value.Length;
            _float_array.Values = ColladaUtils.IntToDoubleArray(value);

            accessor _accessor = new accessor()
            {
                source = string.Format("#{0}-{1}-array", Id, typeName),
                count = (ulong)(value.Length / stride),
                stride = (ulong)stride,
                param = new param[] {
                    new param() {name="X", type="float"},
                    new param() {name="Y", type="float"},
                    new param() {name="Z", type="float"},
                }
            };

            sourceTechnique_common _techniqueCommon = new sourceTechnique_common()
            {
                accessor = _accessor
            };

            return new source()
            {
                id = string.Format("{0}-{1}", Id, typeName),
                Item = _float_array,
                technique_common = _techniqueCommon
            };
        }

        public static source CreateSource(string Id, ColladaArrayType arrayType, double[] value, int stride = 3)
        {
            string typeName = GetDescription(arrayType);

            float_array _float_array = new float_array();
            _float_array.id = string.Format("{0}-{1}-array", Id, typeName);
            _float_array.count = (ulong)value.Length;
            _float_array.Values = value;

            accessor _accessor = new accessor()
            {
                source = string.Format("#{0}-{1}-array", Id, typeName),
                count = (ulong)(value.Length / stride),
                stride = (ulong)stride,
                param = new param[] {
                    new param() {name="X", type="float"},
                    new param() {name="Y", type="float"},
                    new param() {name="Z", type="float"},
                }
            };

            sourceTechnique_common _techniqueCommon = new sourceTechnique_common()
            {
                accessor = _accessor
            };

            return new source()
            {
                id = string.Format("{0}-{1}", Id, typeName),
                Item = _float_array,
                technique_common = _techniqueCommon
            };
        }

        public static node[] ConvertSceneNodes(List<SceneNode> sceneNodes)
        {
            List<node> nodes = new List<node>();

            foreach (SceneNode sceneNode in sceneNodes)
            {
                node node = sceneNode.ToNode();
                nodes.Add(node);
            }

            return nodes.ToArray();
        }

        public static SceneNode? FindParent(List<SceneNode> sceneNodes, string parent)
        {
            foreach (SceneNode sceneNode in sceneNodes)
            {
                SceneNode? foundNode = FindParent(sceneNode.children, parent);
                if (foundNode != null)
                {
                    return foundNode;
                }

                if (sceneNode.id == parent || sceneNode.id == parent + "_0" || sceneNode.id == parent + "_1")
                {
                    return sceneNode;
                }
            }

            return null;
        }
    }

   
    public class ColladaGeometry
    {
        public readonly string Id;
        public readonly string Name;
        private readonly int PPV;

        private List<source> sources;
        private List<object> items;
        private List<InputLocalOffset> inputLocalOffsets;

        private vertices _vertices;

        public ColladaGeometry(string Id, string Name, int ppv = 3)
        {
            this.Id = Id;
            this.Name = Name;
            this.PPV = ppv;

            sources = new List<source>();
            items = new List<object>();
            inputLocalOffsets = new List<InputLocalOffset>();

            // -- verts input
            InputLocal vertsInput = new InputLocal()
            {
                semantic = "POSITION",
                source = string.Format("#{0}-positions", this.Id)
            };

            // -- vertices object
            _vertices = new vertices() { id = string.Format("{0}-vertices", this.Id), input = new InputLocal[] { vertsInput } };
        }

        public void AddSource(source src)
        {
            this.sources.Add(src);
        }

        public void AddSources(source[] srcs)
        {
            this.sources.AddRange(srcs);
        }

        public void AddTriangleInput(InputLocalOffset inputLocalOffset)
        {
            this.inputLocalOffsets.Add(inputLocalOffset);
        }

        public void AddTriangleInputs(InputLocalOffset[] inputLocalOffsets)
        {
            this.inputLocalOffsets.AddRange(inputLocalOffsets);
        }

        public void AddTriangleInput(ColladaSemantic semantic)
        {
            string semanticName = ColladaUtils.GetDescription(semantic);

            string type;

            if (semantic == ColladaSemantic.Vertex) type = "vertices";
            else if (semantic == ColladaSemantic.Normal) type = "normals";
            else throw new Exception("Unknown ColladaSemantic");

            this.inputLocalOffsets.Add(new InputLocalOffset() { semantic = semanticName, source = string.Format("#{0}-{1}", this.Id, type), offset = (ulong)this.inputLocalOffsets.Count });
        }

        public void AddTriangles(int[] indices)
        {
            triangles _triangles = new triangles()
            {
                count = (ulong)(indices.Length / this.PPV),
                input = this.inputLocalOffsets.ToArray(),
                p = ColladaUtils.ConvertTrianglesToString(indices)
            };

            this.items.Add(_triangles);
        }

        public void AddTriangles(double[] indices)
        {
            triangles _triangles = new triangles()
            {
                count = (ulong)((indices.Length / this.PPV) / this.PPV),
                input = this.inputLocalOffsets.ToArray(),
                p = ColladaUtils.ConvertTrianglesToString(indices)
            };

            this.items.Add(_triangles);
        }

        public geometry ToGeometry()
        {
            mesh _mesh = new mesh()
            {
                source = this.sources.ToArray(),
                vertices = this._vertices,
                Items = this.items.ToArray()
            };

            return new geometry
            {
                id = this.Id,
                name = this.Name,
                Item = _mesh
            };
        }
    }

    public class SceneNode
    {
        public string id;
        public node rootNode;
        public List<SceneNode> children;

        public SceneNode(string id, node root)
        {
            this.id = id;
            this.rootNode = root;
            this.children = new List<SceneNode>();
        }

        public bool HasChild(string id)
        {
            return this.children.Any(x => x.id == id);
        }

        public node ToNode()
        {
            node[] children = this.children.Select(x =>
            {
                return x.ToNode();
            }).ToArray();
            Console.WriteLine();
            this.rootNode.node1 = children;
            return this.rootNode;
        }
    }
}
