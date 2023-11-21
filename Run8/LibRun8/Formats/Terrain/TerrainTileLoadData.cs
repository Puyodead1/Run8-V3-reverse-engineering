namespace LibRun8.Formats.Terrain
{
    public class TerrainTileLoadData
    {
        public string filePath { get; set; }
        public string fileNameOnly { get; set; }
        public ETileType type { get; set; }
        public TerrainTile tile { get; set; }
    }
}
