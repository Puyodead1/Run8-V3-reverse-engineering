namespace LibRun8.Common
{
    public class AnimationClip
    {
        public double duration { get; private set; }
        public AnimationKeyframe[] keyframes { get; private set; }

        public AnimationClip(double duration, AnimationKeyframe[] keyframes)
        {
            this.duration = duration;
            this.keyframes = keyframes;
        }
    }
}
