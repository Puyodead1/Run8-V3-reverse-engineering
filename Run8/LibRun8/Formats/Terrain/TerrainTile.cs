using LibRun8.Common;

namespace LibRun8.Formats.Terrain
{
    public class TerrainTile
    {
        public string texture0Name { get; set; }
        public string texture1Name { get; set; }
        public string texture2Name { get; set; }
        public string texture3Name { get; set; }
        public int detailLevel { get; set; }
        public Chunk[,] chunkData;
        public float lonWest { get; set; }
        public float lonEast { get; set; }
        public float latNorth { get; set; }
        public float latSouth { get; set; }
        public string procVeg { get; set; }
        public VertexStruct[] allVerticesTemp;
        public TileIndex tileXZ { get; set; }

        public void CopyToVertexBuffers()
        {
            for (int i = 0; i < this.allVerticesTemp.Length; i++)
            {
                Vector2 textureCoordinate = default;
                textureCoordinate.X = this.allVerticesTemp[i].Position.X / 844.3211f;
                textureCoordinate.Y = -this.allVerticesTemp[i].Position.Z / 1026.0822f;
                this.allVerticesTemp[i].TextureCoordinate = textureCoordinate;
            }
          
            //this.indexBuffer = null;
            //this.allVerticesTemp = null;
        }
    }
}
