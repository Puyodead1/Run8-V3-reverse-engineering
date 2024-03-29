﻿using LibRun8.Common;

namespace LibRun8.Formats.Terrain
{
    public class Terrain
    {
        public static TerrainTile Read(string path)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
            string[] split = fileNameWithoutExtension.Split("_");
            int x = int.Parse(split[0]);
            int z = int.Parse(split[1]);
            TileIndex tileXZ = new TileIndex(x, z);

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    TerrainTileLoadData terrainTileLoadData = GetLoadData(path);
                    if (terrainTileLoadData != null)
                    {
                        switch (terrainTileLoadData.Type)
                        {
                            case ETileType.Ter:
                                terrainTileLoadData.Tile = new TerrainTile();
                                terrainTileLoadData.Tile.TileXZ = tileXZ;
                                break;
                            case ETileType.Tr2:
                                terrainTileLoadData.Tile = new TerrainTile();
                                terrainTileLoadData.Tile.TileXZ = tileXZ;
                                Tr2Loader.LoadTile(terrainTileLoadData);
                                break;
                            case ETileType.Tr3:
                                terrainTileLoadData.Tile = new TerrainTile();
                                terrainTileLoadData.Tile.TileXZ = tileXZ;
                                break;
                            case ETileType.Tr4:
                                terrainTileLoadData.Tile = new TerrainTile();
                                terrainTileLoadData.Tile.TileXZ = tileXZ;
                                Tr4Loader.LoadTile(terrainTileLoadData, false);
                                break;
                        }
                        return terrainTileLoadData.Tile;
                    }
                    else
                    {
                        throw new Exception("Failed to parse terrain");
                    }
                }
            }
        }

        private static TerrainTileLoadData GetLoadData(string path)
        {
            string ext = Path.GetExtension(path);
            switch(ext)
            {
                case ".tr2":
                    return new TerrainTileLoadData
                    {
                        FilePath = path,
                        FileNameOnly = Path.GetFileNameWithoutExtension(path),
                        Type = ETileType.Tr2
                    };
                case ".tr3":
                    return new TerrainTileLoadData
                    {
                        FilePath = path,
                        FileNameOnly = Path.GetFileNameWithoutExtension(path),
                        Type = ETileType.Tr3
                    };
                case ".ter":
                    return new TerrainTileLoadData
                    {
                        FilePath = path,
                        FileNameOnly = Path.GetFileNameWithoutExtension(path),
                        Type = ETileType.Ter
                    };
                case ".tr4":
                    return new TerrainTileLoadData
                    {
                        FilePath = path,
                        FileNameOnly = Path.GetFileNameWithoutExtension(path),
                        Type = ETileType.Tr4
                    };
                default:
                    return null;
            }
        }
    }
}
