using LibRun8.Formats;
using LibRun8.Util;

namespace LibRun8.Common
{
    public class ModelObject
    {
        public string Name { get; set; }
        public string ParentName { get; set; }
        public Vector3 TranslationVector { get; set; } = Vector3.Zero;
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 UnkVec31 { get; set; } = Vector3.Zero;
        public Quaternion UnkQuat0 { get; set; } = Quaternion.Identity;
        public Matrix UnusedScalingMatrix0 { get; set; }
        public Quaternion UnusedRotationMatrix0 { get; set; }
        public Quaternion UnusedRotationMatrix1 { get; set; }
        public Quaternion UnkQuat1 { get; set; } = Quaternion.Identity;
        public Matrix UnusedScalingMatrix1 { get; set; }
        public List<VertexStruct> Vertices;
        public int[] Indices;
        public List<ModelObjectDefinition> ObjectDefinitions { get; set; } = new List<ModelObjectDefinition>();
        public Class252 class252_0 { get; set; }
        public ModelObject ParentObject { get; set; }
        public Vector3 UnkVec30 { get; set; } = Vector3.Zero;
        public Quaternion UnkQuat2 { get; set; } = Quaternion.Identity;
        public bool bool_0 { get; set; }

        public ModelObject(BinaryReader reader, Model model)
        {
            if (model.IsAdvancedModel)
            {
                Name = reader.ReadString();
                ParentName = reader.ReadString();

                Console.WriteLine("Name: " + Name);
                Console.WriteLine("ParentName: " + ParentName);

                TranslationVector = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                UnkVec31 = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                UnkQuat0 = Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.DegreesToRadians(reader.ReadSingle()), Utils.DegreesToRadians(reader.ReadSingle()), Utils.DegreesToRadians(reader.ReadSingle())));
                UnusedScalingMatrix0 = Matrix.Scaling(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                UnusedRotationMatrix0 = Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.DegreesToRadians(reader.ReadSingle()), Utils.DegreesToRadians(reader.ReadSingle()), Utils.DegreesToRadians(reader.ReadSingle())));
                UnusedRotationMatrix1 = Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.DegreesToRadians(reader.ReadSingle()), Utils.DegreesToRadians(reader.ReadSingle()), Utils.DegreesToRadians(reader.ReadSingle())));
                Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                UnkQuat1 = Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.DegreesToRadians(reader.ReadSingle()), Utils.DegreesToRadians(reader.ReadSingle()), Utils.DegreesToRadians(reader.ReadSingle())));
                UnusedScalingMatrix1 = Matrix.Scaling(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                int num3 = reader.ReadInt32();
                Matrix[] array = new Matrix[num3];
                for (int j = 0; j < num3; j++)
                {
                    if (this.class252_0 == null)
                    {
                        this.class252_0 = new Class252
                        {
                            quaternion_0 = new Quaternion[num3],
                            vector3_0 = new Vector3[num3]
                        };
                    }
                    array[j] = new Matrix(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                }
                int num4 = reader.ReadInt32();
                Matrix[] array2 = new Matrix[num3];
                for (int k = 0; k < num4; k++)
                {
                    array2[k] = new Matrix(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                }

                if (num4 != num3)
                {
                    this.class252_0 = null;
                }
                else
                {
                    for (int l = 0; l < num4; l++)
                    {
                        this.class252_0.quaternion_0[l] = Quaternion.RotationMatrix(array2[l]);
                        this.class252_0.vector3_0[l] = array[l].TranslationVector;
                    }
                }
            }
            else
            {
                Name = "";
                ParentName = "";
                TranslationVector = Vector3.Zero;
            }
            int vertexCount = reader.ReadInt32() / 7;
            Vertices = new List<VertexStruct>(vertexCount);
            for (int i = 0; i < vertexCount; i++)
            {
                VertexStruct vertex = default;
                reader.ReadSingle();
                Vector3 position = default;
                Vector3 normal = default;
                Vector2 uv = default;
                position.X = reader.ReadSingle() * 63.7f - TranslationVector.X;
                normal.Y = reader.ReadSingle() / -1.732f;
                position.Z = reader.ReadSingle() / 16f - TranslationVector.Z;
                uv.X = reader.ReadSingle() / 4.8f;
                normal.X = reader.ReadSingle() / 10.962f;
                reader.ReadSingle();
                normal.Z = reader.ReadSingle() / 11.432f;
                uv.Y = reader.ReadSingle() / 9.6f;
                position.Y = -reader.ReadSingle() * 6f - TranslationVector.Y;

                vertex.Position = position;
                vertex.Normal = normal;
                vertex.TextureCoordinate = uv;
                vertex.Binormal = Vector3.Zero;
                vertex.Tangent = Vector3.Zero;
                Vertices.Add(vertex);
                float num = Math.Max(Math.Abs(vertex.Position.X), Math.Max(Math.Abs(vertex.Position.Y), Math.Abs(vertex.Position.Z)));
                if (num > model.BoundingRadius)
                {
                    model.BoundingRadius = num;
                }
            }
            int textureCount = reader.ReadInt32() + 6;
            string[] textureNames = new string[textureCount];
            for (int i = 0; i < textureCount; i++)
            {
                string s = reader.ReadString();
                textureNames[i] = s;
                Console.WriteLine("\tTexture " + i + ": " + s);
            }
            bool isUshortIndexBuffer = reader.ReadBoolean();

            int indexCount = reader.ReadInt32();

            Indices = new int[indexCount];
            for (int i = 0; i < indexCount; i++)
            {
                int a = reader.ReadInt32();
                Indices[i] = a;
            }


            // TODO: calculate binormals and tangents

            int num5 = reader.ReadInt32() - 9;
            bool flag = false;
            if (num5 == 0)
            {
                ModelObjectDefinition class2 = new ModelObjectDefinition
                {
                    IndexCountPerInstance = Indices.Length,
                    BaseVertexLocation = 0,
                    StartIndexLocation = 0
                };
                ObjectDefinitions.Add(class2);
            }
            else
            {
                for (int i = 0; i < num5; i++)
                {
                    ModelObjectDefinition def = new ModelObjectDefinition();
                    float unusedFloat0 = reader.ReadSingle();
                    int texIndex = reader.ReadInt32();
                    if (textureNames.Length > 0)
                    {
                        def.texture2D_0 = textureNames[texIndex];
                        flag |= def.texture2D_2 != null;
                    }

                    def.IndexCountPerInstance = reader.ReadInt32();
                    def.StartIndexLocation = reader.ReadInt32();
                    def.BaseVertexLocation = reader.ReadInt32();
                    ObjectDefinitions.Add(def);
                }
            }
        }
    }
}
