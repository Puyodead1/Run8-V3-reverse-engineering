using LibRun8.Common;
namespace LibRun8.Formats.Terrain
{
    public class Chunk
    {
        public static readonly int CHUNK_SIZE = 25;

        public float[,] HeightMap;
        public float[][] JaggedHeightMap { get { return Util.Utils.ConvertToJaggedArray(HeightMap); } }
        public short Hixels { get; set; }
        public int CX { get; set; }
        public int CZ { get; set; }
        public VertexStruct[] Vertices { get; set; }
    }
}
