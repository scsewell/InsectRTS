/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Runtime.InteropServices;

namespace Engine
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Plane : IEquatable<Plane>
    {
        /// <summary>
        /// The distance of the plane from the origin.
        /// </summary>
        public float distance;

        /// <summary>
        /// The normal of the plane.
        /// </summary>
        public Vector3 normal;

        /// <summary>
        /// Returns a flipped copy of this plane.
        /// </summary>
        public Plane Flipped => new Plane(-normal, -distance);

        /// <summary>
        /// Constructs a new plane from a vector.
        /// </summary>
        /// <param name="plane">The normal in the XYZ components and the disance in the W component.</param>
        public Plane(Vector4 plane) : this(new Vector3(plane.x, plane.y, plane.z), plane.w)
        {
        }

        /// <summary>
        /// Constructs a new plane.
        /// </summary>
        /// <param name="normal">The plane normal.</param>
        /// <param name="distance">The plane distance.</param>
        public Plane(Vector3 normal, float distance)
        {
            Vector3.Normalize(ref normal, out this.normal);
            this.distance = distance;
        }

        /// <summary>
        /// Constructs a new plane from a normal and a point on the plane.
        /// </summary>
        /// <param name="normal">The plane normal.</param>
        /// <param name="point">A point on the plane.</param
        public Plane(Vector3 normal, Vector3 point)
        {
            Vector3.Normalize(ref normal, out this.normal);
            distance = -Vector3.Dot(this.normal, point);
        }

        /// <summary>
        /// Constructs a new plane which contains three given points.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <param name="c">The third point.</param>
        public Plane(Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 cross = Vector3.Cross(b - a, c - a);
            Vector3.Normalize(ref cross, out normal);
            distance = -Vector3.Dot(normal, a);
        }

        /// <summary>
        /// Flips the plane's normal.
        /// </summary>
        public void Flip()
        {
            normal = -normal;
            distance = -distance;
        }

        /// <summary>
        /// Gets the distance from the plane to a point along the plane's normal.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The distance from the plane.</returns>
        public float DistanceToPoint(Vector3 point)
        {
            DistanceToPoint(ref point, out float result);
            return result;
        }

        /// <summary>
        /// Gets the distance from the plane to a point along the plane's normal.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="result">The result as an out parameter.</param>
        public void DistanceToPoint(ref Vector3 point, out float result)
        {
            Vector3.Dot(ref normal, ref point, out result);
            result += distance;
        }

        /// <summary>
        /// Gets the point on the plane closest to a given point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The closest point on the plane.</returns>
        public Vector3 ClosestPointOnPlane(Vector3 point)
        {
            ClosestPointOnPlane(ref point, out point);
            return point;
        }

        /// <summary>
        /// Gets the point on the plane closest to a given point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="result">The result as an out parameter.</param>
        public void ClosestPointOnPlane(ref Vector3 point, out Vector3 result)
        {
            DistanceToPoint(ref point, out float pointDistance);
            result = point - (pointDistance * normal);
        }

        /// <summary>
        /// Returns if a point is on the positive side of the plane (the side the normal is pointing along).
        /// </summary>
        /// <param name="point">The point to classify.</param>
        /// <returns>True if the point is on the positive side, false otherwise.</returns>
        public bool ClassifyPoint(Vector3 point)
        {
            ClassifyPoint(ref point, out bool result);
            return result;
        }

        /// <summary>
        /// Returns if a point is on the positive side of the plane (the side the normal is pointing along).
        /// </summary>
        /// <param name="point">The point to classify.</param>
        /// <param name="result">The result as an out parameter.</param>
        public void ClassifyPoint(ref Vector3 point, out bool result)
        {
            DistanceToPoint(ref point, out float pointDistance);
            result = pointDistance > 0f;
        }

        /// <summary>
        /// Checks if two points are on the same side of the plane.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>True if the points are on the same side of the plane.</returns>
        public bool SameSide(Vector3 a, Vector3 b)
        {
            SameSide(ref a, ref b, out bool result);
            return result;
        }

        /// <summary>
        /// Checks if two points are on the same side of the plane.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <param name="result">The result as an out parameter.</param>
        public void SameSide(ref Vector3 a, ref Vector3 b, out bool result)
        {
            ClassifyPoint(ref a, out bool aSide);
            ClassifyPoint(ref b, out bool bSide);
            result = aSide == bSide;
        }

        /// <summary>
        /// Raycasts this plane.
        /// </summary>
        /// <param name="ray">The ray to check.</param>
        /// <param name="hitDistance">The distance of the hit from the ray origin.</param>
        /// <returns>True if the hit is in front of the ray, otherwise false.</returns>
        public bool Raycast(Ray ray, out float hitDistance)
        {
            return Raycast(ref ray, out hitDistance);
        }

        /// <summary>
        /// Raycasts this plane.
        /// </summary>
        /// <param name="ray">The ray to check.</param>
        /// <param name="hitDistance">The distance of the hit from the ray origin.</param>
        /// <returns>True if the hit is in front of the ray, otherwise false.</returns>
        public bool Raycast(ref Ray ray, out float hitDistance)
        {
            Vector3.Dot(ref normal, ref ray.direction, out float dirDot);

            if (Mathf.Abs(dirDot) < float.Epsilon)
            {
                hitDistance = 0f;
                return false;
            }

            Vector3.Dot(ref normal, ref ray.origin, out float originDot);
            originDot = -originDot - distance;

            hitDistance = originDot / dirDot;
            return hitDistance > 0f;
        }
        
        /// <summary>
        /// Compares whether the current instance is equal to a specified vector.
        /// </summary>
        /// <param name="other">The vector to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Plane other)
        {
            return ((normal == other.normal) && (distance == other.distance));
        }

        /// <summary>
        /// Compares whether the current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Plane) && Equals((Plane)obj);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                return (normal.GetHashCode() * 397) ^ distance.GetHashCode();
            }
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this instance.
        /// </summary>
        public override string ToString()
        {
            return $"{{normal:{normal} distance:{distance}}}";
        }

        /// <summary>
        /// Compares whether two instances are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Plane left, Plane right) => left.Equals(right);

        /// <summary>
        /// Compares whether two instances are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are not equal, <c>false</c> otherwise.</returns>
        public static bool operator !=(Plane left, Plane right) => !left.Equals(right);

        /// <summary>
        /// Casts a plane to a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="plane">The plane to cast.</param>
        public static explicit operator Vector4(Plane plane)
        {
            return new Vector4(plane.normal, plane.distance);
        }
    }
}
