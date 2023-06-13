using Run8Utils;
using System.IO.Compression;
using System.Numerics;
using MoreLinq;
using JeremyAnsel.Media.WavefrontObj;
using System.CommandLine;
using System.IO;
using System;

namespace Terrain
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var fileArgument = new Argument<FileInfo>(
                name: "file",
                description: "Path to .tr2, .tr3, or .tr4 terrain tile");

            var writeObjOption = new Option<bool?>(
                name: "--write-obj",
                description: "Write to obj file",
                getDefaultValue: () => false);

            var rootCommand = new RootCommand("Run8 V3 Terrain Parser and Converter");
            rootCommand.AddArgument(fileArgument);
            rootCommand.AddOption(writeObjOption);

            rootCommand.SetHandler((file, writeObj) =>
            {
                ReadFile(file!, writeObj ?? false);
            },
                fileArgument, writeObjOption);

            return await rootCommand.InvokeAsync(args);
        }

        static void ReadFile(FileInfo file, bool writeObj)
        {
            string path = file.ToString();

            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist");
                return;
            }

            string ext = Path.GetExtension(path);
            if (ext != ".tr2" && ext != ".tr3" && ext != ".tr4")
            {
                Console.WriteLine("Invalid file extension");
                return;
            }

            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(path);
            string objPath = Path.Join(Path.GetDirectoryName(path), fileNameWithoutExt + ".obj");

            Console.WriteLine("Reading file {0}", path);

            string[] split = fileNameWithoutExt.Split("_");
            TileIndex tileIndex = new TileIndex(int.Parse(split[0]), int.Parse(split[1]));
            TerrainTileChunk terrainTile = Utils.MakeTerrainTile(path);

            if (terrainTile == null)
            {
                Console.WriteLine("Failed to create TerrainTile class");
                return;
            }

            terrainTile.terrainTile2 = new TerrainTile2(tileIndex.x, tileIndex.y, true);

            if (terrainTile.tileType == ETileType.Tr2)
            {
                Tr2.Read(terrainTile);
            } 
            else if (terrainTile.tileType == ETileType.Ter)
            {
                Ter.Read(terrainTile);
            }
            else if (terrainTile.tileType == ETileType.Tr4)
            {
                Tr4.Read(terrainTile);
            }
        }


        //static void Write(Class846 class846, string path)
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("Writing OBJ file to {0}", path);

        //    ObjFile objFile = new ObjFile();

        //    Console.WriteLine("Vertex Buffer has " + class846.buffer_1.Length + " elements");
        //    Console.WriteLine("Index Buffer has " + class846.buffer_0.Length + " elements");

        //    foreach (VertexPositionNormalTexture v in class846.buffer_1)
        //    {
        //        objFile.Vertices.Add(new ObjVertex(v.svPosition.X, v.svPosition.Y, v.svPosition.Z));
        //        objFile.VertexNormals.Add(new ObjVector3(v.normal.X, v.normal.Y, v.normal.Z));
        //        objFile.TextureVertices.Add(new ObjVector3(v.texcoord0.X, v.texcoord0.Y, 0));
        //    }

        //    foreach (var faceVerts in class846.buffer_0.Batch(3))
        //    {
        //        ObjFace face = new ObjFace();
        //        foreach (var faceVert in faceVerts)
        //        {
        //            face.Vertices.Add(new ObjTriplet(faceVert + 1, faceVert + 1, faceVert + 1));
        //        }

        //        objFile.Faces.Add(face);
        //    }

        //    objFile.WriteTo(path);
        //}
    }
}
