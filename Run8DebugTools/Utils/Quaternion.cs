using System.Globalization;
using System.Runtime.InteropServices;

namespace Run8Utils
{
    public struct Quaternion: IFormattable
    {
        public Quaternion(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public static void RotationYawPitchRoll(float yaw, float pitch, float roll, out Quaternion result)
        {
            float num = roll * 0.5f;
            float num2 = pitch * 0.5f;
            float num3 = yaw * 0.5f;
            float num4 = (float)Math.Sin((double)num);
            float num5 = (float)Math.Cos((double)num);
            float num6 = (float)Math.Sin((double)num2);
            float num7 = (float)Math.Cos((double)num2);
            float num8 = (float)Math.Sin((double)num3);
            float num9 = (float)Math.Cos((double)num3);

            result.X = num9 * num6 * num5 + num8 * num7 * num4;
            result.Y = num8 * num7 * num5 - num9 * num6 * num4;
            result.Z = num9 * num7 * num4 - num8 * num6 * num5;
            result.W = num9 * num7 * num5 + num8 * num6 * num4;
        }

        public static void RotationMatrix(ref Matrix matrix, out Quaternion result)
        {
            float num = matrix.M11 + matrix.M22 + matrix.M33;
            float num2;
            if (num > 0f)
            {
                num2 = (float)Math.Sqrt((double)(num + 1f));
                result.W = num2 * 0.5f;
                num2 = 0.5f / num2;
                result.X = (matrix.M23 - matrix.M32) * num2;
                result.Y = (matrix.M31 - matrix.M13) * num2;
                result.Z = (matrix.M12 - matrix.M21) * num2;
                return;
            }
            float num3;
            if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                num2 = (float)Math.Sqrt((double)(1f + matrix.M11 - matrix.M22 - matrix.M33));
                num3 = 0.5f / num2;
                result.X = 0.5f * num2;
                result.Y = (matrix.M12 + matrix.M21) * num3;
                result.Z = (matrix.M13 + matrix.M31) * num3;
                result.W = (matrix.M23 - matrix.M32) * num3;
                return;
            }
            if (matrix.M22 > matrix.M33)
            {
                num2 = (float)Math.Sqrt((double)(1f + matrix.M22 - matrix.M11 - matrix.M33));
                num3 = 0.5f / num2;
                result.X = (matrix.M21 + matrix.M12) * num3;
                result.Y = 0.5f * num2;
                result.Z = (matrix.M32 + matrix.M23) * num3;
                result.W = (matrix.M31 - matrix.M13) * num3;
                return;
            }
            num2 = (float)Math.Sqrt((double)(1f + matrix.M33 - matrix.M11 - matrix.M22));
            num3 = 0.5f / num2;
            result.X = (matrix.M31 + matrix.M13) * num3;
            result.Y = (matrix.M32 + matrix.M23) * num3;
            result.Z = 0.5f * num2;
            result.W = (matrix.M12 - matrix.M21) * num3;
        }

        public static Quaternion RotationMatrix(Matrix matrix)
        {
            Quaternion quaternion;
            Quaternion.RotationMatrix(ref matrix, out quaternion);
            return quaternion;
        }

        public System.Numerics.Quaternion ToSystemQuaternion()
        {
            return new System.Numerics.Quaternion(X, Y, Z, W);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2} W:{3}", new object[] { this.X, this.Y, this.Z, this.W });
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2} W:{3}", new object[]
            {
                this.X.ToString(format, CultureInfo.CurrentCulture),
                this.Y.ToString(format, CultureInfo.CurrentCulture),
                this.Z.ToString(format, CultureInfo.CurrentCulture),
                this.W.ToString(format, CultureInfo.CurrentCulture)
            });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "X:{0} Y:{1} Z:{2} W:{3}", new object[] { this.X, this.Y, this.Z, this.W });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString(formatProvider);
            }
            return string.Format(formatProvider, "X:{0} Y:{1} Z:{2} W:{3}", new object[]
            {
                this.X.ToString(format, formatProvider),
                this.Y.ToString(format, formatProvider),
                this.Z.ToString(format, formatProvider),
                this.W.ToString(format, formatProvider)
            });
        }

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Quaternion));

        public static readonly Quaternion Zero = default(Quaternion);

        public static readonly Quaternion One = new Quaternion(1f, 1f, 1f, 1f);

        public static readonly Quaternion Identity = new Quaternion(0f, 0f, 0f, 1f);

        public float X;

        public float Y;

        public float Z;

        public float W;
    }
}
