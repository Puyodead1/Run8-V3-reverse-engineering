namespace LibRun8.Formats.Terrain
{
    public class Tr3 : TerrainTile
    {
        public Tr3(string path, string region, float heightOffset, BinaryReader reader)
        {
            this.region = region;
            this.reader = reader;
            this.heightOffset = heightOffset;
            throw new NotImplementedException();
        }

        public void Write(string path)
        {
            throw new NotImplementedException();
        }
    }
}
