﻿/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

namespace Engine
{
    /// <summary>
    /// Class for generating random values.
    /// </summary>
    public static class Random
    {
        private static System.Random m_random = new System.Random();

        /// <summary>
        /// Gets a random value [0, 1).
        /// </summary>
        public static float Value => (float)m_random.NextDouble();

        //public static int Seed
        //{
        //    get => m_random.
        //}

        /// <summary>
        /// Gets a random value in some range.
        /// </summary>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        public static float GetRange(float min, float max)
        {
            return Mathf.Lerp(min, max, Value);
        }

        /// <summary>
        /// Gets a random vector.
        /// </summary>
        /// <param name="maxMagnitude">The maximum length of the vector.</param>
        public static Vector3 GetVector3(float maxMagnitude)
        {
            return GetVector3() * maxMagnitude;
        }

        /// <summary>
        /// Gets a random vector with length [0, 1).
        /// </summary>
        public static Vector3 GetVector3()
        {
            Vector3 random;
            do
            {
                random.x = GetRange(-1, 1);
                random.y = GetRange(-1, 1);
                random.z = GetRange(-1, 1);
            }
            while (random.LengthSquared > 1);
            return random;
        }

        /// <summary>
        /// Gets a random rotation.
        /// </summary>
        //public static Quaternion GetRotation()
        //{
        //    return Quaternion.FromAxisAngle(GetVector3().Normalized(), Value * MathHelper.TwoPi);
        //}

        /// <summary>
        /// Gets a random color with alpha = 1.
        /// </summary
        /// <param name="minValue">The minimum brightness of the color.</param>
        /// <param name="minSaturation">The minimum saturation of the color.</param>
        //public static Color4 GetColor(float minValue = 0.5f, float minSaturation = 0.5f)
        //{
        //    return Color.FromHSVA(Value, GetRange(minSaturation, 1), GetRange(minValue, 1));
        //}
    }
}
