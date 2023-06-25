namespace LibRun8.Common
{
    public struct VertexStruct
    {
        public Vector3 svPosition { get; set; }

        public Vector3 normal { get; set; }

        public Vector2 texCoord { get; set; }

        public Int4 blendIndicies { get; set; }

        public Vector4 blendWeight { get; set; }

        public override string ToString()
        {
            return string.Format("Position: {0}; Normal: {1}; TextureCoord: {2}", svPosition.ToString(), normal.ToString(), texCoord.ToString());
        }
    }
}
