namespace LibRun8.Common
{
    /// <summary>Represents a 4x4 matrix.</summary>
    /// <remarks><format type="text/markdown"><![CDATA[
    /// [!INCLUDE[vectors-are-rows-paragraph](~/includes/system-numerics-vectors-are-rows.md)]
    /// ]]></format></remarks>
    public struct Matrix
    {
        private static readonly Matrix _identity = new Matrix
        (
            1f, 0f, 0f, 0f,
            0f, 1f, 0f, 0f,
            0f, 0f, 1f, 0f,
            0f, 0f, 0f, 1f
        );

        /// <summary>The first element of the first row.</summary>
        public float M11 { get; set; }

        /// <summary>The second element of the first row.</summary>
        public float M12 { get; set; }

        /// <summary>The third element of the first row.</summary>
        public float M13 { get; set; }

        /// <summary>The fourth element of the first row.</summary>
        public float M14 { get; set; }

        /// <summary>The first element of the second row.</summary>
        public float M21 { get; set; }

        /// <summary>The second element of the second row.</summary>
        public float M22 { get; set; }

        /// <summary>The third element of the second row.</summary>
        public float M23 { get; set; }

        /// <summary>The fourth element of the second row.</summary>
        public float M24 { get; set; }

        /// <summary>The first element of the third row.</summary>
        public float M31 { get; set; }

        /// <summary>The second element of the third row.</summary>
        public float M32 { get; set; }

        /// <summary>The third element of the third row.</summary>
        public float M33 { get; set; }

        /// <summary>The fourth element of the third row.</summary>
        public float M34 { get; set; }

        /// <summary>The first element of the fourth row.</summary>
        public float M41 { get; set; }

        /// <summary>The second element of the fourth row.</summary>
        public float M42 { get; set; }

        /// <summary>The third element of the fourth row.</summary>
        public float M43 { get; set; }

        /// <summary>The fourth element of the fourth row.</summary>
        public float M44 { get; set; }

        /// <summary>Creates a 4x4 matrix from the specified components.</summary>
        /// <param name="m11">The value to assign to the first element in the first row.</param>
        /// <param name="m12">The value to assign to the second element in the first row.</param>
        /// <param name="m13">The value to assign to the third element in the first row.</param>
        /// <param name="m14">The value to assign to the fourth element in the first row.</param>
        /// <param name="m21">The value to assign to the first element in the second row.</param>
        /// <param name="m22">The value to assign to the second element in the second row.</param>
        /// <param name="m23">The value to assign to the third element in the second row.</param>
        /// <param name="m24">The value to assign to the third element in the second row.</param>
        /// <param name="m31">The value to assign to the first element in the third row.</param>
        /// <param name="m32">The value to assign to the second element in the third row.</param>
        /// <param name="m33">The value to assign to the third element in the third row.</param>
        /// <param name="m34">The value to assign to the fourth element in the third row.</param>
        /// <param name="m41">The value to assign to the first element in the fourth row.</param>
        /// <param name="m42">The value to assign to the second element in the fourth row.</param>
        /// <param name="m43">The value to assign to the third element in the fourth row.</param>
        /// <param name="m44">The value to assign to the fourth element in the fourth row.</param>
        public Matrix(float m11, float m12, float m13, float m14,
                         float m21, float m22, float m23, float m24,
                         float m31, float m32, float m33, float m34,
                         float m41, float m42, float m43, float m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;

            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;

            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;

            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        

        /// <summary>Gets the multiplicative identity matrix.</summary>
        /// <value>Gets the multiplicative identity matrix.</value>
        public static Matrix Identity
        {
            get => _identity;
        }

        
        /// <summary>Indicates whether the current matrix is the identity matrix.</summary>
        /// <value><see langword="true" /> if the current matrix is the identity matrix; otherwise, <see langword="false" />.</value>
        public readonly bool IsIdentity
        {
            get
            {
                return M11 == 1f && M22 == 1f && M33 == 1f && M44 == 1f && // Check diagonal element first for early out.
                                    M12 == 0f && M13 == 0f && M14 == 0f &&
                       M21 == 0f && M23 == 0f && M24 == 0f &&
                       M31 == 0f && M32 == 0f && M34 == 0f &&
                       M41 == 0f && M42 == 0f && M43 == 0f;
            }
        }
    }
}
