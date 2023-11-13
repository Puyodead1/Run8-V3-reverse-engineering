using System.Reflection.PortableExecutable;

namespace LibRun8.Formats.Terrain
{
    public class Terrain
    {
        public static TerrainTile Read(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    // extract region from path, Regions\<region>\
                    string[] split1 = path.Split(new string[] { "Regions\\" }, StringSplitOptions.RemoveEmptyEntries);
                    string[] split2 = split1[1].Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                    string region = split2[0];

                    float heightOffset;

                    if (region == "HRS_Southeast")
                    {
                        heightOffset = -2f;
                    } 
                    else
                    {
                        heightOffset = 1.2f;
                    }

                    string ext = Path.GetExtension(path).ToLower();
                    switch (ext)
                    {
                        case ".tr2":
                            return new Tr2(path, region, heightOffset, reader);
                        case ".ter":
                            return new Ter(path, region, heightOffset, reader);
                        case ".tr3":
                            return new Tr3(path, region, heightOffset, reader);
                        case ".tr4":
                            return new Tr4(path, region, heightOffset, reader);
                        default:
                            throw new Exception("Invalid Terrain File!");
                    }
                }
            }
        }
    }
}
