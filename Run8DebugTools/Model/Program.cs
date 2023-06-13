using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using JeremyAnsel.Media.WavefrontObj;
using MoreLinq;
using Run8Utils;
using Quaternion = Run8Utils.Quaternion;

namespace Model
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run8 Model Parser");

            if (args.Length == 0)
            {
                Console.WriteLine("no file specified.");
                return;
            }

            string path = args[0];

            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist");
                return;
            }

            string directoryName = Path.GetDirectoryName(path);

            Matrix matrix_0 = Matrix.Identity;
            Vector3 vector3_1 = Vector3.Zero;
            Vector3 vector3_0 = vector3_1;
            List<Model> list_0;
            float BoundingSphereRadius;
            Object object_0 = new object();

            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    bool flag = false;
                    int num = 1;
                    int num2 = binaryReader.ReadInt32();
                    if (num2 == -969696)
                    {
                        num = binaryReader.ReadInt32();
                        flag = true;
                    }
                    else if (num2 == -969697)
                    {
                        num = binaryReader.ReadInt32();
                        vector3_0 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        vector3_1 = Vector3.Zero;
                        flag = true;
                    }
                    else
                    {
                        binaryReader.BaseStream.Position = 0L;
                    }
                    Console.WriteLine("Model Count: " + num);
                    list_0 = new List<Model>(num);
                    BoundingSphereRadius = 0f;
                    for (int i = 0; i < num; i++)
                    {
                        Model model = new Model();
                        if (flag)
                        {
                            model.name = binaryReader.ReadString();
                            Console.WriteLine("Object Name: " + model.name);
                            model.parentName = binaryReader.ReadString();
                            Console.WriteLine("Parent Object Name: " + model.parentName);
                            model.vector3_3 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            model.vector3_1 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            model.quaternion_1 = Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(MathUtil.DegreesToRadians(binaryReader.ReadSingle()), MathUtil.DegreesToRadians(binaryReader.ReadSingle()), MathUtil.DegreesToRadians(binaryReader.ReadSingle()))).ToSystemQuaternion();
                            Matrix.Scaling(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(MathUtil.DegreesToRadians(binaryReader.ReadSingle()), MathUtil.DegreesToRadians(binaryReader.ReadSingle()), MathUtil.DegreesToRadians(binaryReader.ReadSingle())));
                            Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(MathUtil.DegreesToRadians(binaryReader.ReadSingle()), MathUtil.DegreesToRadians(binaryReader.ReadSingle()), MathUtil.DegreesToRadians(binaryReader.ReadSingle())));
                            model.vector3_2 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            model.quaternion_2 = Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(MathUtil.DegreesToRadians(binaryReader.ReadSingle()), MathUtil.DegreesToRadians(binaryReader.ReadSingle()), MathUtil.DegreesToRadians(binaryReader.ReadSingle()))).ToSystemQuaternion();
                            Matrix.Scaling(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            int num3 = binaryReader.ReadInt32();
                            Matrix[] array = new Matrix[num3];
                            for (int j = 0; j < num3; j++)
                            {
                                if (model.class243_0 == null)
                                {
                                    model.class243_0 = new Class243
                                    {
                                        quaternion_0 = new System.Numerics.Quaternion[num3],
                                        vector3_0 = new Vector3[num3]
                                    };
                                }
                                array[j] = new Matrix(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            }
                            int num4 = binaryReader.ReadInt32();
                            Matrix[] array2 = new Matrix[num3];
                            for (int k = 0; k < num4; k++)
                            {
                                array2[k] = new Matrix(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            }
                            if (num4 != num3)
                            {
                                model.class243_0 = null;
                            }
                            else
                            {
                                for (int l = 0; l < num4; l++)
                                {
                                    model.class243_0.quaternion_0[l] = Quaternion.RotationMatrix(array2[l]).ToSystemQuaternion();
                                    model.class243_0.vector3_0[l] = array[l].TranslationVector;
                                }
                            }
                        }
                        else
                        {
                            model.name = "";
                            model.parentName = "";
                            model.vector3_3 = Vector3.Zero;
                        }
                        List<VertexPositionNormalTexture> list = new List<VertexPositionNormalTexture>();
                        int num5 = binaryReader.ReadInt32() / 7;
                        for (int m = 0; m < num5; m++)
                        {
                            VertexPositionNormalTexture vertPosNormTex = new VertexPositionNormalTexture();
                            binaryReader.ReadSingle();
                            vertPosNormTex.svPosition.X = binaryReader.ReadSingle() * 63.7f - model.vector3_3.X;
                            vertPosNormTex.normal.Y = binaryReader.ReadSingle() / -1.732f;
                            vertPosNormTex.svPosition.Z = binaryReader.ReadSingle() / 16f - model.vector3_3.Z;
                            vertPosNormTex.texcoord0.X = binaryReader.ReadSingle() / 4.8f;
                            vertPosNormTex.normal.X = binaryReader.ReadSingle() / 10.962f;
                            binaryReader.ReadSingle();
                            vertPosNormTex.normal.Z = binaryReader.ReadSingle() / 11.432f;
                            vertPosNormTex.texcoord0.Y = binaryReader.ReadSingle() / 9.6f;
                            vertPosNormTex.svPosition.Y = -binaryReader.ReadSingle() * 6f - model.vector3_3.Y;
                            vertPosNormTex.binormal = Vector3.Zero;
                            vertPosNormTex.tangent = Vector3.Zero;
                            list.Add(vertPosNormTex);
                            float num6 = Math.Max(Math.Abs(vertPosNormTex.svPosition.X), Math.Max(Math.Abs(vertPosNormTex.svPosition.Y), Math.Abs(vertPosNormTex.svPosition.Z)));
                            if (num6 > BoundingSphereRadius)
                            {
                                BoundingSphereRadius = num6;
                            }
                        }
                        num5 = binaryReader.ReadInt32() + 6;
                        List<string> textures = new List<string>();
                        for (int n = 0; n < num5; n++)
                        {
                            string textureName = binaryReader.ReadString();
                            Console.WriteLine("Texture: " + textureName);
                            textures.Add(Path.GetFileNameWithoutExtension(textureName));
                        }
                        bool flag2 = binaryReader.ReadBoolean();
                        int num7 = binaryReader.ReadInt32();
                        if (flag2)
                        {
                            model.indexBuffer = new int[num7];
                            for (int num8 = 0; num8 < num7; num8++)
                            {
                                model.indexBuffer[num8] = (ushort)binaryReader.ReadInt32();
                            }
                            for (int num9 = 0; num9 < model.indexBuffer.Length - 3; num9 += 3)
                            {
                                smethod_0(model.indexBuffer[num9], model.indexBuffer[num9 + 2], model.indexBuffer[num9 + 1], list);
                            }
                        }
                        else
                        {
                            model.indexBuffer = new int[num7];
                            for (int num10 = 0; num10 < num7; num10++)
                            {
                                model.indexBuffer[num10] = binaryReader.ReadInt32();
                            }
                            for (int num11 = 0; num11 < model.indexBuffer.Length - 3; num11 += 3)
                            {
                                smethod_0(model.indexBuffer[num11], model.indexBuffer[num11 + 2], model.indexBuffer[num11 + 1], list);
                            }
                        }
                        num5 = binaryReader.ReadInt32() - 9;
                        bool flag3 = false;
                        if (num5 == 0)
                        {
                            Texture texture = new Texture
                            {
                                indicieCount = model.indexBuffer.Length,
                                baseVertexLocation = 0,
                                indexStartLocation = 0
                            };
                            model.textures.Add(texture);
                        }
                        else
                        {
                            lock (object_0)
                            {
                                for (int num12 = 0; num12 < num5; num12++)
                                {
                                    Texture texture = new Texture();
                                    binaryReader.ReadSingle();
                                    int num13 = binaryReader.ReadInt32();
                                    //if (textures.Count > 0)
                                    //{
                                    //    string text2 = directoryName + "\\" + textures[num13];
                                    //    Console.WriteLine(text2);
                                    //    texture.baseTexture = class476_0.method_5(text2, bool_2);
                                    //    this.method_3(texture, enum61_0, text2, class476_0, bool_2); // loads _MRAO or _mrao
                                    //    this.method_2(texture, text2, class476_0, bool_2); // loads _Normal, _NORMAL, or _normal
                                    //    flag3 |= texture.texture2D_2 != null;
                                    //}
                                    texture.indicieCount = binaryReader.ReadInt32();
                                    texture.indexStartLocation = binaryReader.ReadInt32();
                                    texture.baseVertexLocation = binaryReader.ReadInt32();
                                    model.textures.Add(texture);
                                }
                            }
                        }
                        if (flag3)
                        {
                            model.vertexBuffer = list.ToArray();
                        }
                        else
                        {
                            List<VertexPositionNormalTexture> list3 = new List<VertexPositionNormalTexture>(list.Count);
                            foreach (VertexPositionNormalTexture struct2 in list)
                            {
                                list3.Add(new VertexPositionNormalTexture(struct2.svPosition, struct2.normal, struct2.texcoord0));
                            }
                            model.vertexBuffer = list.ToArray();
                        }
                        list_0.Add(model);

                        ObjFile objFile = new ObjFile();

                        Console.WriteLine("Vertex Buffer has " + model.vertexBuffer.Length + " elements");
                        Console.WriteLine("Index Buffer has " + model.indexBuffer.Length + " elements");

                        foreach (VertexPositionNormalTexture v in model.vertexBuffer)
                        {
                            objFile.Vertices.Add(new ObjVertex(v.svPosition.X, v.svPosition.Y, v.svPosition.Z));
                            objFile.VertexNormals.Add(new ObjVector3(v.normal.X, v.normal.Y, v.normal.Z));
                            objFile.TextureVertices.Add(new ObjVector3(v.texcoord0.X, v.texcoord0.Y, 0));
                        }

                        foreach (var faceVerts in model.indexBuffer.Batch(3))
                        {
                            ObjFace face = new ObjFace();
                            foreach (var faceVert in faceVerts)
                            {
                                face.Vertices.Add(new ObjTriplet(faceVert + 1, faceVert + 1, faceVert + 1));
                            }

                            objFile.Faces.Add(face);
                        }

                        string objPath = Path.Join(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + ".obj");

                        objFile.WriteTo(objPath);
                    }
                }
            }


            using (List<Model>.Enumerator enumerator2 = list_0.GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    Class661 class3 = new Class661();
                    class3.class244_0 = enumerator2.Current;
                    if (!string.IsNullOrEmpty(class3.class244_0.parentName))
                    {
                        class3.class244_0.model = list_0.Find(new Predicate<Model>(class3.method_0));
                    }
                    string text3 = class3.class244_0.name.ToLower();
                    if (text3.Contains("wiper"))
                    {
                        Console.WriteLine("Wiper");
                        class3.class244_0.enum39_0 = Enum39.const_3;
                    }
                    else if (text3.Contains("beacon"))
                    {
                        Console.WriteLine("Beacon");
                        class3.class244_0.enum39_0 = Enum39.const_6;
                    }
                    else if (text3.Contains("glass"))
                    {
                        Console.WriteLine("Glass");
                        class3.class244_0.enum39_0 = (text3.Contains("rain") ? Enum39.const_8 : Enum39.const_7);
                    }
                    else if (text3.Contains("holder"))
                    {
                        Console.WriteLine("Holder");
                        class3.class244_0.enum39_0 = Enum39.const_7;
                    }
                    else if (text3.Contains("window"))
                    {
                        Console.WriteLine("Window (TODO)");
                        //class3.class244_0.enum39_0 = method_4(text3);
                    }
                    else if (text3.Contains("r_door"))
                    {
                        Console.WriteLine("RDoor");
                        class3.class244_0.enum39_0 = Enum39.const_9;
                    }
                    else if (text3.Contains("f_door"))
                    {
                        Console.WriteLine("FDoor");
                        class3.class244_0.enum39_0 = Enum39.const_10;
                    }
                    else if (text3.Contains("carload"))
                    {
                        Console.WriteLine("Carload");
                        class3.class244_0.enum39_0 = Enum39.const_2;
                    }
                }
            }
            BoundingSphereRadius *= 1.2f;


            float num22 = 0f;
        }

        internal static float smethod_0(float float_8)
        {
            return MathUtil.Lerp(1f, 0.15f, float_8);
        }

        static string DecodeString(byte[] bytes)
        {
            byte[] result = new byte[bytes.Length / 2];
            int num = 0;
            for (int i = 0; i < result.Length; i++)
            {
                result[i] |= (byte)(bytes[num++] << 4);
                result[i] |= (byte)(bytes[num++] >> 4);
            }

            return Encoding.UTF8.GetString(result);
        }

        static string ReadString(BinaryReader binaryReader)
        {
            int size = binaryReader.ReadInt32(); // string - size/length
            return DecodeString(binaryReader.ReadBytes(size)); // string - Read the specified size of bytes and decode them
        }

        static void smethod_0(int int_0, int int_1, int int_2, List<VertexPositionNormalTexture> list_1)
        {
            VertexPositionNormalTexture vertexPositionNormalTexture = list_1[int_0];
            VertexPositionNormalTexture vertexPositionNormalTexture2 = list_1[int_1];
            VertexPositionNormalTexture vertexPositionNormalTexture3 = list_1[int_2];
            Vector3 vector = vertexPositionNormalTexture2.svPosition - vertexPositionNormalTexture.svPosition;
            Vector3 vector2 = vertexPositionNormalTexture3.svPosition - vertexPositionNormalTexture.svPosition;
            float num = vertexPositionNormalTexture2.texcoord0.X - vertexPositionNormalTexture.texcoord0.X;
            float num2 = vertexPositionNormalTexture2.texcoord0.Y - vertexPositionNormalTexture.texcoord0.Y;
            float num3 = vertexPositionNormalTexture3.texcoord0.X - vertexPositionNormalTexture.texcoord0.X;
            float num4 = vertexPositionNormalTexture3.texcoord0.Y - vertexPositionNormalTexture.texcoord0.Y;
            float num5 = 1f / (num * num4 - num3 * num2);
            Vector3 zero = Vector3.Zero;
            zero.X = (num4 * vector.X - num2 * vector2.X) * num5;
            zero.Y = (num4 * vector.Y - num2 * vector2.Y) * num5;
            zero.Z = (num4 * vector.Z - num2 * vector2.Z) * num5;
            zero = Vector3.Normalize(zero);
            vertexPositionNormalTexture.tangent = zero;
            vertexPositionNormalTexture2.tangent = zero;
            vertexPositionNormalTexture3.tangent = zero;
            Vector3 vector3 = Vector3.Normalize(Vector3.Cross(zero, vertexPositionNormalTexture.normal));
            vertexPositionNormalTexture.binormal = vector3;
            vertexPositionNormalTexture2.binormal = vector3;
            vertexPositionNormalTexture3.binormal = vector3;
            list_1[int_0] = vertexPositionNormalTexture;
            list_1[int_1] = vertexPositionNormalTexture2;
            list_1[int_2] = vertexPositionNormalTexture3;
        }

        private sealed class Class661
        {
            internal bool method_0(Model class244_1)
            {
                return class244_1.name.Contains(this.class244_0.parentName);
            }

            public Model class244_0;
        }
    }
}
