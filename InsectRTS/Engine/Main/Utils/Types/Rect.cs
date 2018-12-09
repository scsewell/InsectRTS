/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Engine
{
    /// <summary>
    /// Describes a 2d rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Rect : IEquatable<Rect>
    {
        /// <summary>
        /// The rect at (0, 0) with size (0, 0).
        /// </summary>
        public static Rect Zero => new Rect(0f, 0f, 0f, 0f);

        /// <summary>
        /// The x coordinate of the top-left corner.
        /// </summary>
        public float x;

        /// <summary>
        /// The y coordinate of the top-left corner.
        /// </summary>
        public float y;

        /// <summary>
        /// The width.
        /// </summary>
        public float width;

        /// <summary>
        /// The height.
        /// </summary>
        public float height;

        /// <summary>
        /// The x coordinate of the left edge.
        /// </summary>
        public float xMin
        {
            get => Math.Min(x, x + width);
            set
            {
                float previous = xMax;
                x = value;
                width = previous - x;
            }
        }

        /// <summary>
        /// The y coordinate of the top edge.
        /// </summary>
        public float yMin
        {
            get => Math.Min(y, y + height);
            set
            {
                float previous = yMax;
                y = value;
                height = previous - y;
            }
        }

        /// <summary>
        /// The x coordinate of the right edge.
        /// </summary>
        public float xMax
        {
            get => Math.Max(x, x + width);
            set => width = value - xMin;
        }

        /// <summary>
        /// The y coordinate of the bottom edge.
        /// </summary>
        public float yMax
        {
            get => Math.Max(y, y + height);
            set => height = value - yMin;
        }

        /// <summary>
        /// The top-left corner.
        /// </summary>
        public Vector2 Min
        {
            get => new Vector2(xMin, yMin);
            set
            {
                xMin = value.x;
                yMin = value.y;
            }
        }

        /// <summary>
        /// The bottom-right corner.
        /// </summary>
        public Vector2 Max
        {
            get => new Vector2(xMax, yMax);
            set
            {
                xMax = value.x;
                yMax = value.y;
            }
        }

        /// <summary>
        /// The top-left corner.
        /// </summary>
        public Vector2 Position
        {
            get => Min;
            set => Min = value;
        }

        /// <summary>
        /// The width and height.
        /// </summary>
        public Vector2 Size
        {
            get => new Vector2(width, height);
            set
            {
                width = value.x;
                height = value.y;
            }
        }

        /// <summary>
        /// Gets the center of this rect.
        /// </summary>
        public Vector2 Center => new Vector2(x + (width / 2f), y + (height / 2f));

        /// <summary>
        /// Gets an enumerator that iterates over the corners of this rect.
        /// </summary>
        public CornerEnumerator Corners => new CornerEnumerator(Min, Max);

        public struct CornerEnumerator : IEnumerator<Vector2>
        {
            private readonly Vector2 m_min;
            private readonly Vector2 m_max;
            private Vector2 m_current;
            private float m_index;

            public Vector2 Current => m_current;
            object IEnumerator.Current => Current;

            public CornerEnumerator(Vector2 min, Vector2 max)
            {
                m_min = min;
                m_max = max;

                m_current = m_min;
                m_index = -1;
            }

            public CornerEnumerator GetEnumerator() => this;

            public bool MoveNext()
            {
                m_index++;
                switch (m_index)
                {
                    case 0:
                        return true;
                    case 1:
                        m_current.x = m_max.x;
                        return true;
                    case 2:
                        m_current.y = m_max.y;
                        return true;
                    case 3:
                        m_current.x = m_min.x;
                        return true;
                }
                return false;
            }

            public void Reset()
            {
                m_current = m_min;
                m_index = -1;
            }

            public void Dispose() { }
        }

        /// <summary>
        /// Creates a new rect the specified position, width, and height.
        /// </summary>
        /// <param name="x">The x coordinate of the top-left corner.</param>
        /// <param name="y">The y coordinate of the top-left corner./param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Rect(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Creates a new rect the specified position and size.
        /// </summary>
        /// <param name="position">The x and y coordinates of the top-left corner.</param>
        /// <param name="size">The width and height.</param>
        public Rect(Vector2 position, Vector2 size)
        {
            x = position.x;
            y = position.y;
            width = size.x;
            height = size.y;
        }

        /// <summary>
        /// Adjusts the edges of this rect. Positive values expand the edges outwards.
        /// </summary>
        /// <param name="amount">Amount to move out the edges.</param>
        public void Inflate(float amount)
        {
            Inflate(amount, amount);
        }

        /// <summary>
        /// Adjusts the edges of this rect. Positive values expand the edges outwards.
        /// </summary>
        /// <param name="x">Amount to move out the left and right edges.</param>
        /// <param name="y">Amount to move out the top and bottom edges.</param>
        public void Inflate(float x, float y)
        {
            xMin -= x;
            yMin -= y;
            xMax += x;
            yMax += y;
        }

        /// <summary>
        /// Gets whether or not a point lies within the bounds of this rect.
        /// </summary>
        /// <param name="point">The point to check.</param>
        public bool Contains(Vector2 point)
        {
            Contains(ref point, out bool result);
            return result;
        }

        /// <summary>
        /// Gets whether or not a point lies within the bounds of this rect.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <param name="result">The result as an output parameter.</param>
        public void Contains(ref Vector2 point, out bool result)
        {
            result = xMin <= point.x &&
                     yMin <= point.y &&
                     point.x < xMax &&
                     point.y < yMax;
        }

        /// <summary>
        /// Gets whether or not a rect lies within the bounds of this rect.
        /// </summary>
        /// <param name="other">The rect to check.</param>
        public bool Contains(Rect other)
        {
            Contains(ref other, out bool result);
            return result;
        }

        /// <summary>
        /// Gets whether or not a rect lies within the bounds of this rect.
        /// </summary>
        /// <param name="other">The rect to check.</param>
        /// <param name="result">The result as an output parameter.</param>
        public void Contains(ref Rect other, out bool result)
        {
            result = xMin <= other.xMin &&
                     yMin <= other.yMin &&
                     other.xMax <= xMax &&
                     other.yMax <= yMax;
        }

        /// <summary>
        /// Checks if another rect intersects this rect.
        /// </summary>
        /// <param name="other">The rect to compare.</param>
        public bool Intersects(Rect other)
        {
            Intersects(ref other, out bool result);
            return result;
        }

        /// <summary>
        /// Checks if another rect intersects this rect.
        /// </summary>
        /// <param name="other">The rect to compare.</param>
        /// <param name="result">The result as an output parameter.</param>
        public void Intersects(ref Rect other, out bool result)
        {
            result = other.xMin < xMax &&
                     other.yMin < yMax &&
                     xMin < other.xMax &&
                     yMin < other.yMax;
        }

        /// <summary>
        /// Gets a rect that contains the intersects region of two rects.
        /// </summary>
        /// <param name="a">The first rect.</param>
        /// <param name="b">The second rect.</param>
        public static Rect Intersect(Rect a, Rect b)
        {
            Intersect(ref a, ref b, out Rect rectangle);
            return rectangle;
        }

        /// <summary>
        /// Gets a rect that contains the intersecting region of two rects. Returns 
        /// <see cref="Zero"> if there is no intersection.
        /// </summary>
        /// <param name="a">The first rect.</param>
        /// <param name="b">The second rect.</param>
        /// <param name="result">The intersection as an output parameter.</param>
        public static void Intersect(ref Rect a, ref Rect b, out Rect result)
        {
            if (a.Intersects(b))
            {
                result.x = Math.Max(a.xMin, b.xMin);
                result.y = Math.Max(a.yMin, b.yMin);
                result.width = Math.Min(a.xMax, b.xMax) - result.x;
                result.height = Math.Min(a.yMax, b.yMax) - result.y;
            }
            else
            {
                result = Zero;
            }
        }

        /// <summary>
        /// Gets a rect that completely contains two rects.
        /// </summary>
        /// <param name="a">The first rect.</param>
        /// <param name="b">The second rect.</param>
        public static Rect Union(Rect a, Rect b)
        {
            Union(ref a, ref b, out Rect result);
            return result;
        }

        /// <summary>
        /// Gets a rect that completely contains two rects.
        /// </summary>
        /// <param name="a">The first rect.</param>
        /// <param name="b">The second rect.</param>
        /// <param name="result">The union of the two rectangles as an output parameter.</param>
        public static void Union(ref Rect a, ref Rect b, out Rect result)
        {
            result.x = Math.Min(a.xMin, b.xMin);
            result.y = Math.Min(a.yMin, b.yMin);
            result.width = Math.Max(a.xMax, b.xMax) - result.x;
            result.height = Math.Max(a.yMax, b.yMax) - result.y;
        }

        /// <summary>
        /// Gets a point from normalized coordinates in a rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="normalizedCoords">The normalized coordinates of a point in the rect.</param>
        public static Vector2 NormalizedToPoint(Rect rect, Vector2 normalizedCoords)
        {
            NormalizedToPoint(ref rect, ref normalizedCoords, out normalizedCoords);
            return normalizedCoords;
        }

        /// <summary>
        /// Gets a point from normalized coordinates in a rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="normalizedCoords">The normalized coordinates of a point in the rect.</param>
        /// <param name="result">The point as an output parameter.</param>
        public static void NormalizedToPoint(ref Rect rect, ref Vector2 normalizedCoords, out Vector2 result)
        {
            result.x = Mathf.Lerp(rect.xMin, rect.xMax, normalizedCoords.x);
            result.y = Mathf.Lerp(rect.yMin, rect.yMax, normalizedCoords.y);
        }

        /// <summary>
        /// Gets the normalized coordinates of a point in a rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="point">The point to transform.</param>
        public static Vector2 PointToNormalized(Rect rect, Vector2 point)
        {
            PointToNormalized(ref rect, ref point, out point);
            return point;
        }

        /// <summary>
        /// Gets the normalized coordinates of a point in a rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="point">The point to transform.</param>
        /// <param name="result">The normalized coords as an output parameter.</param>
        public static void PointToNormalized(ref Rect rect, ref Vector2 point, out Vector2 result)
        {
            result.x = Mathf.InverseLerp(rect.xMin, rect.xMax, point.x);
            result.y = Mathf.InverseLerp(rect.yMin, rect.yMax, point.y);
        }
        
        /// <summary>
        /// Compares whether current instance is equal to a specified rect.
        /// </summary>
        /// <param name="other">The rect to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Rect other)
        {
            return
                x == other.x &&
                y == other.y &&
                width == other.width &&
                height == other.height;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Rect) && Equals((Rect)obj);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) + y.GetHashCode();
                hashCode = (hashCode * 397) + width.GetHashCode();
                hashCode = (hashCode * 397) + height.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this instance.
        /// </summary>
        public override string ToString()
        {
            return $"({x.ToString()}, {y.ToString()}, {width.ToString()}, {height.ToString()})";
        }

        /// <summary>
        /// Compares whether two rects are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Rect left, Rect right) => left.Equals(right);

        /// <summary>
        /// Compares whether two rects are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are not equal, <c>false</c> otherwise.</returns>	
        public static bool operator !=(Rect left, Rect right) => !left.Equals(right);
    }
}
