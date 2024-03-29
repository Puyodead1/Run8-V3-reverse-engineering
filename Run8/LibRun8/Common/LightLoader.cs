﻿namespace LibRun8.Common
{
    public class LightLoader
    {
        public bool billboardGlare { get; set; }

        public float LightRange { get; set; }

        public float lightWidth { get; set; }

        public float LightIntensity { get; set; }

        public float decayExponent { get; set; } = 1f;

        public Vector3 LightOffsetXYZ { get; set; }

        public bool isSpotLight { get; set; }

        public Vector4 Color { get; set; }

        public Vector3 LightDirectionDeg { get; set; }

        public bool flashing { get; set; }

        public float flashTimeRandomVariation { get; set; }

        public double flashTimerSeconds { get; set; }

        public bool hasDayNiteSensor { get; set; }

        public float dayNiteSensorAmbientLevel { get; set; }

        public Vector3[] GlareList { get; set; }

        public float LightGlareRadiusMeters { get; set; } = 0.35f;

        public bool isHepPowered { get; set; }

        public bool isMarkerLight { get; set; }

        public bool isNumberboardLight { get; set; }

        public bool IsLimitedYardLight { get; set; }

        public bool renderGlareOnly { get; set; }

        public bool isIncandescent { get; set; } = true;

        public float glowScalar { get; set; }
    }
}
