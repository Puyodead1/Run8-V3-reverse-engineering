namespace LibRun8.Common
{
    public struct VertexStruct
    {
        public float UnusedFloat0 { get; set; }
        public float UnusedFloat1 { get; set; }

        public Vector3 Position;

        public Vector3 Normal;

        public Vector2 TextureCoordinate;

        public Vector3 Tangent;

        public Vector3 Binormal;

        public override string ToString()
        {
            return string.Format("Position: {0}; Normal: {1}; TextureCoordinate: {2}", Position.ToString(), Normal.ToString(), TextureCoordinate.ToString());
        }
    }
}
