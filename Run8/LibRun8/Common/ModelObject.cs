using LibRun8.Formats;
using LibRun8.Utils;
using System.IO;
using System.Numerics;

namespace LibRun8.Common
{
    public class ModelObject
    {
        public string Name { get; set; }
        public string ParentName { get; set; }
        public Vector3 TranslationVector { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 UnkVec33 { get; set; }
        public Vector3 UnkVec31 { get; set; }
        public Quaternion UnkQuat1 { get; set; }
        public Quaternion UnkQuat2 { get; set; }
        public List<VertexStruct> Vertices { get; set; }
        public int[] Indices { get; set; }

        public ModelObject(BinaryReader reader, Model model)
        {
            if (model.IsAdvancedModel)
            {
                // do advanced loading
                //throw new NotImplementedException();

                Name = reader.ReadString();
                ParentName = reader.ReadString();

                Console.WriteLine("Name: " + Name);
                Console.WriteLine("ParentName: " + ParentName);

                UnkVec33 = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                UnkVec33 = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                UnkQuat1 = Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.Utils.DegreesToRadians(reader.ReadSingle()), Utils.Utils.DegreesToRadians(reader.ReadSingle()), Utils.Utils.DegreesToRadians(reader.ReadSingle())));
                Matrix.Scaling(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.Utils.DegreesToRadians(reader.ReadSingle()), Utils.Utils.DegreesToRadians(reader.ReadSingle()), Utils.Utils.DegreesToRadians(reader.ReadSingle())));
                Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.Utils.DegreesToRadians(reader.ReadSingle()), Utils.Utils.DegreesToRadians(reader.ReadSingle()), Utils.Utils.DegreesToRadians(reader.ReadSingle())));
                Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                UnkQuat2 = Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.Utils.DegreesToRadians(reader.ReadSingle()), Utils.Utils.DegreesToRadians(reader.ReadSingle()), Utils.Utils.DegreesToRadians(reader.ReadSingle())));
                Matrix.Scaling(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                int num3 = reader.ReadInt32();
                Matrix[] array = new Matrix[num3];
                for (int j = 0; j < num3; j++)
                {
                    //if (modelObject.class249_0 == null)
                    //{
                    //    modelObject.class249_0 = new Class249
                    //    {
                    //        quaternion_0 = new Quaternion[num3],
                    //        vector3_0 = new Vector3[num3]
                    //    };
                    //}
                    array[j] = new Matrix(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                }
                int num4 = reader.ReadInt32();
                Matrix[] array2 = new Matrix[num3];
                for (int k = 0; k < num4; k++)
                {
                    array2[k] = new Matrix(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                }

                // TODO: 112
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
                Vector3 pos = default;
                Vector3 normal = default;
                Vector2 texcoord = default;
                pos.X = reader.ReadSingle() * 63.7f - TranslationVector.X;
                normal.Y = reader.ReadSingle() / -1.732f;
                pos.Z = reader.ReadSingle() / 16f - TranslationVector.Z;
                texcoord.X = reader.ReadSingle() / 4.8f;
                normal.X = reader.ReadSingle() / 10.962f;
                reader.ReadSingle();
                normal.Z = reader.ReadSingle() / 11.432f;
                texcoord.Y = reader.ReadSingle() / 9.6f;
                pos.Y = reader.ReadSingle() * 6f - TranslationVector.Y;
                vertex.Position = pos;
                vertex.Normal = normal;
                vertex.TextureCoordinate = texcoord;
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
                Console.WriteLine("Texture " + i + ": " + s);
            }
            bool isUshortIndexBuffer = reader.ReadBoolean();

            int indexCount = reader.ReadInt32();
            Indices = new int[indexCount];
            for (int i = 0; i < indexCount; i++)
            {
                Indices[i] = reader.ReadInt32();
            }

            // TODO: calculate binormals and tangents

            int num5 = reader.ReadInt32() - 9;
            if (num5 == 0)
            {
                // TODO:
            }
            else
            {
                for (int i = 0; i < num5; i++)
                {
                    reader.ReadSingle();
                    int num13 = reader.ReadInt32();
                    if (textureNames.Length > 0)
                    {
                        // texture shit, 214
                        Console.WriteLine(textureNames[num13]);
                    }

                    int ic = reader.ReadInt32();
                    int sil = reader.ReadInt32();
                    int bvl = reader.ReadInt32();
                }
            }
        }
    }
}
