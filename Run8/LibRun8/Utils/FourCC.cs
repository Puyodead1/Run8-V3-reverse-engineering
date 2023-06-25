using System.Globalization;
using System.Runtime.InteropServices;

namespace LibRun8.Utils
{
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct FourCC
    {
        private uint value;

        /// <summary>
        /// Initializes a new instance of the FourCC struct.
        /// </summary>
        /// <param name="fourCC">The fourCC value as a string .</param>
        public FourCC(string fourCC)
        {
            if (fourCC.Length != 4)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid length for FourCC(\"{0}\". Must be be 4 characters long ", new object[] { fourCC }), "fourCC");
            }
            this.value = (uint)(((uint)fourCC[3] << 24) | ((uint)fourCC[2] << 16) | ((uint)fourCC[1] << 8) | fourCC[0]);
        }

        /// <summary>
        /// Initializes a new instance of the FourCC struct.
        /// </summary>
        /// <param name="fourCC">The fourCC value as an uint.</param>
        public FourCC(uint fourCC)
        {
            this.value = fourCC;
        }

        /// <summary>
        /// Initializes a new instance of the FourCC struct.
        /// </summary>
        /// <param name="fourCC">The fourCC value as an int.</param>
        public FourCC(int fourCC)
        {
            this.value = (uint)fourCC;
        }

        /// <summary>
        /// Performs an implicit conversion from FourCC to Int32.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator uint(FourCC d)
        {
            return d.value;
        }

        /// <summary>
        /// Performs an implicit conversion from FourCC to Int32.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator int(FourCC d)
        {
            return (int)d.value;
        }

        /// <summary>
        /// Performs an implicit conversion from Int32 to FourCC.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator FourCC(uint d)
        {
            return new FourCC(d);
        }

        /// <summary>
        /// Performs an implicit conversion from Int32 to FourCC.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator FourCC(int d)
        {
            return new FourCC(d);
        }

        /// <summary>
        /// Performs an implicit conversion from FourCC to String.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(FourCC d)
        {
            return d.ToString();
        }

        /// <summary>
		/// Performs an implicit conversion from String to FourCC.
		/// </summary>
		/// <param name="d">The d.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
        public static implicit operator FourCC(string d)
        {
            return new FourCC(d);
        }

        public override string ToString()
        {
            return string.Format("{0}", new string(new char[]
            {
                (char)(this.value & 255U),
                (char)((this.value >> 8) & 255U),
                (char)((this.value >> 16) & 255U),
                (char)((this.value >> 24) & 255U)
            }));
        }

        public bool Equals(FourCC other)
        {
            return this.value == other.value;
        }

        public override bool Equals(object obj)
        {
            return !object.ReferenceEquals(null, obj) && obj is FourCC && this.Equals((FourCC)obj);
        }

        public override int GetHashCode()
        {
            return (int)this.value;
        }

        public static bool operator ==(FourCC left, FourCC right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FourCC left, FourCC right)
        {
            return !left.Equals(right);
        }
    }
}
