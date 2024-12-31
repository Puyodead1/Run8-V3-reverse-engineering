using LibRun8.Common;
using System;
using static LibRun8.Formats.AISignalDatabase;

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
                // read the "Type"
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

                foreach(ModelObject obj in item.Objects)
                {
                    if (!string.IsNullOrEmpty(obj.ParentName))
                    {
                        obj.ParentObject = item.Objects.Find(x => x.Name.Contains(obj.ParentName));
                    }

                    string parentNameLower = obj.ParentName.ToLower();

                    if (parentNameLower.Contains("wiper"))
                    {
                        obj.Type = ModelType.Wiper;
                    }
                    else if (parentNameLower.Contains("beacon"))
                    {
                        obj.Type = ModelType.Beacon;
                    }
                    else if (parentNameLower.Contains("hepglass"))
                    {
                        obj.Type = ModelType.HEPGlass;
                    }
                    else if (parentNameLower.Contains("glass_wheelslip"))
                    {
                        obj.Type = ModelType.GlassWheelslip;
                    }
                    else if (parentNameLower.Contains("glass_pcs"))
                    {
                        obj.Type = ModelType.GlassPCS;
                    }
                    else if (parentNameLower.Contains("glass"))
                    {
                        obj.Type = (parentNameLower.Contains("rain") ? ModelType.RainGlass : ModelType.GlassHolder);
                    }
                    else if (parentNameLower.Contains("holder"))
                    {
                        obj.Type = ModelType.GlassHolder;
                    }
                    else if (parentNameLower.Contains("window"))
                    {
                        obj.Type = GetWindowType(parentNameLower);
                    }
                    else if (parentNameLower.Contains("r_door"))
                    {
                        obj.Type = ModelType.RearDoor;
                    }
                    else if (parentNameLower.Contains("f_door"))
                    {
                        obj.Type = ModelType.FrontDoor;
                    }
                    else if (parentNameLower.Contains(" door"))
                    {
                        obj.Type = GetDoorType(parentNameLower);
                    }
                    else if (parentNameLower.Contains("carload"))
                    {
                        obj.Type = ModelType.CarLoad;
                    }
                    else if (parentNameLower.Contains("interior_low"))
                    {
                        obj.Type = ModelType.InteriorLow;
                    }
                    else if (parentNameLower.Contains("interior_high"))
                    {
                        obj.Type = ModelType.InteriorHigh;
                    }
                }
            }

            //item.method_7();

            return item;
        }

        public void method_7()
        {
            Matrix matrix = Matrix.Identity;
            Vector3 vector = Vector3.Zero;

            if (this.UnkVec3 != Vector3.Zero)
            {
                vector += this.UnkMatrix.Right * this.UnkVec3.X;
                vector += this.UnkMatrix.Up * this.UnkVec3.Y;
                vector += this.UnkMatrix.Forward * this.UnkVec3.Z;
            }

            for (int i = 0; i < this.Objects.Count; i++)
            {
                ModelObject obj = this.Objects[i];
                if (obj.ParentObject == null)
                {
                    matrix = this.UnkMatrix;
                    if (this.Objects.Count != 1)
                    {
                        obj.UnkQuat0 = Quaternion.RotationMatrix(this.UnkMatrix);
                        obj.UnkVec30 = this.UnkMatrix.TranslationVector;
                    }
                }
                else
                {
                    float offset = 0f;
                    obj.CalculateOffset(offset);
                    matrix = Matrix.RotationQuaternion(obj.UnkQuat0);
                    matrix.TranslationVector = obj.UnkVec30;
                }

                matrix.TranslationVector += vector;
                Console.WriteLine(matrix);
            }
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

        public static ModelType GetWindowType(string modelName)
        {
            if (modelName.Contains("engineer") || modelName.Contains("driver"))
            {
                return ModelType.WindowEngDriver;
            }
            if (modelName.Contains("fireman") || modelName.Contains("conductor"))
            {
                return ModelType.WindowFiremanConductor_3Or4;
            }
            if (modelName.Contains("window03") || modelName.Contains("window04"))
            {
                return ModelType.WindowFiremanConductor_3Or4;
            }
            if (!modelName.Contains("window01") && !modelName.Contains("window02"))
            {
                return ModelType.Window1Or2;
            }
            return ModelType.WindowEngDriver;
        }

        public static ModelType GetDoorType(string modelName)
        {
            if (modelName.Contains("front"))
            {
                return ModelType.FrontDoor;
            }
            if (modelName.Contains("rear"))
            {
                return ModelType.RearDoor;
            }
            return ModelType.Window1Or2;
        }
    }
}
