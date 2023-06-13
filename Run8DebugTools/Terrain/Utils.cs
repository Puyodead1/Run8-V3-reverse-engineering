using Run8Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrain
{
    public class Utils
    {
       public static TerrainTileChunk MakeTerrainTile(string filePath)
        {
            if(filePath.EndsWith(".tr4"))
            {
                return new TerrainTileChunk
                {
                    filePath = filePath,
                    fileName = Path.GetFileName(filePath),
                    tileType = ETileType.Tr4
                };
            }

            if (filePath.EndsWith(".tr2"))
            {
                return new TerrainTileChunk
                {
                    filePath = filePath,
                    fileName = Path.GetFileName(filePath),
                    tileType = ETileType.Tr2
                };
            }

            if (filePath.EndsWith(".ter"))
            {
                return new TerrainTileChunk
                {
                    filePath = filePath,
                    fileName = Path.GetFileName(filePath),
                    tileType = ETileType.Ter
                };
            }

            return null;
        }

        public static void ReadTextures(BinaryReader binaryReader, TerrainTileChunk terrainTile)
        {
            terrainTile.terrainTile2.texture1 = binaryReader.ReadString();
            terrainTile.terrainTile2.texture2 = binaryReader.ReadString();
            terrainTile.terrainTile2.texture3 = binaryReader.ReadString();
            terrainTile.terrainTile2.texture4 = binaryReader.ReadString();

            if (terrainTile.terrainTile2.texture1.Contains("##"))
            {
                string[] array = terrainTile.terrainTile2.texture1.Split("##", StringSplitOptions.RemoveEmptyEntries);
                if (array.Length >= 1)
                {
                    terrainTile.terrainTile2.texture1 = array[0];
                }

                if (array.Length == 2)
                {
                    terrainTile.terrainTile2.texture5 = array[1];
                }
            }
            else
            {
                terrainTile.terrainTile2.texture5 = "run8_Dirt";
            }

            Console.WriteLine("texture1: {0}", terrainTile.terrainTile2.texture1);
            Console.WriteLine("texture2: {0}", terrainTile.terrainTile2.texture2);
            Console.WriteLine("texture3: {0}", terrainTile.terrainTile2.texture3);
            Console.WriteLine("texture4: {0}", terrainTile.terrainTile2.texture4);
        }

        public static void AdjustTile(TerrainTile2 terrainTile2)
        {
            float num = 33.772842f;
            float num2 = 41.043285f;
            List<int> list = new List<int>();
            int num3 = 0;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    int num4 = 0;
                    terrainTile2.tiles[i, j].vertexPositionNormalTexture_0 = new VertexPositionNormalTexture[(terrainTile2.tiles[i, j].short_0 * terrainTile2.tiles[i, j].short_0)];
                    int count = list.Count;
                    for (int k = 0; k < terrainTile2.tiles[i, j].short_0; k++)
                    {
                        for (int l = 0; l < terrainTile2.tiles[i, j].short_0; l++)
                        {
                            VertexPositionNormalTexture vertexPositionNormalTexture = default;
                            vertexPositionNormalTexture.svPosition.X = i * num + num / (terrainTile2.tiles[i, j].short_0 - 1) * k;
                            vertexPositionNormalTexture.svPosition.Z = -(j * num2 + num2 / (terrainTile2.tiles[i, j].short_0 - 1) * l);
                            vertexPositionNormalTexture.svPosition.Y = terrainTile2.tiles[i, j].vertexPositionNormalTexturePositions[k, l];
                            vertexPositionNormalTexture.texcoord0.X = vertexPositionNormalTexture.svPosition.X / 844.3211f;
                            vertexPositionNormalTexture.texcoord0.Y = -vertexPositionNormalTexture.svPosition.Z / 1026.0822f;
                            terrainTile2.tiles[i, j].vertexPositionNormalTexture_0[num4] = vertexPositionNormalTexture;
                            num3++;
                            num4++;
                        }
                    }
                    //Class856.smethod_11((int)terrainTile2.tiles[i, j].short_0, num3, list);
                    //Class856.smethod_8(terrainTile2.tiles[i, j].vertexPositionNormalTexture_0);
                }
            }
            //Class837.smethod_0(terrainTile2.tiles);
            List<VertexPositionNormalTexture> list2 = new List<VertexPositionNormalTexture>(num3);
            for (int m = 0; m < 25; m++)
            {
                for (int n = 0; n < 25; n++)
                {
                    for (int num5 = 0; num5 < terrainTile2.tiles[m, n].vertexPositionNormalTexture_0.Length; num5++)
                    {
                        list2.Add(terrainTile2.tiles[m, n].vertexPositionNormalTexture_0[num5]);
                    }
                    terrainTile2.tiles[m, n].vertexPositionNormalTexture_0 = null;
                }
            }
            terrainTile2.vertexPositionNormalTexture_0 = list2.ToArray();
            terrainTile2.int_2 = list.ToArray();
            //Class856.smethod_4(terrainTile2, terrainTile2.vertexPositionNormalTexture_0);
        }
    }
}
