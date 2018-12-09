/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Runtime.InteropServices;

namespace Engine
{
    /// <summary>
    /// Describes a ray in 3d space.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Ray : IEquatable<Ray>
    {
        private Vector3 m_origin;
        private Vector3 m_direction;

        /// <summary>
        /// The origin of the ray.
        /// </summary>
        public Vector3 Origin
        {
            get => m_origin;
            set => m_origin = value;
        }

        /// <summary>
        /// The direction of the ray.
        /// </summary>
        public Vector3 Direction
        {
            get => m_direction;
            set => m_direction = value.Normalized;
        }

        /// <summary>
        /// Creates a new ray instance.
        /// </summary>
        /// <param name="origin">The origin of the ray.</param>
        /// <param name="direction">The direction of the ray.</param>
        public Ray(Vector3 origin, Vector3 direction)
        {
            m_origin = origin;
            m_direction = direction.Normalized;
        }
        
        /// <summary>
        /// Gets the point at distance <paramref name="t"/> from the ray origin.
        /// </summary>
        /// <param name="t">The distance along the ray.</param>
        public Vector3 GetPoint(float t)
        {
            return m_origin + (m_direction * t);
        }

        /*
        // adapted from http://www.scratchapixel.com/lessons/3d-basic-lessons/lesson-7-intersecting-simple-shapes/ray-box-intersection/
        public float? Intersects(BoundingBox box)
        {
            const float Epsilon = 1e-6f;

            float? tMin = null, tMax = null;

            if (Math.Abs(m_direction.X) < Epsilon)
            {
                if (m_origin.X < box.Min.X || m_origin.X > box.Max.X)
                    return null;
            }
            else
            {
                tMin = (box.Min.X - m_origin.X) / m_direction.X;
                tMax = (box.Max.X - m_origin.X) / m_direction.X;

                if (tMin > tMax)
                {
                    var temp = tMin;
                    tMin = tMax;
                    tMax = temp;
                }
            }

            if (Math.Abs(m_direction.Y) < Epsilon)
            {
                if (m_origin.Y < box.Min.Y || m_origin.Y > box.Max.Y)
                    return null;
            }
            else
            {
                var tMinY = (box.Min.Y - m_origin.Y) / m_direction.Y;
                var tMaxY = (box.Max.Y - m_origin.Y) / m_direction.Y;

                if (tMinY > tMaxY)
                {
                    var temp = tMinY;
                    tMinY = tMaxY;
                    tMaxY = temp;
                }

                if ((tMin.HasValue && tMin > tMaxY) || (tMax.HasValue && tMinY > tMax))
                    return null;

                if (!tMin.HasValue || tMinY > tMin) tMin = tMinY;
                if (!tMax.HasValue || tMaxY < tMax) tMax = tMaxY;
            }

            if (Math.Abs(m_direction.Z) < Epsilon)
            {
                if (m_origin.Z < box.Min.Z || m_origin.Z > box.Max.Z)
                    return null;
            }
            else
            {
                var tMinZ = (box.Min.Z - m_origin.Z) / m_direction.Z;
                var tMaxZ = (box.Max.Z - m_origin.Z) / m_direction.Z;

                if (tMinZ > tMaxZ)
                {
                    var temp = tMinZ;
                    tMinZ = tMaxZ;
                    tMaxZ = temp;
                }

                if ((tMin.HasValue && tMin > tMaxZ) || (tMax.HasValue && tMinZ > tMax))
                    return null;

                if (!tMin.HasValue || tMinZ > tMin) tMin = tMinZ;
                if (!tMax.HasValue || tMaxZ < tMax) tMax = tMaxZ;
            }

            // having a positive tMin and a negative tMax means the ray is inside the box
            // we expect the intesection distance to be 0 in that case
            if ((tMin.HasValue && tMin < 0) && tMax > 0) return 0;

            // a negative tMin means that the intersection point is behind the ray's origin
            // we discard these as not hitting the AABB
            if (tMin < 0) return null;

            return tMin;
        }


        public void Intersects(ref BoundingBox box, out float? result)
        {
			result = Intersects(box);
        }
        
   //     public float? Intersects(BoundingFrustum frustum)
   //     {
   //         if (frustum == null)
			//{
			//	throw new ArgumentNullException("frustum");
			//}
			
			//return frustum.Intersects(this);			
   //     }

        public float? Intersects(BoundingSphere sphere)
        {
            float? result;
            Intersects(ref sphere, out result);
            return result;
        }

        public float? Intersects(Plane plane)
        {
            float? result;
            Intersects(ref plane, out result);
            return result;
        }

        public void Intersects(ref Plane plane, out float? result)
        {
            var den = Vector3.Dot(m_direction, plane.Normal);
            if (Math.Abs(den) < 0.00001f)
            {
                result = null;
                return;
            }

            result = (-plane.D - Vector3.Dot(plane.Normal, m_origin)) / den;

            if (result < 0.0f)
            {
                if (result < -0.00001f)
                {
                    result = null;
                    return;
                }

                result = 0.0f;
            }
        }

        public void Intersects(ref BoundingSphere sphere, out float? result)
        {
            // Find the vector between where the ray starts the the sphere's centre
            Vector3 difference = sphere.Center - this.m_origin;

            float differenceLengthSquared = difference.LengthSquared();
            float sphereRadiusSquared = sphere.Radius * sphere.Radius;

            float distanceAlongRay;

            // If the distance between the ray start and the sphere's centre is less than
            // the radius of the sphere, it means we've intersected. N.B. checking the LengthSquared is faster.
            if (differenceLengthSquared < sphereRadiusSquared)
            {
                result = 0.0f;
                return;
            }

            Vector3.Dot(ref this.m_direction, ref difference, out distanceAlongRay);
            // If the ray is pointing away from the sphere then we don't ever intersect
            if (distanceAlongRay < 0)
            {
                result = null;
                return;
            }

            // Next we kinda use Pythagoras to check if we are within the bounds of the sphere
            // if x = radius of sphere
            // if y = distance between ray position and sphere centre
            // if z = the distance we've travelled along the ray
            // if x^2 + z^2 - y^2 < 0, we do not intersect
            float dist = sphereRadiusSquared + distanceAlongRay * distanceAlongRay - differenceLengthSquared;

            result = (dist < 0) ? null : distanceAlongRay - (float?)Math.Sqrt(dist);
        }
        */

        /// <summary>
        /// Compares whether this instance is equal to another.
        /// </summary>
        /// <param name="other">The ray to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Ray other)
        {
            return m_origin == other.m_origin && m_direction == other.m_direction;
        }
        
        /// <summary>
        /// Compares whether this instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Ray) && Equals((Ray)obj);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                return (m_origin.GetHashCode() * 397) ^ m_direction.GetHashCode();
            }
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this instance.
        /// </summary>
        public override string ToString()
        {
            return $"{{origin:{m_origin} + direction:{m_direction}}}";
        }

        /// <summary>
        /// Compares whether two instance are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Ray left, Ray right) => left.Equals(right);

        /// <summary>
        /// Compares whether two instance are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are not equal, <c>false</c> otherwise.</returns>	
        public static bool operator !=(Ray left, Ray right) => !left.Equals(right);
    }
}
