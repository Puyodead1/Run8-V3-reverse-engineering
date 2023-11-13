using LibRun8.Common;

namespace LibRun8.Formats.Terrain
{
    public class TerrainTile
    {
        public string region { get; set; }
        public BinaryReader reader;
        public string texture0Name { get; set; }
        public string texture1Name { get; set; }
        public string texture2Name { get; set; }
        public string texture3Name { get; set; }
        public string texture4Name { get; set; }
        public int detailLevel { get; set; } = 5;
        public Chunk[,] chunkData;
        public Chunk[][] jaggedChunkData { get { return Utils.Utils.ConvertToJaggedArray(chunkData); } }
        public float heightOffset { get; set; }
        public bool unknown0 { get; set; }
        public float? lonEast { get; set; }
        public float? lonWest { get; set; }
        public float? latNorth { get; set; }
        public float? latSouth { get; set; }
        public string? unknown1 { get; set; }
        public VertexStruct[] allVerticesTemp;
        public int[] indexBuffer;
        public Vector2 centerXZ { get; set; }
        public float centerY { get; set; }

        internal void LoadTextures()
        {
            texture0Name = reader.ReadString();
            texture1Name = reader.ReadString();
            texture2Name = reader.ReadString();
            texture3Name = reader.ReadString();

            if (texture0Name.Contains("##"))
            {
                string[] split = texture0Name.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length >= 1)
                {
                    texture0Name = split[0];
                }
                if (split.Length == 2)
                {
                    texture4Name = split[1];
                }
            } 
            else
            {
                texture4Name = "run8_Dirt";
            }

            if (region == "HRS_Southeast")
            {
                if (texture0Name == "sandy" || texture0Name == "Sandy")
                {
                    texture0Name = "ALine_sandy";
                }
                if (texture1Name == "sandy" || texture1Name == "Sandy")
                {
                    texture1Name = "ALine_sandy";
                }
                if (texture2Name == "sandy" || texture2Name == "Sandy")
                {
                    texture2Name = "ALine_sandy";
                }
                if (texture3Name == "sandy" || texture3Name == "Sandy")
                {
                    texture3Name = "ALine_sandy";
                }
                if (texture0Name == "run8_Dirt")
                {
                    texture0Name = "ALine_Dirt";
                }
                if (texture1Name == "run8_Dirt")
                {
                    texture1Name = "ALine_Dirt";
                }
                if (texture2Name == "run8_Dirt")
                {
                    texture2Name = "ALine_Dirt";
                }
                if (texture3Name == "run8_Dirt")
                {
                    texture3Name = "ALine_Dirt";
                }
            }
            else if (region == "SouthernCA")
            {
                // TODO: all this does is get grass textures based on the day of the year
                //texture0Name = TileUtil.smethod_0(texture0Name);
                //texture1Name = TileUtil.smethod_0(texture1Name);
                //texture2Name = TileUtil.smethod_0(texture2Name);
                //texture3Name = TileUtil.smethod_0(texture3Name);
                //texture4Name = TileUtil.smethod_0(texture4Name);
            }
        }
    }
}
