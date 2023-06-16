namespace LibRun8.Common
{
    public struct VertexStruct
    {
        public Vector3 svPosition { get; set; }

        public Vector3 normal { get; set; }

        public Vector2 texCoord { get; set; }

        public Int4 blendIndicies { get; set; }

        public Vector4 blendWeight { get; set; }
    }
}
