namespace LibRun8.Common
{
    public class Vector4
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        //
        // Summary:
        //     Initializes a new instance of the Vector4 struct.
        //
        // Parameters:
        //   value:
        //     The value that will be assigned to all components.
        public Vector4(float value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        //
        // Summary:
        //     Initializes a new instance of the Vector4 struct.
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

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
