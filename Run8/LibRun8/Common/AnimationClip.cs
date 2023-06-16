namespace LibRun8.Common
{
    public class AnimationClip
    {
        public TimeSpan duration { get; private set; }
        public AnimationKeyframe[] keyframes { get; private set; }

        public AnimationClip(TimeSpan duration, AnimationKeyframe[] keyframes)
        {
            this.duration = duration;
            this.keyframes = keyframes;
        }
    }
}
