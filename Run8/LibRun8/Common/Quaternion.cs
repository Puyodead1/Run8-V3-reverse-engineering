// part of sharpdx, stripped down

using System.Globalization;
using System.Runtime.InteropServices;

namespace LibRun8.Common
{
    /// <summary>
    /// Represents a four dimensional mathematical quaternion.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Quaternion : IFormattable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Quaternion" /> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Quaternion(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
            this.W = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Quaternion" /> struct.
        /// </summary>
        /// <param name="value">A vector containing the values with which to initialize the components.</param>
        public Quaternion(Vector4 value)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = value.W;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Quaternion" /> struct.
        /// </summary>
        /// <param name="value">A vector containing the values with which to initialize the X, Y, and Z components.</param>
        /// <param name="w">Initial value for the W component of the quaternion.</param>
        public Quaternion(Vector3 value, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Quaternion" /> struct.
        /// </summary>
        /// <param name="value">A vector containing the values with which to initialize the X and Y components.</param>
        /// <param name="z">Initial value for the Z component of the quaternion.</param>
        /// <param name="w">Initial value for the W component of the quaternion.</param>
        public Quaternion(Vector2 value, float z, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Quaternion" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the quaternion.</param>
        /// <param name="y">Initial value for the Y component of the quaternion.</param>
        /// <param name="z">Initial value for the Z component of the quaternion.</param>
        /// <param name="w">Initial value for the W component of the quaternion.</param>
        public Quaternion(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Quaternion" /> struct.
        /// </summary>
        /// <param name="values">The values to assign to the X, Y, Z, and W components of the quaternion. This must be an array with four elements.</param>
        /// <exception cref="T:System.ArgumentNullException">Thrown when <paramref name="values" /> is <c>null</c>.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when <paramref name="values" /> contains more or less than four elements.</exception>
        public Quaternion(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 4)
            {
                throw new ArgumentOutOfRangeException("values", "There must be four and only four input values for Quaternion.");
            }
            this.X = values[0];
            this.Y = values[1];
            this.Z = values[2];
            this.W = values[3];
        }

        /// <summary>
        /// Gets a value indicating whether this instance is equivalent to the identity quaternion.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is an identity quaternion; otherwise, <c>false</c>.
        /// </value>
        // Token: 0x170000DD RID: 221
        // (get) Token: 0x06000967 RID: 2407 RVA: 0x00028388 File Offset: 0x00026588
        public bool IsIdentity
        {
            get
            {
                return this.Equals(Quaternion.Identity);
            }
        }

        /// <summary>
        /// Gets or sets the component at the specified index.
        /// </summary>
        /// <value>The value of the X, Y, Z, or W component, depending on the index.</value>
        /// <param name="index">The index of the component to access. Use 0 for the X component, 1 for the Y component, 2 for the Z component, and 3 for the W component.</param>
        /// <returns>The value of the component at the specified index.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when the <paramref name="index" /> is out of the range [0, 3].</exception>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.X;
                    case 1:
                        return this.Y;
                    case 2:
                        return this.Z;
                    case 3:
                        return this.W;
                    default:
                        throw new ArgumentOutOfRangeException("index", "Indices for Quaternion run from 0 to 3, inclusive.");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.X = value;
                        return;
                    case 1:
                        this.Y = value;
                        return;
                    case 2:
                        this.Z = value;
                        return;
                    case 3:
                        this.W = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException("index", "Indices for Quaternion run from 0 to 3, inclusive.");
                }
            }
        }

        /// <summary>
        /// Conjugates the quaternion.
        /// </summary>
        public void Conjugate()
        {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }

        /// <summary>
        /// Calculates the length of the quaternion.
        /// </summary>
        /// <returns>The length of the quaternion.</returns>
        /// <remarks>
        /// <see cref="M:SharpDX.Quaternion.LengthSquared" /> may be preferred when only the relative length is needed
        /// and speed is of the essence.
        /// </remarks>
        // Token: 0x0600096F RID: 2415 RVA: 0x000285F0 File Offset: 0x000267F0
        public float Length()
        {
            return (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W));
        }

        /// <summary>
        /// Calculates the squared length of the quaternion.
        /// </summary>
        /// <returns>The squared length of the quaternion.</returns>
        /// <remarks>
        /// This method may be preferred to <see cref="M:SharpDX.Quaternion.Length" /> when only a relative length is needed
        /// and speed is of the essence.
        /// </remarks>
        public float LengthSquared()
        {
            return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
        }

        /// <summary>
        /// Creates an array containing the elements of the quaternion.
        /// </summary>
        /// <returns>A four-element array containing the components of the quaternion.</returns>
        public float[] ToArray()
        {
            return new float[] { this.X, this.Y, this.Z, this.W };
        }

        /// <summary>
        /// Adds two quaternions.
        /// </summary>
        /// <param name="left">The first quaternion to add.</param>
        /// <param name="right">The second quaternion to add.</param>
        /// <param name="result">When the method completes, contains the sum of the two quaternions.</param>
        public static void Add(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result = default;
            result.X = left.X + right.X;
            result.Y = left.Y + right.Y;
            result.Z = left.Z + right.Z;
            result.W = left.W + right.W;
        }

        /// <summary>
        /// Adds two quaternions.
        /// </summary>
        /// <param name="left">The first quaternion to add.</param>
        /// <param name="right">The second quaternion to add.</param>
        /// <returns>The sum of the two quaternions.</returns>
        public static Quaternion Add(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Quaternion.Add(ref left, ref right, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Subtracts two quaternions.
        /// </summary>
        /// <param name="left">The first quaternion to subtract.</param>
        /// <param name="right">The second quaternion to subtract.</param>
        /// <param name="result">When the method completes, contains the difference of the two quaternions.</param>
        public static void Subtract(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result = default;
            result.X = left.X - right.X;
            result.Y = left.Y - right.Y;
            result.Z = left.Z - right.Z;
            result.W = left.W - right.W;
        }

        /// <summary>
        /// Subtracts two quaternions.
        /// </summary>
        /// <param name="left">The first quaternion to subtract.</param>
        /// <param name="right">The second quaternion to subtract.</param>
        /// <returns>The difference of the two quaternions.</returns>
        public static Quaternion Subtract(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Quaternion.Subtract(ref left, ref right, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Scales a quaternion by the given value.
        /// </summary>
        /// <param name="value">The quaternion to scale.</param>
        /// <param name="scale">The amount by which to scale the quaternion.</param>
        /// <param name="result">When the method completes, contains the scaled quaternion.</param>
        public static void Multiply(ref Quaternion value, float scale, out Quaternion result)
        {
            result = default;
            result.X = value.X * scale;
            result.Y = value.Y * scale;
            result.Z = value.Z * scale;
            result.W = value.W * scale;
        }

        /// <summary>
        /// Scales a quaternion by the given value.
        /// </summary>
        /// <param name="value">The quaternion to scale.</param>
        /// <param name="scale">The amount by which to scale the quaternion.</param>
        /// <returns>The scaled quaternion.</returns>
        public static Quaternion Multiply(Quaternion value, float scale)
        {
            Quaternion quaternion;
            Quaternion.Multiply(ref value, scale, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Multiplies a quaternion by another.
        /// </summary>
        /// <param name="left">The first quaternion to multiply.</param>
        /// <param name="right">The second quaternion to multiply.</param>
        /// <param name="result">When the method completes, contains the multiplied quaternion.</param>
        public static void Multiply(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result = default;
            float x = left.X;
            float y = left.Y;
            float z = left.Z;
            float w = left.W;
            float x2 = right.X;
            float y2 = right.Y;
            float z2 = right.Z;
            float w2 = right.W;
            float num = y * z2 - z * y2;
            float num2 = z * x2 - x * z2;
            float num3 = x * y2 - y * x2;
            float num4 = x * x2 + y * y2 + z * z2;
            result.X = x * w2 + x2 * w + num;
            result.Y = y * w2 + y2 * w + num2;
            result.Z = z * w2 + z2 * w + num3;
            result.W = w * w2 - num4;
        }

        /// <summary>
        /// Multiplies a quaternion by another.
        /// </summary>
        /// <param name="left">The first quaternion to multiply.</param>
        /// <param name="right">The second quaternion to multiply.</param>
        /// <returns>The multiplied quaternion.</returns>
        public static Quaternion Multiply(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Quaternion.Multiply(ref left, ref right, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Reverses the direction of a given quaternion.
        /// </summary>
        /// <param name="value">The quaternion to negate.</param>
        /// <param name="result">When the method completes, contains a quaternion facing in the opposite direction.</param>
        public static void Negate(ref Quaternion value, out Quaternion result)
        {
            result = default;
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        /// <summary>
        /// Reverses the direction of a given quaternion.
        /// </summary>
        /// <param name="value">The quaternion to negate.</param>
        /// <returns>A quaternion facing in the opposite direction.</returns>
        public static Quaternion Negate(Quaternion value)
        {
            Quaternion quaternion;
            Quaternion.Negate(ref value, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Conjugates a quaternion.
        /// </summary>
        /// <param name="value">The quaternion to conjugate.</param>
        /// <param name="result">When the method completes, contains the conjugated quaternion.</param>
        public static void Conjugate(ref Quaternion value, out Quaternion result)
        {
            result = default;
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }

        /// <summary>
        /// Conjugates a quaternion.
        /// </summary>
        /// <param name="value">The quaternion to conjugate.</param>
        /// <returns>The conjugated quaternion.</returns>
        public static Quaternion Conjugate(Quaternion value)
        {
            Quaternion quaternion;
            Quaternion.Conjugate(ref value, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Calculates the dot product of two quaternions.
        /// </summary>
        /// <param name="left">First source quaternion.</param>
        /// <param name="right">Second source quaternion.</param>
        /// <param name="result">When the method completes, contains the dot product of the two quaternions.</param>
        public static void Dot(ref Quaternion left, ref Quaternion right, out float result)
        {
            result = left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W;
        }

        /// <summary>
        /// Calculates the dot product of two quaternions.
        /// </summary>
        /// <param name="left">First source quaternion.</param>
        /// <param name="right">Second source quaternion.</param>
        /// <returns>The dot product of the two quaternions.</returns>
        public static float Dot(Quaternion left, Quaternion right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W;
        }


        /// <summary>
        /// Creates a quaternion given a rotation matrix.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <param name="result">When the method completes, contains the newly created quaternion.</param>
        // Token: 0x0600098F RID: 2447 RVA: 0x00028DF0 File Offset: 0x00026FF0
        public static void RotationMatrix(ref Matrix matrix, out Quaternion result)
        {
            result = default;
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

        /// <summary>
        /// Creates a quaternion given a rotation matrix.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>The newly created quaternion.</returns>
        public static Quaternion RotationMatrix(Matrix matrix)
        {
            Quaternion quaternion;
            Quaternion.RotationMatrix(ref matrix, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Creates a quaternion given a yaw, pitch, and roll value.
        /// </summary>
        /// <param name="yaw">The yaw of rotation.</param>
        /// <param name="pitch">The pitch of rotation.</param>
        /// <param name="roll">The roll of rotation.</param>
        /// <param name="result">When the method completes, contains the newly created quaternion.</param>
        public static void RotationYawPitchRoll(float yaw, float pitch, float roll, out Quaternion result)
        {
            result = default;
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

        /// <summary>
        /// Creates a quaternion given a yaw, pitch, and roll value.
        /// </summary>
        /// <param name="yaw">The yaw of rotation.</param>
        /// <param name="pitch">The pitch of rotation.</param>
        /// <param name="roll">The roll of rotation.</param>
        /// <returns>The newly created quaternion.</returns>
        public static Quaternion RotationYawPitchRoll(float yaw, float pitch, float roll)
        {
            Quaternion quaternion;
            Quaternion.RotationYawPitchRoll(yaw, pitch, roll, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Adds two quaternions.
        /// </summary>
        /// <param name="left">The first quaternion to add.</param>
        /// <param name="right">The second quaternion to add.</param>
        /// <returns>The sum of the two quaternions.</returns>
        public static Quaternion operator +(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Quaternion.Add(ref left, ref right, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Subtracts two quaternions.
        /// </summary>
        /// <param name="left">The first quaternion to subtract.</param>
        /// <param name="right">The second quaternion to subtract.</param>
        /// <returns>The difference of the two quaternions.</returns>
        public static Quaternion operator -(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Quaternion.Subtract(ref left, ref right, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Reverses the direction of a given quaternion.
        /// </summary>
        /// <param name="value">The quaternion to negate.</param>
        /// <returns>A quaternion facing in the opposite direction.</returns>
        public static Quaternion operator -(Quaternion value)
        {
            Quaternion quaternion;
            Quaternion.Negate(ref value, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Scales a quaternion by the given value.
        /// </summary>
        /// <param name="value">The quaternion to scale.</param>
        /// <param name="scale">The amount by which to scale the quaternion.</param>
        /// <returns>The scaled quaternion.</returns>
        public static Quaternion operator *(float scale, Quaternion value)
        {
            Quaternion quaternion;
            Quaternion.Multiply(ref value, scale, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Scales a quaternion by the given value.
        /// </summary>
        /// <param name="value">The quaternion to scale.</param>
        /// <param name="scale">The amount by which to scale the quaternion.</param>
        /// <returns>The scaled quaternion.</returns>
        public static Quaternion operator *(Quaternion value, float scale)
        {
            Quaternion quaternion;
            Quaternion.Multiply(ref value, scale, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Multiplies a quaternion by another.
        /// </summary>
        /// <param name="left">The first quaternion to multiply.</param>
        /// <param name="right">The second quaternion to multiply.</param>
        /// <returns>The multiplied quaternion.</returns>
        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Quaternion.Multiply(ref left, ref right, out quaternion);
            return quaternion;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2} W:{3}", new object[] { this.X, this.Y, this.Z, this.W });
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
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

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "X:{0} Y:{1} Z:{2} W:{3}", new object[] { this.X, this.Y, this.Z, this.W });
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
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

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int num = this.X.GetHashCode();
            num = (num * 397) ^ this.Y.GetHashCode();
            num = (num * 397) ^ this.Z.GetHashCode();
            return (num * 397) ^ this.W.GetHashCode();
        }

        /// <summary>
        /// The size of the <see cref="T:SharpDX.Quaternion" /> type, in bytes.
        /// </summary>
        // Token: 0x04000EF3 RID: 3827
        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Quaternion));

        /// <summary>
        /// A <see cref="T:SharpDX.Quaternion" /> with all of its components set to zero.
        /// </summary>
        // Token: 0x04000EF4 RID: 3828
        public static readonly Quaternion Zero = default(Quaternion);

        /// <summary>
        /// A <see cref="T:SharpDX.Quaternion" /> with all of its components set to one.
        /// </summary>
        // Token: 0x04000EF5 RID: 3829
        public static readonly Quaternion One = new Quaternion(1f, 1f, 1f, 1f);

        /// <summary>
        /// The identity <see cref="T:SharpDX.Quaternion" /> (0, 0, 0, 1).
        /// </summary>
        // Token: 0x04000EF6 RID: 3830
        public static readonly Quaternion Identity = new Quaternion(0f, 0f, 0f, 1f);

        /// <summary>
        /// The X component of the quaternion.
        /// </summary>
        // Token: 0x04000EF7 RID: 3831
        public float X { get; set; }

        /// <summary>
        /// The Y component of the quaternion.
        /// </summary>
        // Token: 0x04000EF8 RID: 3832
        public float Y { get; set; }

        /// <summary>
        /// The Z component of the quaternion.
        /// </summary>
        // Token: 0x04000EF9 RID: 3833
        public float Z { get; set; }

        /// <summary>
        /// The W component of the quaternion.
        /// </summary>
        // Token: 0x04000EFA RID: 3834
        public float W { get; set; }
    }
}
