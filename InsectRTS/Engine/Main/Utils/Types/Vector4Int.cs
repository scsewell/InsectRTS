/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Runtime.InteropServices;

namespace Engine
{
    /// <summary>
    /// Describes an integer 4d vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector4Int : IEquatable<Vector4Int>
    {
        /// <summary>
        /// Returns a vector with components (0, 0, 0, 0).
        /// </summary>
        public static readonly Vector4Int Zero = new Vector4Int(0, 0, 0, 0);

        /// <summary>
        /// Returns a vector with components (1, 1, 1, 1).
        /// </summary>
        public static readonly Vector4Int One = new Vector4Int(1, 1, 1, 1);

        /// <summary>
        /// Returns a vector with components (1, 0, 0, 0).
        /// </summary>
        public static readonly Vector4Int UnitX = new Vector4Int(1, 0, 0, 0);

        /// <summary>
        /// Returns a vector with components (0, 1, 0, 0).
        /// </summary>
        public static readonly Vector4Int UnitY = new Vector4Int(0, 1, 0, 0);

        /// <summary>
        /// Returns a vector with components (0, 0, 1, 0).
        /// </summary>
        public static readonly Vector4Int UnitZ = new Vector4Int(0, 0, 1, 0);

        /// <summary>
        /// Returns a vector with components (0, 0, 0, 1).
        /// </summary>
        public static readonly Vector4Int UnitW = new Vector4Int(0, 0, 0, 1);

        /// <summary>
        /// The x coordinate.
        /// </summary>
        public int x;

        /// <summary>
        /// The y coordinate.
        /// </summary>
        public int y;

        /// <summary>
        /// The z coordinate.
        /// </summary>
        public int z;

        /// <summary>
        /// The w coordinate.
        /// </summary>
        public int w;

        /// <summary>
        /// Constructs a 4d vector from four values.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4Int(int x, int y, int z, int w)
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
        public Vector4Int(int value)
        {
            x = value;
            y = value;
            z = value;
            w = value;
        }

        /// <summary>
        /// Constructs a 4d vector with X and Z from a <see cref="Vector2Int"/> and Z and W from scalars.
        /// </summary>
        /// <param name="value">The x and y coordinates.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4Int(Vector2Int value, int z, int w)
        {
            x = value.x;
            y = value.y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a 4d vector from a pair of <see cref="Vector2Int"/>.
        /// </summary>
        /// <param name="xy">The x and y coordinates.</param>
        /// <param name="xy">The z and w coordinates.</param>
        public Vector4Int(Vector2Int xy, Vector2Int zw)
        {
            x = xy.x;
            y = xy.y;
            z = zw.x;
            w = zw.y;
        }

        /// <summary>
        /// Constructs a 4d vector with X, Y, Z from <see cref="Vector3Int"/> and W from a scalar.
        /// </summary>
        /// <param name="value">The x, y and z coordinates.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4Int(Vector3Int value, int w)
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
        public int this[int index]
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
        /// Returns the squared length of this vector.
        /// </summary>
        public float LengthSquared => (x * x) + (y * y) + (z * z) + (w * w);

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector4Int ComponentMin(Vector4Int a, Vector4Int b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            a.z = a.z < b.z ? a.z : b.z;
            a.w = a.w < b.w ? a.w : b.w;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector4Int ComponentMax(Vector4Int a, Vector4Int b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            a.z = a.z > b.z ? a.z : b.z;
            a.w = a.w > b.w ? a.w : b.w;
            return a;
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector4Int ComponentClamp(Vector4Int vector, Vector4Int min, Vector4Int max)
        {
            return new Vector4Int(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y),
                Mathf.Clamp(vector.z, min.z, max.z),
                Mathf.Clamp(vector.w, min.w, max.w));
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static float Distance(Vector4Int a, Vector4Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            int dz = a.z - b.z;
            int dw = a.w - b.w;
            return Mathf.Sqrt((dx * dx) + (dy * dy) + (dz * dz) + (dw * dw));
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static float DistanceSquared(Vector4Int a, Vector4Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            int dz = a.z - b.z;
            int dw = a.w - b.w;
            return (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        /// <summary>
        /// Returns an integer vector from the <see cref="Mathf.CeilToInt(float)"> of each component.
        /// </summary>
        /// <param name="vector">The vector to ceil.</param>
        public static Vector4Int Ceil(Vector4 vector)
        {
            return new Vector4Int(
                Mathf.CeilToInt(vector.x),
                Mathf.CeilToInt(vector.y),
                Mathf.CeilToInt(vector.z),
                Mathf.CeilToInt(vector.w));
        }

        /// <summary>
        /// Returns an integer vector with the <see cref="Mathf.FloorToInt(float)"> of each component.
        /// </summary>
        /// <param name="vector">The vector to floor.</param>
        public static Vector4Int Floor(Vector4 vector)
        {
            return new Vector4Int(
                Mathf.FloorToInt(vector.x),
                Mathf.FloorToInt(vector.y),
                Mathf.FloorToInt(vector.z),
                Mathf.FloorToInt(vector.w));
        }

        /// <summary>
        /// Returns an integer vector with the <see cref="Mathf.RoundToInt(float)"> of each component.
        /// </summary>
        /// <param name="vector">The vector to round.</param>
        public static Vector4Int Round(Vector4 vector)
        {
            return new Vector4Int(
                Mathf.RoundToInt(vector.x),
                Mathf.RoundToInt(vector.y),
                Mathf.RoundToInt(vector.z),
                Mathf.RoundToInt(vector.w));
        }

        /// <summary>
        /// Compares whether current instance is equal to a specified vector.
        /// </summary>
        /// <param name="other">The vector to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Vector4Int other)
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
            return (obj is Vector4Int) && Equals((Vector4Int)obj);
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
            return $"({x}, {y}, {z}, {w})";
        }

        /// <summary>
        /// Adds the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Vector4Int operator +(Vector4Int left, Vector4Int right)
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
        public static Vector4Int operator -(Vector4Int left, Vector4Int right)
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
        public static Vector4Int operator -(Vector4Int vector)
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
        public static Vector4Int operator *(Vector4Int vector, int scale)
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
        public static Vector4Int operator *(int scale, Vector4Int vector)
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
        public static Vector4Int operator *(Vector4Int vector, Vector4Int scale)
        {
            vector.x *= scale.x;
            vector.y *= scale.y;
            vector.z *= scale.z;
            vector.w *= scale.w;
            return vector;
        }

        /// <summary>
        /// Compares whether two vectors are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Vector4Int left, Vector4Int right)
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
        public static bool operator !=(Vector4Int left, Vector4Int right)
        {
            return
                left.x != right.x ||
                left.y != right.y ||
                left.z != right.z ||
                left.w != right.w;
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector2Int"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static implicit operator Vector2Int(Vector4Int vector)
        {
            return new Vector2Int(vector.x, vector.y);
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector3Int"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static implicit operator Vector3Int(Vector4Int vector)
        {
            return new Vector3Int(vector.x, vector.y, vector.z);
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static explicit operator Vector4(Vector4Int vector)
        {
            return new Vector4(vector.x, vector.y, vector.z, vector.w);
        }
    }
}
