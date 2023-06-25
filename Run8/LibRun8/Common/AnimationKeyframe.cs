namespace LibRun8.Common
{
    public class AnimationKeyframe
    {
        public int bone { get; private set; }
        public double time { get; private set; }
        public Matrix transform { get; set; }

        public AnimationKeyframe(int bone, double time, Matrix transform)
        {
            this.bone = bone;
            this.time = time;
            this.transform = transform;
        }
    }
}
