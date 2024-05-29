using LibRun8.Common;

namespace ModelLoaderTest
{
    public class Class252
    {
        // Token: 0x06001379 RID: 4985 RVA: 0x0000CC48 File Offset: 0x0000AE48
        public void Dispose()
        {
            //this.buffer_0.Dispose();
            //this.buffer_1.Dispose();
        }

        // Token: 0x0600137A RID: 4986 RVA: 0x0007DDC4 File Offset: 0x0007BFC4
        public void method_1(float float_0)
        {
            Vector3 vector = Vector3.Transform(Vector3.ForwardRH, this.class252_0.quaternion_0);
            Vector3 vector2 = Vector3.Transform(Vector3.Up, this.class252_0.quaternion_0);
            Vector3 vector3 = Vector3.Transform(Vector3.Right, this.class252_0.quaternion_0);
            //this.vector3_2 != Vector3.Zero;
            if (this.class251_0 != null)
            {
                this.class251_0.method_0(float_0);
                this.quaternion_0 = this.class252_0.quaternion_0 * this.class251_0.quaternion_1 * this.quaternion_2 * this.RotationMatrix;
                this.vector3_0 = this.class252_0.vector3_0;
                this.vector3_0 += vector3 * this.class251_0.vector3_1.X;
                this.vector3_0 += vector2 * this.class251_0.vector3_1.Y;
                this.vector3_0 += vector * this.class251_0.vector3_1.Z;
            }
            else
            {
                this.quaternion_0 = this.class252_0.quaternion_0;
                this.vector3_0 = this.class252_0.vector3_0;
                this.vector3_0 += vector3 * this.PositionOffset.X;
                this.vector3_0 += vector2 * this.PositionOffset.Y;
                this.vector3_0 += vector * this.PositionOffset.Z;
            }
            Vector3 vector4 = this.vector3_3 - this.class252_0.vector3_3 + this.vector3_1;
            this.vector3_0 += vector3 * vector4.X;
            this.vector3_0 += vector2 * vector4.Y;
            this.vector3_0 += vector * -vector4.Z;
        }

        //public Enum41 enum41_0;
        public bool isUshortIndexBuffer = false;

        public string string_0 { get; set; }

        public string string_1 { get; set; }

        //public SharpDX.Toolkit.Graphics.Buffer buffer_0;
        public Struct7[] VertexBuffer;

        //public SharpDX.Toolkit.Graphics.Buffer buffer_1;

        public ushort[] IndexBuffer1;
        public int[] IndexBuffer2;

        public List<Class141> list_0 = new List<Class141>();

        public Quaternion quaternion_0 { get; set; }  = Quaternion.Identity;

        public Vector3 vector3_0 { get; set; } = Vector3.Zero;

        public Vector3 vector3_1 { get; set; }  = Vector3.Zero;

        public Quaternion RotationMatrix { get; set; } = Quaternion.Identity;

        public Quaternion quaternion_2 { get; set; } = Quaternion.Identity;

        public Vector3 PositionOffset { get; set; } = Vector3.Zero;

        public Vector3 vector3_3 { get; set; } = Vector3.Zero;

        public Class251 class251_0 { get; set; }

        public Class252 class252_0 { get; set; }

        public bool bool_0 { get; set; } = true;
    }
}
