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
    /// Describes a 4d vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector4 : IEquatable<Vector4>
    {
        /// <summary>
        /// Returns a vector with components (0, 0, 0, 0).
        /// </summary>
        public static readonly Vector4 Zero = new Vector4(0f, 0f, 0f, 0f);

        /// <summary>
        /// Returns a vector with components (1, 1, 1, 1).
        /// </summary>
        public static readonly Vector4 One = new Vector4(1f, 1f, 1f, 1f);

        /// <summary>
        /// Returns a vector with components (1, 0, 0, 0).
        /// </summary>
        public static readonly Vector4 UnitX = new Vector4(1f, 0f, 0f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 1, 0, 0).
        /// </summary>
        public static readonly Vector4 UnitY = new Vector4(0f, 1f, 0f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 0, 1, 0).
        /// </summary>
        public static readonly Vector4 UnitZ = new Vector4(0f, 0f, 1f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 0, 0, 1).
        /// </summary>
        public static readonly Vector4 UnitW = new Vector4(0f, 0f, 0f, 1f);

        /// <summary>
        /// The x coordinate.
        /// </summary>
        public float x;

        /// <summary>
        /// The y coordinate.
        /// </summary>
        public float y;

        /// <summary>
        /// The z coordinate.
        /// </summary>
        public float z;

        /// <summary>
        /// The w coordinate.
        /// </summary>
        public float w;

        /// <summary>
        /// Constructs a 4d vector from four values.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a 4d vector with X, Y, Z and W set to the same value.
        /// </summary>
        /// <param name="value">The x, y, z and w coordinates.</param>
        public Vector4(float value)
        {
            x = value;
            y = value;
            z = value;
            w = value;
        }

        /// <summary>
        /// Constructs a 4d vector with X and Z from a <see cref="Vector2"/> and Z and W from scalars.
        /// </summary>
        /// <param name="value">The x and y coordinates.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4(Vector2 value, float z, float w)
        {
            x = value.x;
            y = value.y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a 4d vector from a pair of <see cref="Vector2"/>.
        /// </summary>
        /// <param name="xy">The x and y coordinates.</param>
        /// <param name="xy">The z and w coordinates.</param>
        public Vector4(Vector2 xy, Vector2 zw)
        {
            x = xy.x;
            y = xy.y;
            z = zw.x;
            w = zw.y;
        }

        /// <summary>
        /// Constructs a 4d vector with X, Y, Z from <see cref="Vector3"/> and W from a scalar.
        /// </summary>
        /// <param name="value">The x, y and z coordinates.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4(Vector3 value, float w)
        {
            x = value.x;
            y = value.y;
            z = value.z;
            this.w = w;
        }

        /// <summary>
        /// Gets or sets the value at an index of the vector.
        /// </summary>
        /// <param name="index">The index of the component from the vector.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or greater than 3.</exception>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
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
                    case 2: z = value; break;
                    case 3: w = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Returns the length of this vector.
        /// </summary>
        public float Length => Mathf.Sqrt((x * x) + (y * y) + (z * z) + (w * w));

        /// <summary>
        /// Returns an approximation for the length of this vector.
        /// </summary>
        /// <remarks>
        /// This property uses an approximation of the square root function to calculate vector magnitude, with
        /// an upper error bound of 0.001.
        /// </remarks>
        public float LengthFast => 1f / Mathf.InverseSqrtFast((x * x) + (y * y) + (z * z) + (w * w));

        /// <summary>
        /// Returns the squared length of this vector.
        /// </summary>
        public float LengthSquared => (x * x) + (y * y) + (z * z) + (w * w);

        /// <summary>
        /// Returns a copy of the vector scaled to unit length.
        /// </summary>
        public Vector4 Normalized => Normalize(this);

        /// <summary>
        /// Returns a copy of the vector scaled to approximately unit length.
        /// </summary>
        /// <remarks>
        /// This property uses an approximation of the square root function to calculate vector magnitude, with
        /// an upper error bound of 0.001.
        /// </remarks>
        public Vector4 NormalizedFast => NormalizeFast(this);

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector4 ComponentMin(Vector4 a, Vector4 b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            a.z = a.z < b.z ? a.z : b.z;
            a.w = a.w < b.w ? a.w : b.w;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <param name="result">The component-wise minimum.</param>
        public static void ComponentMin(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
            result.z = a.z < b.z ? a.z : b.z;
            result.w = a.w < b.w ? a.w : b.w;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector4 ComponentMax(Vector4 a, Vector4 b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            a.z = a.z > b.z ? a.z : b.z;
            a.w = a.w > b.w ? a.w : b.w;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <param name="result">The component-wise maximum.</param>
        public static void ComponentMax(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
            result.z = a.z > b.z ? a.z : b.z;
            result.w = a.w > b.w ? a.w : b.w;
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector4 ComponentClamp(Vector4 vector, Vector4 min, Vector4 max)
        {
            return new Vector4(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y),
                Mathf.Clamp(vector.z, min.z, max.z),
                Mathf.Clamp(vector.w, min.w, max.w));
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <param name="result">The clamped value as an output parameter.</param>
        public static void ComponentClamp(ref Vector4 vector, ref Vector4 min, ref Vector4 max, out Vector4 result)
        {
            result.x = Mathf.Clamp(vector.x, min.x, max.x);
            result.y = Mathf.Clamp(vector.y, min.y, max.y);
            result.z = Mathf.Clamp(vector.z, min.z, max.z);
            result.w = Mathf.Clamp(vector.w, min.w, max.w);
        }

        /// <summary>
        /// Returns the vector with the lesser magnitude.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        public static Vector4 Min(Vector4 left, Vector4 right)
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
        public static void Min(ref Vector4 left, ref Vector4 right, out Vector4 result)
        {
            result = left.LengthSquared < right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with the greater magnitude.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        public static Vector4 Max(Vector4 left, Vector4 right)
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
        public static void Max(ref Vector4 left, ref Vector4 right, out Vector4 result)
        {
            result = left.LengthSquared >= right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with a magnitude less than or equal to the specified length.
        /// </summary>
        /// <param name="vector">The vector to clamp.</param>
        /// <param name="maxLength">The maxiumum maginude of the returned vector.</param>
        public static Vector4 MagnitudeClamp(Vector4 vector, float maxLength)
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
        public static Vector4 Add(Vector4 a, Vector4 b)
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
        public static void Add(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            result.x = a.x + b.x;
            result.y = a.y + b.y;
            result.z = a.z + b.z;
            result.w = a.w + b.w;
        }

        /// <summary>
        /// Performs vector subtraction.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        public static Vector4 Subtract(Vector4 a, Vector4 b)
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
        public static void Subtract(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            result.x = a.x - b.x;
            result.y = a.y - b.y;
            result.z = a.z - b.z;
            result.w = a.w - b.w;
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">The vector to negate.</param>
        public static Vector4 Negate(Vector4 vector)
        {
            Negate(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">The vector to negate.</param>
        /// <param name="result">The result of the vector inversion as an output parameter.</param>
        public static void Negate(ref Vector4 vector, out Vector4 result)
        {
            result.x = -vector.x;
            result.y = -vector.y;
            result.z = -vector.z;
            result.w = -vector.w;
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector4 Multiply(Vector4 vector, float scale)
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
        public static void Multiply(ref Vector4 vector, float scale, out Vector4 result)
        {
            result.x = vector.x * scale;
            result.y = vector.y * scale;
            result.z = vector.z * scale;
            result.w = vector.w * scale;
        }

        /// <summary>
        /// Multiplies a vector by the components another.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector4 Multiply(Vector4 vector, Vector4 scale)
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
        public static void Multiply(ref Vector4 vector, ref Vector4 scale, out Vector4 result)
        {
            result.x = vector.x * scale.x;
            result.y = vector.y * scale.y;
            result.z = vector.z * scale.z;
            result.w = vector.w * scale.w;
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector4 Divide(Vector4 vector, float scale)
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
        public static void Divide(ref Vector4 vector, float scale, out Vector4 result)
        {
            result.x = vector.x / scale;
            result.y = vector.y / scale;
            result.z = vector.z / scale;
            result.w = vector.w / scale;
        }

        /// <summary>
        /// Divides a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector4 Divide(Vector4 vector, Vector4 scale)
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
        public static void Divide(ref Vector4 vector, ref Vector4 scale, out Vector4 result)
        {
            result.x = vector.x / scale.x;
            result.y = vector.y / scale.y;
            result.z = vector.z / scale.z;
            result.w = vector.w / scale.w;
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static float Dot(Vector4 a, Vector4 b)
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
        public static void Dot(ref Vector4 a, ref Vector4 b, out float result)
        {
            result = (a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w);
        }

        /// <summary>
        /// Projects a vector along a normal.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="normal">The normal the vector is projected on to.</param>
        public static Vector4 Project(Vector4 vector, Vector4 normal)
        {
            Project(ref vector, ref normal, out vector);
            return vector;
        }

        /// <summary>
        /// Projects a vector along a normal.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="normal">The normal the vector is projected on to.</param>
        /// <param name="result">Projected vector as an output parameter.</param>
        public static void Project(ref Vector4 vector, ref Vector4 normal, out Vector4 result)
        {
            float sqrLen = normal.LengthSquared;
            if (sqrLen < float.Epsilon)
            {
                result = Zero;
            }
            else
            {
                result = normal * Dot(vector, normal) / sqrLen;
            }
        }

        /// <summary>
        /// Scale a vector to unit length.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        public static Vector4 Normalize(Vector4 vector)
        {
            Normalize(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Scale a vector to unit length.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        /// <param name="result">The normalized vector as an output parameter.</param>
        public static void Normalize(ref Vector4 vector, out Vector4 result)
        {
            float scale = 1f / vector.Length;
            result.x = vector.x * scale;
            result.y = vector.y * scale;
            result.z = vector.z * scale;
            result.w = vector.w * scale;
        }

        /// <summary>
        /// Scale a vector to approximately unit length.
        /// </summary>
        /// <remarks>
        /// This property uses an approximation of the square root function to calculate vector magnitude, with
        /// an upper error bound of 0.001.
        /// </remarks>
        /// <param name="vector">The vector to normalize.</param>
        public static Vector4 NormalizeFast(Vector4 vector)
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
        public static void NormalizeFast(ref Vector4 vector, out Vector4 result)
        {
            float scale = Mathf.InverseSqrtFast(vector.LengthSquared);
            result.x = vector.x * scale;
            result.y = vector.y * scale;
            result.z = vector.z * scale;
            result.w = vector.w * scale;
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static float Distance(Vector4 a, Vector4 b)
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
        public static void Distance(ref Vector4 a, ref Vector4 b, out float result)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;
            float dw = a.w - b.w;
            result = Mathf.Sqrt((dx * dx) + (dy * dy) + (dz * dz) + (dw * dw));
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static float DistanceSquared(Vector4 a, Vector4 b)
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
        public static void DistanceSquared(ref Vector4 a, ref Vector4 b, out float result)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;
            float dw = a.w - b.w;
            result = (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
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
        public static void Lerp(ref Vector4 a, ref Vector4 b, float t, out Vector4 result)
        {
            result.x = (t * (b.x - a.x)) + a.x;
            result.y = (t * (b.y - a.y)) + a.y;
            result.z = (t * (b.z - a.z)) + a.z;
            result.w = (t * (b.w - a.w)) + a.w;
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        public static Vector4 LerpClamped(Vector4 a, Vector4 b, float t)
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
        public static void LerpClamped(ref Vector4 a, ref Vector4 b, float t, out Vector4 result)
        {
            t = Mathf.Clamp01(t);
            result.x = (t * (b.x - a.x)) + a.x;
            result.y = (t * (b.y - a.y)) + a.y;
            result.z = (t * (b.z - a.z)) + a.z;
            result.w = (t * (b.w - a.w)) + a.w;
        }

        /// <summary>
        /// Move a towards b up to some amount.
        /// </summary>
        /// <param name="a">The vector to move from.</param>
        /// <param name="b">The vector to move towards.</param>
        /// <param name="maxDelta">The maximum distance that a can move.</param>
        public static Vector4 MoveTowards(Vector4 a, Vector4 b, float maxDelta)
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
        public static void MoveTowards(ref Vector4 a, ref Vector4 b, float maxDelta, out Vector4 result)
        {
            Vector4 delta = b - a;
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
        /// Rotates a vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        public static Vector4 Rotate(Vector4 vector, Quaternion rotation)
        {
            Rotate(ref vector, ref rotation, out vector);
            return vector;
        }

        /// <summary>
        /// Rotates a vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <param name="result">The transformed vector as an output parameter.</param>
        public static void Rotate(ref Vector4 vector, ref Quaternion rotation, out Vector4 result)
        {
            Quaternion v = (Quaternion)vector;
            Quaternion.Invert(ref rotation, out Quaternion invRotation);
            Quaternion.Multiply(ref rotation, ref v, out Quaternion t);
            Quaternion.Multiply(ref t, ref invRotation, out v);

            result.x = v.x;
            result.y = v.y;
            result.z = v.z;
            result.w = v.w;
        }

        /// <summary>
        /// Applies a rotation to all vectors within array and places the results in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        public static void Rotate(Vector4[] srcArray, ref Quaternion rotation, Vector4[] destArray)
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
        public static void Rotate(Vector4[] srcArray, int srcIndex, ref Quaternion rotation, Vector4[] destArray, int destIndex, int length)
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
        /// Transform a vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        public static Vector4 Transform(Vector4 vector, Matrix matrix)
        {
            Transform(ref vector, ref matrix, out Vector4 result);
            return result;
        }

        /// <summary>
        /// Transform a vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="result">The transformed vector as an output parameter.</param>
        public static void Transform(ref Vector4 vector, ref Matrix matrix, out Vector4 result)
        {
            result.x = (vector.x * matrix.m00) + (vector.y * matrix.m10) + (vector.z * matrix.m20) + (vector.w * matrix.m30);
            result.y = (vector.x * matrix.m01) + (vector.y * matrix.m11) + (vector.z * matrix.m21) + (vector.w * matrix.m31);
            result.z = (vector.x * matrix.m02) + (vector.y * matrix.m12) + (vector.z * matrix.m22) + (vector.w * matrix.m32);
            result.w = (vector.x * matrix.m03) + (vector.y * matrix.m13) + (vector.z * matrix.m23) + (vector.w * matrix.m33);
        }

        /// <summary>
        /// Applies a transformation to all vectors within array and places the resuls in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="srcIndex">The starting index in the source array.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        /// <param name="destIndex">The starting index in the destination array.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform(Vector4[] srcArray, ref Matrix matrix, Vector4[] destArray)
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
                Transform(ref srcArray[i], ref matrix, out destArray[i]);
            }
        }
        
        /// <summary>
        /// Applies a transformation to all vectors within array and places the resuls in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="srcIndex">The starting index in the source array.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        /// <param name="destIndex">The starting index in the destination array.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform(Vector4[] srcArray, int srcIndex, ref Matrix matrix, Vector4[] destArray, int destIndex, int length)
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
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            }
            if (destArray.Length < destIndex + length)
            {
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");
            }

            for (int i = 0; i < length; i++)
            {
                Transform(ref srcArray[srcIndex + i], ref matrix, out destArray[destIndex + i]);
            }
        }
        
        /// <summary>
        /// Compares whether current instance is equal to a specified vector.
        /// </summary>
        /// <param name="other">The vector to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Vector4 other)
        {
            return
                x == other.x &&
                y == other.y &&
                z == other.z &&
                w == other.w;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Vector4) && Equals((Vector4)obj);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ z.GetHashCode();
                hashCode = (hashCode * 397) ^ w.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this instance.
        /// </summary>
        public override string ToString()
        {
            const string format = "F2";
            return $"({x.ToString(format)}, {y.ToString(format)}, {z.ToString(format)}, {w.ToString(format)})";
        }

        /// <summary>
        /// Adds the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            left.x += right.x;
            left.y += right.y;
            left.z += right.z;
            left.w += right.w;
            return left;
        }

        /// <summary>
        /// Subtracts the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of the vector subtraction.</returns>
        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            left.x -= right.x;
            left.y -= right.y;
            left.z -= right.z;
            left.w -= right.w;
            return left;
        }

        /// <summary>
        /// Negates the specified vector.
        /// </summary>
        /// <param name="vector">Operand.</param>
        /// <returns>Result of the negation.</returns>
        public static Vector4 operator -(Vector4 vector)
        {
            vector.x = -vector.x;
            vector.y = -vector.y;
            vector.z = -vector.z;
            vector.w = -vector.w;
            return vector;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector4 operator *(Vector4 vector, float scale)
        {
            vector.x *= scale;
            vector.y *= scale;
            vector.z *= scale;
            vector.w *= scale;
            return vector;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="vector">Right operand.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector4 operator *(float scale, Vector4 vector)
        {
            vector.x *= scale;
            vector.y *= scale;
            vector.z *= scale;
            vector.w *= scale;
            return vector;
        }

        /// <summary>
        /// Component-wise multiplication of two vectors.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the vector multiplication.</returns>
        public static Vector4 operator *(Vector4 vector, Vector4 scale)
        {
            vector.x *= scale.x;
            vector.y *= scale.y;
            vector.z *= scale.z;
            vector.w *= scale.w;
            return vector;
        }

        /// <summary>
        /// Divides the components of a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="divider">Right operand.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        public static Vector4 operator /(Vector4 vector, float divider)
        {
            float factor = 1f / divider;
            vector.x *= factor;
            vector.y *= factor;
            vector.z *= factor;
            vector.w *= factor;
            return vector;
        }

        /// <summary>
        /// Component-wise division of one vector by another.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static Vector4 operator /(Vector4 vector, Vector4 scale)
        {
            vector.x /= scale.x;
            vector.y /= scale.y;
            vector.z /= scale.z;
            vector.w /= scale.w;
            return vector;
        }

        /// <summary>
        /// Compares whether two vectors are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Vector4 left, Vector4 right)
        {
            return
                left.x == right.x &&
                left.y == right.y &&
                left.z == right.z &&
                left.w == right.w;
        }

        /// <summary>
        /// Compares whether two vectors are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are not equal, <c>false</c> otherwise.</returns>	
        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return
                left.x != right.x ||
                left.y != right.y ||
                left.z != right.z ||
                left.w != right.w;
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static implicit operator Vector2(Vector4 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static implicit operator Vector3(Vector4 vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }

        /// <summary>
        /// Cast the vector to a <see cref="Color"/> by mapping xyzw to rgba.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static implicit operator Color(Vector4 vector)
        {
            return new Color(vector.x, vector.y, vector.z, vector.w);
        }

        /// <summary>
        /// Cast the vector to a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static explicit operator Quaternion(Vector4 vector)
        {
            return new Quaternion(vector.x, vector.y, vector.z, vector.w);
        }

        /// <summary>
        /// Cast the vector to a <see cref="OpenTK.Vector4"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator OpenTK.Vector4(Vector4 vector)
        {
            return Unsafe.ReinterpretCast<Vector4, OpenTK.Vector4>(vector);
        }
    }
}
