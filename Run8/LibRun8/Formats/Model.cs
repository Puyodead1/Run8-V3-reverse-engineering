using LibRun8.Common;

namespace LibRun8.Formats
{
    public class Model : FileFormat
    {
        public int ObjectCount { get; set; } = 1;
        public Vector3 UnkVec3 { get; set; }  = Vector3.Zero;
        public Matrix UnkMatrix { get; set; } = Matrix.Identity;
        public bool IsAdvancedModel { get; set; } = false;
        public List<ModelObject> Objects { get; set; }
        public float BoundingRadius { get; set; } = 0f;

        public static Model Read(Stream stream)
        {
            Model item = new Model();

            using (BinaryReader reader = new BinaryReader(stream))
            {
                // read the "type"
                int type = reader.ReadInt32();

                if (type == -969696)
                {
                    item.ObjectCount = reader.ReadInt32();
                    item.IsAdvancedModel = true;
                }
                else if (type == -969697)
                {
                    item.ObjectCount = reader.ReadInt32();
                    item.UnkVec3 = Vector3.Read(reader);
                    item.IsAdvancedModel = true;
                }
                else
                {
                    reader.BaseStream.Position = 0;
                }

                item.Objects = new List<ModelObject>(item.ObjectCount);

                for (int i = 0; i < item.ObjectCount; i++)
                {
                    ModelObject obj = new ModelObject(reader, item);
                    item.Objects.Add(obj);
                }
            }

            return item;
        }

        public static Model Read(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return Read(fileStream);
            }
        }

        public override void Write(string path)
        {
            throw new NotImplementedException();
        }
    }
}
