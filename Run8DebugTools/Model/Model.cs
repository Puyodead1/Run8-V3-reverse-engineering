using System.Collections.Generic;
using System.Numerics;
using Run8Utils;

namespace Model
{
    internal class Model
    {
        internal void method_1(float float_0)
        {
            Vector3 vector = Vector3.Transform(ForwardRH, this.model.quaternion_0);
            Vector3 vector2 = Vector3.Transform(Up, this.model.quaternion_0);
            Vector3 vector3 = Vector3.Transform(Right, this.model.quaternion_0);
            if (this.class243_0 != null)
            {
                this.class243_0.method_0(float_0);
                this.quaternion_0 = this.model.quaternion_0 * this.class243_0.quaternion_1 * this.quaternion_2 * this.quaternion_1;
                this.vector3_0 = this.model.vector3_0;
                this.vector3_0 += vector3 * this.class243_0.vector3_1.X;
                this.vector3_0 += vector2 * this.class243_0.vector3_1.Y;
                this.vector3_0 += vector * this.class243_0.vector3_1.Z;
            }
            else
            {
                this.quaternion_0 = this.model.quaternion_0;
                this.vector3_0 = this.model.vector3_0;
                this.vector3_0 += vector3 * this.vector3_2.X;
                this.vector3_0 += vector2 * this.vector3_2.Y;
                this.vector3_0 += vector * this.vector3_2.Z;
            }
            Vector3 vector4 = this.vector3_3 - this.model.vector3_3 + this.vector3_1;
            this.vector3_0 += vector3 * vector4.X;
            this.vector3_0 += vector2 * vector4.Y;
            this.vector3_0 += vector * -vector4.Z;
        }

        public static readonly Vector3 ForwardRH = new Vector3(0f, 0f, -1f);
        public static readonly Vector3 Up = new Vector3(0f, 1f, 0f);
        public static readonly Vector3 Right = new Vector3(1f, 0f, 0f);

        internal Enum39 enum39_0;

        internal string name;

        internal string parentName;

        internal VertexPositionNormalTexture[] vertexBuffer;

        internal int[] indexBuffer;

        internal List<Texture> textures = new List<Texture>();

        internal System.Numerics.Quaternion quaternion_0 = System.Numerics.Quaternion.Identity;

        internal Vector3 vector3_0 = Vector3.Zero;

        internal Vector3 vector3_1 = Vector3.Zero;

        internal System.Numerics.Quaternion quaternion_1 = System.Numerics.Quaternion.Identity;

        internal System.Numerics.Quaternion quaternion_2 = System.Numerics.Quaternion.Identity;

        internal Vector3 vector3_2 = Vector3.Zero;

        internal Vector3 vector3_3 = Vector3.Zero;

        internal Class243 class243_0;

        internal Model model;

        internal bool bool_0 = true;

        public Model()
        {
        }
    }
}
