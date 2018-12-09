/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Runtime.InteropServices;

namespace Engine
{
    /// <summary>
    /// Describes an integer 2d vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector2Int : IEquatable<Vector2Int>
    {
        /// <summary>
        /// Returns a vector with components (0, 0).
        /// </summary>
        public static readonly Vector2Int Zero = new Vector2Int(0, 0);

        /// <summary>
        /// Returns a vector with components (1, 1).
        /// </summary>
        public static readonly Vector2Int One = new Vector2Int(1, 1);

        /// <summary>
        /// Returns a vector with components (1, 0).
        /// </summary>
        public static readonly Vector2Int UnitX = new Vector2Int(1, 0);

        /// <summary>
        /// Returns a vector with components (0, 1).
        /// </summary>
        public static readonly Vector2Int UnitY = new Vector2Int(0, 1);
        
        /// <summary>
        /// The x coordinate.
        /// </summary>
        public int x;

        /// <summary>
        /// The y coordinate.
        /// </summary>
        public int y;

        /// <summary>
        /// Constructs a 2d vector from two values.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Constructs a 2d vector with X and Y set to the same value.
        /// </summary>
        /// <param name="value">The x and y coordinates.</param>
        public Vector2Int(int value)
        {
            x = value;
            y = value;
        }
        
        /// <summary>
        /// Gets or sets the value at an index of the vector.
        /// </summary>
        /// <param name="index">The index of the component from the vector.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or greater than 1.</exception>
        public int this[int index]
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
        /// Returns the squared length of this vector.
        /// </summary>
        public float LengthSquared => (x * x) + (y * y);

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector2Int ComponentMin(Vector2Int a, Vector2Int b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            return a;
        }
        
        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector2Int ComponentMax(Vector2Int a, Vector2Int b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            return a;
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector2Int ComponentClamp(Vector2Int vector, Vector2Int min, Vector2Int max)
        {
            return new Vector2Int(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y));
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static float Distance(Vector2Int a, Vector2Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            return Mathf.Sqrt((dx * dx) + (dy * dy));
        }
        
        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static float DistanceSquared(Vector2Int a, Vector2Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            return (dx * dx) + (dy * dy);
        }

        /// <summary>
        /// Returns an integer vector from the <see cref="Mathf.CeilToInt(float)"> of each component.
        /// </summary>
        /// <param name="vector">The vector to ceil.</param>
        public static Vector2Int Ceil(Vector2 vector)
        {
            return new Vector2Int(
                Mathf.CeilToInt(vector.x),
                Mathf.CeilToInt(vector.y));
        }

        /// <summary>
        /// Returns an integer vector with the <see cref="Mathf.FloorToInt(float)"> of each component.
        /// </summary>
        /// <param name="vector">The vector to floor.</param>
        public static Vector2Int Floor(Vector2 vector)
        {
            return new Vector2Int(
                Mathf.FloorToInt(vector.x),
                Mathf.FloorToInt(vector.y));
        }

        /// <summary>
        /// Returns an integer vector with the <see cref="Mathf.RoundToInt(float)"> of each component.
        /// </summary>
        /// <param name="vector">The vector to round.</param>
        public static Vector2Int Round(Vector2 vector)
        {
            return new Vector2Int(
                Mathf.RoundToInt(vector.x),
                Mathf.RoundToInt(vector.y));
        }

        /// <summary>
        /// Compares whether current instance is equal to a specified vector.
        /// </summary>
        /// <param name="other">The vector to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Vector2Int other)
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
            return (obj is Vector2Int) && Equals((Vector2Int)obj);
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
            return $"({x}, {y})";
        }

        /// <summary>
        /// Adds the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Vector2Int operator +(Vector2Int left, Vector2Int right)
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
        public static Vector2Int operator -(Vector2Int left, Vector2Int right)
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
        public static Vector2Int operator -(Vector2Int vector)
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
        public static Vector2Int operator *(Vector2Int vector, int scale)
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
        public static Vector2Int operator *(int scale, Vector2Int vector)
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
        public static Vector2Int operator *(Vector2Int vector, Vector2Int scale)
        {
            vector.x *= scale.x;
            vector.y *= scale.y;
            return vector;
        }
        
        /// <summary>
        /// Compares whether two vectors are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Vector2Int left, Vector2Int right)
        {
            return left.x == right.x && left.y == right.y;
        }

        /// <summary>
        /// Compares whether two vectors are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are not equal, <c>false</c> otherwise.</returns>	
        public static bool operator !=(Vector2Int left, Vector2Int right)
        {
            return left.x != right.x || left.y != right.y;
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static explicit operator Vector2(Vector2Int vector)
        {
            return new Vector2(vector.x, vector.y);
        }
    }
}
