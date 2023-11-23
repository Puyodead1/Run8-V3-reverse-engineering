using LibRun8.Common;

namespace LibRun8.Formats.Terrain
{
    public class TerrainTile
    {
        public string Texture0Name { get; set; }
        public string Texture1Name { get; set; }
        public string Texture2Name { get; set; }
        public string Texture3Name { get; set; }
        public int DetailLevel { get; set; }
        public Chunk[,] ChunkData;
        public float LonWest { get; set; }
        public float LonEast { get; set; }
        public float LatNorth { get; set; }
        public float LatSouth { get; set; }
        public string ProcVeg { get; set; }
        public VertexStruct[] AllVerticesTemp;
        public TileIndex TileXZ { get; set; }
        public List<SceneryAssetLoader> LoadList { get; set; } = new List<SceneryAssetLoader>();
        public ProceduralVegetation Plants { get; set; }
        public byte[] WeightMap { get; set; }

        public void CopyToVertexBuffers()
        {
            for (int i = 0; i < this.AllVerticesTemp.Length; i++)
            {
                Vector2 textureCoordinate = default;
                textureCoordinate.X = this.AllVerticesTemp[i].Position.X / 844.3211f;
                textureCoordinate.Y = -this.AllVerticesTemp[i].Position.Z / 1026.0822f;
                this.AllVerticesTemp[i].TextureCoordinate = textureCoordinate;
            }
          
            //this.indexBuffer = null;
            //this.allVerticesTemp = null;
        }
    }
}
