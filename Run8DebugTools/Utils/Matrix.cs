using System.Numerics;

namespace Run8Utils
{
    public struct Matrix
    {
        public Matrix(float value)
        {
            this.M44 = value;
            this.M43 = value;
            this.M42 = value;
            this.M41 = value;
            this.M34 = value;
            this.M33 = value;
            this.M32 = value;
            this.M31 = value;
            this.M24 = value;
            this.M23 = value;
            this.M22 = value;
            this.M21 = value;
            this.M14 = value;
            this.M13 = value;
            this.M12 = value;
            this.M11 = value;
        }

        public Matrix(float M11, float M12, float M13, float M14, float M21, float M22, float M23, float M24, float M31, float M32, float M33, float M34, float M41, float M42, float M43, float M44)
        {
            this.M11 = M11;
            this.M12 = M12;
            this.M13 = M13;
            this.M14 = M14;
            this.M21 = M21;
            this.M22 = M22;
            this.M23 = M23;
            this.M24 = M24;
            this.M31 = M31;
            this.M32 = M32;
            this.M33 = M33;
            this.M34 = M34;
            this.M41 = M41;
            this.M42 = M42;
            this.M43 = M43;
            this.M44 = M44;
        }

        public static void RotationQuaternion(ref Quaternion rotation, out Matrix result)
        {
            float num = rotation.X * rotation.X;
            float num2 = rotation.Y * rotation.Y;
            float num3 = rotation.Z * rotation.Z;
            float num4 = rotation.X * rotation.Y;
            float num5 = rotation.Z * rotation.W;
            float num6 = rotation.Z * rotation.X;
            float num7 = rotation.Y * rotation.W;
            float num8 = rotation.Y * rotation.Z;
            float num9 = rotation.X * rotation.W;
            result = Matrix.Identity;
            result.M11 = 1f - 2f * (num2 + num3);
            result.M12 = 2f * (num4 + num5);
            result.M13 = 2f * (num6 - num7);
            result.M21 = 2f * (num4 - num5);
            result.M22 = 1f - 2f * (num3 + num);
            result.M23 = 2f * (num8 + num9);
            result.M31 = 2f * (num6 + num7);
            result.M32 = 2f * (num8 - num9);
            result.M33 = 1f - 2f * (num2 + num);
        }

        public Matrix4x4 ToMatrix4x4()
        {
            return new Matrix4x4(M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44);
        }

        public static void RotationYawPitchRoll(float yaw, float pitch, float roll, out Matrix result)
        {
            Quaternion quaternion = default(Quaternion);
            Quaternion.RotationYawPitchRoll(yaw, pitch, roll, out quaternion);
            Matrix.RotationQuaternion(ref quaternion, out result);
        }

        public static Matrix RotationYawPitchRoll(float yaw, float pitch, float roll)
        {
            Matrix matrix;
            Matrix.RotationYawPitchRoll(yaw, pitch, roll, out matrix);
            return matrix;
        }

        public static Matrix Scaling(float x, float y, float z)
        {
            Matrix matrix;
            Matrix.Scaling(x, y, z, out matrix);
            return matrix;
        }

        public static void Scaling(float x, float y, float z, out Matrix result)
        {
            result = Matrix.Identity;
            result.M11 = x;
            result.M22 = y;
            result.M33 = z;
        }

        public static void Scaling(float scale, out Matrix result)
        {
            result = Matrix.Identity;
            result.M33 = scale;
            result.M22 = scale;
            result.M11 = scale;
        }

        public static Matrix Scaling(float scale)
        {
            Matrix matrix;
            Matrix.Scaling(scale, out matrix);
            return matrix;
        }

        public static readonly Matrix Identity = new Matrix
        {
            M11 = 1f,
            M22 = 1f,
            M33 = 1f,
            M44 = 1f
        };

        public Vector3 TranslationVector
        {
            get
            {
                return new Vector3(this.M41, this.M42, this.M43);
            }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
            }
        }

        public float M11;

        /// <summary>
        /// Value at row 1 column 2 of the matrix.
        /// </summary>
        public float M12;

        /// <summary>
        /// Value at row 1 column 3 of the matrix.
        /// </summary>
        public float M13;

        /// <summary>
        /// Value at row 1 column 4 of the matrix.
        /// </summary>
        public float M14;

        /// <summary>
        /// Value at row 2 column 1 of the matrix.
        /// </summary>
        public float M21;

        /// <summary>
        /// Value at row 2 column 2 of the matrix.
        /// </summary>
        public float M22;

        /// <summary>
        /// Value at row 2 column 3 of the matrix.
        /// </summary>
        public float M23;

        /// <summary>
        /// Value at row 2 column 4 of the matrix.
        /// </summary>
        public float M24;

        /// <summary>
        /// Value at row 3 column 1 of the matrix.
        /// </summary>
        public float M31;

        /// <summary>
        /// Value at row 3 column 2 of the matrix.
        /// </summary>
        public float M32;

        /// <summary>
        /// Value at row 3 column 3 of the matrix.
        /// </summary>
        public float M33;

        /// <summary>
        /// Value at row 3 column 4 of the matrix.
        /// </summary>
        public float M34;

        /// <summary>
        /// Value at row 4 column 1 of the matrix.
        /// </summary>
        public float M41;

        /// <summary>
        /// Value at row 4 column 2 of the matrix.
        /// </summary>
        public float M42;

        /// <summary>
        /// Value at row 4 column 3 of the matrix.
        /// </summary>
        public float M43;

        /// <summary>
        /// Value at row 4 column 4 of the matrix.
        /// </summary>
        public float M44;
    }
}
