﻿/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

/*
namespace Engine
{
	internal class PlaneHelper
    {
        /// <summary>
        /// Returns a value indicating what side (positive/negative) of a plane a point is
        /// </summary>
        /// <param name="point">The point to check with</param>
        /// <param name="plane">The plane to check against</param>
        /// <returns>Greater than zero if on the positive side, less than zero if on the negative size, 0 otherwise</returns>
        public static float ClassifyPoint(ref Vector3 point, ref Plane plane)
        {
            return point.x * plane.Normal.x + point.y * plane.Normal.y + point.z * plane.Normal.z + plane.D;
        }

        /// <summary>
        /// Returns the perpendicular distance from a point to a plane
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <param name="plane">The place to check</param>
        /// <returns>The perpendicular distance from the point to the plane</returns>
        public static float PerpendicularDistance(ref Vector3 point, ref Plane plane)
        {
            // dist = (ax + by + cz + d) / sqrt(a*a + b*b + c*c)
            return (float)Math.Abs((plane.Normal.x * point.x + plane.Normal.y * point.y + plane.Normal.z * point.z)
                                    / Math.Sqrt(plane.Normal.x * plane.Normal.x + plane.Normal.y * plane.Normal.y + plane.Normal.z * plane.Normal.z));
        }
    }
	
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct Plane : IEquatable<Plane>
    {
        #region Public Fields

        [DataMember]
        public float D;

        [DataMember]
        public Vector3 Normal;

        #endregion Public Fields


        #region Constructors

        public Plane(Vector4 value)
            : this(new Vector3(value.x, value.y, value.z), value.w)
        {

        }

        public Plane(Vector3 normal, float d)
        {
            Normal = normal;
            D = d;
        }

        public Plane(Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 ab = b - a;
            Vector3 ac = c - a;

            Vector3 cross = Vector3.Cross(ab, ac);
            Vector3.Normalize(ref cross, out Normal);
            D = -(Vector3.Dot(Normal, a));
        }

        public Plane(float a, float b, float c, float d)
            : this(new Vector3(a, b, c), d)
        {

        }

        #endregion Constructors


        #region Public Methods

        public float Dot(Vector4 value)
        {
            return ((((this.Normal.x * value.x) + (this.Normal.y * value.y)) + (this.Normal.z * value.z)) + (this.D * value.w));
        }

        public void Dot(ref Vector4 value, out float result)
        {
            result = (((this.Normal.x * value.x) + (this.Normal.y * value.y)) + (this.Normal.z * value.z)) + (this.D * value.w);
        }

        public float DotCoordinate(Vector3 value)
        {
            return ((((this.Normal.x * value.x) + (this.Normal.y * value.y)) + (this.Normal.z * value.z)) + this.D);
        }

        public void DotCoordinate(ref Vector3 value, out float result)
        {
            result = (((this.Normal.x * value.x) + (this.Normal.y * value.y)) + (this.Normal.z * value.z)) + this.D;
        }

        public float DotNormal(Vector3 value)
        {
            return (((this.Normal.x * value.x) + (this.Normal.y * value.y)) + (this.Normal.z * value.z));
        }

        public void DotNormal(ref Vector3 value, out float result)
        {
            result = ((this.Normal.x * value.x) + (this.Normal.y * value.y)) + (this.Normal.z * value.z);
        }

        /// <summary>
        /// Transforms a normalized plane by a matrix.
        /// </summary>
        /// <param name="plane">The normalized plane to transform.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed plane.</returns>
        public static Plane Transform(Plane plane, Matrix matrix)
        {
            Plane result;
            Transform(ref plane, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Transforms a normalized plane by a matrix.
        /// </summary>
        /// <param name="plane">The normalized plane to transform.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <param name="result">The transformed plane.</param>
        public static void Transform(ref Plane plane, ref Matrix matrix, out Plane result)
        {
            // See "Transforming Normals" in http://www.glprogramming.com/red/appendixf.html
            // for an explanation of how this works.

            Matrix transformedMatrix;
            Matrix.Invert(ref matrix, out transformedMatrix);
            Matrix.Transpose(ref transformedMatrix, out transformedMatrix);

            var vector = new Vector4(plane.Normal, plane.D);

            Vector4 transformedVector;
            Vector4.Transform(ref vector, ref transformedMatrix, out transformedVector);

            result = new Plane(transformedVector);
        }

        /// <summary>
        /// Transforms a normalized plane by a quaternion rotation.
        /// </summary>
        /// <param name="plane">The normalized plane to transform.</param>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <returns>The transformed plane.</returns>
        public static Plane Transform(Plane plane, Quaternion rotation)
        {
            Plane result;
            Transform(ref plane, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Transforms a normalized plane by a quaternion rotation.
        /// </summary>
        /// <param name="plane">The normalized plane to transform.</param>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <param name="result">The transformed plane.</param>
        public static void Transform(ref Plane plane, ref Quaternion rotation, out Plane result)
        {
            Vector3.Transform(ref plane.Normal, ref rotation, out result.Normal);
            result.D = plane.D;
        }

        public void Normalize()
        {
            float length = Normal.Length();
            float factor =  1f / length;            
            Vector3.Multiply(ref Normal, factor, out Normal);
            D = D * factor;
        }

        public static Plane Normalize(Plane value)
        {
			Plane ret;
			Normalize(ref value, out ret);
			return ret;
        }

        public static void Normalize(ref Plane value, out Plane result)
        {
            float length = value.Normal.Length();
            float factor =  1f / length;            
            Vector3.Multiply(ref value.Normal, factor, out result.Normal);
            result.D = value.D * factor;
        }

        public static bool operator !=(Plane plane1, Plane plane2)
        {
            return !plane1.Equals(plane2);
        }

        public static bool operator ==(Plane plane1, Plane plane2)
        {
            return plane1.Equals(plane2);
        }

        public override bool Equals(object other)
        {
            return (other is Plane) ? this.Equals((Plane)other) : false;
        }

        public bool Equals(Plane other)
        {
            return ((Normal == other.Normal) && (D == other.D));
        }

        public override int GetHashCode()
        {
            return Normal.GetHashCode() ^ D.GetHashCode();
        }

        public PlaneIntersectionType Intersects(BoundingBox box)
        {
            return box.Intersects(this);
        }

        public void Intersects(ref BoundingBox box, out PlaneIntersectionType result)
        {
            box.Intersects (ref this, out result);
        }

        public PlaneIntersectionType Intersects(BoundingFrustum frustum)
        {
            return frustum.Intersects(this);
        }

        public PlaneIntersectionType Intersects(BoundingSphere sphere)
        {
            return sphere.Intersects(this);
        }

        public void Intersects(ref BoundingSphere sphere, out PlaneIntersectionType result)
        {
            sphere.Intersects(ref this, out result);
        }

        internal PlaneIntersectionType Intersects(ref Vector3 point)
        {
            float distance;
            DotCoordinate(ref point, out distance);

            if (distance > 0)
                return PlaneIntersectionType.Front;

            if (distance < 0)
                return PlaneIntersectionType.Back;

            return PlaneIntersectionType.Intersecting;
        }

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.Normal.DebugDisplayString, "  ",
                    this.D.ToString()
                    );
            }
        }

        public override string ToString()
        {
            return "{Normal:" + Normal + " D:" + D + "}";
        }

        /// <summary>
        /// Deconstruction method for <see cref="Plane"/>.
        /// </summary>
        /// <param name="normal"></param>
        /// <param name="d"></param>
        public void Deconstruct(out Vector3 normal, out float d)
        {
            normal = Normal;
            d = D;
        }

        #endregion
    }
}
*/