/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Engine
{
    /// <summary>
    /// Describes a 2d vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector2 : IEquatable<Vector2>
    {
        /// <summary>
        /// Returns a vector with components (0, 0).
        /// </summary>
        public static readonly Vector2 Zero = new Vector2(0f, 0f);

        /// <summary>
        /// Returns a vector with components (1, 1).
        /// </summary>
        public static readonly Vector2 One = new Vector2(1f, 1f);

        /// <summary>
        /// Returns a vector with components (1, 0).
        /// </summary>
        public static readonly Vector2 UnitX = new Vector2(1f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 1).
        /// </summary>
        public static readonly Vector2 UnitY = new Vector2(0f, 1f);

        /// <summary>
        /// The x coordinate.
        /// </summary>
        public float x;

        /// <summary>
        /// The y coordinate.
        /// </summary>
        public float y;

        /// <summary>
        /// Constructs a 2d vector from two values.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Constructs a 2d vector with X and Y set to the same value.
        /// </summary>
        /// <param name="value">The x and y coordinates.</param>
        public Vector2(float value)
        {
            x = value;
            y = value;
        }

        /// <summary>
        /// Gets or sets the value at an index of the vector.
        /// </summary>
        /// <param name="index">The index of the component from the vector.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or greater than 1.</exception>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Returns the length of this vector.
        /// </summary>
        public float Length => Mathf.Sqrt((x * x) + (y * y));

        /// <summary>
        /// Returns an approximation for the length of this vector.
        /// </summary>
        /// <remarks>
        /// This property uses an approximation of the square root function to calculate vector magnitude, with
        /// an upper error bound of 0.001.
        /// </remarks>
        public float LengthFast => 1.0f / Mathf.InverseSqrtFast((x * x) + (y * y));

        /// <summary>
        /// Returns the squared length of this vector.
        /// </summary>
        public float LengthSquared => (x * x) + (y * y);

        /// <summary>
        /// Returns a copy of the vector scaled to unit length.
        /// </summary>
        public Vector2 Normalized => Normalize(this);

        /// <summary>
        /// Returns a copy of the vector scaled to approximately unit length.
        /// </summary>
        /// <remarks>
        /// This property uses an approximation of the square root function to calculate vector magnitude, with
        /// an upper error bound of 0.001.
        /// </remarks>
        public Vector2 NormalizedFast => NormalizeFast(this);

        /// <summary>
        /// Gets the perpendicular vector on the right side of this vector.
        /// </summary>
        public Vector2 PerpendicularRight => new Vector2(y, -x);

        /// <summary>
        /// Gets the perpendicular vector on the left side of this vector.
        /// </summary>
        public Vector2 PerpendicularLeft => new Vector2(-y, x);
        
        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector2 ComponentMin(Vector2 a, Vector2 b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <param name="result">The component-wise minimum.</param>
        public static void ComponentMin(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector2 ComponentMax(Vector2 a, Vector2 b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <param name="result">The component-wise maximum.</param>
        public static void ComponentMax(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector2 ComponentClamp(Vector2 vector, Vector2 min, Vector2 max)
        {
            return new Vector2(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y));
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <param name="result">The clamped value as an output parameter.</param>
        public static void ComponentClamp(ref Vector2 vector, ref Vector2 min, ref Vector2 max, out Vector2 result)
        {
            result.x = Mathf.Clamp(vector.x, min.x, max.x);
            result.y = Mathf.Clamp(vector.y, min.y, max.y);
        }

        /// <summary>
        /// Returns the vector with the lesser magnitude.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        public static Vector2 Min(Vector2 left, Vector2 right)
        {
            return left.LengthSquared < right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with the lesser magnitude. If the magnitudes are equal, the second vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <param name="result">The magnitude-wise minimum.</param>
        public static void Min(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            result = left.LengthSquared < right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with the greater magnitude.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        public static Vector2 Max(Vector2 left, Vector2 right)
        {
            return left.LengthSquared >= right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with the maximum magnitude. If the magnitudes are equal, the first vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <param name="result">The magnitude-wise maximum.</param>
        public static void Max(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            result = left.LengthSquared >= right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with a magnitude less than or equal to the specified length.
        /// </summary>
        /// <param name="vector">The vector to clamp.</param>
        /// <param name="maxLength">The maxiumum maginude of the returned vector.</param>
        public static Vector2 MagnitudeClamp(Vector2 vector, float maxLength)
        {
            if (vector.LengthSquared > maxLength * maxLength)
            {
                return vector.Normalized * maxLength;
            }
            return vector;
        }

        /// <summary>
        /// Performs vector addition.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        public static Vector2 Add(Vector2 a, Vector2 b)
        {
            Add(ref a, ref b, out a);
            return a;
        }

        /// <summary>
        /// Performs vector addition.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <param name="result">The result of the vector addition.</param>
        public static void Add(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            result.x = a.x + b.x;
            result.y = a.y + b.y;
        }

        /// <summary>
        /// Performs vector subtraction.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        public static Vector2 Subtract(Vector2 a, Vector2 b)
        {
            Subtract(ref a, ref b, out a);
            return a;
        }

        /// <summary>
        /// Performs vector subtraction.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <param name="result">The result of the vector subtraction as an output parameter.</param>
        public static void Subtract(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            result.x = a.x - b.x;
            result.y = a.y - b.y;
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">The vector to negate.</param>
        public static Vector2 Negate(Vector2 vector)
        {
            Negate(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">The vector to negate.</param>
        /// <param name="result">The result of the vector inversion as an output parameter.</param>
        public static void Negate(ref Vector2 vector, out Vector2 result)
        {
            result.x = -vector.x;
            result.y = -vector.y;
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector2 Multiply(Vector2 vector, float scale)
        {
            Multiply(ref vector, scale, out vector);
            return vector;
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Multiply(ref Vector2 vector, float scale, out Vector2 result)
        {
            result.x = vector.x * scale;
            result.y = vector.y * scale;
        }

        /// <summary>
        /// Multiplies a vector by the components another.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector2 Multiply(Vector2 vector, Vector2 scale)
        {
            Multiply(ref vector, ref scale, out vector);
            return vector;
        }

        /// <summary>
        /// Multiplies a vector by the components another.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Multiply(ref Vector2 vector, ref Vector2 scale, out Vector2 result)
        {
            result.x = vector.x * scale.x;
            result.y = vector.y * scale.y;
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector2 Divide(Vector2 vector, float scale)
        {
            Divide(ref vector, scale, out vector);
            return vector;
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Divide(ref Vector2 vector, float scale, out Vector2 result)
        {
            result.x = vector.x / scale;
            result.y = vector.y / scale;
        }

        /// <summary>
        /// Divides a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector2 Divide(Vector2 vector, Vector2 scale)
        {
            Divide(ref vector, ref scale, out vector);
            return vector;
        }

        /// <summary>
        /// Divide a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Divide(ref Vector2 vector, ref Vector2 scale, out Vector2 result)
        {
            result.x = vector.x / scale.x;
            result.y = vector.y / scale.y;
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static float Dot(Vector2 a, Vector2 b)
        {
            Dot(ref a, ref b, out float result);
            return result;
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="result">The dot product of two vectors as an output parameter.</param>
        public static void Dot(ref Vector2 a, ref Vector2 b, out float result)
        {
            result = (a.x * b.x) + (a.y * b.y);
        }

        /// <summary>
        /// Reflects a vector about a normal.
        /// </summary>
        /// <param name="vector">The vector to reflect.</param>
        /// <param name="normal">The reflection normal.</param>
        public static Vector2 Reflect(Vector2 vector, Vector2 normal)
        {
            Reflect(ref vector, ref normal, out vector);
            return vector;
        }

        /// <summary>
        /// Reflects a vector about a normal.
        /// </summary>
        /// <param name="vector">The vector to reflect.</param>
        /// <param name="normal">The reflection normal.</param>
        /// <param name="result">Reflected vector as an output parameter.</param>
        public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            result = vector - (2f * Dot(vector, normal) * normal);
        }

        /// <summary>
        /// Scale a vector to unit length.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        public static Vector2 Normalize(Vector2 vector)
        {
            Normalize(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Scale a vector to unit length.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        /// <param name="result">The normalized vector as an output parameter.</param>
        public static void Normalize(ref Vector2 vector, out Vector2 result)
        {
            float scale = 1f / vector.Length;
            result.x = vector.x * scale;
            result.y = vector.y * scale;
        }

        /// <summary>
        /// Scale a vector to approximately unit length.
        /// </summary>
        /// <remarks>
        /// This property uses an approximation of the square root function to calculate vector magnitude, with
        /// an upper error bound of 0.001.
        /// </remarks>
        /// <param name="vector">The vector to normalize.</param>
        public static Vector2 NormalizeFast(Vector2 vector)
        {
            NormalizeFast(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Scale a vector to approximately unit length.
        /// </summary>
        /// <remarks>
        /// This property uses an approximation of the square root function to calculate vector magnitude, with
        /// an upper error bound of 0.001.
        /// </remarks>
        /// <param name="vector">The vector to normalize.</param>
        /// <param name="result">The normalized vector as an output parameter.</param>
        public static void NormalizeFast(ref Vector2 vector, out Vector2 result)
        {
            float scale = Mathf.InverseSqrtFast(vector.LengthSquared);
            result.x = vector.x * scale;
            result.y = vector.y * scale;
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static float Distance(Vector2 a, Vector2 b)
        {
            Distance(ref a, ref b, out float result);
            return result;
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="result">The distance between two vectors as an output parameter.</param>
        public static void Distance(ref Vector2 a, ref Vector2 b, out float result)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            result = Mathf.Sqrt((dx * dx) + (dy * dy));
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static float DistanceSquared(Vector2 a, Vector2 b)
        {
            DistanceSquared(ref a, ref b, out float result);
            return result;
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="result">The squared distance between two vectors as an output parameter.</param>
        public static void DistanceSquared(ref Vector2 a, ref Vector2 b, out float result)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            result = (dx * dx) + (dy * dy);
        }

        /// <summary>
        /// Returns the absolute value of the angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        public static float Angle(Vector2 from, Vector2 to)
        {
            Angle(ref from, ref to, out float result);
            return result;
        }

        /// <summary>
        /// Returns the absolute value of the angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        /// <param name="result">The angle between two vectors as an output parameter.</param>
        public static void Angle(ref Vector2 from, ref Vector2 to, out float result)
        {
            float denominator = Mathf.Sqrt(from.LengthSquared * to.LengthSquared);
            if (denominator < 1e-15f)
            {
                result = 0f;
            }
            else
            {
                result = Mathf.Acos(Mathf.Clamp(Dot(from, to) / denominator, -1f, 1f));
            }
        }

        /// <summary>
        /// Returns the signed angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        public static float SignedAngle(Vector2 from, Vector2 to)
        {
            SignedAngle(ref from, ref to, out float result);
            return result;
        }

        /// <summary>
        /// Returns the signed angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        public static void SignedAngle(ref Vector2 from, ref Vector2 to, out float result)
        {
            result = Mathf.Sign((from.x * to.y) - (from.x * to.y)) * Angle(from, to);
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            Lerp(ref a, ref b, t, out a);
            return a;
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        public static void Lerp(ref Vector2 a, ref Vector2 b, float t, out Vector2 result)
        {
            result.x = (t * (b.x - a.x)) + a.x;
            result.y = (t * (b.y - a.y)) + a.y;
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        public static Vector2 LerpClamped(Vector2 a, Vector2 b, float t)
        {
            LerpClamped(ref a, ref b, t, out a);
            return a;
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        public static void LerpClamped(ref Vector2 a, ref Vector2 b, float t, out Vector2 result)
        {
            t = Mathf.Clamp01(t);
            result.x = (t * (b.x - a.x)) + a.x;
            result.y = (t * (b.y - a.y)) + a.y;
        }

        /// <summary>
        /// Move a towards b up to some amount.
        /// </summary>
        /// <param name="a">The vector to move from.</param>
        /// <param name="b">The vector to move towards.</param>
        /// <param name="maxDelta">The maximum distance that a can move.</param>
        public static Vector2 MoveTowards(Vector2 a, Vector2 b, float maxDelta)
        {
            MoveTowards(ref a, ref b, maxDelta, out a);
            return a;
        }

        /// <summary>
        /// Move a towards b up to some amount.
        /// </summary>
        /// <param name="a">The vector to move from.</param>
        /// <param name="b">The vector to move towards.</param>
        /// <param name="maxDelta">The maximum distance that a can move.</param>
        public static void MoveTowards(ref Vector2 a, ref Vector2 b, float maxDelta, out Vector2 result)
        {
            Vector2 delta = b - a;
            if (delta.LengthSquared <= maxDelta * maxDelta)
            {
                result = b;
            }
            else
            {
                result = a + delta.Normalized * maxDelta;
            }
        }

        /// <summary>
        /// Creates a vector that contains the cartesian coordinates of a vector specified in barycentric coordinates.
        /// </summary>
        /// <param name="a">The first vector of triangle.</param>
        /// <param name="b">The second vector of triangle.</param>
        /// <param name="c">The third vector of triangle.</param>
        /// <param name="u">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of triangle.</param>
        /// <param name="v">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of triangle.</param>
        /// <returns>The cartesian translation of barycentric coordinates.</returns>
        public static Vector2 Barycentric(Vector2 a, Vector2 b, Vector2 c, float u, float v)
        {
            return a + (b - a) * u + (c - a) * v;
        }

        /// <summary>
        /// Creates a vector that contains the cartesian coordinates of a vector specified in barycentric coordinates.
        /// </summary>
        /// <param name="a">The first vector of triangle.</param>
        /// <param name="b">The second vector of triangle.</param>
        /// <param name="c">The third vector of triangle.</param>
        /// <param name="u">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of triangle.</param>
        /// <param name="v">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of triangle.</param>
        /// <param name="result">The cartesian translation of barycentric coordinates as an output parameter.</param>
        public static void Barycentric(ref Vector2 a, ref Vector2 b, ref Vector2 c, float u, float v, out Vector2 result)
        {
            result = a; // copy

            var temp = b; // copy
            Subtract(ref temp, ref a, out temp);
            Multiply(ref temp, u, out temp);
            Add(ref result, ref temp, out result);

            temp = c; // copy
            Subtract(ref temp, ref a, out temp);
            Multiply(ref temp, v, out temp);
            Add(ref result, ref temp, out result);
        }

        /// <summary>
        /// Creates a vector that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="a">The first vector in interpolation.</param>
        /// <param name="b">The second vector in interpolation.</param>
        /// <param name="c">The third vector in interpolation.</param>
        /// <param name="d">The fourth vector in interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        public static Vector2 CatmullRom(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
        {
            return new Vector2(
                Mathf.CatmullRom(a.x, b.x, c.x, d.x, t),
                Mathf.CatmullRom(a.y, b.y, c.y, d.y, t));
        }

        /// <summary>
        /// Creates a vector that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="a">The first vector in interpolation.</param>
        /// <param name="b">The second vector in interpolation.</param>
        /// <param name="c">The third vector in interpolation.</param>
        /// <param name="d">The fourth vector in interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        /// <param name="result">The result of CatmullRom interpolation as an output parameter.</param>
        public static void CatmullRom(ref Vector2 a, ref Vector2 b, ref Vector2 c, ref Vector2 d, float t, out Vector2 result)
        {
            result.x = Mathf.CatmullRom(a.x, b.x, c.x, d.x, t);
            result.y = Mathf.CatmullRom(a.y, b.y, c.y, d.y, t);
        }

        /// <summary>
        /// Creates a vector that contains hermite spline interpolation.
        /// </summary>
        /// <param name="v1">The first position vector.</param>
        /// <param name="t1">The first tangent vector.</param>
        /// <param name="v2">The second position vector.</param>
        /// <param name="t2">The second tangent vector.</param>
        /// <param name="t">Weighting factor.</param>
        public static Vector2 Hermite(Vector2 v1, Vector2 t1, Vector2 v2, Vector2 t2, float t)
        {
            return new Vector2(
                Mathf.Hermite(v1.x, t1.x, v2.x, t2.x, t),
                Mathf.Hermite(v1.y, t1.y, v2.y, t2.y, t));
        }

        /// <summary>
        /// Creates a vector that contains hermite spline interpolation.
        /// </summary>
        /// <param name="v1">The first position vector.</param>
        /// <param name="t1">The first tangent vector.</param>
        /// <param name="v2">The second position vector.</param>
        /// <param name="t2">The second tangent vector.</param>
        /// <param name="t">Weighting factor.</param>
        /// <param name="result">The hermite spline interpolation vector as an output parameter.</param>
        public static void Hermite(ref Vector2 v1, ref Vector2 t1, ref Vector2 v2, ref Vector2 t2, float t, out Vector2 result)
        {
            result.x = Mathf.Hermite(v1.x, t1.x, v2.x, t2.x, t);
            result.y = Mathf.Hermite(v1.y, t1.y, v2.y, t2.y, t);
        }

        /// <summary>
        /// Creates a vector that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="a">The first vector in interpolation.</param>
        /// <param name="b">The second vector in interpolation.</param>
        /// <param name="t">Weighting value.</param>
        public static Vector2 SmoothStep(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(
                Mathf.SmoothStep(a.x, b.x, t),
                Mathf.SmoothStep(a.y, b.y, t));
        }

        /// <summary>
        /// Creates a vector that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="a">The first vector in interpolation.</param>
        /// <param name="b">The second vector in interpolation.</param>
        /// <param name="t">Weighting value.</param>
        /// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
        public static void SmoothStep(ref Vector2 a, ref Vector2 b, float t, out Vector2 result)
        {
            result.x = Mathf.SmoothStep(a.x, b.x, t);
            result.y = Mathf.SmoothStep(a.y, b.y, t);
        }

        /// <summary>
        /// Rotates a vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        public static Vector2 Rotate(Vector2 vector, Quaternion rotation)
        {
            Rotate(ref vector, ref rotation, out Vector2 result);
            return result;
        }

        /// <summary>
        /// Rotates a vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <param name="result">The transformed vector as an output parameter.</param>
        public static void Rotate(ref Vector2 vector, ref Quaternion rotation, out Vector2 result)
        {
            Vector3 r1 = new Vector3(rotation.x + rotation.x, rotation.y + rotation.y, rotation.z + rotation.z);
            Vector3 r2 = r1 * new Vector3(rotation.x, rotation.x, rotation.w);
            Vector3 r3 = r1 * new Vector3(1f, rotation.y, rotation.z);
            
            result.x = vector.x * (1f - r3.y - r3.z) + vector.y * (r2.y - r2.z);
            result.y = vector.x * (r2.y + r2.z) + vector.y * (1f - r2.x - r3.z);
        }

        /// <summary>
        /// Applies a rotation to all vectors within array and places the results in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        public static void Rotate(Vector2[] srcArray, ref Quaternion rotation, Vector2[] destArray)
        {
            if (srcArray == null)
            {
                throw new ArgumentNullException("srcArray");
            }
            if (destArray == null)
            {
                throw new ArgumentNullException("destArray");
            }
            if (destArray.Length < srcArray.Length)
            {
                throw new ArgumentException("Destination array is smaller than source array.");
            }

            for (int i = 0; i < srcArray.Length; i++)
            {
                Rotate(ref srcArray[i], ref rotation, out destArray[i]);
            }
        }

        /// <summary>
        /// Applies a rotation to all vectors within array and places the resuls in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="srcIndex">The starting index in the source array.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        /// <param name="destIndex">The starting index in the destination array.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Rotate(Vector2[] srcArray, int srcIndex, ref Quaternion rotation, Vector2[] destArray, int destIndex, int length)
        {
            if (srcArray == null)
            {
                throw new ArgumentNullException("srcArray");
            }
            if (destArray == null)
            {
                throw new ArgumentNullException("destArray");
            }
            if (srcArray.Length < srcIndex + length)
            {
                throw new ArgumentException("Source array length is lesser than srcIndex + length");
            }
            if (destArray.Length < destIndex + length)
            {
                throw new ArgumentException("Destination array length is lesser than destIndex + length");
            }

            for (int i = 0; i < length; i++)
            {
                Rotate(ref srcArray[srcIndex + i], ref rotation, out destArray[destIndex + i]);
            }
        }

        /// <summary>
        /// Compares whether current instance is equal to a specified vector.
        /// </summary>
        /// <param name="other">The vector to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Vector2 other)
        {
            return x == other.x && y == other.y;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Vector2) && Equals((Vector2)obj);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this instance.
        /// </summary>
        public override string ToString()
        {
            const string format = "F2";
            return $"({x.ToString(format)}, {y.ToString(format)})";
        }

        /// <summary>
        /// Adds the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            left.x += right.x;
            left.y += right.y;
            return left;
        }

        /// <summary>
        /// Subtracts the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of the vector subtraction.</returns>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            left.x -= right.x;
            left.y -= right.y;
            return left;
        }

        /// <summary>
        /// Negates the specified vector.
        /// </summary>
        /// <param name="vector">Operand.</param>
        /// <returns>Result of the negation.</returns>
        public static Vector2 operator -(Vector2 vector)
        {
            vector.x = -vector.x;
            vector.y = -vector.y;
            return vector;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector2 operator *(Vector2 vector, float scale)
        {
            vector.x *= scale;
            vector.y *= scale;
            return vector;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="vector">Right operand.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector2 operator *(float scale, Vector2 vector)
        {
            vector.x *= scale;
            vector.y *= scale;
            return vector;
        }

        /// <summary>
        /// Component-wise multiplication of two vectors.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the vector multiplication.</returns>
        public static Vector2 operator *(Vector2 vector, Vector2 scale)
        {
            vector.x *= scale.x;
            vector.y *= scale.y;
            return vector;
        }

        /// <summary>
        /// Divides the components of a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="divider">Right operand.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        public static Vector2 operator /(Vector2 vector, float divider)
        {
            float factor = 1f / divider;
            vector.x *= factor;
            vector.y *= factor;
            return vector;
        }

        /// <summary>
        /// Component-wise division of one vector by another.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static Vector2 operator /(Vector2 vector, Vector2 scale)
        {
            vector.x /= scale.x;
            vector.y /= scale.y;
            return vector;
        }

        /// <summary>
        /// Compares whether two vectors are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.x == right.x && left.y == right.y;
        }

        /// <summary>
        /// Compares whether two vectors are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are not equal, <c>false</c> otherwise.</returns>	
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return left.x != right.x || left.y != right.y;
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static implicit operator Vector3(Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0f);
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static implicit operator Vector4(Vector2 vector)
        {
            return new Vector4(vector.x, vector.y, 0f, 0f);
        }

        /// <summary>
        /// Cast the vector to a <see cref="OpenTK.Vector2"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator OpenTK.Vector2(Vector2 vector)
        {
            return Unsafe.ReinterpretCast<Vector2, OpenTK.Vector2>(vector);
        }
    }
}
