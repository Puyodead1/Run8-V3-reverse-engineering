namespace LibRun8.Common
{
    public struct Int4
    {
        //
        // Summary:
        //     The X component of the vector.
        public int X { get; set; }

        //
        // Summary:
        //     The Y component of the vector.
        public int Y { get; set; }

        //
        // Summary:
        //     The Z component of the vector.
        public int Z { get; set; }

        //
        // Summary:
        //     The W component of the vector.
        public int W { get; set; }

        //
        // Summary:
        //     Initializes a new instance of the Int4 struct.
        //
        // Parameters:
        //   value:
        //     The value that will be assigned to all components.
        public Int4(int value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        //
        // Summary:
        //     Initializes a new instance of the Int4 struct.
        //
        // Parameters:
        //   x:
        //     Initial value for the X component of the vector.
        //
        //   y:
        //     Initial value for the Y component of the vector.
        //
        //   z:
        //     Initial value for the Z component of the vector.
        //
        //   w:
        //     Initial value for the W component of the vector.
        public Int4(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
