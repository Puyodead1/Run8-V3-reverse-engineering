namespace LibRun8.Common
{
    public struct Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        //
        // Summary:
        //     Initializes a new instance of the Vector3 struct.
        //
        // Parameters:
        //   value:
        //     The value that will be assigned to all components.
        public Vector3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        //
        // Summary:
        //     Initializes a new instance of the Vector3 struct.
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
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return string.Format("X={0};Y={1};Z={2}", X, Y, Z);
        }

        public static Vector3 Read(BinaryReader reader)
        {
            Vector3 v = new Vector3();
            v.X = reader.ReadSingle();
            v.Y = reader.ReadSingle();
            v.Z = reader.ReadSingle();
            return v;
        }
    }
}
