/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Runtime.InteropServices;

namespace Engine
{
    /// <summary>
    /// An efficient mathematical representation for three dimensional rotations.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Quaternion : IEquatable<Quaternion>
    {
        public static readonly Quaternion Identity = new Quaternion(0f, 0f, 0f, 1f);

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
        /// The rotation component.
        /// </summary>
        public float w;

        /// <summary>
        /// Constructs a quaternion with X, Y, Z and W from four values.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The rotation component.</param>
        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a quaternion with X, Y, Z from a <see cref="Vector3"/> and rotation component from a scalar.
        /// </summary>
        /// <param name="axis">The x, y, z coordinates.</param>
        /// <param name="w">The rotation component.</param>
        public Quaternion(Vector3 axis, float w)
        {
            x = axis.x;
            y = axis.y;
            z = axis.z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a quaternion from <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The x, y, z coordinates and the rotation component.</param>
        public Quaternion(Vector4 value)
        {
            x = value.x;
            y = value.y;
            z = value.z;
            w = value.w;
        }

        /// <summary>
        /// Constructs a quaternion from euler angles given in radians.
        /// </summary>
        /// <param name="x">Counterclockwise rotation around X axis in radians.</param>
        /// <param name="y">Counterclockwise rotation around Y axis in radians.</param>
        /// <param name="z">Counterclockwise rotation around Z axis in radians.</param>
        public Quaternion(float x, float y, float z)
        {
            FromEuler(x, y, z, out this);
        }

        /// <summary>
        /// Constructs a quaternion from euler angles given in radians.
        /// </summary>
        /// <param name="eulerAngles">The pitch, yaw, and roll.</param>
        public Quaternion(Vector3 eulerAngles)
        {
            FromEuler(eulerAngles.x, eulerAngles.y, eulerAngles.z, out this);
        }

        /// <summary>
        /// Gets or sets the value at an index of the quaternion.
        /// </summary>
        /// <param name="index">The index of the component from the quaterion.</param>
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
        /// Returns the magnitude of the quaternion.
        /// </summary>
        public float Length => Mathf.Sqrt((x * x) + (y * y) + (z * z) + (w * w));

        /// <summary>
        /// Returns the squared magnitude of the quaternion.
        /// </summary>
        public float LengthSquared => (x * x) + (y * y) + (z * z) + (w * w);

        /// <summary>
        /// Returns a copy of the quaterion scaled to unit length.
        /// </summary>
        public Quaternion Normalized => Normalize(this);

        /// <summary>
        /// Returns the inverse of this quaternion.
        /// </summary>
        public Quaternion Inverse => Invert(this);

        /// <summary>
        /// Convert this instance to an axis-angle representation.
        /// </summary>
        /// <returns>A vector that is the axis-angle representation of this quaternion.</returns>
        public Vector4 ToAxisAngle()
        {
            ToAxisAngle(out Vector3 axis, out float angle);
            return new Vector4(axis, angle);
        }

        /// <summary>
        /// Convert the quaternion to axis angle representation.
        /// </summary>
        /// <param name="axis">The resultant axis.</param>
        /// <param name="angle">The resultant angle in radians.</param>
        public void ToAxisAngle(out Vector3 axis, out float angle)
        {
            Quaternion q = this;
            if (Math.Abs(q.w) > 1f)
            {
                Normalize(ref q, out q);
            }

            angle = 2f * (float)Math.Acos(q.w);

            float len = (float)Math.Sqrt(1f - (q.w * q.w));
            if (len > float.Epsilon)
            {
                axis.x = q.x / len;
                axis.y = q.y / len;
                axis.z = q.z / len;
            }
            else
            {
                axis = Vector3.UnitX;
            }
        }

        /// <summary>
        /// Computes the angle between two quaternions in radians.
        /// </summary>
        /// <param name="a">The first quaternion.</param>
        /// <param name="b">The second quaternion.</param>
        public static float Angle(Quaternion a, Quaternion b)
        {
            Angle(ref a, ref b, out float result);
            return result;
        }

        /// <summary>
        /// Computes the angle between two quaternions in radians.
        /// </summary>
        /// <param name="a">The first quaternion.</param>
        /// <param name="b">The second quaternion.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void Angle(ref Quaternion a, ref Quaternion b, out float result)
        {
            result = Mathf.Acos(Mathf.Min(Mathf.Abs(Dot(a, b)), 1f)) * 2f;
        }

        /// <summary>
        /// Add two quaternions.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        public static Quaternion Add(Quaternion left, Quaternion right)
        {
            Add(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Add two quaternions.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <param name="result">The result of the addition.</param>
        public static void Add(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result.x = left.x + right.x;
            result.y = left.y + right.y;
            result.z = left.z + right.z;
            result.w = left.w + right.w;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        public static Quaternion Subtract(Quaternion left, Quaternion right)
        {
            Subtract(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <param name="result">The result of the operation.</param>
        public static void Subtract(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result.x = left.x - right.x;
            result.y = left.y - right.y;
            result.z = left.z - right.z;
            result.w = left.w - right.w;
        }

        /// <summary>
        /// Multiplies two quaternions.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        public static Quaternion Multiply(Quaternion left, Quaternion right)
        {
            Multiply(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Multiplies two quaternions.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        public static void Multiply(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            float x1 = left.x;
            float y1 = left.y;
            float z1 = left.z;
            float w1 = left.w;

            float x2 = right.x;
            float y2 = right.y;
            float z2 = right.z;
            float w2 = right.w;

            result.x = (x2 * w1) + (x1 * w2) + (y2 * z1) - (z2 * y1);
            result.y = (y2 * w1) + (y1 * w2) + (z2 * x1) - (x2 * z1);
            result.z = (z2 * w1) + (z1 * w2) + (x2 * y1) - (y2 * x1);
            result.w = (w2 * w1) - (x2 * x1) - (y2 * y1) - (z2 * z1);
        }

        /// <summary>
        /// Multiplies a quaterion and a scalar.
        /// </summary>
        /// <param name="q">The quaterion to scale.</param>
        /// <param name="scale">A scalar multiplier.</param>
        public static Quaternion Multiply(Quaternion q, float scale)
        {
            Multiply(ref q, scale, out q);
            return q;
        }

        /// <summary>
        /// Multiplies a quaterion and a scalar.
        /// </summary>
        /// <param name="q">The quaterion to scale.</param>
        /// <param name="scale">A scalar multiplier.</param>
        /// <param name="result">The result of the quaternion multiplication with a scalar as an output parameter.</param>
        public static void Multiply(ref Quaternion q, float scale, out Quaternion result)
        {
            result.x = q.x * scale;
            result.y = q.y * scale;
            result.z = q.z * scale;
            result.w = q.w * scale;
        }

        /// <summary>
        /// Computes the dot product of two quaternions.
        /// </summary>
        /// <param name="a">The left instance.</param>
        /// <param name="b">The right instance.</param>
        public static float Dot(Quaternion a, Quaternion b)
        {
            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w);
        }

        /// <summary>
        /// Computes the dot product of two quaternions.
        /// </summary>
        /// <param name="a">The left instance.</param>
        /// <param name="b">The right instance.</param>
        /// <param name="result">The dot product of two quaternions as an output parameter.</param>
        public static void Dot(ref Quaternion a, ref Quaternion b, out float result)
        {
            result = (a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w);
        }

        /// <summary>
        /// Scale a quaternion to unit length.
        /// </summary>
        /// <param name="q">The quaternion to normalize.</param>
        public static Quaternion Normalize(Quaternion q)
        {
            Normalize(ref q, out q);
            return q;
        }

        /// <summary>
        /// Scale a quaternion to unit length.
        /// </summary>
        /// <param name="q">The quaternion to normalize.</param>
        /// <param name="result">The normalized quaternion.</param>
        public static void Normalize(ref Quaternion q, out Quaternion result)
        {
            float scale = 1f / q.Length;
            result.x = q.x * scale;
            result.y = q.y * scale;
            result.z = q.z * scale;
            result.w = q.w * scale;
        }

        /// <summary>
        /// Get the conjugate of a quaternion.
        /// </summary>
        /// <param name="q">The quaternion.</param>
        public static Quaternion Conjugate(Quaternion q)
        {
            Conjugate(ref q, out q);
            return q;
        }

        /// <summary>
        /// Get the conjugate of a quaternion.
        /// </summary>
        /// <param name="q">The quaternion.</param>
        /// <param name="result">The conjugate of the given quaternion.</param>
        public static void Conjugate(ref Quaternion q, out Quaternion result)
        {
            result.x = -q.x;
            result.y = -q.y;
            result.z = -q.z;
            result.w = q.w;
        }

        /// <summary>
        /// Get the inverse of a quaternion.
        /// </summary>
        /// <param name="q">The quaternion to invert.</param>
        /// <returns>The inverse of the given quaternion.</returns>
        public static Quaternion Invert(Quaternion q)
        {
            Invert(ref q, out Quaternion result);
            return result;
        }

        /// <summary>
        /// Get the inverse of a quaternion.
        /// </summary>
        /// <param name="q">The quaternion to invert.</param>
        /// <param name="result">The inverse of the given quaternion.</param>
        public static void Invert(ref Quaternion q, out Quaternion result)
        {
            float sqrLen = q.LengthSquared;
            if (sqrLen < float.Epsilon)
            {
                result = Identity;
            }
            else
            {
                float scale = 1f / sqrLen;
                result.x = q.x * -scale;
                result.y = q.y * -scale;
                result.z = q.z * -scale;
                result.w = q.w * scale;
            }
        }

        /// <summary>
        /// Creates a new quaternion from the specified axis and angle.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle in radians.</param>
        public static Quaternion FromAxisAngle(Vector3 axis, float angle)
        {
            FromAxisAngle(ref axis, angle, out Quaternion result);
            return result;
        }

        /// <summary>
        /// Creates a new quaternion from the specified axis and angle.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <param name="result">The new quaternion builded from axis and angle as an output parameter.</param>
        public static void FromAxisAngle(ref Vector3 axis, float angle, out Quaternion result)
        {
            float half = angle * 0.5f;
            float sin = (float)Math.Sin(half);
            float cos = (float)Math.Cos(half);
            result.x = axis.x * sin;
            result.y = axis.y * sin;
            result.z = axis.z * sin;
            result.w = cos;
        }

        /// <summary>
        /// Creates a new quaternion from the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        public static Quaternion FromMatrix(Matrix matrix)
        {
            FromMatrix(ref matrix, out Quaternion result);
            return result;
        }

        /// <summary>
        /// Creates a new quaternion from the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <param name="result">A quaternion composed from the rotation part of the matrix as an output parameter.</param>
        public static void FromMatrix(ref Matrix matrix, out Quaternion result)
        {
            float trace = matrix.m00 + matrix.m11 + matrix.m22;

            if (trace > 0f)
            {
                float s = (float)Math.Sqrt(trace + 1f) * 2f;
                float invS = 1f / s;

                result.x = (matrix.m21 - matrix.m12) * invS;
                result.y = (matrix.m20 - matrix.m02) * invS;
                result.z = (matrix.m10 - matrix.m01) * invS;
                result.w = s * 0.25f;
            }
            else
            {
                if (matrix.m00 >= matrix.m11 && matrix.m00 >= matrix.m22)
                {
                    float s = (float)Math.Sqrt(1f + matrix.m00 - matrix.m11 - matrix.m22) * 2f;
                    float invS = 1f / s;

                    result.x = s * 0.25f;
                    result.y = (matrix.m01 + matrix.m10) * invS;
                    result.z = (matrix.m02 + matrix.m20) * invS;
                    result.w = (matrix.m21 - matrix.m12) * invS;
                }
                else if (matrix.m11 > matrix.m22)
                {
                    float s = (float)Math.Sqrt(1f + matrix.m11 - matrix.m00 - matrix.m22) * 2f;
                    float invS = 1f / s;

                    result.x = (matrix.m01 + matrix.m10) * invS;
                    result.y = s * 0.25f;
                    result.z = (matrix.m12 + matrix.m21) * invS;
                    result.w = (matrix.m02 - matrix.m20) * invS;
                }
                else
                {
                    float s = (float)Math.Sqrt(1f + matrix.m22 - matrix.m00 - matrix.m11) * 2f;
                    float invS = 1f / s;

                    result.x = (matrix.m02 + matrix.m20) * invS;
                    result.y = (matrix.m12 + matrix.m21) * invS;
                    result.z = s * 0.25f;
                    result.w = (matrix.m10 - matrix.m01) * invS;
                }
            }
        }

        /// <summary>
        /// Creates a new quaternion from the specified yaw, pitch and roll angles.
        /// Rotations are applied roll first, pitch second, and yaw third.
        /// </summary>
        /// <param name="yaw">Yaw around the y axis in radians.</param>
        /// <param name="pitch">Pitch around the x axis in radians.</param>
        /// <param name="roll">Roll around the z axis in radians.</param>
        public static Quaternion FromEuler(float yaw, float pitch, float roll)
        {
            FromEuler(yaw, pitch, roll, out Quaternion result);
            return result;
        }

        /// <summary>
        /// Creates a new quaternion from the specified yaw, pitch and roll angles.
        /// Rotations are applied roll first, pitch second, and yaw third.
        /// </summary>
        /// <param name="yaw">Yaw around the y axis in radians.</param>
        /// <param name="pitch">Pitch around the x axis in radians.</param>
        /// <param name="roll">Roll around the z axis in radians.</param>
        /// <param name="result">A new quaternion from the concatenated yaw, pitch, and roll angles as an output parameter.</param>
 		public static void FromEuler(float yaw, float pitch, float roll, out Quaternion result)
        {
            float halfRoll = roll * 0.5f;
            float sr = (float)Math.Sin(halfRoll);
            float cr = (float)Math.Cos(halfRoll);

            float halfPitch = pitch * 0.5f;
            float sp = (float)Math.Sin(halfPitch);
            float cp = (float)Math.Cos(halfPitch);

            float halfYaw = yaw * 0.5f;
            float sy = (float)Math.Sin(halfYaw);
            float cy = (float)Math.Cos(halfYaw);

            result.x = cy * sp * cr + sy * cp * sr;
            result.y = sy * cp * cr - cy * sp * sr;
            result.z = cy * cp * sr - sy * sp * cr;
            result.w = cy * cp * cr + sy * sp * sr;
        }

        /// <summary>
        /// Performs a linear blend between two quaternions.
        /// </summary>
        /// <param name="a">The quaternion to interpolate from.</param>
        /// <param name="b">The quaternion to interpolate to.</param>
        /// <param name="t">The blend amount where 0 returns <paramref name="a"/> and 1 <paramref name="b"/>.</param>
        public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
        {
            Lerp(ref a, ref b, t, out a);
            return a;
        }

        /// <summary>
        /// Performs a linear blend between two quaternions.
        /// </summary>
        /// <param name="a">The quaternion to interpolate from.</param>
        /// <param name="b">The quaternion to interpolate to.</param>
        /// <param name="t">The blend amount where 0 returns <paramref name="a"/> and 1 <paramref name="b"/>.</param>
        /// <param name="result">The result of linear blending between two quaternions as an output parameter.</param>
        public static void Lerp(ref Quaternion a, ref Quaternion b, float t, out Quaternion result)
        {
            Dot(ref a, ref b, out float cosHalfAngle);

            float blendA = 1f - t;
            float blendB = cosHalfAngle < 0f ? -t : t;

            result.x = (blendA * a.x) + (blendB * b.x);
            result.y = (blendA * a.y) + (blendB * b.y);
            result.z = (blendA * a.z) + (blendB * b.z);
            result.w = (blendA * a.w) + (blendB * b.w);

            if (result.LengthSquared < float.Epsilon)
            {
                result = Identity;
                return;
            }

            Normalize(ref result, out result);
        }

        /// <summary>
        /// Performs a spherical linear blend between two quaternions.
        /// </summary>
        /// <param name="a">The quaternion to interpolate from.</param>
        /// <param name="b">The quaternion to interpolate to.</param>
        /// <param name="t">The blend amount where 0 returns <paramref name="a"/> and 1 <paramref name="b"/>.</param>
        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            Slerp(ref a, ref b, t, out a);
            return a;
        }

        /// <summary>
        /// Performs a spherical linear blend between two quaternions.
        /// </summary>
        /// <param name="a">The quaternion to interpolate from.</param>
        /// <param name="b">The quaternion to interpolate to.</param>
        /// <param name="t">The blend amount where 0 returns <paramref name="a"/> and 1 <paramref name="b"/>.</param>
        /// <param name="result">The result of spherical linear blending between two quaternions as an output parameter.</param>
        public static void Slerp(ref Quaternion a, ref Quaternion b, float t, out Quaternion result)
        {
            // if either input is zero, return the other
            if (a.LengthSquared < float.Epsilon)
            {
                if (b.LengthSquared < float.Epsilon)
                {
                    result = Identity;
                    return;
                }
                result = b;
                return;
            }
            if (b.LengthSquared < float.Epsilon)
            {
                result = a;
                return;
            }

            Dot(ref a, ref b, out float cosHalfAngle);

            if (cosHalfAngle <= -1f || 1f <= cosHalfAngle)
            {
                // angle = 0, so just return either input
                result = a;
                return;
            }

            bool conjugate = false;
            if (cosHalfAngle < 0f)
            {
                conjugate = true;
                cosHalfAngle = -cosHalfAngle;
            }

            float blendA;
            float blendB;
            if (cosHalfAngle < 0.99f)
            {
                // do proper slerp for big angles
                float halfAngle = (float)Math.Acos(cosHalfAngle);
                float invSinHalfAngle = 1f / (float)Math.Sin(halfAngle);
                blendA = (float)Math.Sin(halfAngle * (1f - t)) * invSinHalfAngle;
                blendB = (float)Math.Sin(halfAngle * t) * (conjugate ? -invSinHalfAngle : invSinHalfAngle);
            }
            else
            {
                // do lerp if angle is really small
                blendA = 1f - t;
                blendB = conjugate ? -t : t;
            }

            result.x = (blendA * a.x) + (blendB * b.x);
            result.y = (blendA * a.y) + (blendB * b.y);
            result.z = (blendA * a.z) + (blendB * b.z);
            result.w = (blendA * a.w) + (blendB * b.w);

            if (result.LengthSquared < float.Epsilon)
            {
                result = Identity;
                return;
            }

            Normalize(ref result, out result);
        }

        /// <summary>
        /// Rotates a towards b up to some amount.
        /// </summary>
        /// <param name="a">The quaternion to rotate from.</param>
        /// <param name="b">The quaternion to rotate to.</param>
        /// <param name="maxDelta">The maximum change in rotation from a.</param>
        public static Quaternion RotateTowards(Quaternion a, ref Quaternion b, float maxDelta)
        {
            RotateTowards(ref a, ref b, maxDelta, out a);
            return a;
        }

        /// <summary>
        /// Rotates a towards b up to some amount.
        /// </summary>
        /// <param name="a">The quaternion to rotate from.</param>
        /// <param name="b">The quaternion to rotate to.</param>
        /// <param name="maxDelta">The maximum change in rotation from a in radians.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void RotateTowards(ref Quaternion a, ref Quaternion b, float maxDelta, out Quaternion result)
        {
            float angle = Angle(a, b);
            if (angle < float.Epsilon)
            {
                result = b;
            }
            else
            {
                Slerp(ref a, ref b, Mathf.Min(maxDelta / angle, 1f), out result);
            }
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="other">The <see cref="Quaternion"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Quaternion other)
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
            if (obj is Quaternion)
            {
                return Equals((Quaternion)obj);
            }
            return false;
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
            const string format = "N4";
            return $"({x.ToString(format)}, {y.ToString(format)}, {z.ToString(format)}, {w.ToString(format)})";
        }

        /// <summary>
        /// Adds two quaternions.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public static Quaternion operator +(Quaternion left, Quaternion right)
        {
            left.x += right.x;
            left.y += right.y;
            left.z += right.z;
            left.w += right.w;
            return left;
        }

        /// <summary>
        /// Subtracts two quaternions.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public static Quaternion operator -(Quaternion left, Quaternion right)
        {
            left.x -= right.x;
            left.y -= right.y;
            left.z -= right.z;
            left.w -= right.w;
            return left;
        }

        /// <summary>
        /// Multiplies two quaternions.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public static Quaternion operator *(Quaternion left, Quaternion right) => Multiply(left, right);
        
        /// <summary>
        /// Multiplies the components of quaternion by a scalar.
        /// </summary>
        /// <param name="scale">The left operand.</param>
        /// <param name="q">The right operand.</param>
        public static Quaternion operator *(float scale, Quaternion q)
        {
            q.x *= scale;
            q.y *= scale;
            q.z *= scale;
            q.w *= scale;
            return q;
        }

        /// <summary>
        /// Multiplies the components of quaternion by a scalar.
        /// </summary>
        /// <param name="q">The left operand.</param>
        /// <param name="scale">The right operand.</param>
        public static Quaternion operator *(Quaternion q, float scale)
        {
            q.x *= scale;
            q.y *= scale;
            q.z *= scale;
            q.w *= scale;
            return q;
        }

        /// <summary>
        /// Compares two instances for equality.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True, if left equals right, false otherwise.</returns>
        public static bool operator ==(Quaternion left, Quaternion right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances for inequality.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True, if left does not equal right, false otherwise.</returns>
        public static bool operator !=(Quaternion left, Quaternion right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Cast the quaterion to a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="q">The quaterion to cast.</param>
        public static explicit operator Vector4(Quaternion q)
        {
            return new Vector4(q.x, q.y, q.z, q.w);
        }
    }
}
