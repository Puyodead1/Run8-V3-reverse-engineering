// part of sharpdx, stripped down

using System.Globalization;
using System.Runtime.InteropServices;

namespace LibRun8.Common
{
    /// <summary>
    /// Represents a 4x4 mathematical matrix.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix : IFormattable
    {
        /// <summary>
        /// Gets or sets the up <see cref="T:SharpDX.Vector3" /> of the matrix; that is M21, M22, and M23.
        /// </summary>
        public Vector3 Up
        {
            get
            {
                Vector3 vector = default;
                vector.X = this.M21;
                vector.Y = this.M22;
                vector.Z = this.M23;
                return vector;
            }
            set
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the down <see cref="T:SharpDX.Vector3" /> of the matrix; that is -M21, -M22, and -M23.
        /// </summary>
        public Vector3 Down
        {
            get
            {
                Vector3 vector = default;
                vector.X = -this.M21;
                vector.Y = -this.M22;
                vector.Z = -this.M23;
                return vector;
            }
            set
            {
                this.M21 = -value.X;
                this.M22 = -value.Y;
                this.M23 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the right <see cref="T:SharpDX.Vector3" /> of the matrix; that is M11, M12, and M13.
        /// </summary>
        public Vector3 Right
        {
            get
            {
                Vector3 vector = default;
                vector.X = this.M11;
                vector.Y = this.M12;
                vector.Z = this.M13;
                return vector;
            }
            set
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the left <see cref="T:SharpDX.Vector3" /> of the matrix; that is -M11, -M12, and -M13.
        /// </summary>
        public Vector3 Left
        {
            get
            {
                Vector3 vector = default;
                vector.X = -this.M11;
                vector.Y = -this.M12;
                vector.Z = -this.M13;
                return vector;
            }
            set
            {
                this.M11 = -value.X;
                this.M12 = -value.Y;
                this.M13 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the forward <see cref="T:SharpDX.Vector3" /> of the matrix; that is -M31, -M32, and -M33.
        /// </summary>
        public Vector3 Forward
        {
            get
            {
                Vector3 vector = default;
                vector.X = -this.M31;
                vector.Y = -this.M32;
                vector.Z = -this.M33;
                return vector;
            }
            set
            {
                this.M31 = -value.X;
                this.M32 = -value.Y;
                this.M33 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the backward <see cref="T:SharpDX.Vector3" /> of the matrix; that is M31, M32, and M33.
        /// </summary>
        public Vector3 Backward
        {
            get
            {
                Vector3 vector = default;
                vector.X = this.M31;
                vector.Y = this.M32;
                vector.Z = this.M33;
                return vector;
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Matrix" /> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Matrix" /> struct.
        /// </summary>
        /// <param name="M11">The value to assign at row 1 column 1 of the matrix.</param>
        /// <param name="M12">The value to assign at row 1 column 2 of the matrix.</param>
        /// <param name="M13">The value to assign at row 1 column 3 of the matrix.</param>
        /// <param name="M14">The value to assign at row 1 column 4 of the matrix.</param>
        /// <param name="M21">The value to assign at row 2 column 1 of the matrix.</param>
        /// <param name="M22">The value to assign at row 2 column 2 of the matrix.</param>
        /// <param name="M23">The value to assign at row 2 column 3 of the matrix.</param>
        /// <param name="M24">The value to assign at row 2 column 4 of the matrix.</param>
        /// <param name="M31">The value to assign at row 3 column 1 of the matrix.</param>
        /// <param name="M32">The value to assign at row 3 column 2 of the matrix.</param>
        /// <param name="M33">The value to assign at row 3 column 3 of the matrix.</param>
        /// <param name="M34">The value to assign at row 3 column 4 of the matrix.</param>
        /// <param name="M41">The value to assign at row 4 column 1 of the matrix.</param>
        /// <param name="M42">The value to assign at row 4 column 2 of the matrix.</param>
        /// <param name="M43">The value to assign at row 4 column 3 of the matrix.</param>
        /// <param name="M44">The value to assign at row 4 column 4 of the matrix.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpDX.Matrix" /> struct.
        /// </summary>
        /// <param name="values">The values to assign to the components of the matrix. This must be an array with sixteen elements.</param>
        /// <exception cref="T:System.ArgumentNullException">Thrown when <paramref name="values" /> is <c>null</c>.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when <paramref name="values" /> contains more or less than sixteen elements.</exception>
        public Matrix(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 16)
            {
                throw new ArgumentOutOfRangeException("values", "There must be sixteen and only sixteen input values for Matrix.");
            }
            this.M11 = values[0];
            this.M12 = values[1];
            this.M13 = values[2];
            this.M14 = values[3];
            this.M21 = values[4];
            this.M22 = values[5];
            this.M23 = values[6];
            this.M24 = values[7];
            this.M31 = values[8];
            this.M32 = values[9];
            this.M33 = values[10];
            this.M34 = values[11];
            this.M41 = values[12];
            this.M42 = values[13];
            this.M43 = values[14];
            this.M44 = values[15];
        }

        /// <summary>
        /// Gets or sets the first row in the matrix; that is M11, M12, M13, and M14.
        /// </summary>
        public Vector4 Row1
        {
            get
            {
                return new Vector4(this.M11, this.M12, this.M13, this.M14);
            }
            set
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
                this.M14 = value.W;
            }
        }

        /// <summary>
        /// Gets or sets the second row in the matrix; that is M21, M22, M23, and M24.
        /// </summary>
        public Vector4 Row2
        {
            get
            {
                return new Vector4(this.M21, this.M22, this.M23, this.M24);
            }
            set
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
                this.M24 = value.W;
            }
        }

        /// <summary>
        /// Gets or sets the third row in the matrix; that is M31, M32, M33, and M34.
        /// </summary>
        public Vector4 Row3
        {
            get
            {
                return new Vector4(this.M31, this.M32, this.M33, this.M34);
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
                this.M34 = value.W;
            }
        }

        /// <summary>
        /// Gets or sets the fourth row in the matrix; that is M41, M42, M43, and M44.
        /// </summary>
        public Vector4 Row4
        {
            get
            {
                return new Vector4(this.M41, this.M42, this.M43, this.M44);
            }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
                this.M44 = value.W;
            }
        }

        /// <summary>
        /// Gets or sets the first column in the matrix; that is M11, M21, M31, and M41.
        /// </summary>
        public Vector4 Column1
        {
            get
            {
                return new Vector4(this.M11, this.M21, this.M31, this.M41);
            }
            set
            {
                this.M11 = value.X;
                this.M21 = value.Y;
                this.M31 = value.Z;
                this.M41 = value.W;
            }
        }

        /// <summary>
        /// Gets or sets the second column in the matrix; that is M12, M22, M32, and M42.
        /// </summary>
        public Vector4 Column2
        {
            get
            {
                return new Vector4(this.M12, this.M22, this.M32, this.M42);
            }
            set
            {
                this.M12 = value.X;
                this.M22 = value.Y;
                this.M32 = value.Z;
                this.M42 = value.W;
            }
        }

        /// <summary>
        /// Gets or sets the third column in the matrix; that is M13, M23, M33, and M43.
        /// </summary>
        public Vector4 Column3
        {
            get
            {
                return new Vector4(this.M13, this.M23, this.M33, this.M43);
            }
            set
            {
                this.M13 = value.X;
                this.M23 = value.Y;
                this.M33 = value.Z;
                this.M43 = value.W;
            }
        }

        /// <summary>
        /// Gets or sets the fourth column in the matrix; that is M14, M24, M34, and M44.
        /// </summary>
        public Vector4 Column4
        {
            get
            {
                return new Vector4(this.M14, this.M24, this.M34, this.M44);
            }
            set
            {
                this.M14 = value.X;
                this.M24 = value.Y;
                this.M34 = value.Z;
                this.M44 = value.W;
            }
        }

        /// <summary>
        /// Gets or sets the translation of the matrix; that is M41, M42, and M43.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the scale of the matrix; that is M11, M22, and M33.
        /// </summary>
        public Vector3 ScaleVector
        {
            get
            {
                return new Vector3(this.M11, this.M22, this.M33);
            }
            set
            {
                this.M11 = value.X;
                this.M22 = value.Y;
                this.M33 = value.Z;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is an identity matrix.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is an identity matrix; otherwise, <c>false</c>.
        /// </value>
        public bool IsIdentity
        {
            get
            {
                return this.Equals(Matrix.Identity);
            }
        }

        /// <summary>
        /// Gets or sets the component at the specified index.
        /// </summary>
        /// <value>The value of the matrix component, depending on the index.</value>
        /// <param name="index">The zero-based index of the component to access.</param>
        /// <returns>The value of the component at the specified index.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when the <paramref name="index" /> is out of the range [0, 15].</exception>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.M11;
                    case 1:
                        return this.M12;
                    case 2:
                        return this.M13;
                    case 3:
                        return this.M14;
                    case 4:
                        return this.M21;
                    case 5:
                        return this.M22;
                    case 6:
                        return this.M23;
                    case 7:
                        return this.M24;
                    case 8:
                        return this.M31;
                    case 9:
                        return this.M32;
                    case 10:
                        return this.M33;
                    case 11:
                        return this.M34;
                    case 12:
                        return this.M41;
                    case 13:
                        return this.M42;
                    case 14:
                        return this.M43;
                    case 15:
                        return this.M44;
                    default:
                        throw new ArgumentOutOfRangeException("index", "Indices for Matrix run from 0 to 15, inclusive.");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.M11 = value;
                        return;
                    case 1:
                        this.M12 = value;
                        return;
                    case 2:
                        this.M13 = value;
                        return;
                    case 3:
                        this.M14 = value;
                        return;
                    case 4:
                        this.M21 = value;
                        return;
                    case 5:
                        this.M22 = value;
                        return;
                    case 6:
                        this.M23 = value;
                        return;
                    case 7:
                        this.M24 = value;
                        return;
                    case 8:
                        this.M31 = value;
                        return;
                    case 9:
                        this.M32 = value;
                        return;
                    case 10:
                        this.M33 = value;
                        return;
                    case 11:
                        this.M34 = value;
                        return;
                    case 12:
                        this.M41 = value;
                        return;
                    case 13:
                        this.M42 = value;
                        return;
                    case 14:
                        this.M43 = value;
                        return;
                    case 15:
                        this.M44 = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException("index", "Indices for Matrix run from 0 to 15, inclusive.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the component at the specified index.
        /// </summary>
        /// <value>The value of the matrix component, depending on the index.</value>
        /// <param name="row">The row of the matrix to access.</param>
        /// <param name="column">The column of the matrix to access.</param>
        /// <returns>The value of the component at the specified index.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when the <paramref name="row" /> or <paramref name="column" />is out of the range [0, 3].</exception>
        // Token: 0x170000B9 RID: 185
        public float this[int row, int column]
        {
            get
            {
                if (row < 0 || row > 3)
                {
                    throw new ArgumentOutOfRangeException("row", "Rows and columns for matrices run from 0 to 3, inclusive.");
                }
                if (column < 0 || column > 3)
                {
                    throw new ArgumentOutOfRangeException("column", "Rows and columns for matrices run from 0 to 3, inclusive.");
                }
                return this[row * 4 + column];
            }
            set
            {
                if (row < 0 || row > 3)
                {
                    throw new ArgumentOutOfRangeException("row", "Rows and columns for matrices run from 0 to 3, inclusive.");
                }
                if (column < 0 || column > 3)
                {
                    throw new ArgumentOutOfRangeException("column", "Rows and columns for matrices run from 0 to 3, inclusive.");
                }
                this[row * 4 + column] = value;
            }
        }

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        /// <returns>The determinant of the matrix.</returns>
        // Token: 0x060007E8 RID: 2024 RVA: 0x00020858 File Offset: 0x0001EA58
        public float Determinant()
        {
            float num = this.M33 * this.M44 - this.M34 * this.M43;
            float num2 = this.M32 * this.M44 - this.M34 * this.M42;
            float num3 = this.M32 * this.M43 - this.M33 * this.M42;
            float num4 = this.M31 * this.M44 - this.M34 * this.M41;
            float num5 = this.M31 * this.M43 - this.M33 * this.M41;
            float num6 = this.M31 * this.M42 - this.M32 * this.M41;
            return this.M11 * (this.M22 * num - this.M23 * num2 + this.M24 * num3) - this.M12 * (this.M21 * num - this.M23 * num4 + this.M24 * num5) + this.M13 * (this.M21 * num2 - this.M22 * num4 + this.M24 * num6) - this.M14 * (this.M21 * num3 - this.M22 * num5 + this.M23 * num6);
        }

        /// <summary>
        /// Inverts the matrix.
        /// </summary>
        // Token: 0x060007E9 RID: 2025 RVA: 0x0002099C File Offset: 0x0001EB9C
        public void Invert()
        {
            Matrix.Invert(ref this, out this);
        }

        /// <summary>
        /// Exchanges two rows in the matrix.
        /// </summary>
        /// <param name="firstRow">The first row to exchange. This is an index of the row starting at zero.</param>
        /// <param name="secondRow">The second row to exchange. This is an index of the row starting at zero.</param>
        // Token: 0x060007F1 RID: 2033 RVA: 0x00020EEC File Offset: 0x0001F0EC
        public void ExchangeRows(int firstRow, int secondRow)
        {
            if (firstRow < 0)
            {
                throw new ArgumentOutOfRangeException("firstRow", "The parameter firstRow must be greater than or equal to zero.");
            }
            if (firstRow > 3)
            {
                throw new ArgumentOutOfRangeException("firstRow", "The parameter firstRow must be less than or equal to three.");
            }
            if (secondRow < 0)
            {
                throw new ArgumentOutOfRangeException("secondRow", "The parameter secondRow must be greater than or equal to zero.");
            }
            if (secondRow > 3)
            {
                throw new ArgumentOutOfRangeException("secondRow", "The parameter secondRow must be less than or equal to three.");
            }
            if (firstRow == secondRow)
            {
                return;
            }
            float num = this[secondRow, 0];
            float num2 = this[secondRow, 1];
            float num3 = this[secondRow, 2];
            float num4 = this[secondRow, 3];
            this[secondRow, 0] = this[firstRow, 0];
            this[secondRow, 1] = this[firstRow, 1];
            this[secondRow, 2] = this[firstRow, 2];
            this[secondRow, 3] = this[firstRow, 3];
            this[firstRow, 0] = num;
            this[firstRow, 1] = num2;
            this[firstRow, 2] = num3;
            this[firstRow, 3] = num4;
        }

        /// <summary>
        /// Exchanges two columns in the matrix.
        /// </summary>
        /// <param name="firstColumn">The first column to exchange. This is an index of the column starting at zero.</param>
        /// <param name="secondColumn">The second column to exchange. This is an index of the column starting at zero.</param>
        // Token: 0x060007F2 RID: 2034 RVA: 0x00020FD8 File Offset: 0x0001F1D8
        public void ExchangeColumns(int firstColumn, int secondColumn)
        {
            if (firstColumn < 0)
            {
                throw new ArgumentOutOfRangeException("firstColumn", "The parameter firstColumn must be greater than or equal to zero.");
            }
            if (firstColumn > 3)
            {
                throw new ArgumentOutOfRangeException("firstColumn", "The parameter firstColumn must be less than or equal to three.");
            }
            if (secondColumn < 0)
            {
                throw new ArgumentOutOfRangeException("secondColumn", "The parameter secondColumn must be greater than or equal to zero.");
            }
            if (secondColumn > 3)
            {
                throw new ArgumentOutOfRangeException("secondColumn", "The parameter secondColumn must be less than or equal to three.");
            }
            if (firstColumn == secondColumn)
            {
                return;
            }
            float num = this[0, secondColumn];
            float num2 = this[1, secondColumn];
            float num3 = this[2, secondColumn];
            float num4 = this[3, secondColumn];
            this[0, secondColumn] = this[0, firstColumn];
            this[1, secondColumn] = this[1, firstColumn];
            this[2, secondColumn] = this[2, firstColumn];
            this[3, secondColumn] = this[3, firstColumn];
            this[0, firstColumn] = num;
            this[1, firstColumn] = num2;
            this[2, firstColumn] = num3;
            this[3, firstColumn] = num4;
        }

        /// <summary>
        /// Creates an array containing the elements of the matrix.
        /// </summary>
        /// <returns>A sixteen-element array containing the components of the matrix.</returns>
        // Token: 0x060007F3 RID: 2035 RVA: 0x000210C4 File Offset: 0x0001F2C4
        public float[] ToArray()
        {
            return new float[]
            {
                this.M11, this.M12, this.M13, this.M14, this.M21, this.M22, this.M23, this.M24, this.M31, this.M32,
                this.M33, this.M34, this.M41, this.M42, this.M43, this.M44
            };
        }

        /// <summary>
        /// Determines the sum of two matrices.
        /// </summary>
        /// <param name="left">The first matrix to add.</param>
        /// <param name="right">The second matrix to add.</param>
        /// <param name="result">When the method completes, contains the sum of the two matrices.</param>
        // Token: 0x060007F4 RID: 2036 RVA: 0x00021174 File Offset: 0x0001F374
        public static void Add(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result.M11 = left.M11 + right.M11;
            result.M12 = left.M12 + right.M12;
            result.M13 = left.M13 + right.M13;
            result.M14 = left.M14 + right.M14;
            result.M21 = left.M21 + right.M21;
            result.M22 = left.M22 + right.M22;
            result.M23 = left.M23 + right.M23;
            result.M24 = left.M24 + right.M24;
            result.M31 = left.M31 + right.M31;
            result.M32 = left.M32 + right.M32;
            result.M33 = left.M33 + right.M33;
            result.M34 = left.M34 + right.M34;
            result.M41 = left.M41 + right.M41;
            result.M42 = left.M42 + right.M42;
            result.M43 = left.M43 + right.M43;
            result.M44 = left.M44 + right.M44;
        }

        /// <summary>
        /// Determines the sum of two matrices.
        /// </summary>
        /// <param name="left">The first matrix to add.</param>
        /// <param name="right">The second matrix to add.</param>
        /// <returns>The sum of the two matrices.</returns>
        // Token: 0x060007F5 RID: 2037 RVA: 0x000212B4 File Offset: 0x0001F4B4
        public static Matrix Add(Matrix left, Matrix right)
        {
            Matrix matrix;
            Matrix.Add(ref left, ref right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Determines the difference between two matrices.
        /// </summary>
        /// <param name="left">The first matrix to subtract.</param>
        /// <param name="right">The second matrix to subtract.</param>
        /// <param name="result">When the method completes, contains the difference between the two matrices.</param>
        // Token: 0x060007F6 RID: 2038 RVA: 0x000212D0 File Offset: 0x0001F4D0
        public static void Subtract(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result.M11 = left.M11 - right.M11;
            result.M12 = left.M12 - right.M12;
            result.M13 = left.M13 - right.M13;
            result.M14 = left.M14 - right.M14;
            result.M21 = left.M21 - right.M21;
            result.M22 = left.M22 - right.M22;
            result.M23 = left.M23 - right.M23;
            result.M24 = left.M24 - right.M24;
            result.M31 = left.M31 - right.M31;
            result.M32 = left.M32 - right.M32;
            result.M33 = left.M33 - right.M33;
            result.M34 = left.M34 - right.M34;
            result.M41 = left.M41 - right.M41;
            result.M42 = left.M42 - right.M42;
            result.M43 = left.M43 - right.M43;
            result.M44 = left.M44 - right.M44;
        }

        /// <summary>
        /// Determines the difference between two matrices.
        /// </summary>
        /// <param name="left">The first matrix to subtract.</param>
        /// <param name="right">The second matrix to subtract.</param>
        /// <returns>The difference between the two matrices.</returns>
        // Token: 0x060007F7 RID: 2039 RVA: 0x00021410 File Offset: 0x0001F610
        public static Matrix Subtract(Matrix left, Matrix right)
        {
            Matrix matrix;
            Matrix.Subtract(ref left, ref right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Scales a matrix by the given value.
        /// </summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The amount by which to scale.</param>
        /// <param name="result">When the method completes, contains the scaled matrix.</param>
        // Token: 0x060007F8 RID: 2040 RVA: 0x0002142C File Offset: 0x0001F62C
        public static void Multiply(ref Matrix left, float right, out Matrix result)
        {
            result.M11 = left.M11 * right;
            result.M12 = left.M12 * right;
            result.M13 = left.M13 * right;
            result.M14 = left.M14 * right;
            result.M21 = left.M21 * right;
            result.M22 = left.M22 * right;
            result.M23 = left.M23 * right;
            result.M24 = left.M24 * right;
            result.M31 = left.M31 * right;
            result.M32 = left.M32 * right;
            result.M33 = left.M33 * right;
            result.M34 = left.M34 * right;
            result.M41 = left.M41 * right;
            result.M42 = left.M42 * right;
            result.M43 = left.M43 * right;
            result.M44 = left.M44 * right;
        }

        /// <summary>
        /// Scales a matrix by the given value.
        /// </summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The amount by which to scale.</param>
        /// <returns>The scaled matrix.</returns>
        // Token: 0x060007F9 RID: 2041 RVA: 0x0002151C File Offset: 0x0001F71C
        public static Matrix Multiply(Matrix left, float right)
        {
            Matrix matrix;
            Matrix.Multiply(ref left, right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Determines the product of two matrices.
        /// </summary>
        /// <param name="left">The first matrix to multiply.</param>
        /// <param name="right">The second matrix to multiply.</param>
        /// <param name="result">The product of the two matrices.</param>
        // Token: 0x060007FA RID: 2042 RVA: 0x00021534 File Offset: 0x0001F734
        public static void Multiply(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result = new Matrix
            {
                M11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41,
                M12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42,
                M13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43,
                M14 = left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34 + left.M14 * right.M44,
                M21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41,
                M22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42,
                M23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43,
                M24 = left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34 + left.M24 * right.M44,
                M31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41,
                M32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42,
                M33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43,
                M34 = left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34 + left.M34 * right.M44,
                M41 = left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41,
                M42 = left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42,
                M43 = left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33 + left.M44 * right.M43,
                M44 = left.M41 * right.M14 + left.M42 * right.M24 + left.M43 * right.M34 + left.M44 * right.M44
            };
        }

        /// <summary>
        /// Determines the product of two matrices.
        /// </summary>
        /// <param name="left">The first matrix to multiply.</param>
        /// <param name="right">The second matrix to multiply.</param>
        /// <returns>The product of the two matrices.</returns>
        // Token: 0x060007FB RID: 2043 RVA: 0x00021930 File Offset: 0x0001FB30
        public static Matrix Multiply(Matrix left, Matrix right)
        {
            Matrix matrix;
            Matrix.Multiply(ref left, ref right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Scales a matrix by the given value.
        /// </summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The amount by which to scale.</param>
        /// <param name="result">When the method completes, contains the scaled matrix.</param>
        // Token: 0x060007FC RID: 2044 RVA: 0x0002194C File Offset: 0x0001FB4C
        public static void Divide(ref Matrix left, float right, out Matrix result)
        {
            float num = 1f / right;
            result.M11 = left.M11 * num;
            result.M12 = left.M12 * num;
            result.M13 = left.M13 * num;
            result.M14 = left.M14 * num;
            result.M21 = left.M21 * num;
            result.M22 = left.M22 * num;
            result.M23 = left.M23 * num;
            result.M24 = left.M24 * num;
            result.M31 = left.M31 * num;
            result.M32 = left.M32 * num;
            result.M33 = left.M33 * num;
            result.M34 = left.M34 * num;
            result.M41 = left.M41 * num;
            result.M42 = left.M42 * num;
            result.M43 = left.M43 * num;
            result.M44 = left.M44 * num;
        }

        /// <summary>
        /// Scales a matrix by the given value.
        /// </summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The amount by which to scale.</param>
        /// <returns>The scaled matrix.</returns>
        // Token: 0x060007FD RID: 2045 RVA: 0x00021A44 File Offset: 0x0001FC44
        public static Matrix Divide(Matrix left, float right)
        {
            Matrix matrix;
            Matrix.Divide(ref left, right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Determines the quotient of two matrices.
        /// </summary>
        /// <param name="left">The first matrix to divide.</param>
        /// <param name="right">The second matrix to divide.</param>
        /// <param name="result">When the method completes, contains the quotient of the two matrices.</param>
        // Token: 0x060007FE RID: 2046 RVA: 0x00021A5C File Offset: 0x0001FC5C
        public static void Divide(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result.M11 = left.M11 / right.M11;
            result.M12 = left.M12 / right.M12;
            result.M13 = left.M13 / right.M13;
            result.M14 = left.M14 / right.M14;
            result.M21 = left.M21 / right.M21;
            result.M22 = left.M22 / right.M22;
            result.M23 = left.M23 / right.M23;
            result.M24 = left.M24 / right.M24;
            result.M31 = left.M31 / right.M31;
            result.M32 = left.M32 / right.M32;
            result.M33 = left.M33 / right.M33;
            result.M34 = left.M34 / right.M34;
            result.M41 = left.M41 / right.M41;
            result.M42 = left.M42 / right.M42;
            result.M43 = left.M43 / right.M43;
            result.M44 = left.M44 / right.M44;
        }

        /// <summary>
        /// Determines the quotient of two matrices.
        /// </summary>
        /// <param name="left">The first matrix to divide.</param>
        /// <param name="right">The second matrix to divide.</param>
        /// <returns>The quotient of the two matrices.</returns>
        // Token: 0x060007FF RID: 2047 RVA: 0x00021B9C File Offset: 0x0001FD9C
        public static Matrix Divide(Matrix left, Matrix right)
        {
            Matrix matrix;
            Matrix.Divide(ref left, ref right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Performs the exponential operation on a matrix.
        /// </summary>
        /// <param name="value">The matrix to perform the operation on.</param>
        /// <param name="exponent">The exponent to raise the matrix to.</param>
        /// <param name="result">When the method completes, contains the exponential matrix.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when the <paramref name="exponent" /> is negative.</exception>
        // Token: 0x06000800 RID: 2048 RVA: 0x00021BB8 File Offset: 0x0001FDB8
        public static void Exponent(ref Matrix value, int exponent, out Matrix result)
        {
            if (exponent < 0)
            {
                throw new ArgumentOutOfRangeException("exponent", "The exponent can not be negative.");
            }
            if (exponent == 0)
            {
                result = Matrix.Identity;
                return;
            }
            if (exponent == 1)
            {
                result = value;
                return;
            }
            Matrix matrix = Matrix.Identity;
            Matrix matrix2 = value;
            for (; ; )
            {
                if ((exponent & 1) != 0)
                {
                    matrix *= matrix2;
                }
                exponent /= 2;
                if (exponent <= 0)
                {
                    break;
                }
                matrix2 *= matrix2;
            }
            result = matrix;
        }

        /// <summary>
        /// Performs the exponential operation on a matrix.
        /// </summary>
        /// <param name="value">The matrix to perform the operation on.</param>
        /// <param name="exponent">The exponent to raise the matrix to.</param>
        /// <returns>The exponential matrix.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when the <paramref name="exponent" /> is negative.</exception>
        // Token: 0x06000801 RID: 2049 RVA: 0x00021C30 File Offset: 0x0001FE30
        public static Matrix Exponent(Matrix value, int exponent)
        {
            Matrix matrix;
            Matrix.Exponent(ref value, exponent, out matrix);
            return matrix;
        }

        /// <summary>
        /// Negates a matrix.
        /// </summary>
        /// <param name="value">The matrix to be negated.</param>
        /// <param name="result">When the method completes, contains the negated matrix.</param>
        // Token: 0x06000802 RID: 2050 RVA: 0x00021C48 File Offset: 0x0001FE48
        public static void Negate(ref Matrix value, out Matrix result)
        {
            result.M11 = -value.M11;
            result.M12 = -value.M12;
            result.M13 = -value.M13;
            result.M14 = -value.M14;
            result.M21 = -value.M21;
            result.M22 = -value.M22;
            result.M23 = -value.M23;
            result.M24 = -value.M24;
            result.M31 = -value.M31;
            result.M32 = -value.M32;
            result.M33 = -value.M33;
            result.M34 = -value.M34;
            result.M41 = -value.M41;
            result.M42 = -value.M42;
            result.M43 = -value.M43;
            result.M44 = -value.M44;
        }

        /// <summary>
        /// Negates a matrix.
        /// </summary>
        /// <param name="value">The matrix to be negated.</param>
        /// <returns>The negated matrix.</returns>
        // Token: 0x06000803 RID: 2051 RVA: 0x00021D28 File Offset: 0x0001FF28
        public static Matrix Negate(Matrix value)
        {
            Matrix matrix;
            Matrix.Negate(ref value, out matrix);
            return matrix;
        }

        /// <summary>
        /// Calculates the inverse of the specified matrix.
        /// </summary>
        /// <param name="value">The matrix whose inverse is to be calculated.</param>
        /// <param name="result">When the method completes, contains the inverse of the specified matrix.</param>
        // Token: 0x0600080B RID: 2059 RVA: 0x000220F0 File Offset: 0x000202F0
        public static void Invert(ref Matrix value, out Matrix result)
        {
            float num = value.M31 * value.M42 - value.M32 * value.M41;
            float num2 = value.M31 * value.M43 - value.M33 * value.M41;
            float num3 = value.M34 * value.M41 - value.M31 * value.M44;
            float num4 = value.M32 * value.M43 - value.M33 * value.M42;
            float num5 = value.M34 * value.M42 - value.M32 * value.M44;
            float num6 = value.M33 * value.M44 - value.M34 * value.M43;
            float num7 = value.M22 * num6 + value.M23 * num5 + value.M24 * num4;
            float num8 = value.M21 * num6 + value.M23 * num3 + value.M24 * num2;
            float num9 = value.M21 * -num5 + value.M22 * num3 + value.M24 * num;
            float num10 = value.M21 * num4 + value.M22 * -num2 + value.M23 * num;
            float num11 = value.M11 * num7 - value.M12 * num8 + value.M13 * num9 - value.M14 * num10;
            if (Math.Abs(num11) == 0f)
            {
                result = Matrix.Zero;
                return;
            }
            num11 = 1f / num11;
            float num12 = value.M11 * value.M22 - value.M12 * value.M21;
            float num13 = value.M11 * value.M23 - value.M13 * value.M21;
            float num14 = value.M14 * value.M21 - value.M11 * value.M24;
            float num15 = value.M12 * value.M23 - value.M13 * value.M22;
            float num16 = value.M14 * value.M22 - value.M12 * value.M24;
            float num17 = value.M13 * value.M24 - value.M14 * value.M23;
            float num18 = value.M12 * num6 + value.M13 * num5 + value.M14 * num4;
            float num19 = value.M11 * num6 + value.M13 * num3 + value.M14 * num2;
            float num20 = value.M11 * -num5 + value.M12 * num3 + value.M14 * num;
            float num21 = value.M11 * num4 + value.M12 * -num2 + value.M13 * num;
            float num22 = value.M42 * num17 + value.M43 * num16 + value.M44 * num15;
            float num23 = value.M41 * num17 + value.M43 * num14 + value.M44 * num13;
            float num24 = value.M41 * -num16 + value.M42 * num14 + value.M44 * num12;
            float num25 = value.M41 * num15 + value.M42 * -num13 + value.M43 * num12;
            float num26 = value.M32 * num17 + value.M33 * num16 + value.M34 * num15;
            float num27 = value.M31 * num17 + value.M33 * num14 + value.M34 * num13;
            float num28 = value.M31 * -num16 + value.M32 * num14 + value.M34 * num12;
            float num29 = value.M31 * num15 + value.M32 * -num13 + value.M33 * num12;
            result.M11 = num7 * num11;
            result.M12 = -num18 * num11;
            result.M13 = num22 * num11;
            result.M14 = -num26 * num11;
            result.M21 = -num8 * num11;
            result.M22 = num19 * num11;
            result.M23 = -num23 * num11;
            result.M24 = num27 * num11;
            result.M31 = num9 * num11;
            result.M32 = -num20 * num11;
            result.M33 = num24 * num11;
            result.M34 = -num28 * num11;
            result.M41 = -num10 * num11;
            result.M42 = num21 * num11;
            result.M43 = -num25 * num11;
            result.M44 = num29 * num11;
        }

        /// <summary>
        /// Calculates the inverse of the specified matrix.
        /// </summary>
        /// <param name="value">The matrix whose inverse is to be calculated.</param>
        /// <returns>The inverse of the specified matrix.</returns>
        // Token: 0x0600080C RID: 2060 RVA: 0x00022544 File Offset: 0x00020744
        public static Matrix Invert(Matrix value)
        {
            value.Invert();
            return value;
        }

        /// <summary>
        /// Creates a left-handed, orthographic projection matrix.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <param name="result">When the method completes, contains the created projection matrix.</param>
        // Token: 0x06000820 RID: 2080 RVA: 0x000235F4 File Offset: 0x000217F4
        public static void OrthoLH(float width, float height, float znear, float zfar, out Matrix result)
        {
            float num = width * 0.5f;
            float num2 = height * 0.5f;
            Matrix.OrthoOffCenterLH(-num, num, -num2, num2, znear, zfar, out result);
        }

        /// <summary>
        /// Creates a left-handed, orthographic projection matrix.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <returns>The created projection matrix.</returns>
        // Token: 0x06000821 RID: 2081 RVA: 0x00023620 File Offset: 0x00021820
        public static Matrix OrthoLH(float width, float height, float znear, float zfar)
        {
            Matrix matrix;
            Matrix.OrthoLH(width, height, znear, zfar, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a right-handed, orthographic projection matrix.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <param name="result">When the method completes, contains the created projection matrix.</param>
        // Token: 0x06000822 RID: 2082 RVA: 0x0002363C File Offset: 0x0002183C
        public static void OrthoRH(float width, float height, float znear, float zfar, out Matrix result)
        {
            float num = width * 0.5f;
            float num2 = height * 0.5f;
            Matrix.OrthoOffCenterRH(-num, num, -num2, num2, znear, zfar, out result);
        }

        /// <summary>
        /// Creates a right-handed, orthographic projection matrix.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <returns>The created projection matrix.</returns>
        // Token: 0x06000823 RID: 2083 RVA: 0x00023668 File Offset: 0x00021868
        public static Matrix OrthoRH(float width, float height, float znear, float zfar)
        {
            Matrix matrix;
            Matrix.OrthoRH(width, height, znear, zfar, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a left-handed, customized orthographic projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the viewing volume.</param>
        /// <param name="right">Maximum x-value of the viewing volume.</param>
        /// <param name="bottom">Minimum y-value of the viewing volume.</param>
        /// <param name="top">Maximum y-value of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <param name="result">When the method completes, contains the created projection matrix.</param>
        // Token: 0x06000824 RID: 2084 RVA: 0x00023684 File Offset: 0x00021884
        public static void OrthoOffCenterLH(float left, float right, float bottom, float top, float znear, float zfar, out Matrix result)
        {
            float num = 1f / (zfar - znear);
            result = Matrix.Identity;
            result.M11 = 2f / (right - left);
            result.M22 = 2f / (top - bottom);
            result.M33 = num;
            result.M41 = (left + right) / (left - right);
            result.M42 = (top + bottom) / (bottom - top);
            result.M43 = -znear * num;
        }

        /// <summary>
        /// Creates a left-handed, customized orthographic projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the viewing volume.</param>
        /// <param name="right">Maximum x-value of the viewing volume.</param>
        /// <param name="bottom">Minimum y-value of the viewing volume.</param>
        /// <param name="top">Maximum y-value of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <returns>The created projection matrix.</returns>
        // Token: 0x06000825 RID: 2085 RVA: 0x000236FC File Offset: 0x000218FC
        public static Matrix OrthoOffCenterLH(float left, float right, float bottom, float top, float znear, float zfar)
        {
            Matrix matrix;
            Matrix.OrthoOffCenterLH(left, right, bottom, top, znear, zfar, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a right-handed, customized orthographic projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the viewing volume.</param>
        /// <param name="right">Maximum x-value of the viewing volume.</param>
        /// <param name="bottom">Minimum y-value of the viewing volume.</param>
        /// <param name="top">Maximum y-value of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <param name="result">When the method completes, contains the created projection matrix.</param>
        // Token: 0x06000826 RID: 2086 RVA: 0x0002371C File Offset: 0x0002191C
        public static void OrthoOffCenterRH(float left, float right, float bottom, float top, float znear, float zfar, out Matrix result)
        {
            Matrix.OrthoOffCenterLH(left, right, bottom, top, znear, zfar, out result);
            result.M33 *= -1f;
        }

        /// <summary>
        /// Creates a right-handed, customized orthographic projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the viewing volume.</param>
        /// <param name="right">Maximum x-value of the viewing volume.</param>
        /// <param name="bottom">Minimum y-value of the viewing volume.</param>
        /// <param name="top">Maximum y-value of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <returns>The created projection matrix.</returns>
        // Token: 0x06000827 RID: 2087 RVA: 0x00023740 File Offset: 0x00021940
        public static Matrix OrthoOffCenterRH(float left, float right, float bottom, float top, float znear, float zfar)
        {
            Matrix matrix;
            Matrix.OrthoOffCenterRH(left, right, bottom, top, znear, zfar, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a left-handed, perspective projection matrix.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <param name="result">When the method completes, contains the created projection matrix.</param>
        // Token: 0x06000828 RID: 2088 RVA: 0x00023760 File Offset: 0x00021960
        public static void PerspectiveLH(float width, float height, float znear, float zfar, out Matrix result)
        {
            float num = width * 0.5f;
            float num2 = height * 0.5f;
            Matrix.PerspectiveOffCenterLH(-num, num, -num2, num2, znear, zfar, out result);
        }

        /// <summary>
        /// Creates a left-handed, perspective projection matrix.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <returns>The created projection matrix.</returns>
        // Token: 0x06000829 RID: 2089 RVA: 0x0002378C File Offset: 0x0002198C
        public static Matrix PerspectiveLH(float width, float height, float znear, float zfar)
        {
            Matrix matrix;
            Matrix.PerspectiveLH(width, height, znear, zfar, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a right-handed, perspective projection matrix.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <param name="result">When the method completes, contains the created projection matrix.</param>
        // Token: 0x0600082A RID: 2090 RVA: 0x000237A8 File Offset: 0x000219A8
        public static void PerspectiveRH(float width, float height, float znear, float zfar, out Matrix result)
        {
            float num = width * 0.5f;
            float num2 = height * 0.5f;
            Matrix.PerspectiveOffCenterRH(-num, num, -num2, num2, znear, zfar, out result);
        }

        /// <summary>
        /// Creates a right-handed, perspective projection matrix.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <returns>The created projection matrix.</returns>
        // Token: 0x0600082B RID: 2091 RVA: 0x000237D4 File Offset: 0x000219D4
        public static Matrix PerspectiveRH(float width, float height, float znear, float zfar)
        {
            Matrix matrix;
            Matrix.PerspectiveRH(width, height, znear, zfar, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a left-handed, perspective projection matrix based on a field of view.
        /// </summary>
        /// <param name="fov">Field of view in the y direction, in radians.</param>
        /// <param name="aspect">Aspect ratio, defined as view space width divided by height.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <param name="result">When the method completes, contains the created projection matrix.</param>
        // Token: 0x0600082C RID: 2092 RVA: 0x000237F0 File Offset: 0x000219F0
        public static void PerspectiveFovLH(float fov, float aspect, float znear, float zfar, out Matrix result)
        {
            float num = (float)(1.0 / Math.Tan((double)(fov * 0.5f)));
            float num2 = num / aspect;
            float num3 = znear / num2;
            float num4 = znear / num;
            Matrix.PerspectiveOffCenterLH(-num3, num3, -num4, num4, znear, zfar, out result);
        }

        /// <summary>
        /// Creates a left-handed, perspective projection matrix based on a field of view.
        /// </summary>
        /// <param name="fov">Field of view in the y direction, in radians.</param>
        /// <param name="aspect">Aspect ratio, defined as view space width divided by height.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <returns>The created projection matrix.</returns>
        // Token: 0x0600082D RID: 2093 RVA: 0x00023834 File Offset: 0x00021A34
        public static Matrix PerspectiveFovLH(float fov, float aspect, float znear, float zfar)
        {
            Matrix matrix;
            Matrix.PerspectiveFovLH(fov, aspect, znear, zfar, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a right-handed, perspective projection matrix based on a field of view.
        /// </summary>
        /// <param name="fov">Field of view in the y direction, in radians.</param>
        /// <param name="aspect">Aspect ratio, defined as view space width divided by height.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <param name="result">When the method completes, contains the created projection matrix.</param>
        // Token: 0x0600082E RID: 2094 RVA: 0x00023850 File Offset: 0x00021A50
        public static void PerspectiveFovRH(float fov, float aspect, float znear, float zfar, out Matrix result)
        {
            float num = (float)(1.0 / Math.Tan((double)(fov * 0.5f)));
            float num2 = num / aspect;
            float num3 = znear / num2;
            float num4 = znear / num;
            Matrix.PerspectiveOffCenterRH(-num3, num3, -num4, num4, znear, zfar, out result);
        }

        /// <summary>
        /// Creates a right-handed, perspective projection matrix based on a field of view.
        /// </summary>
        /// <param name="fov">Field of view in the y direction, in radians.</param>
        /// <param name="aspect">Aspect ratio, defined as view space width divided by height.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <returns>The created projection matrix.</returns>
        // Token: 0x0600082F RID: 2095 RVA: 0x00023894 File Offset: 0x00021A94
        public static Matrix PerspectiveFovRH(float fov, float aspect, float znear, float zfar)
        {
            Matrix matrix;
            Matrix.PerspectiveFovRH(fov, aspect, znear, zfar, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a left-handed, customized perspective projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the viewing volume.</param>
        /// <param name="right">Maximum x-value of the viewing volume.</param>
        /// <param name="bottom">Minimum y-value of the viewing volume.</param>
        /// <param name="top">Maximum y-value of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <param name="result">When the method completes, contains the created projection matrix.</param>
        // Token: 0x06000830 RID: 2096 RVA: 0x000238B0 File Offset: 0x00021AB0
        public static void PerspectiveOffCenterLH(float left, float right, float bottom, float top, float znear, float zfar, out Matrix result)
        {
            float num = zfar / (zfar - znear);
            result = default(Matrix);
            result.M11 = 2f * znear / (right - left);
            result.M22 = 2f * znear / (top - bottom);
            result.M31 = (left + right) / (left - right);
            result.M32 = (top + bottom) / (bottom - top);
            result.M33 = num;
            result.M34 = 1f;
            result.M43 = -znear * num;
        }

        /// <summary>
        /// Creates a left-handed, customized perspective projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the viewing volume.</param>
        /// <param name="right">Maximum x-value of the viewing volume.</param>
        /// <param name="bottom">Minimum y-value of the viewing volume.</param>
        /// <param name="top">Maximum y-value of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <returns>The created projection matrix.</returns>
        // Token: 0x06000831 RID: 2097 RVA: 0x00023930 File Offset: 0x00021B30
        public static Matrix PerspectiveOffCenterLH(float left, float right, float bottom, float top, float znear, float zfar)
        {
            Matrix matrix;
            Matrix.PerspectiveOffCenterLH(left, right, bottom, top, znear, zfar, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a right-handed, customized perspective projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the viewing volume.</param>
        /// <param name="right">Maximum x-value of the viewing volume.</param>
        /// <param name="bottom">Minimum y-value of the viewing volume.</param>
        /// <param name="top">Maximum y-value of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <param name="result">When the method completes, contains the created projection matrix.</param>
        // Token: 0x06000832 RID: 2098 RVA: 0x00023950 File Offset: 0x00021B50
        public static void PerspectiveOffCenterRH(float left, float right, float bottom, float top, float znear, float zfar, out Matrix result)
        {
            Matrix.PerspectiveOffCenterLH(left, right, bottom, top, znear, zfar, out result);
            result.M31 *= -1f;
            result.M32 *= -1f;
            result.M33 *= -1f;
            result.M34 *= -1f;
        }

        /// <summary>
        /// Creates a right-handed, customized perspective projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the viewing volume.</param>
        /// <param name="right">Maximum x-value of the viewing volume.</param>
        /// <param name="bottom">Minimum y-value of the viewing volume.</param>
        /// <param name="top">Maximum y-value of the viewing volume.</param>
        /// <param name="znear">Minimum z-value of the viewing volume.</param>
        /// <param name="zfar">Maximum z-value of the viewing volume.</param>
        /// <returns>The created projection matrix.</returns>
        // Token: 0x06000833 RID: 2099 RVA: 0x000239B8 File Offset: 0x00021BB8
        public static Matrix PerspectiveOffCenterRH(float left, float right, float bottom, float top, float znear, float zfar)
        {
            Matrix matrix;
            Matrix.PerspectiveOffCenterRH(left, right, bottom, top, znear, zfar, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a matrix that scales along the x-axis, y-axis, and y-axis.
        /// </summary>
        /// <param name="scale">Scaling factor for all three axes.</param>
        /// <param name="result">When the method completes, contains the created scaling matrix.</param>
        // Token: 0x06000838 RID: 2104 RVA: 0x00023C88 File Offset: 0x00021E88
        public static void Scaling(ref Vector3 scale, out Matrix result)
        {
            Matrix.Scaling(scale.X, scale.Y, scale.Z, out result);
        }

        /// <summary>
        /// Creates a matrix that scales along the x-axis, y-axis, and y-axis.
        /// </summary>
        /// <param name="scale">Scaling factor for all three axes.</param>
        /// <returns>The created scaling matrix.</returns>
        // Token: 0x06000839 RID: 2105 RVA: 0x00023CA4 File Offset: 0x00021EA4
        public static Matrix Scaling(Vector3 scale)
        {
            Matrix matrix;
            Matrix.Scaling(ref scale, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a matrix that scales along the x-axis, y-axis, and y-axis.
        /// </summary>
        /// <param name="x">Scaling factor that is applied along the x-axis.</param>
        /// <param name="y">Scaling factor that is applied along the y-axis.</param>
        /// <param name="z">Scaling factor that is applied along the z-axis.</param>
        /// <param name="result">When the method completes, contains the created scaling matrix.</param>
        // Token: 0x0600083A RID: 2106 RVA: 0x00023CBC File Offset: 0x00021EBC
        public static void Scaling(float x, float y, float z, out Matrix result)
        {
            result = Matrix.Identity;
            result.M11 = x;
            result.M22 = y;
            result.M33 = z;
        }

        /// <summary>
        /// Creates a matrix that scales along the x-axis, y-axis, and y-axis.
        /// </summary>
        /// <param name="x">Scaling factor that is applied along the x-axis.</param>
        /// <param name="y">Scaling factor that is applied along the y-axis.</param>
        /// <param name="z">Scaling factor that is applied along the z-axis.</param>
        /// <returns>The created scaling matrix.</returns>
        // Token: 0x0600083B RID: 2107 RVA: 0x00023CE0 File Offset: 0x00021EE0
        public static Matrix Scaling(float x, float y, float z)
        {
            Matrix matrix;
            Matrix.Scaling(x, y, z, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a matrix that uniformly scales along all three axis.
        /// </summary>
        /// <param name="scale">The uniform scale that is applied along all axis.</param>
        /// <param name="result">When the method completes, contains the created scaling matrix.</param>
        // Token: 0x0600083C RID: 2108 RVA: 0x00023CF8 File Offset: 0x00021EF8
        public static void Scaling(float scale, out Matrix result)
        {
            result = Matrix.Identity;
            result.M33 = scale;
            result.M22 = scale;
            result.M11 = scale;
        }

        /// <summary>
        /// Creates a matrix that uniformly scales along all three axis.
        /// </summary>
        /// <param name="scale">The uniform scale that is applied along all axis.</param>
        /// <returns>The created scaling matrix.</returns>
        // Token: 0x0600083D RID: 2109 RVA: 0x00023D2C File Offset: 0x00021F2C
        public static Matrix Scaling(float scale)
        {
            Matrix matrix;
            Matrix.Scaling(scale, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a matrix that rotates around the x-axis.
        /// </summary>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <param name="result">When the method completes, contains the created rotation matrix.</param>
        // Token: 0x0600083E RID: 2110 RVA: 0x00023D44 File Offset: 0x00021F44
        public static void RotationX(float angle, out Matrix result)
        {
            float num = (float)Math.Cos((double)angle);
            float num2 = (float)Math.Sin((double)angle);
            result = Matrix.Identity;
            result.M22 = num;
            result.M23 = num2;
            result.M32 = -num2;
            result.M33 = num;
        }

        /// <summary>
        /// Creates a matrix that rotates around the x-axis.
        /// </summary>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <returns>The created rotation matrix.</returns>
        // Token: 0x0600083F RID: 2111 RVA: 0x00023D8C File Offset: 0x00021F8C
        public static Matrix RotationX(float angle)
        {
            Matrix matrix;
            Matrix.RotationX(angle, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a matrix that rotates around the y-axis.
        /// </summary>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <param name="result">When the method completes, contains the created rotation matrix.</param>
        // Token: 0x06000840 RID: 2112 RVA: 0x00023DA4 File Offset: 0x00021FA4
        public static void RotationY(float angle, out Matrix result)
        {
            float num = (float)Math.Cos((double)angle);
            float num2 = (float)Math.Sin((double)angle);
            result = Matrix.Identity;
            result.M11 = num;
            result.M13 = -num2;
            result.M31 = num2;
            result.M33 = num;
        }

        /// <summary>
        /// Creates a matrix that rotates around the y-axis.
        /// </summary>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <returns>The created rotation matrix.</returns>
        // Token: 0x06000841 RID: 2113 RVA: 0x00023DEC File Offset: 0x00021FEC
        public static Matrix RotationY(float angle)
        {
            Matrix matrix;
            Matrix.RotationY(angle, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a matrix that rotates around the z-axis.
        /// </summary>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <param name="result">When the method completes, contains the created rotation matrix.</param>
        // Token: 0x06000842 RID: 2114 RVA: 0x00023E04 File Offset: 0x00022004
        public static void RotationZ(float angle, out Matrix result)
        {
            float num = (float)Math.Cos((double)angle);
            float num2 = (float)Math.Sin((double)angle);
            result = Matrix.Identity;
            result.M11 = num;
            result.M12 = num2;
            result.M21 = -num2;
            result.M22 = num;
        }

        /// <summary>
        /// Creates a matrix that rotates around the z-axis.
        /// </summary>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <returns>The created rotation matrix.</returns>
        // Token: 0x06000843 RID: 2115 RVA: 0x00023E4C File Offset: 0x0002204C
        public static Matrix RotationZ(float angle)
        {
            Matrix matrix;
            Matrix.RotationZ(angle, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a matrix that rotates around an arbitrary axis.
        /// </summary>
        /// <param name="axis">The axis around which to rotate. This parameter is assumed to be normalized.</param>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <param name="result">When the method completes, contains the created rotation matrix.</param>
        // Token: 0x06000844 RID: 2116 RVA: 0x00023E64 File Offset: 0x00022064
        public static void RotationAxis(ref Vector3 axis, float angle, out Matrix result)
        {
            float x = axis.X;
            float y = axis.Y;
            float z = axis.Z;
            float num = (float)Math.Cos((double)angle);
            float num2 = (float)Math.Sin((double)angle);
            float num3 = x * x;
            float num4 = y * y;
            float num5 = z * z;
            float num6 = x * y;
            float num7 = x * z;
            float num8 = y * z;
            result = Matrix.Identity;
            result.M11 = num3 + num * (1f - num3);
            result.M12 = num6 - num * num6 + num2 * z;
            result.M13 = num7 - num * num7 - num2 * y;
            result.M21 = num6 - num * num6 - num2 * z;
            result.M22 = num4 + num * (1f - num4);
            result.M23 = num8 - num * num8 + num2 * x;
            result.M31 = num7 - num * num7 + num2 * y;
            result.M32 = num8 - num * num8 - num2 * x;
            result.M33 = num5 + num * (1f - num5);
        }

        /// <summary>
        /// Creates a matrix that rotates around an arbitrary axis.
        /// </summary>
        /// <param name="axis">The axis around which to rotate. This parameter is assumed to be normalized.</param>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <returns>The created rotation matrix.</returns>
        // Token: 0x06000845 RID: 2117 RVA: 0x00023F68 File Offset: 0x00022168
        public static Matrix RotationAxis(Vector3 axis, float angle)
        {
            Matrix matrix;
            Matrix.RotationAxis(ref axis, angle, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a rotation matrix from a quaternion.
        /// </summary>
        /// <param name="rotation">The quaternion to use to build the matrix.</param>
        /// <param name="result">The created rotation matrix.</param>
        // Token: 0x06000846 RID: 2118 RVA: 0x00023F80 File Offset: 0x00022180
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

        /// <summary>
        /// Creates a rotation matrix from a quaternion.
        /// </summary>
        /// <param name="rotation">The quaternion to use to build the matrix.</param>
        /// <returns>The created rotation matrix.</returns>
        // Token: 0x06000847 RID: 2119 RVA: 0x000240C0 File Offset: 0x000222C0
        public static Matrix RotationQuaternion(Quaternion rotation)
        {
            Matrix matrix;
            Matrix.RotationQuaternion(ref rotation, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a rotation matrix with a specified yaw, pitch, and roll.
        /// </summary>
        /// <param name="yaw">Yaw around the y-axis, in radians.</param>
        /// <param name="pitch">Pitch around the x-axis, in radians.</param>
        /// <param name="roll">Roll around the z-axis, in radians.</param>
        /// <param name="result">When the method completes, contains the created rotation matrix.</param>
        // Token: 0x06000848 RID: 2120 RVA: 0x000240D8 File Offset: 0x000222D8
        public static void RotationYawPitchRoll(float yaw, float pitch, float roll, out Matrix result)
        {
            Quaternion quaternion = default(Quaternion);
            Quaternion.RotationYawPitchRoll(yaw, pitch, roll, out quaternion);
            Matrix.RotationQuaternion(ref quaternion, out result);
        }

        /// <summary>
        /// Creates a rotation matrix with a specified yaw, pitch, and roll.
        /// </summary>
        /// <param name="yaw">Yaw around the y-axis, in radians.</param>
        /// <param name="pitch">Pitch around the x-axis, in radians.</param>
        /// <param name="roll">Roll around the z-axis, in radians.</param>
        /// <returns>The created rotation matrix.</returns>
        // Token: 0x06000849 RID: 2121 RVA: 0x00024100 File Offset: 0x00022300
        public static Matrix RotationYawPitchRoll(float yaw, float pitch, float roll)
        {
            Matrix matrix;
            Matrix.RotationYawPitchRoll(yaw, pitch, roll, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a translation matrix using the specified offsets.
        /// </summary>
        /// <param name="value">The offset for all three coordinate planes.</param>
        /// <param name="result">When the method completes, contains the created translation matrix.</param>
        // Token: 0x0600084A RID: 2122 RVA: 0x00024118 File Offset: 0x00022318
        public static void Translation(ref Vector3 value, out Matrix result)
        {
            Matrix.Translation(value.X, value.Y, value.Z, out result);
        }

        /// <summary>
        /// Creates a translation matrix using the specified offsets.
        /// </summary>
        /// <param name="value">The offset for all three coordinate planes.</param>
        /// <returns>The created translation matrix.</returns>
        // Token: 0x0600084B RID: 2123 RVA: 0x00024134 File Offset: 0x00022334
        public static Matrix Translation(Vector3 value)
        {
            Matrix matrix;
            Matrix.Translation(ref value, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a translation matrix using the specified offsets.
        /// </summary>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="z">Z-coordinate offset.</param>
        /// <param name="result">When the method completes, contains the created translation matrix.</param>
        // Token: 0x0600084C RID: 2124 RVA: 0x0002414C File Offset: 0x0002234C
        public static void Translation(float x, float y, float z, out Matrix result)
        {
            result = Matrix.Identity;
            result.M41 = x;
            result.M42 = y;
            result.M43 = z;
        }

        /// <summary>
        /// Creates a translation matrix using the specified offsets.
        /// </summary>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="z">Z-coordinate offset.</param>
        /// <returns>The created translation matrix.</returns>
        // Token: 0x0600084D RID: 2125 RVA: 0x00024170 File Offset: 0x00022370
        public static Matrix Translation(float x, float y, float z)
        {
            Matrix matrix;
            Matrix.Translation(x, y, z, out matrix);
            return matrix;
        }

        /// <summary>
        /// Adds two matrices.
        /// </summary>
        /// <param name="left">The first matrix to add.</param>
        /// <param name="right">The second matrix to add.</param>
        /// <returns>The sum of the two matrices.</returns>
        // Token: 0x0600085B RID: 2139 RVA: 0x00024684 File Offset: 0x00022884
        public static Matrix operator +(Matrix left, Matrix right)
        {
            Matrix matrix;
            Matrix.Add(ref left, ref right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Assert a matrix (return it unchanged).
        /// </summary>
        /// <param name="value">The matrix to assert (unchanged).</param>
        /// <returns>The asserted (unchanged) matrix.</returns>
        // Token: 0x0600085C RID: 2140 RVA: 0x000246A0 File Offset: 0x000228A0
        public static Matrix operator +(Matrix value)
        {
            return value;
        }

        /// <summary>
        /// Subtracts two matrices.
        /// </summary>
        /// <param name="left">The first matrix to subtract.</param>
        /// <param name="right">The second matrix to subtract.</param>
        /// <returns>The difference between the two matrices.</returns>
        // Token: 0x0600085D RID: 2141 RVA: 0x000246A4 File Offset: 0x000228A4
        public static Matrix operator -(Matrix left, Matrix right)
        {
            Matrix matrix;
            Matrix.Subtract(ref left, ref right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Negates a matrix.
        /// </summary>
        /// <param name="value">The matrix to negate.</param>
        /// <returns>The negated matrix.</returns>
        // Token: 0x0600085E RID: 2142 RVA: 0x000246C0 File Offset: 0x000228C0
        public static Matrix operator -(Matrix value)
        {
            Matrix matrix;
            Matrix.Negate(ref value, out matrix);
            return matrix;
        }

        /// <summary>
        /// Scales a matrix by a given value.
        /// </summary>
        /// <param name="right">The matrix to scale.</param>
        /// <param name="left">The amount by which to scale.</param>
        /// <returns>The scaled matrix.</returns>
        // Token: 0x0600085F RID: 2143 RVA: 0x000246D8 File Offset: 0x000228D8
        public static Matrix operator *(float left, Matrix right)
        {
            Matrix matrix;
            Matrix.Multiply(ref right, left, out matrix);
            return matrix;
        }

        /// <summary>
        /// Scales a matrix by a given value.
        /// </summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The amount by which to scale.</param>
        /// <returns>The scaled matrix.</returns>
        // Token: 0x06000860 RID: 2144 RVA: 0x000246F0 File Offset: 0x000228F0
        public static Matrix operator *(Matrix left, float right)
        {
            Matrix matrix;
            Matrix.Multiply(ref left, right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        /// <param name="left">The first matrix to multiply.</param>
        /// <param name="right">The second matrix to multiply.</param>
        /// <returns>The product of the two matrices.</returns>
        // Token: 0x06000861 RID: 2145 RVA: 0x00024708 File Offset: 0x00022908
        public static Matrix operator *(Matrix left, Matrix right)
        {
            Matrix matrix;
            Matrix.Multiply(ref left, ref right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Scales a matrix by a given value.
        /// </summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The amount by which to scale.</param>
        /// <returns>The scaled matrix.</returns>
        // Token: 0x06000862 RID: 2146 RVA: 0x00024724 File Offset: 0x00022924
        public static Matrix operator /(Matrix left, float right)
        {
            Matrix matrix;
            Matrix.Divide(ref left, right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Divides two matrices.
        /// </summary>
        /// <param name="left">The first matrix to divide.</param>
        /// <param name="right">The second matrix to divide.</param>
        /// <returns>The quotient of the two matrices.</returns>
        // Token: 0x06000863 RID: 2147 RVA: 0x0002473C File Offset: 0x0002293C
        public static Matrix operator /(Matrix left, Matrix right)
        {
            Matrix matrix;
            Matrix.Divide(ref left, ref right, out matrix);
            return matrix;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
        // Token: 0x06000866 RID: 2150 RVA: 0x00024774 File Offset: 0x00022974
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M23:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}]", new object[]
            {
                this.M11, this.M12, this.M13, this.M14, this.M21, this.M22, this.M23, this.M24, this.M31, this.M32,
                this.M33, this.M34, this.M41, this.M42, this.M43, this.M44
            });
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
        // Token: 0x06000867 RID: 2151 RVA: 0x00024880 File Offset: 0x00022A80
        public string ToString(string format)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format(format, new object[]
            {
                CultureInfo.CurrentCulture,
                "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M23:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}]",
                this.M11.ToString(format, CultureInfo.CurrentCulture),
                this.M12.ToString(format, CultureInfo.CurrentCulture),
                this.M13.ToString(format, CultureInfo.CurrentCulture),
                this.M14.ToString(format, CultureInfo.CurrentCulture),
                this.M21.ToString(format, CultureInfo.CurrentCulture),
                this.M22.ToString(format, CultureInfo.CurrentCulture),
                this.M23.ToString(format, CultureInfo.CurrentCulture),
                this.M24.ToString(format, CultureInfo.CurrentCulture),
                this.M31.ToString(format, CultureInfo.CurrentCulture),
                this.M32.ToString(format, CultureInfo.CurrentCulture),
                this.M33.ToString(format, CultureInfo.CurrentCulture),
                this.M34.ToString(format, CultureInfo.CurrentCulture),
                this.M41.ToString(format, CultureInfo.CurrentCulture),
                this.M42.ToString(format, CultureInfo.CurrentCulture),
                this.M43.ToString(format, CultureInfo.CurrentCulture),
                this.M44.ToString(format, CultureInfo.CurrentCulture)
            });
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
        // Token: 0x06000868 RID: 2152 RVA: 0x00024A08 File Offset: 0x00022C08
        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M23:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}]", new object[]
            {
                this.M11.ToString(formatProvider),
                this.M12.ToString(formatProvider),
                this.M13.ToString(formatProvider),
                this.M14.ToString(formatProvider),
                this.M21.ToString(formatProvider),
                this.M22.ToString(formatProvider),
                this.M23.ToString(formatProvider),
                this.M24.ToString(formatProvider),
                this.M31.ToString(formatProvider),
                this.M32.ToString(formatProvider),
                this.M33.ToString(formatProvider),
                this.M34.ToString(formatProvider),
                this.M41.ToString(formatProvider),
                this.M42.ToString(formatProvider),
                this.M43.ToString(formatProvider),
                this.M44.ToString(formatProvider)
            });
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
        // Token: 0x06000869 RID: 2153 RVA: 0x00024B20 File Offset: 0x00022D20
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString(formatProvider);
            }
            return string.Format(format, new object[]
            {
                formatProvider,
                "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M23:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}]",
                this.M11.ToString(format, formatProvider),
                this.M12.ToString(format, formatProvider),
                this.M13.ToString(format, formatProvider),
                this.M14.ToString(format, formatProvider),
                this.M21.ToString(format, formatProvider),
                this.M22.ToString(format, formatProvider),
                this.M23.ToString(format, formatProvider),
                this.M24.ToString(format, formatProvider),
                this.M31.ToString(format, formatProvider),
                this.M32.ToString(format, formatProvider),
                this.M33.ToString(format, formatProvider),
                this.M34.ToString(format, formatProvider),
                this.M41.ToString(format, formatProvider),
                this.M42.ToString(format, formatProvider),
                this.M43.ToString(format, formatProvider),
                this.M44.ToString(format, formatProvider)
            });
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        // Token: 0x0600086A RID: 2154 RVA: 0x00024C5C File Offset: 0x00022E5C
        public override int GetHashCode()
        {
            int num = this.M11.GetHashCode();
            num = (num * 397) ^ this.M12.GetHashCode();
            num = (num * 397) ^ this.M13.GetHashCode();
            num = (num * 397) ^ this.M14.GetHashCode();
            num = (num * 397) ^ this.M21.GetHashCode();
            num = (num * 397) ^ this.M22.GetHashCode();
            num = (num * 397) ^ this.M23.GetHashCode();
            num = (num * 397) ^ this.M24.GetHashCode();
            num = (num * 397) ^ this.M31.GetHashCode();
            num = (num * 397) ^ this.M32.GetHashCode();
            num = (num * 397) ^ this.M33.GetHashCode();
            num = (num * 397) ^ this.M34.GetHashCode();
            num = (num * 397) ^ this.M41.GetHashCode();
            num = (num * 397) ^ this.M42.GetHashCode();
            num = (num * 397) ^ this.M43.GetHashCode();
            return (num * 397) ^ this.M44.GetHashCode();
        }

        /// <summary>
        /// The size of the <see cref="T:SharpDX.Matrix" /> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Matrix));

        /// <summary>
        /// A <see cref="T:SharpDX.Matrix" /> with all of its components set to zero.
        /// </summary>
        public static readonly Matrix Zero = default(Matrix);

        /// <summary>
        /// The identity <see cref="T:SharpDX.Matrix" />.
        /// </summary>
        public static readonly Matrix Identity = new Matrix
        {
            M11 = 1f,
            M22 = 1f,
            M33 = 1f,
            M44 = 1f
        };

        /// <summary>
        /// Value at row 1 column 1 of the matrix.
        /// </summary>
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
