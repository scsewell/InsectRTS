/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using OpenTK;

namespace Engine
{
    /// <summary>
    /// Contains math constants and functions.
    /// </summary>
    public static class Mathf
    {
        /// <summary>
        /// Defines the value of π as a <see cref="float"/>.
        /// </summary>
        public const float Pi = (float)Math.PI;

        /// <summary>
        /// Defines the value of π/2 as a <see cref="float"/>.
        /// </summary>
        public const float PiOver2 = Pi / 2f;

        /// <summary>
        /// Defines the value of π/3 as a <see cref="float"/>.
        /// </summary>
        public const float PiOver3 = Pi / 3f;

        /// <summary>
        /// Defines the value of π/4 as a <see cref="float"/>.
        /// </summary>
        public const float PiOver4 = Pi / 4f;

        /// <summary>
        /// Defines the value of π/6 as a <see cref="float"/>.
        /// </summary>
        public const float PiOver6 = Pi / 6f;

        /// <summary>
        /// Defines the value of 2π as a <see cref="float"/>.
        /// </summary>
        public const float TwoPi = 2f * Pi;

        /// <summary>
        /// Defines the value of 2π as a <see cref="float"/>.
        /// </summary>
        public const float Tau = TwoPi;

        /// <summary>
        /// Defines the ratio of degrees per radian.
        /// </summary>
        public const float DegToRad = Pi / 180f;

        /// <summary>
        /// Defines the ratio of radians per degree.
        /// </summary>
        public const float RadToDeg = 180f / Pi;

        /// <summary>
        /// Defines the value of E as a <see cref="float"/>.
        /// </summary>
        public const float E = (float)Math.E;

        /// <summary>
        /// Defines the base-10 logarithm of E as a <see cref="float"/>.
        /// </summary>
        public const float Log10E = 0.434294482f;

        /// <summary>
        /// Defines the base-2 logarithm of E as a <see cref="float"/>.
        /// </summary>
        public const float Log2E = 1.442695041f;


        /// <summary>
        /// Returns the least of two numbers.
        /// </summary>
        public static int Min(int a, int b) => a < b ? a : b;

        /// <summary>
        /// Returns the least of two numbers.
        /// </summary>
        public static long Min(long a, long b) => a < b ? a : b;

        /// <summary>
        /// Returns the least of two numbers.
        /// </summary>
        public static float Min(float a, float b) => a < b ? a : b;

        /// <summary>
        /// Returns the least of two numbers.
        /// </summary>
        public static double Min(double a, double b) => a < b ? a : b;

        /// <summary>
        /// Returns the least of a set of numbers.
        /// </summary>
        public static float Min(params int[] values)
        {
            int count = values.Length;
            if (count == 0)
            {
                return 0;
            }

            var min = values[0];
            for (int i = 1; i < count; i++)
            {
                var value = values[i];
                if (value < min)
                {
                    min = value;
                }
            }

            return min;
        }

        /// <summary>
        /// Returns the least of a set of numbers.
        /// </summary>
        public static float Min(params float[] values)
        {
            int count = values.Length;
            if (count == 0)
            {
                return 0;
            }

            var min = values[0];
            for (int i = 1; i < count; i++)
            {
                var value = values[i];
                if (value < min)
                {
                    min = value;
                }
            }

            return min;
        }

        /// <summary>
        /// Returns the greater of two numbers.
        /// </summary>
        public static int Max(int a, int b) => a > b ? a : b;

        /// <summary>
        /// Returns the greater of two numbers.
        /// </summary>
        public static long Max(long a, long b) => a > b ? a : b;

        /// <summary>
        /// Returns the greater of two numbers.
        /// </summary>
        public static float Max(float a, float b) => a > b ? a : b;

        /// <summary>
        /// Returns the greater of two numbers.
        /// </summary>
        public static double Max(double a, double b) => a > b ? a : b;

        /// <summary>
        /// Returns the greatest of a set of numbers.
        /// </summary>
        public static float Max(params int[] values)
        {
            int count = values.Length;
            if (count == 0)
            {
                return 0;
            }

            var min = values[0];
            for (int i = 1; i < count; i++)
            {
                var value = values[i];
                if (value > min)
                {
                    min = value;
                }
            }

            return min;
        }

        /// <summary>
        /// Returns the greatest of a set of numbers.
        /// </summary>
        public static float Max(params float[] values)
        {
            int count = values.Length;
            if (count == 0)
            {
                return 0;
            }

            var min = values[0];
            for (int i = 1; i < count; i++)
            {
                var value = values[i];
                if (value > min)
                {
                    min = value;
                }
            }

            return min;
        }


        /// <summary>
        /// Clamps a number between a minimum and a maximum.
        /// </summary>
        /// <param name="n">The number to clamp.</param>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        public static int Clamp(int n, int min, int max) => Max(Min(n, max), min);

        /// <summary>
        /// Clamps a number between a minimum and a maximum.
        /// </summary>
        /// <param name="n">The number to clamp.</param>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        public static long Clamp(long n, long min, long max) => Max(Min(n, max), min);

        /// <summary>
        /// Clamps a number between a minimum and a maximum.
        /// </summary>
        /// <param name="n">The number to clamp.</param>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        public static float Clamp(float n, float min, float max) => Max(Min(n, max), min);

        /// <summary>
        /// Clamps a number between a minimum and a maximum.
        /// </summary>
        /// <param name="n">The number to clamp.</param>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        public static double Clamp(double n, double min, double max) => Max(Min(n, max), min);

        /// <summary>
        /// Clamps a number between 0 and 1.
        /// </summary>
        /// <param name="n">The number to clamp.</param>
        public static float Clamp01(float n) => Max(Min(n, 1f), 0f);

        /// <summary>
        /// Clamps a number between 0 and 1.
        /// </summary>
        /// <param name="n">The number to clamp.</param>
        public static double Clamp01(double n) => Max(Min(n, 1f), 0f);


        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        public static int Abs(int n) => Math.Abs(n);

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        public static long Abs(long n) => Math.Abs(n);

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        public static float Abs(float n) => Math.Abs(n);

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        public static double Abs(double n) => Math.Abs(n);


        /// <summary>
        /// Gets the sign of a number.
        /// </summary>
        public static float Sign(float n) => n >= 0f ? 1f : -1f;


        /// <summary>
        /// Returns the smallest integer greater than or equal to a number.
        /// </summary>
        public static int CeilToInt(float n) => (int)Math.Ceiling(n);

        /// <summary>
        /// Returns the largest integer less than or equal to a number.
        /// </summary>
        public static int FloorToInt(float n) => (int)Math.Floor(n);

        /// <summary>
        /// Rounds a number to the nearest integer.
        /// </summary>
        public static int RoundToInt(float n) => (int)Math.Round(n);


        /// <summary>
        /// Returns the smallest integer greater than or equal to a number.
        /// </summary>
        public static float Ceil(float n) => (float)Math.Ceiling(n);

        /// <summary>
        /// Returns the largest integer less than or equal to a number.
        /// </summary>
        public static float Floor(float n) => (float)Math.Floor(n);

        /// <summary>
        /// Rounds a number to the nearest integer.
        /// </summary>
        public static float Round(float n) => (float)Math.Round(n);


        /// <summary>
        /// Returns the square root of a number.
        /// </summary>
        public static float Sqrt(float n) => (float)Math.Sqrt(n);


        /// <summary>
        /// Returns the sine of an angle given in radians.
        /// </summary>
        public static float Sin(float n) => (float)Math.Sin(n);

        /// <summary>
        /// Returns the cosine of an angle given in radians.
        /// </summary>
        public static float Cos(float n) => (float)Math.Cos(n);

        /// <summary>
        /// Returns the tangent of an angle given in radians.
        /// </summary>
        public static float Tan(float n) => (float)Math.Tan(n);

        /// <summary>
        /// Returns the arcsine of a number in radians.
        /// </summary>
        public static float Asin(float n) => (float)Math.Asin(n);

        /// <summary>
        /// Returns the arccosine of a number in radians.
        /// </summary>
        public static float Acos(float n) => (float)Math.Acos(n);

        /// <summary>
        /// Returns the arctangent of a number in radians.
        /// </summary>
        public static float Atan(float n) => (float)Math.Atan(n);

        /// <summary>
        /// Returns the angle in radians with a tan of y/x.
        /// </summary>
        public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);


        /// <summary>
        /// Returns the number b raised to the power p.
        /// </summary>
        public static float Pow(float b, float p) => (float)Math.Pow(b, p);

        /// <summary>
        /// Returns e raised to the power p.
        /// </summary>
        public static float Exp(float p) => (float)Math.Exp(p);

        /// <summary>
        /// Returns logarithm base e of a number.
        /// </summary>
        public static float Log(float n) => (float)Math.Log(n);

        /// <summary>
        /// Returns logarithm with base b of a number.
        /// </summary>
        public static float Log(float n, float b) => (float)Math.Log(n, b);

        /// <summary>
        /// Returns logarithm base 10 of a number.
        /// </summary>
        public static float Log10(float n) => (float)Math.Log10(n);


        /// <summary>
        /// Returns the next power of two that is greater than or equal to the specified number.
        /// </summary>
        public static int NextPowerOfTwo(int n) => MathHelper.NextPowerOfTwo(n);

        /// <summary>
        /// Returns the next power of two that is greater than or equal to the specified number.
        /// </summary>
        public static long NextPowerOfTwo(long n) => MathHelper.NextPowerOfTwo(n);

        /// <summary>
        /// Returns the next power of two that is greater than or equal to the specified number.
        /// </summary>
        public static float NextPowerOfTwo(float n) => MathHelper.NextPowerOfTwo(n);

        /// <summary>
        /// Returns the next power of two that is greater than or equal to the specified number.
        /// </summary>
        public static double NextPowerOfTwo(double n) => MathHelper.NextPowerOfTwo(n);


        /// <summary>
        /// Calculates the factorial of a given natural number.
        /// </summary>
        public static long Factorial(int n) => MathHelper.Factorial(n);

        /// <summary>
        /// Calculates the binomial coefficient <paramref name="n"/> above <paramref name="k"/>.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="k">The k.</param>
        /// <returns>n! / (k! * (n - k)!).</returns>
        public static long BinomialCoefficient(int n, int k) => MathHelper.BinomialCoefficient(n, k);


        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// See <see cref="MathHelper.LerpPrecise"/> for a less efficient version with more precision around edge cases.
        /// </summary>
        /// <param name="a">From value.</param>
        /// <param name="b">To value.</param>
        /// <param name="t">Value indicating the weight of b.</param>
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// This method is a less efficient, more precise version of <see cref="MathHelper.Lerp"/>.
        /// </summary>
        /// <param name="a">From value.</param>
        /// <param name="b">To value.</param>
        /// <param name="t">Value between 0 and 1 indicating the weight of b.</param>
        public static float LerpPrecise(float a, float b, float t)
        {
            return ((1 - t) * a) + (b * t);
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// See <see cref="MathHelper.LerpClampedPrecise"/> for a less efficient version with more precision around edge cases.
        /// </summary>
        /// <param name="a">From value.</param>
        /// <param name="b">To value.</param>
        /// <param name="t">Value between 0 and 1 indicating the weight of b.</param>
        public static float LerpClamped(float a, float b, float t) => Lerp(a, b, Clamp01(t));

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// This method is a less efficient, more precise version of <see cref="MathHelper.LerpClamped"/>.
        /// </summary>
        /// <param name="a">From value.</param>
        /// <param name="b">To value.</param>
        /// <param name="t">Value between 0 and 1 indicating the weight of b.</param>
        public static float LerpClampedPrecise(float a, float b, float t) => LerpPrecise(a, b, Clamp01(t));

        /// <summary>
        /// Calculates the interpolation factor between a and b that results in a specific value.
        /// </summary>
        /// <param name="a">From value.</param>
        /// <param name="b">To value.</param>
        /// <param name="value">The value interpolated to.</param>
        public static float InverseLerp(float a, float b, float value)
        {
            if (a != b)
            {
                return (value - a) / (b - a);
            }
            return 0f;
        }

        /// <summary>
        /// Linearly interpolates from angle a to b by factor t.
        /// </summary>
        /// <param name="a">The angle to move from in radians.</param>
        /// <param name="b">The angle to move towards in radians.</param>
        /// <param name="t">Value between 0 and 1 indicating the weight of b.</param>
        public static float LerpAngle(float a, float b, float t)
        {
            float delta = Cycle(b - a, TwoPi);
            if (delta > Pi)
            {
                delta -= TwoPi;
            }
            return a + delta * t;
        }

        /// <summary>
        /// Linearly interpolates from angle a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">The angle to move from in radians.</param>
        /// <param name="b">The angle to move towards in radians.</param>
        /// <param name="t">Value between 0 and 1 indicating the weight of b.</param>
        public static float LerpAngleClamped(float a, float b, float t) => LerpAngle(a, b, Clamp01(t));


        /// <summary>
        /// Move a towards b up to some amount.
        /// </summary>
        /// <param name="a">The value to move from.</param>
        /// <param name="b">The value to move towards.</param>
        /// <param name="maxDelta">The maximum amount from can move.</param>
        public static float MoveTowards(float a, float b, float maxDelta)
        {
            float delta = b - a;
            if (Abs(delta) <= maxDelta)
            {
                return b;
            }
            return a + Sign(delta) * maxDelta;
        }

        /// <summary>
        /// Move from one angle to another up to some amount.
        /// </summary>
        /// <param name="from">The angle to move from in radians.</param>
        /// <param name="to">The angle to move to in radians.</param>
        /// <param name="maxDelta">The maximum amount from can move.</param>
        static public float MoveTowardsAngle(float from, float to, float maxDelta)
        {
            float delta = DeltaAngle(from, to);
            if (-maxDelta < delta && delta < maxDelta)
            {
                return to;
            }
            return MoveTowards(from, from + delta, maxDelta);
        }


        /// <summary>
        /// Loop a number so that it is wrapped zero and size.
        /// </summary>
        /// <param name="n">The number to wrap.</param>
        /// <param name="size">The period of the wrap function.</param>
        public static float Cycle(float n, float size)
        {
            return n - Floor(n / size) * size;
        }

        /// <summary>
        /// PingPongs a number so that it is wrapped between zero and size.
        /// </summary>
        /// <param name="n">The number to wrap.</param>
        /// <param name="size">The value to wrap around.</param>
        public static float PingPong(float n, float size)
        {
            return size - Abs(Cycle(n, size * 2f) - size);
        }

        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        public static float WrapAngle(float angle)
        {
            if ((angle > -Pi) && (angle <= Pi))
            {
                return angle;
            }

            angle %= TwoPi;

            if (angle <= -Pi)
            {
                return angle + TwoPi;
            }
            if (angle > Pi)
            {
                return angle - TwoPi;
            }
            return angle;
        }


        /// <summary>
        /// Returns the signed difference between two angles.
        /// </summary>
        /// <param name="from">The angle to move from in radians.</param>
        /// <param name="to">The angle to move to in radians.</param>
        public static float DeltaAngle(float from, float to)
        {
            float delta = Cycle(to - from, TwoPi);
            if (delta > Pi)
            {
                delta -= TwoPi;
            }
            return delta;
        }

        /// <summary>
        /// Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.
        /// </summary>
        /// <param name="a">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="b">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="c">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="u">The normalized barycentric (areal) coordinate b2, equal to the weighting factor for vertex 2, the coordinate of which is specified in value2.</param>
        /// <param name="v">The normalized barycentric (areal) coordinate b3, equal to the weighting factor for vertex 3, the coordinate of which is specified in value3.</param>
        /// <returns>Cartesian coordinate of the specified point with respect to the axis being used.</returns>
        public static float Barycentric(float a, float b, float c, float u, float v)
        {
            return a + (b - a) * u + (c - a) * v;
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="v1">The first position in the interpolation.</param>
        /// <param name="v2">The second position in the interpolation.</param>
        /// <param name="v3">The third position in the interpolation.</param>
        /// <param name="v4">The fourth position in the interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        public static float CatmullRom(float v1, float v2, float v3, float v4, float t)
        {
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            float t2 = t * t;
            float t3 = t2 * t;
            return 0.5f * (2.0f * v2 +
                (v3 - v1) * t +
                (2.0f * v1 - 5.0f * v2 + 4.0f * v3 - v4) * t2 +
                (3.0f * v2 - v1 - 3.0f * v3 + v4) * t3);
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        /// <param name="v1">Source position.</param>
        /// <param name="t1">Source tangent.</param>
        /// <param name="v2">Source position.</param>
        /// <param name="t2">Source tangent.</param>
        /// <param name="t">Weighting factor.</param>
        /// <returns>The result of the Hermite spline interpolation.</returns>
        public static float Hermite(float v1, float t1, float v2, float t2, float t)
        {
            if (t == 0f)
            {
                return v1;
            }
            else if (t == 1f)
            {
                return v2;
            }

            float tSquare = t * t;
            float tCube = tSquare * t;
            return
                (2 * v1 - 2 * v2 + t2 + t1) * tCube +
                (3 * v2 - 3 * v1 - 2 * t1 - t2) * tSquare +
                t1 * t +
                v1;
        }

        /// <summary>
        /// Interpolates between two values using a cubic equation.
        /// </summary>
        /// <param name="a">From value. Must be less than b.</param>
        /// <param name="b">To value. Must be greater than a.</param>
        /// <param name="t">Value between 0 and 1 indicating the weight of b.</param>
        public static float SmoothStep(float a, float b, float t)
        {
            float x = Clamp01((t - a) / (b - a));
            return x * x * (3f - 2f * x);
        }


        /// <summary>
        /// Transforms a value from linear space to gamma space.
        /// </summary>
        /// <param name="value">The linear value to transform.</param>
        /// <returns>An sRGB value.</returns>
        public static float LinearToSrgb(float value)
        {
            if (value <= 0.0031308)
            {
                return 12.92f * value;
            }
            else
            {
                return ((1.0f + 0.055f) * (float)Math.Pow(value, 1.0f / 2.4f)) - 0.055f;
            }
        }

        /// <summary>
        /// Transforms a value from gamma space to linear space.
        /// </summary>
        /// <param name="value">The sRGB value to transform.</param>
        /// <returns>A linear value.</returns>
        public static float SrgbToLinear(float value)
        {
            if (value <= 0.04045f)
            {
                return value / 12.92f;
            }
            else
            {
                return (float)Math.Pow((value + 0.055f) / (1.0f + 0.055f), 2.4f);
            }
        }


        /// <summary>
        /// Returns an approximation of the inverse square root of a number.
        /// </summary>
        /// <param name="x">A number.</param>
        /// <returns>An approximation of the inverse square root of the specified number, with an upper error bound of 0.001.</returns>
        /// <remarks>
        /// This is an improved implementation of the the method known as Carmack's inverse square root
        /// which is found in the Quake III source code. This implementation comes from
        /// http://www.codemaestro.com/reviews/review00000105.html. For the history of this method, see
        /// http://www.beyond3d.com/content/articles/8/.
        /// </remarks>
        public static float InverseSqrtFast(float x) => MathHelper.InverseSqrtFast(x);


        /// <summary>
        /// Approximates floating point equality with a maximum number of different bits.
        /// This is typically used in place of an epsilon comparison.
        /// see: https://randomascii.wordpress.com/2012/02/25/comparing-floating-point-numbers-2012-edition/
        /// see: https://stackoverflow.com/questions/3874627/floating-point-comparison-functions-for-c-sharp.
        /// </summary>
        /// <param name="a">The first value to compare.</param>
        /// <param name="b">>The second value to compare.</param>
        /// <param name="maxDeltaBits">The number of floating point bits to check.</param>
        /// <returns>True if the values are approximately equal, otherwise false.</returns>
        public static bool ApproximatelyEqual(float a, float b, int maxDeltaBits = 1) => MathHelper.ApproximatelyEqual(a, b, maxDeltaBits);

        /// <summary>
        /// Approximates single-precision floating point equality by an epsilon (maximum error) value.
        /// This method is designed as a "fits-all" solution and attempts to handle as many cases as possible.
        /// </summary>
        /// <param name="a">The first float.</param>
        /// <param name="b">The second float.</param>
        /// <param name="epsilon">The maximum error between the two.</param>
        /// <returns>
        ///  <value>true</value> if the values are approximately equal within the error margin, otherwise
        ///  <value>false</value>.
        /// </returns>
        public static bool ApproximatelyEqualEpsilon(float a, float b, float epsilon = 0.001f) => MathHelper.ApproximatelyEqualEpsilon(a, b, epsilon);

        /// <summary>
        /// Approximates double-precision floating point equality by an epsilon (maximum error) value.
        /// This method is designed as a "fits-all" solution and attempts to handle as many cases as possible.
        /// </summary>
        /// <param name="a">The first double.</param>
        /// <param name="b">The second double.</param>
        /// <param name="epsilon">The maximum error between the two.</param>
        /// <returns>
        ///  <value>true</value> if the values are approximately equal within the error margin, otherwise
        ///  <value>false</value>.
        /// </returns>
        public static bool ApproximatelyEqualEpsilon(double a, double b, double epsilon = 0.001) => MathHelper.ApproximatelyEqualEpsilon(a, b, epsilon);

        /// <summary>
        /// Approximates equivalence between two single-precision floating-point numbers on a direct human scale.
        /// It is important to note that this does not approximate equality - instead, it merely checks whether or not
        /// two numbers could be considered equivalent to each other within a certain tolerance. The tolerance is
        /// inclusive.
        /// </summary>
        /// <param name="a">The first value to compare.</param>
        /// <param name="b">The second value to compare.</param>
        /// <param name="tolerance">The tolerance within which the two values would be considered equivalent.</param>
        /// <returns>Whether or not the values can be considered equivalent within the tolerance.</returns>
        public static bool ApproximatelyEquivalent(float a, float b, float tolerance) => MathHelper.ApproximatelyEquivalent(a, b, tolerance);

        /// <summary>
        /// Approximates equivalence between two double-precision floating-point numbers on a direct human scale.
        /// It is important to note that this does not approximate equality - instead, it merely checks whether or not
        /// two numbers could be considered equivalent to each other within a certain tolerance. The tolerance is
        /// inclusive.
        /// </summary>
        /// <param name="a">The first value to compare.</param>
        /// <param name="b">The second value to compare.</param>
        /// <param name="tolerance">The tolerance within which the two values would be considered equivalent.</param>
        /// <returns>Whether or not the values can be considered equivalent within the tolerance.</returns>
        public static bool ApproximatelyEquivalent(double a, double b, double tolerance) => MathHelper.ApproximatelyEquivalent(a, b, tolerance);
    }
}
