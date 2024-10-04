namespace LibRun8.Common
{
    public class DecalLoader
    {
        public List<int> Digits { get; set; } = new List<int>();
        public string TextureName { get; set; }
        public float Size { get; set; }
        public Vector3 OffsetXYZ { get; set; }
        public Vector3 RotationDegXYZ { get; set; }
        public Vector3 ColorRGB { get; set; }
        public bool IsGaugeNeedle { get; set; }
    }
}
