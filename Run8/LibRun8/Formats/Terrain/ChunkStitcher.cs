using LibRun8.Common;

namespace LibRun8.Formats.Terrain
{
    public class ChunkStitcher
    {
        public static void StitchChucks(Chunk[,] chunkData)
        {
            for (int i = 0; i < Chunk.CHUNK_SIZE; i++)
            {
                for (int j = 0; j < Chunk.CHUNK_SIZE - 1; j++)
                {
                    if (chunkData[j, i].hixels == chunkData[j + 1, i].hixels)
                    {
                        MatchNeighboursX(chunkData[j, i], chunkData[j + 1, i]);
                    }
                }
            }
            for (int k = 0; k < Chunk.CHUNK_SIZE; k++)
            {
                for (int l = 0; l < Chunk.CHUNK_SIZE - 1; l++)
                {
                    if (chunkData[k, l + 1].hixels == chunkData[k, l].hixels)
                    {
                        MatchNeighboursZ(chunkData[k, l + 1], chunkData[k, l]);
                    }
                }
            }
            for (int m = 0; m < Chunk.CHUNK_SIZE; m++)
            {
                for (int n = 0; n < Chunk.CHUNK_SIZE - 1; n++)
                {
                    if (chunkData[n, m].hixels != chunkData[n + 1, m].hixels)
                    {
                        MatchNeighboursX(chunkData[n, m], chunkData[n + 1, m]);
                    }
                }
            }
            for (int num = 0; num < Chunk.CHUNK_SIZE; num++)
            {
                for (int num2 = 0; num2 < Chunk.CHUNK_SIZE - 1; num2++)
                {
                    if (chunkData[num, num2 + 1].hixels != chunkData[num, num2].hixels)
                    {
                        MatchNeighboursZ(chunkData[num, num2 + 1], chunkData[num, num2]);
                    }
                }
            }
        }

        private static void MatchNeighboursX(Chunk left, Chunk right)
        {
            float maxLeftX = left.vertices.Max((VertexStruct vertex) => vertex.Position.X);
            float min = right.vertices.Min((VertexStruct vertex) => vertex.Position.X);
            List<VertexStruct> list = left.vertices.Where((VertexStruct vertex) => AreFloatsApproximatelyEqual(vertex.Position.X, maxLeftX)).ToList();
            List<VertexStruct> list2 = right.vertices.Where((VertexStruct vertex) => AreFloatsApproximatelyEqual(vertex.Position.X, min)).ToList();
            if (list.Count > list2.Count)
            {
                MatchNeighboursX(list2, 0, list2.Count() - 1, list, 0, list.Count() - 1);
                int num = 0;
                for (int i = 0; i < left.vertices.Length; i++)
                {
                    if (AreFloatsApproximatelyEqual(left.vertices[i].Position.X, maxLeftX))
                    {
                        left.vertices[i] = list[num++];
                    }
                }
                return;
            }
            if (list.Count < list2.Count)
            {
                MatchNeighboursX(list, 0, list.Count() - 1, list2, 0, list2.Count() - 1);
                int num2 = 0;
                for (int j = 0; j < right.vertices.Length; j++)
                {
                    if (AreFloatsApproximatelyEqual(right.vertices[j].Position.X, maxLeftX))
                    {
                        right.vertices[j] = list2[num2++];
                    }
                }
                return;
            }
            int num3 = 0;
            for (int k = 0; k < right.vertices.Length; k++)
            {
                if (AreFloatsApproximatelyEqual(right.vertices[k].Position.X, maxLeftX))
                {
                    right.vertices[k] = list[num3++];
                }
            }
        }

        private static void MatchNeighboursX(List<VertexStruct> smallVertices, int smallVerticesStartIndex, int smallVerticesEndIndex, List<VertexStruct> largeVertices, int largeVerticesStartIndex, int largeVerticesEndIndex)
        {
            int num = (largeVerticesEndIndex - largeVerticesStartIndex) / 2 + largeVerticesStartIndex;
            if (smallVerticesEndIndex - smallVerticesStartIndex == 1)
            {
                for (int i = largeVerticesStartIndex; i < num; i++)
                {
                    VertexStruct VertexStruct = largeVertices[i];

                    Vector3 position = VertexStruct.Position;
                    position.X = smallVertices[smallVerticesStartIndex].Position.X;
                    position.Y = smallVertices[smallVerticesStartIndex].Position.Y;
                    position.Z = smallVertices[smallVerticesStartIndex].Position.Z;
                    VertexStruct.Position = position;

                    VertexStruct.Normal = smallVertices[smallVerticesStartIndex].Normal;
                    largeVertices[i] = VertexStruct;
                }
                for (int j = num; j <= largeVerticesEndIndex; j++)
                {
                    VertexStruct vertexStruct2 = largeVertices[j];
                    Vector3 position = vertexStruct2.Position;
                    position.X = smallVertices[smallVerticesEndIndex].Position.X;
                    position.Y = smallVertices[smallVerticesEndIndex].Position.Y;
                    position.Z = smallVertices[smallVerticesEndIndex].Position.Z;
                    vertexStruct2.Position = position;

                    vertexStruct2.Normal = smallVertices[smallVerticesEndIndex].Normal;
                    largeVertices[j] = vertexStruct2;
                }
                return;
            }
            int num2 = (smallVerticesEndIndex - smallVerticesStartIndex) / 2 + smallVerticesStartIndex;
            MatchNeighboursX(smallVertices, smallVerticesStartIndex, num2, largeVertices, largeVerticesStartIndex, num);
            MatchNeighboursX(smallVertices, num2, smallVerticesEndIndex, largeVertices, num, largeVerticesEndIndex);
        }

        private static void MatchNeighboursZ(Chunk left, Chunk right)
        {
            float maxLeftZ = left.vertices.Max((VertexStruct vertex) => vertex.Position.Z);
            float min = right.vertices.Min((VertexStruct vertex) => vertex.Position.Z);
            List<VertexStruct> list = left.vertices.Where((VertexStruct vertex) => AreFloatsApproximatelyEqual(vertex.Position.Z, maxLeftZ)).ToList();
            List<VertexStruct> list2 = right.vertices.Where((VertexStruct vertex) => AreFloatsApproximatelyEqual(vertex.Position.Z, min)).ToList();
            if (list.Count > list2.Count)
            {
                MatchNeighboursZ(list2, 0, list2.Count() - 1, list, 0, list.Count() - 1);
                int num = 0;
                for (int i = 0; i < left.vertices.Length; i++)
                {
                    if (AreFloatsApproximatelyEqual(left.vertices[i].Position.Z, maxLeftZ))
                    {
                        left.vertices[i] = list[num++];
                    }
                }
                return;
            }
            if (list.Count < list2.Count)
            {
                MatchNeighboursZ(list, 0, list.Count() - 1, list2, 0, list2.Count() - 1);
                int num2 = 0;
                for (int j = 0; j < right.vertices.Length; j++)
                {
                    if (AreFloatsApproximatelyEqual(right.vertices[j].Position.Z, maxLeftZ))
                    {
                        right.vertices[j] = list2[num2++];
                    }
                }
                return;
            }
            int num3 = 0;
            for (int k = 0; k < right.vertices.Length; k++)
            {
                if (AreFloatsApproximatelyEqual(right.vertices[k].Position.Z, maxLeftZ))
                {
                    right.vertices[k] = list[num3++];
                }
            }
        }

        private static void MatchNeighboursZ(List<VertexStruct> smallVertices, int smallVerticesStartIndex, int smallVerticesEndIndex, List<VertexStruct> largeVertices, int largeVerticesStartIndex, int largeVerticesEndIndex)
        {
            int num = (largeVerticesEndIndex - largeVerticesStartIndex) / 2 + largeVerticesStartIndex;
            if (smallVerticesEndIndex - smallVerticesStartIndex == 1)
            {
                for (int i = largeVerticesStartIndex; i < num; i++)
                {
                    VertexStruct vertexStruct = largeVertices[i];
                    Vector3 position = vertexStruct.Position;
                    position.X = smallVertices[smallVerticesStartIndex].Position.X;
                    position.Y = smallVertices[smallVerticesStartIndex].Position.Y;
                    position.Z = smallVertices[smallVerticesStartIndex].Position.Z;
                    vertexStruct.Position = position;

                    vertexStruct.Normal = smallVertices[smallVerticesStartIndex].Normal;
                    largeVertices[i] = vertexStruct;
                }
                for (int j = num; j <= largeVerticesEndIndex; j++)
                {
                    VertexStruct vertexStruct2 = largeVertices[j];
                    Vector3 position = vertexStruct2.Position;
                    position.X = smallVertices[smallVerticesEndIndex].Position.X;
                    position.Y = smallVertices[smallVerticesEndIndex].Position.Y;
                    position.Z = smallVertices[smallVerticesEndIndex].Position.Z;
                    vertexStruct2.Position = position;

                    vertexStruct2.Normal = smallVertices[smallVerticesEndIndex].Normal;
                    largeVertices[j] = vertexStruct2;
                }
                return;
            }
            int num2 = (smallVerticesEndIndex - smallVerticesStartIndex) / 2 + smallVerticesStartIndex;
            MatchNeighboursZ(smallVertices, smallVerticesStartIndex, num2, largeVertices, largeVerticesStartIndex, num);
            MatchNeighboursZ(smallVertices, num2, smallVerticesEndIndex, largeVertices, num, largeVerticesEndIndex);
        }

        private static bool AreFloatsApproximatelyEqual(float max, float min)
        {
            return (double)Math.Abs(max - min) < 0.001;
        }
    }
}
