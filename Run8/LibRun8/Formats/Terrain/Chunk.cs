using LibRun8.Common;
namespace LibRun8.Formats.Terrain
{
    public class Chunk
    {
        public static readonly int CHUNK_SIZE = 25;

        public float[,] heightMap;
        public float[][] jaggedHeightMap { get { return Utils.Utils.ConvertToJaggedArray(heightMap); } }
        public short hixels { get; set; }
        public int cx { get; set; }
        public int cz { get; set; }
        public VertexStruct[] vertices;
    }
}
