using LibRun8.Common;
using LibRun8.Util;

namespace ModelLoaderTest
{
    public class Class251
    {
        public void method_0(float float_0)
        {
            if (this.quaternion_0.Length < 2)
            {
                this.quaternion_1 = Quaternion.Identity;
                this.vector3_1 = Vector3.Zero;
                return;
            }
            int num = Utils.Clamp((int)(float_0 * (float)this.quaternion_0.Length - 2f), 0, this.quaternion_0.Length - 2);
            int num2 = Utils.Clamp(num + 1, 0, this.quaternion_0.Length - 1);
            Quaternion quaternion = this.quaternion_0[num];
            Quaternion quaternion2 = this.quaternion_0[num2];
            Vector3 vector = this.vector3_0[num];
            Vector3 vector2 = this.vector3_0[num2];
            float num3 = 1f / (float)(this.quaternion_0.Length - 1);
            float num4 = Utils.Lerp(0f, 1f, float_0 - num3 * (float)num / num3);
            if (quaternion != quaternion2)
            {
                this.quaternion_1 = Quaternion.Lerp(quaternion, quaternion2, num4);
            }
            else
            {
                this.quaternion_1 = quaternion;
            }
            this.vector3_1 = Vector3.Lerp(vector, vector2, num4);
        }

        internal Quaternion[] quaternion_0;

        internal Vector3[] vector3_0;

        internal Quaternion quaternion_1;

        internal Vector3 vector3_1;
    }
}
