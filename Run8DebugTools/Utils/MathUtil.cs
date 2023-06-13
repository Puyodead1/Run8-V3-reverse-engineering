namespace Run8Utils
{
    public class MathUtil
    {
        public static float DegreesToRadians(float degree)
        {
            return degree * 0.017453292f;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            if (value <= max)
            {
                return value;
            }
            return max;
        }

        public static float Lerp(float from, float to, float amount)
        {
            return (1f - amount) * from + amount * to;
        }
    }
}
