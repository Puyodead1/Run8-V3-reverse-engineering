namespace LibRun8.Common
{
    public struct VertexStruct
    {
        public Vector3 Position { get; set; }

        public Vector3 Normal { get; set; }

        public Vector2 TextureCoordinate { get; set; }

        public Int4 BlendIndicies { get; set; }

        public Vector4 BlendWeight { get; set; }

        public override string ToString()
        {
            return string.Format("Position: {0}; Normal: {1}; TextureCoordinate: {2}", Position.ToString(), Normal.ToString(), TextureCoordinate.ToString());
        }
    }
}
