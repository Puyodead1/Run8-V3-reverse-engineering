namespace LibRun8.Formats.Terrain
{
    public class TerrainTileLoadData
    {
        public string FilePath { get; set; }
        public string FileNameOnly { get; set; }
        public ETileType Type { get; set; }
        public TerrainTile Tile { get; set; }
    }
}
