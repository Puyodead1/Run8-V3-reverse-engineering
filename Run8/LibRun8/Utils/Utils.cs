using LibRun8.Common;
using System.Security.Cryptography;
using System.Text;

namespace LibRun8.Util
{
    public static class Utils
    {
        public static byte[] ComputeMD5ForBytes(byte[] data)
        {
            byte[] bytes;
            using (MD5 md = MD5.Create())
            {
                bytes = Encoding.ASCII.GetBytes(BitConverter.ToString(md.ComputeHash(data)).Replace("-", string.Empty));
            }
            return bytes;
        }

        public static bool CompareByteArrays(byte[] arr1, byte[] arr2)
        {
            if (arr1 == null || arr2 == null)
            {
                return false;
            }
            if (arr1.Length == arr2.Length)
            {
                for (int i = 0; i < arr1.Length; i++)
                {
                    if (arr1[i] != arr2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public static T[][] ConvertToJaggedArray<T>(T[,] twoDArray)
        {
            int rows = twoDArray.GetLength(0);
            int cols = twoDArray.GetLength(1);

            // Initialize the jagged array
            T[][] jaggedArray = new T[rows][];

            // Populate the jagged array
            for (int i = 0; i < rows; i++)
            {
                jaggedArray[i] = new T[cols];
                for (int j = 0; j < cols; j++)
                {
                    jaggedArray[i][j] = twoDArray[i, j];
                }
            }

            return jaggedArray;
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degree">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// Part of SharpDX
        public static float DegreesToRadians(float degree)
        {
            return degree * 0.017453292f;
        }

        /// <summary>
        /// Clamps the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The result of clamping a value between min and max</returns>
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            if (value <= max)
            {
                return value;
            }
            return max;
        }

        /// <summary>
        /// Interpolates between two values using a linear function by a given amount.
        /// </summary>
        /// <remarks>
        /// See http://www.encyclopediaofmath.org/index.php/Linear_interpolation and
        /// http://fgiesen.wordpress.com/2012/08/15/linear-interpolation-past-present-and-future/
        /// </remarks>
        /// <param name="from">Value to interpolate from.</param>
        /// <param name="to">Value to interpolate to.</param>
        /// <param name="amount">Interpolation amount.</param>
        /// <returns>The result of linear interpolation of values based on the amount.</returns>
        public static float Lerp(float from, float to, float amount)
        {
            return (1f - amount) * from + amount * to;
        }

        /// <summary>
        /// Determines whether the specified value is close to zero (0.0f).
        /// </summary>
        /// <param name="a">The floating value.</param>
        /// <returns><c>true</c> if the specified value is close to zero (0.0f); otherwise, <c>false</c>.</returns>
        public static bool IsZero(float a)
        {
            return Math.Abs(a) < 1E-06f;
        }

        /// <summary>
        /// Checks if a and b are almost equals, taking into account the magnitude of floating point numbers (unlike <see cref="M:SharpDX.MathUtil.WithinEpsilon(System.Single,System.Single,System.Single)" /> method). See Remarks.
        /// See remarks.
        /// </summary>
        /// <param name="a">The left value to compare.</param>
        /// <param name="b">The right value to compare.</param>
        /// <returns><c>true</c> if a almost equal to b, <c>false</c> otherwise</returns>
        /// <remarks>
        /// The code is using the technique described by Bruce Dawson in 
        /// <a href="http://randomascii.wordpress.com/2012/02/25/comparing-floating-point-numbers-2012-edition/">Comparing Floating point numbers 2012 edition</a>. 
        /// </remarks>
        public unsafe static bool NearEqual(float a, float b)
        {
            if (Utils.IsZero(a - b))
            {
                return true;
            }
            int num = *(int*)(&a);
            int num2 = *(int*)(&b);
            if (num < 0 != num2 < 0)
            {
                return false;
            }
            int num3 = Math.Abs(num - num2);
            return num3 <= 4;
        }

        public static void smethod_0(int int_0, int int_1, int int_2, List<VertexStruct> list_1)
        {
            VertexStruct @struct = list_1[int_0];
            VertexStruct struct2 = list_1[int_1];
            VertexStruct struct3 = list_1[int_2];
            Vector3 vector = struct2.Position - @struct.Position;
            Vector3 vector2 = struct3.Position - @struct.Position;
            float num = struct2.TextureCoordinate.X - @struct.TextureCoordinate.X;
            float num2 = struct2.TextureCoordinate.Y - @struct.TextureCoordinate.Y;
            float num3 = struct3.TextureCoordinate.X - @struct.TextureCoordinate.X;
            float num4 = struct3.TextureCoordinate.Y - @struct.TextureCoordinate.Y;
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
            @struct.Binormal = vector3;
            struct2.Binormal = vector3;
            struct3.Binormal = vector3;
            list_1[int_0] = @struct;
            list_1[int_1] = struct2;
            list_1[int_2] = struct3;
        }
    }
}
