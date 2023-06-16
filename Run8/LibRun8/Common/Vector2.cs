namespace LibRun8.Common
{
    public struct Vector2
    {
        //
        // Summary:
        //     The X component of the vector.
        public float X { get; set; }

        //
        // Summary:
        //     The Y component of the vector.
        public float Y { get; set; }

        //
        // Summary:
        //     Initializes a new instance of the Vector2 struct.
        //
        // Parameters:
        //   value:
        //     The value that will be assigned to all components.
        public Vector2(float value)
        {
            X = value;
            Y = value;
        }

        //
        // Summary:
        //     Initializes a new instance of the Vector2 struct.
        //
        // Parameters:
        //   x:
        //     Initial value for the X component of the vector.
        //
        //   y:
        //     Initial value for the Y component of the vector.
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
