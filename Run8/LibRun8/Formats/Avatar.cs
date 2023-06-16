using LibRun8.Common;

namespace LibRun8.Formats
{
    public class Avatar : FileFormat
    {
        public VertexStruct[] verticies { get; set; }
        public string[] textures { get; set; }
        public int[] indexBuffer { get; set; }
        public Struct138[] struct138s { get; set; }
        public int[] skeletonHiearchy { get; set; }
        public Dictionary<string, int> boneIndices { get; set; }
        public Matrix[] bindPose { get; set; }
        public Matrix[] inverseBindPose { get; set; }
        public Dictionary<string, AnimationClip> animationClips { get; set; }

        public static Avatar Read(string path)
        {
            Avatar avatar = new Avatar();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    int vertexCount = binaryReader.ReadInt32() / 7;
                    avatar.verticies = new VertexStruct[vertexCount];

                    for(int i = 0; i < vertexCount; i++)
                    {
                        VertexStruct vertexStruct = new VertexStruct();
                        Vector3 svPosition = new Vector3();
                        Vector3 normal = new Vector3();
                        Vector2 texCoord = new Vector2();
                        Int4 blendIndicies = new Int4();
                        Vector4 blendWeight = new Vector4();

                        binaryReader.ReadSingle(); // reserved
                        svPosition.X = binaryReader.ReadSingle() * 63.7f;
                        normal.Y = binaryReader.ReadSingle() / -1.732f;
                        svPosition.Z = binaryReader.ReadSingle() / 16f;
                        texCoord.X = binaryReader.ReadSingle() / 4.8f;
                        normal.X = binaryReader.ReadSingle() / 10.962f;
                        binaryReader.ReadSingle(); // reserved
                        normal.Z = binaryReader.ReadSingle() / 11.432f;
                        texCoord.Y = binaryReader.ReadSingle() / 9.6f;
                        svPosition.Y = -binaryReader.ReadSingle() * 6f;
                        blendIndicies.W = binaryReader.ReadByte();
                        blendWeight.Z = binaryReader.ReadSingle();
                        blendIndicies.X = binaryReader.ReadByte();
                        blendWeight.Y = binaryReader.ReadSingle();
                        blendIndicies.Y = binaryReader.ReadByte();
                        blendWeight.W = binaryReader.ReadSingle();
                        blendIndicies.Z = binaryReader.ReadByte();
                        blendWeight.X = binaryReader.ReadSingle();

                        vertexStruct.svPosition = svPosition;
                        vertexStruct.normal = normal;
                        vertexStruct.texCoord = texCoord;
                        vertexStruct.blendIndicies = blendIndicies;
                        vertexStruct.blendWeight = blendWeight;

                        avatar.verticies[i] = vertexStruct;
                    }

                    int textureCount = binaryReader.ReadInt32() + 6;
                    avatar.textures = new string[textureCount];
                    
                    for(int i = 0; i < textureCount; i++)
                    {
                        avatar.textures[i] = binaryReader.ReadString();
                    }

                    bool isUshortBuffer = binaryReader.ReadBoolean();
                    int indexBufferSize = binaryReader.ReadInt32();
                    avatar.indexBuffer = new int[indexBufferSize];

                    for (int i = 0; i < indexBufferSize; i++)
                    {
                        avatar.indexBuffer[i] = binaryReader.ReadInt32();
                    }

                    int struct138Count = binaryReader.ReadInt32() - 9;

                    if(struct138Count == 0)
                    {
                        avatar.struct138s = new Struct138[1];
                        avatar.struct138s[0] = new Struct138
                        {
                            indexCount = avatar.indexBuffer.Length,
                            baseVertexLocation = 0,
                            startIndexLocation = 0,
                        };
                    } 
                    else
                    {
                        avatar.struct138s = new Struct138[struct138Count];
                        for(int i = 0; i < struct138Count; i++)
                        {
                            Struct138 @struct = new Struct138();
                            binaryReader.ReadSingle(); // reserved
                            int textureIndex = binaryReader.ReadInt32();
                            @struct.mraoTexture = avatar.textures[textureIndex] + "_mrao";
                            @struct.indexCount = binaryReader.ReadInt32();
                            @struct.startIndexLocation = binaryReader.ReadInt32();
                            @struct.baseVertexLocation = binaryReader.ReadInt32();
                            avatar.struct138s[i] = @struct;
                        }
                    }

                    int skeletonHiearchyCount = binaryReader.ReadInt32();
                    avatar.skeletonHiearchy = new int[skeletonHiearchyCount];

                    for(int i = 0; i < skeletonHiearchyCount; i++)
                    {
                        avatar.skeletonHiearchy[i] = binaryReader.ReadInt32();
                    }

                    avatar.boneIndices = new Dictionary<string, int>();
                    int boneIndexCount = binaryReader.ReadInt32();

                    for(int i = 0; i < boneIndexCount; i++)
                    {
                        avatar.boneIndices.Add(binaryReader.ReadString(), binaryReader.ReadInt32());
                    }

                    int bindPoseCount = binaryReader.ReadInt32();
                    avatar.bindPose = new Matrix[bindPoseCount];

                    for(int i = 0; i < bindPoseCount; i++)
                    {
                        avatar.bindPose[i] = new Matrix(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                    }

                    int inverseBindPoseCount = binaryReader.ReadInt32();
                    avatar.inverseBindPose = new Matrix[inverseBindPoseCount];

                    for (int i = 0; i < inverseBindPoseCount; i++)
                    {
                        avatar.inverseBindPose[i] = new Matrix(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                    }

                    int animationClipCount = binaryReader.ReadInt32();
                    avatar.animationClips = new Dictionary<string, AnimationClip>();

                    for (int i = 0; i < animationClipCount; i++)
                    {
                        string key = binaryReader.ReadString();
                        double duration = binaryReader.ReadDouble();

                        int keyframeCount = binaryReader.ReadInt32();
                        AnimationKeyframe[] keyframes = new AnimationKeyframe[keyframeCount];

                        for (int j = 0; j < keyframeCount; j++)
                        {
                            int bone = binaryReader.ReadInt32();
                            double time = binaryReader.ReadDouble();
                            Matrix matrix = new Matrix(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                            keyframes[j] = new AnimationKeyframe(bone, TimeSpan.FromMilliseconds(time), matrix);
                        }
                        avatar.animationClips.Add(key, new AnimationClip(TimeSpan.FromMilliseconds(duration), keyframes));
                    }
                }
            }

            return avatar;
        }

        public override void Write()
        {
            throw new NotImplementedException();
        }
    }

    public struct Struct138
    {
        public string mraoTexture { get; set; }

        public int baseVertexLocation { get; set; }

        public int startIndexLocation { get; set; }

        public int indexCount { get; set; }
    }
}
