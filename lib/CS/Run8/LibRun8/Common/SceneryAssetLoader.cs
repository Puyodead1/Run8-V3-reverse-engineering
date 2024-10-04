namespace LibRun8.Common
{
    public class SceneryAssetLoader
    {
        public string ModelName { get; set; }
        public TileIndex TileXZ { get; set; }
        public Vector3 PositionXYZ { get; set; }
        public Vector3 RotationXYZ { get; set; }
        public Vector3 Scale { get; set; }
        public bool DisregardBoundingTest { get; set; }
        public bool Bool0 { get; set; }
        public List<DecalLoader> DecalLoadList { get; set; }
    }
}
