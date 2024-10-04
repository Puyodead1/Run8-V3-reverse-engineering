using LibRun8.Common;
using LibRun8.Util;
using System.Text;

namespace ModelLoaderTest
{
    public class Class678
    {
        public float BsRadius { get; private set; }

        public void method_1(string string_0/*, Class492 class492_0, Enum63 enum63_0*/, Vector3 vector3_1, bool bool_2 = false)
        {
            //this.uint_0 = Class492.uint_0;
            this.ObjectList.Clear();
            string directoryName = Path.GetDirectoryName(string_0);
            this.matrix_0 = Matrix.Identity;
            this.vector3_0 = vector3_1;
            using (FileStream fileStream = File.OpenRead(string_0))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.UTF8))
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
                        this.vector3_0 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                        vector3_1 = Vector3.Zero;
                        flag = true;
                    }
                    else
                    {
                        binaryReader.BaseStream.Position = 0L;
                    }
                    this.ObjectList = new List<Class252>(num);
                    this.BsRadius = 0f;
                    int i = 0;
                    while (i < num)
                    {
                        Class252 @class = new Class252();
                        if (flag)
                        {
                            @class.string_0 = binaryReader.ReadString();
                            @class.string_1 = binaryReader.ReadString();
                            @class.vector3_3 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            @class.vector3_1 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            @class.RotationMatrix = Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.DegreesToRadians(binaryReader.ReadSingle()), Utils.DegreesToRadians(binaryReader.ReadSingle()), Utils.DegreesToRadians(binaryReader.ReadSingle())));
                            Matrix.Scaling(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.DegreesToRadians(binaryReader.ReadSingle()), Utils.DegreesToRadians(binaryReader.ReadSingle()), Utils.DegreesToRadians(binaryReader.ReadSingle())));
                            Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.DegreesToRadians(binaryReader.ReadSingle()), Utils.DegreesToRadians(binaryReader.ReadSingle()), Utils.DegreesToRadians(binaryReader.ReadSingle())));
                            @class.PositionOffset = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), -binaryReader.ReadSingle());
                            @class.quaternion_2 = Quaternion.RotationMatrix(Matrix.RotationYawPitchRoll(Utils.DegreesToRadians(binaryReader.ReadSingle()), Utils.DegreesToRadians(binaryReader.ReadSingle()), Utils.DegreesToRadians(binaryReader.ReadSingle())));
                            Matrix.Scaling(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            int num3 = binaryReader.ReadInt32();
                            Matrix[] array = new Matrix[num3];
                            for (int j = 0; j < num3; j++)
                            {
                                if (@class.class251_0 == null)
                                {
                                    @class.class251_0 = new Class251
                                    {
                                        quaternion_0 = new Quaternion[num3],
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
                                @class.class251_0 = null;
                            }
                            else
                            {
                                for (int l = 0; l < num4; l++)
                                {
                                    @class.class251_0.quaternion_0[l] = Quaternion.RotationMatrix(array2[l]);
                                    @class.class251_0.vector3_0[l] = array[l].TranslationVector;
                                }
                            }
                        }
                        else
                        {
                            @class.string_0 = "";
                            @class.string_1 = "";
                            @class.vector3_3 = Vector3.Zero;
                        }
                        List<Struct7> list = new List<Struct7>();
                        int num5 = binaryReader.ReadInt32() / 7;
                        for (int m = 0; m < num5; m++)
                        {
                            Struct7 @struct = default(Struct7);
                            binaryReader.ReadSingle();
                            @struct.Position.X = binaryReader.ReadSingle() * 63.7f - @class.vector3_3.X;
                            @struct.Normal.Y = binaryReader.ReadSingle() / -1.732f;
                            @struct.Position.Z = binaryReader.ReadSingle() / 16f - @class.vector3_3.Z;
                            @struct.TexCoord0.X = binaryReader.ReadSingle() / 4.8f;
                            @struct.Normal.X = binaryReader.ReadSingle() / 10.962f;
                            binaryReader.ReadSingle();
                            @struct.Normal.Z = binaryReader.ReadSingle() / 11.432f;
                            @struct.TexCoord0.Y = binaryReader.ReadSingle() / 9.6f;
                            @struct.Position.Y = -binaryReader.ReadSingle() * 6f - @class.vector3_3.Y;
                            @struct.BiNormal = Vector3.Zero;
                            @struct.Tangent = Vector3.Zero;

                            // transform the position
                            // Vector3.Transform(@struct.Position, @class.RotationMatrix);

                            // offset the position
                            @struct.Position += @class.PositionOffset;

                            list.Add(@struct);
                            float num6 = Math.Max(Math.Abs(@struct.Position.X), Math.Max(Math.Abs(@struct.Position.Y), Math.Abs(@struct.Position.Z)));
                            if (num6 > this.BsRadius)
                            {
                                this.BsRadius = num6;
                            }
                        }
                        num5 = binaryReader.ReadInt32() + 6;
                        List<string> list2 = new List<string>();
                        for (int n = 0; n < num5; n++)
                        {
                            string text = binaryReader.ReadString();
                            list2.Add(Path.GetFileNameWithoutExtension(text));
                        }
                        @class.isUshortIndexBuffer = binaryReader.ReadBoolean();
                        int num7 = binaryReader.ReadInt32();

                        if (@class.isUshortIndexBuffer)
                        {
                            ushort[] array3 = new ushort[num7];
                            for (int num8 = 0; num8 < num7; num8++)
                            {
                                array3[num8] = (ushort)binaryReader.ReadInt32();
                            }
                            //@class.buffer_1 = SharpDX.Toolkit.Graphics.Buffer.Index.New<ushort>(graphicsDevice_0, array3, ResourceUsage.Immutable);
                            @class.IndexBuffer1 = array3.ToArray(); 
                            for (int num9 = 0; num9 < array3.Length - 3; num9 += 3)
                            {
                                Class678.smethod_0((int)array3[num9], (int)array3[num9 + 2], (int)array3[num9 + 1], list);
                            }
                        }
                        else
                        {
                            int[] array4 = new int[num7];
                            for (int num10 = 0; num10 < num7; num10++)
                            {
                                array4[num10] = binaryReader.ReadInt32();
                            }
                            //@class.buffer_1 = SharpDX.Toolkit.Graphics.Buffer.Index.New<int>(graphicsDevice_0, array4, ResourceUsage.Immutable);
                            @class.IndexBuffer2 = array4.ToArray();
                            for (int num11 = 0; num11 < array4.Length - 3; num11 += 3)
                            {
                                Class678.smethod_0(array4[num11], array4[num11 + 2], array4[num11 + 1], list);
                            }
                        }

                        //int[] array4 = new int[num7];
                        //for (int num10 = 0; num10 < num7; num10++)
                        //{
                        //    array4[num10] = binaryReader.ReadInt32();
                        //}
                        ////@class.buffer_1 = SharpDX.Toolkit.Graphics.Buffer.Index.New<int>(graphicsDevice_0, array4, ResourceUsage.Immutable);
                        //@class.IndexBuffer = array4;
                        //for (int num11 = 0; num11 < array4.Length - 3; num11 += 3)
                        //{
                        //    Class678.smethod_0(array4[num11], array4[num11 + 2], array4[num11 + 1], list);
                        //}


                        num5 = binaryReader.ReadInt32() - 9;
                        bool flag3 = false;
                        if (num5 == 0)
                        {
                            Class141 class2 = new Class141
                            {
                                //int_2 = @class.buffer_1.ElementCount,
                                IndexCountPerInstance = @class.isUshortIndexBuffer ? @class.IndexBuffer1.Length : @class.IndexBuffer2.Length,
                                BaseVertexLocation = 0,
                                StartIndexLocation = 0
                            };
                            @class.list_0.Add(class2);
                        }
                        else
                        {
                            //object object_ = class492_0.object_0;
                            //lock (object_)
                            //{
                            for (int num12 = 0; num12 < num5; num12++)
                            {
                                Class141 class3 = new Class141();
                                binaryReader.ReadSingle();
                                int num13 = binaryReader.ReadInt32();
                                if (list2.Count > 0)
                                {
                                    string text2 = directoryName + "\\" + list2[num13];
                                    //class3.texture2D_0 = class492_0.method_8(text2, bool_2);
                                    class3.texture2D_0 = text2;
                                    //this.method_3(class3, enum63_0, text2, class492_0, bool_2);
                                    //this.method_2(class3, text2, class492_0, bool_2);
                                    flag3 |= class3.texture2D_2 != null;
                                }
                                class3.IndexCountPerInstance = binaryReader.ReadInt32();
                                class3.StartIndexLocation = binaryReader.ReadInt32();
                                class3.BaseVertexLocation = binaryReader.ReadInt32();
                                @class.list_0.Add(class3);
                            }
                            //}
                        }
                        if (flag3)
                        {
                            //@class.buffer_0 = SharpDX.Toolkit.Graphics.Buffer.Vertex.New<Struct7>(graphicsDevice_0, list.ToArray(), ResourceUsage.Immutable);
                            @class.VertexBuffer = list.ToArray();
                            using (List<Class141>.Enumerator enumerator = @class.list_0.GetEnumerator())
                            {
                                //while (enumerator.MoveNext())
                                //{
                                //    Class141 class4 = enumerator.Current;
                                //    //if (class4.texture2D_2 == null)
                                //    //{
                                //    //    class4.texture2D_2 = Class606.texture2D_0;
                                //    //}
                                //}
                                //goto IL_8CC;
                                this.ObjectList.Add(@class);
                                i++;
                                continue;
                            }
                        }
                        //goto IL_85A;
                        //IL_8CC:
                        //    this.ObjectList.Add(@class);
                        //    i++;
                        //    continue;
                        //IL_85A:
                        //List<VertexPositionNormalTexture> list3 = new List<VertexPositionNormalTexture>(list.Count);
                        //foreach (Struct7 struct2 in list)
                        //{
                        //    list3.Add(new VertexPositionNormalTexture(struct2.vector3_0, struct2.vector3_1, struct2.vector2_0));
                        //}
                        //@class.VertexBuffer = SharpDX.Toolkit.Graphics.Buffer.Vertex.New<VertexPositionNormalTexture>(graphicsDevice_0, list3.ToArray(), ResourceUsage.Immutable);
                        @class.VertexBuffer = list.ToArray();
                        //goto IL_8CC;
                        this.ObjectList.Add(@class);
                        i++;
                        continue;
                    }
                }
            }
            //using (List<Class252>.Enumerator enumerator3 = this.ObjectList.GetEnumerator())
            //{
            //    while (enumerator3.MoveNext())
            //    {
            //        Class678.Class679 class5 = new Class678.Class679();
            //        class5.class252_0 = enumerator3.Current;
            //        if (!string.IsNullOrEmpty(class5.class252_0.string_1))
            //        {
            //            class5.class252_0.class252_0 = this.ObjectList.Find(new Predicate<Class252>(class5.method_0));
            //        }
            //        string text3 = class5.class252_0.string_0.ToLower();
            //        if (text3.Contains("wiper"))
            //        {
            //            class5.class252_0.enum41_0 = Enum41.const_5;
            //        }
            //        else if (text3.Contains("beacon"))
            //        {
            //            class5.class252_0.enum41_0 = Enum41.const_8;
            //        }
            //        else if (text3.Contains("hepglass"))
            //        {
            //            class5.class252_0.enum41_0 = Enum41.const_9;
            //        }
            //        else if (text3.Contains("glass_wheelslip"))
            //        {
            //            class5.class252_0.enum41_0 = Enum41.const_10;
            //        }
            //        else if (text3.Contains("glass_pcs"))
            //        {
            //            class5.class252_0.enum41_0 = Enum41.const_11;
            //        }
            //        else if (text3.Contains("glass"))
            //        {
            //            class5.class252_0.enum41_0 = (text3.Contains("rain") ? Enum41.const_13 : Enum41.const_12);
            //        }
            //        else if (text3.Contains("holder"))
            //        {
            //            class5.class252_0.enum41_0 = Enum41.const_12;
            //        }
            //        else if (text3.Contains("window"))
            //        {
            //            class5.class252_0.enum41_0 = this.method_4(text3);
            //        }
            //        else if (text3.Contains("r_door"))
            //        {
            //            class5.class252_0.enum41_0 = Enum41.const_14;
            //        }
            //        else if (text3.Contains("f_door"))
            //        {
            //            class5.class252_0.enum41_0 = Enum41.const_15;
            //        }
            //        else if (text3.Contains(" door"))
            //        {
            //            class5.class252_0.enum41_0 = this.method_5(text3);
            //        }
            //        else if (text3.Contains("carload"))
            //        {
            //            class5.class252_0.enum41_0 = Enum41.const_2;
            //        }
            //        else if (text3.Contains("interior_low"))
            //        {
            //            class5.class252_0.enum41_0 = Enum41.const_3;
            //        }
            //        else if (text3.Contains("interior_high"))
            //        {
            //            class5.class252_0.enum41_0 = Enum41.const_4;
            //        }
            //    }
            //}
            this.BsRadius *= 1.2f;
        }

        public static void smethod_0(int int_0, int int_1, int int_2, List<Struct7> list_1)
        {
            Struct7 @struct = list_1[int_0];
            Struct7 struct2 = list_1[int_1];
            Struct7 struct3 = list_1[int_2];
            Vector3 vector = struct2.Position - @struct.Position;
            Vector3 vector2 = struct3.Position - @struct.Position;
            float num = struct2.TexCoord0.X - @struct.TexCoord0.X;
            float num2 = struct2.TexCoord0.Y - @struct.TexCoord0.Y;
            float num3 = struct3.TexCoord0.X - @struct.TexCoord0.X;
            float num4 = struct3.TexCoord0.Y - @struct.TexCoord0.Y;
            float num5 = 1f / (num * num4 - num3 * num2);
            Vector3 zero = Vector3.Zero;
            zero.X = (num4 * vector.X - num2 * vector2.X) * num5;
            zero.Y = (num4 * vector.Y - num2 * vector2.Y) * num5;
            zero.Z = (num4 * vector.Z - num2 * vector2.Z) * num5;
            zero.Normalize();
            @struct.Tangent = zero;
            struct2.Tangent = zero;
            struct3.Tangent = zero;
            Vector3 vector3 = Vector3.Normalize(Vector3.Cross(zero, @struct.Normal));
            @struct.BiNormal = vector3;
            struct2.BiNormal = vector3;
            struct3.BiNormal = vector3;
            list_1[int_0] = @struct;
            list_1[int_1] = struct2;
            list_1[int_2] = struct3;
        }

        public static bool bool_0 = true;

        //public static VertexInputLayout vertexInputLayout_0 = VertexInputLayout.New<VertexPositionNormalTexture>(0);

        //public static VertexInputLayout vertexInputLayout_1 = VertexInputLayout.New<Struct7>(0);

        public List<Class252> ObjectList = new List<Class252>();

        public Matrix matrix_0;

        public Vector3 vector3_0;

        //// Token: 0x04001DD8 RID: 7640
        //[CompilerGenerated]
        //private float float_0;

        public uint uint_0;

        public static bool bool_1 = false;
    }
}
