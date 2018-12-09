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
    /// Represents the right-handed 4x4 floating point matrix, which can store translation, scale and rotation information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Matrix : IEquatable<Matrix>
    {
        /// <summary>
        /// Returns the zero matrix.
        /// </summary>
        public static readonly Matrix Zero = new Matrix(Vector4.Zero, Vector4.Zero, Vector4.Zero, Vector4.Zero);

        /// <summary>
        /// Returns the identity matrix.
        /// </summary>
        public static readonly Matrix Identity = new Matrix(Vector4.UnitX, Vector4.UnitY, Vector4.UnitZ, Vector4.UnitW);

        /// <summary>
        /// A first row and first column value.
        /// </summary>
        public float m00;
        /// <summary>
        /// A first row and second column value.
        /// </summary>
        public float m01;
        /// <summary>
        /// A first row and third column value.
        /// </summary>
        public float m02;
        /// <summary>
        /// A first row and fourth column value.
        /// </summary>
        public float m03;
        /// <summary>
        /// A second row and first column value.
        /// </summary>
        public float m10;
        /// <summary>
        /// A second row and second column value.
        /// </summary>
        public float m11;
        /// <summary>
        /// A second row and third column value.
        /// </summary>
        public float m12;
        /// <summary>
        /// A second row and fourth column value.
        /// </summary>
        public float m13;
        /// <summary>
        /// A third row and first column value.
        /// </summary>
        public float m20;
        /// <summary>
        /// A third row and second column value.
        /// </summary>
        public float m21;
        /// <summary>
        /// A third row and third column value.
        /// </summary>
        public float m22;
        /// <summary>
        /// A third row and fourth column value.
        /// </summary>
        public float m23;
        /// <summary>
        /// A fourth row and first column value.
        /// </summary>
        public float m30;
        /// <summary>
        /// A fourth row and second column value.
        /// </summary>
        public float m31;
        /// <summary>
        /// A fourth row and third column value.
        /// </summary>
        public float m32;
        /// <summary>
        /// A fourth row and fourth column value.
        /// </summary>
        public float m33;

        /// <summary>
        /// Constructs a matrix.
        /// </summary>
        /// <param name="m00">A first row and first column value.</param>
        /// <param name="m01">A first row and second column value.</param>
        /// <param name="m02">A first row and third column value.</param>
        /// <param name="m03">A first row and fourth column value.</param>
        /// <param name="m10">A second row and first column value.</param>
        /// <param name="m11">A second row and second column value.</param>
        /// <param name="m12">A second row and third column value.</param>
        /// <param name="m13">A second row and fourth column value.</param>
        /// <param name="m20">A third row and first column value.</param>
        /// <param name="m21">A third row and second column value.</param>
        /// <param name="m22">A third row and third column value.</param>
        /// <param name="m23">A third row and fourth column value.</param>
        /// <param name="m30">A fourth row and first column value.</param>
        /// <param name="m31">A fourth row and second column value.</param>
        /// <param name="m32">A fourth row and third column value.</param>
        /// <param name="m33">A fourth row and fourth column value.</param>
        public Matrix(
            float m00, float m01, float m02, float m03,
            float m10, float m11, float m12, float m13,
            float m20, float m21, float m22, float m23,
            float m30, float m31, float m32, float m33
        )
        {
            this.m00 = m00;
            this.m01 = m01;
            this.m02 = m02;
            this.m03 = m03;
            this.m10 = m10;
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m20 = m20;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m30 = m30;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
        }

        /// <summary>
        /// Constructs a matrix.
        /// </summary>
        /// <param name="row0">A first row of the created matrix.</param>
        /// <param name="row1">A second row of the created matrix.</param>
        /// <param name="row2">A third row of the created matrix.</param>
        /// <param name="row3">A fourth row of the created matrix.</param>
        public Matrix(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            m00 = row0.x;
            m01 = row0.y;
            m02 = row0.z;
            m03 = row0.w;

            m10 = row1.x;
            m11 = row1.y;
            m12 = row1.z;
            m13 = row1.w;

            m20 = row2.x;
            m21 = row2.y;
            m22 = row2.z;
            m23 = row2.w;

            m30 = row3.x;
            m31 = row3.y;
            m32 = row3.z;
            m33 = row3.w;
        }

        /// <summary>
        /// Gets or sets the value at an index of the matrix.
        /// </summary>
        /// <param name="index">The index of the matrix element.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or greater than 15.</exception>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return m00;
                    case 1: return m01;
                    case 2: return m02;
                    case 3: return m03;
                    case 4: return m10;
                    case 5: return m11;
                    case 6: return m12;
                    case 7: return m13;
                    case 8: return m20;
                    case 9: return m21;
                    case 10: return m22;
                    case 11: return m23;
                    case 12: return m30;
                    case 13: return m31;
                    case 14: return m32;
                    case 15: return m33;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: m00 = value; break;
                    case 1: m01 = value; break;
                    case 2: m02 = value; break;
                    case 3: m03 = value; break;
                    case 4: m10 = value; break;
                    case 5: m11 = value; break;
                    case 6: m12 = value; break;
                    case 7: m13 = value; break;
                    case 8: m20 = value; break;
                    case 9: m21 = value; break;
                    case 10: m22 = value; break;
                    case 11: m23 = value; break;
                    case 12: m30 = value; break;
                    case 13: m31 = value; break;
                    case 14: m32 = value; break;
                    case 15: m33 = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Gets or sets the value at an row and column of the matrix.
        /// </summary>
        /// <param name="row">The row of the matrix element.</param>
        /// <param name="column">The column of the matrix element.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the row or column is less than 0 or greater than 3.</exception>
        public float this[int row, int column]
        {
            get => this[(row * 4) + column];
            set => this[(row * 4) + column] = value;
        }

        /// <summary>
        /// The first row of this matrix.
        /// </summary>
        public Vector4 Row0
        {
            get => new Vector4(m00, m01, m02, m03);
            set
            {
                m00 = value.x;
                m01 = value.y;
                m02 = value.z;
                m03 = value.w;
            }
        }

        /// <summary>
        /// The second row of this matrix.
        /// </summary>
        public Vector4 Row1
        {
            get => new Vector4(m10, m11, m12, m13);
            set
            {
                m10 = value.x;
                m11 = value.y;
                m12 = value.z;
                m13 = value.w;
            }
        }

        /// <summary>
        /// The third row of this matrix.
        /// </summary>
        public Vector4 Row2
        {
            get => new Vector4(m20, m21, m22, m23);
            set
            {
                m20 = value.x;
                m21 = value.y;
                m22 = value.z;
                m23 = value.w;
            }
        }

        /// <summary>
        /// The fourth row of this matrix.
        /// </summary>
        public Vector4 Row3
        {
            get => new Vector4(m30, m31, m32, m33);
            set
            {
                m30 = value.x;
                m31 = value.y;
                m32 = value.z;
                m33 = value.w;
            }
        }

        /// <summary>
        /// The first column of this matrix.
        /// </summary>
        public Vector4 Column0
        {
            get => new Vector4(m00, m10, m20, m30);
            set
            {
                m00 = value.x;
                m10 = value.y;
                m20 = value.z;
                m30 = value.w;
            }
        }

        /// <summary>
        /// The second column of this matrix.
        /// </summary>
        public Vector4 Column1
        {
            get => new Vector4(m01, m11, m21, m31);
            set
            {
                m01 = value.x;
                m11 = value.y;
                m21 = value.z;
                m31 = value.w;
            }
        }

        /// <summary>
        /// The third column of this matrix.
        /// </summary>
        public Vector4 Column2
        {
            get => new Vector4(m02, m12, m22, m32);
            set
            {
                m02 = value.x;
                m12 = value.y;
                m22 = value.z;
                m32 = value.w;
            }
        }

        /// <summary>
        /// The fourth column of this matrix.
        /// </summary>
        public Vector4 Column3
        {
            get => new Vector4(m03, m13, m23, m33);
            set
            {
                m03 = value.x;
                m13 = value.y;
                m23 = value.z;
                m33 = value.w;
            }
        }

        /// <summary>
        /// The values along the main diagonal of the matrix.
        /// </summary>
        public Vector4 Diagonal
        {
            get => new Vector4(m00, m11, m22, m33);
            set
            {
                m00 = value.x;
                m11 = value.y;
                m22 = value.z;
                m33 = value.w;
            }
        }

        /// <summary>
        /// The left vector formed from the first row -M11, -M12, -M13 elements.
        /// </summary>
        public Vector3 Left
        {
            get => new Vector3(-m00, -m01, -m02);
            set
            {
                m00 = -value.x;
                m01 = -value.y;
                m02 = -value.z;
            }
        }

        /// <summary>
        /// The right vector formed from the first row M11, M12, M13 elements.
        /// </summary>
        public Vector3 Right
        {
            get => new Vector3(m00, m01, m02);
            set
            {
                m00 = value.x;
                m01 = value.y;
                m02 = value.z;
            }
        }

        /// <summary>
        /// The down vector formed from the second row -M21, -M22, -M23 elements.
        /// </summary>
        public Vector3 Down
        {
            get => new Vector3(-m10, -m11, -m12);
            set
            {
                m10 = -value.x;
                m11 = -value.y;
                m12 = -value.z;
            }
        }

        /// <summary>
        /// The upper vector formed from the second row M21, M22, M23 elements.
        /// </summary>
        public Vector3 Up
        {
            get => new Vector3(m10, m11, m12);
            set
            {
                m10 = value.x;
                m11 = value.y;
                m12 = value.z;
            }
        }

        /// <summary>
        /// The forward vector formed from the third row -M31, -M32, -M33 elements.
        /// </summary>
        public Vector3 Forward
        {
            get => new Vector3(-m20, -m21, -m22);
            set
            {
                m20 = -value.x;
                m21 = -value.y;
                m22 = -value.z;
            }
        }

        /// <summary>
        /// The backward vector formed from the third row M31, M32, M33 elements.
        /// </summary>
        public Vector3 Backward
        {
            get => new Vector3(m20, m21, m22);
            set
            {
                m20 = value.x;
                m21 = value.y;
                m22 = value.z;
            }
        }

        /// <summary>
        /// Position stored in this matrix.
        /// </summary>
        public Vector3 Translation
        {
            get => new Vector3(m30, m31, m32);
            set
            {
                m30 = value.x;
                m31 = value.y;
                m32 = value.z;
            }
        }

        /// <summary>
        /// Gets the trace of the matrix, the sum of the values along the diagonal.
        /// </summary>
        public float Trace => m00 + m11 + m22 + m33;

        /// <summary>
        /// The determinant of this <see cref="Matrix"/>.
        /// </summary>
        public float Determinant
        {
            get
            {
                float num22 = m00;
                float num21 = m01;
                float num20 = m02;
                float num19 = m03;
                float num12 = m10;
                float num11 = m11;
                float num10 = m12;
                float num9 = m13;
                float num8 = m20;
                float num7 = m21;
                float num6 = m22;
                float num5 = m23;
                float num4 = m30;
                float num3 = m31;
                float num2 = m32;
                float num = m33;

                float num18 = (num6 * num) - (num5 * num2);
                float num17 = (num7 * num) - (num5 * num3);
                float num16 = (num7 * num2) - (num6 * num3);
                float num15 = (num8 * num) - (num5 * num4);
                float num14 = (num8 * num2) - (num6 * num4);
                float num13 = (num8 * num3) - (num7 * num4);

                return
                    (num22 * ((num11 * num18) - (num10 * num17) + (num9 * num16))) -
                    (num21 * ((num12 * num18) - (num10 * num15) + (num9 * num14))) +
                    (num20 * ((num12 * num17) - (num11 * num15) + (num9 * num13))) -
                    (num19 * ((num12 * num16) - (num11 * num14) + (num10 * num13)));
            }
        }

        /// <summary>
        /// Gets a row from the matrix.
        /// </summary>
        /// <param name="index">The zero indexed row number.</param>
        public Vector4 GetRow(int index)
        {
            switch (index)
            {
                case 0: return new Vector4(m00, m01, m02, m03);
                case 1: return new Vector4(m10, m11, m12, m13);
                case 2: return new Vector4(m20, m21, m22, m23);
                case 3: return new Vector4(m30, m31, m32, m33);
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets a column from the matrix.
        /// </summary>
        /// <param name="index">The zero indexed column number.</param>
        public Vector4 GetColumn(int index)
        {
            switch (index)
            {
                case 0: return new Vector4(m00, m10, m20, m30);
                case 1: return new Vector4(m01, m11, m21, m31);
                case 2: return new Vector4(m02, m12, m22, m32);
                case 3: return new Vector4(m03, m13, m23, m33);
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Sets a row in the matrix.
        /// </summary>
        /// <param name="index">The zero indexed row number.</param>
        public void SetRow(int index, Vector4 row)
        {
            this[index, 0] = row.x;
            this[index, 1] = row.y;
            this[index, 2] = row.z;
            this[index, 3] = row.w;
        }

        /// <summary>
        /// Sets a row in the matrix.
        /// </summary>
        /// <param name="index">The zero indexed column number.</param>
        public void SetColumn(int index, Vector4 column)
        {
            this[0, index] = column.x;
            this[1, index] = column.y;
            this[2, index] = column.z;
            this[3, index] = column.w;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains sum of two matrixes.
        /// </summary>
        /// <param name="left">The left operand of the addition.</param>
        /// <param name="right">The right operand of the addition.</param>
        /// <returns>The result of the matrix addition.</returns>
        public static Matrix Add(Matrix left, Matrix right)
        {
            Add(ref left, ref right, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains sum of two matrixes.
        /// </summary>
        /// <param name="left">The left operand of the addition.</param>
        /// <param name="right">The right operand of the addition.</param>
        /// <param name="result">The result of the matrix addition as an output parameter.</param>
        public static void Add(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result.m00 = left.m00 + right.m00;
            result.m01 = left.m01 + right.m01;
            result.m02 = left.m02 + right.m02;
            result.m03 = left.m03 + right.m03;
            result.m10 = left.m10 + right.m10;
            result.m11 = left.m11 + right.m11;
            result.m12 = left.m12 + right.m12;
            result.m13 = left.m13 + right.m13;
            result.m20 = left.m20 + right.m20;
            result.m21 = left.m21 + right.m21;
            result.m22 = left.m22 + right.m22;
            result.m23 = left.m23 + right.m23;
            result.m30 = left.m30 + right.m30;
            result.m31 = left.m31 + right.m31;
            result.m32 = left.m32 + right.m32;
            result.m33 = left.m33 + right.m33;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains difference of two matrixes.
        /// </summary>
        /// <param name="left">The left operand of the subtraction.</param>
        /// <param name="right">The right operand of the subtraction.</param>
        /// <returns>The result of the matrix subtraction.</returns>
        public static Matrix Subtract(Matrix left, Matrix right)
        {
            Subtract(ref left, ref right, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains difference of two matrixes.
        /// </summary>
        /// <param name="left">The left operand of the subtraction.</param>
        /// <param name="right">The right operand of the subtraction.</param>
        /// <param name="result">The result of the matrix subtraction as an output parameter.</param>
        public static void Subtract(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result.m00 = left.m00 - right.m00;
            result.m01 = left.m01 - right.m01;
            result.m02 = left.m02 - right.m02;
            result.m03 = left.m03 - right.m03;
            result.m10 = left.m10 - right.m10;
            result.m11 = left.m11 - right.m11;
            result.m12 = left.m12 - right.m12;
            result.m13 = left.m13 - right.m13;
            result.m20 = left.m20 - right.m20;
            result.m21 = left.m21 - right.m21;
            result.m22 = left.m22 - right.m22;
            result.m23 = left.m23 - right.m23;
            result.m30 = left.m30 - right.m30;
            result.m31 = left.m31 - right.m31;
            result.m32 = left.m32 - right.m32;
            result.m33 = left.m33 - right.m33;
        }

        /// <summary>
        /// Returns a matrix with the all values negated.
        /// </summary>
        /// <param name="matrix">The matrix to negate.</param>
        /// <returns>Result of the matrix negation.</returns>
        public static Matrix Negate(Matrix matrix)
        {
            Negate(ref matrix, out Matrix result);
            return result;
        }

        /// <summary>
        /// Returns a matrix with the all values negated.
        /// </summary>
        /// <param name="matrix">The matrix to negate.</param>
        /// <param name="result">Result of the matrix negation as an output parameter.</param>
        public static void Negate(ref Matrix matrix, out Matrix result)
        {
            result.m00 = -matrix.m00;
            result.m01 = -matrix.m01;
            result.m02 = -matrix.m02;
            result.m03 = -matrix.m03;
            result.m10 = -matrix.m10;
            result.m11 = -matrix.m11;
            result.m12 = -matrix.m12;
            result.m13 = -matrix.m13;
            result.m20 = -matrix.m20;
            result.m21 = -matrix.m21;
            result.m22 = -matrix.m22;
            result.m23 = -matrix.m23;
            result.m30 = -matrix.m30;
            result.m31 = -matrix.m31;
            result.m32 = -matrix.m32;
            result.m33 = -matrix.m33;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains a multiplication of two matrix.
        /// </summary>
        /// <param name="left">Source <see cref="Matrix"/>.</param>
        /// <param name="right">Source <see cref="Matrix"/>.</param>
        /// <returns>Result of the matrix multiplication.</returns>
        public static Matrix Multiply(Matrix left, Matrix right)
        {
            Multiply(ref left, ref right, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains a multiplication of two matrices.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="result">Result of the matrix multiplication as an output parameter.</param>
        public static void Multiply(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result.m00 = (left.m00 * right.m00) + (left.m01 * right.m10) + (left.m02 * right.m20) + (left.m03 * right.m30);
            result.m01 = (left.m00 * right.m01) + (left.m01 * right.m11) + (left.m02 * right.m21) + (left.m03 * right.m31);
            result.m02 = (left.m00 * right.m02) + (left.m01 * right.m12) + (left.m02 * right.m22) + (left.m03 * right.m32);
            result.m03 = (left.m00 * right.m03) + (left.m01 * right.m13) + (left.m02 * right.m23) + (left.m03 * right.m33);
            result.m10 = (left.m10 * right.m00) + (left.m11 * right.m10) + (left.m12 * right.m20) + (left.m13 * right.m30);
            result.m11 = (left.m10 * right.m01) + (left.m11 * right.m11) + (left.m12 * right.m21) + (left.m13 * right.m31);
            result.m12 = (left.m10 * right.m02) + (left.m11 * right.m12) + (left.m12 * right.m22) + (left.m13 * right.m32);
            result.m13 = (left.m10 * right.m03) + (left.m11 * right.m13) + (left.m12 * right.m23) + (left.m13 * right.m33);
            result.m20 = (left.m20 * right.m00) + (left.m21 * right.m10) + (left.m22 * right.m20) + (left.m23 * right.m30);
            result.m21 = (left.m20 * right.m01) + (left.m21 * right.m11) + (left.m22 * right.m21) + (left.m23 * right.m31);
            result.m22 = (left.m20 * right.m02) + (left.m21 * right.m12) + (left.m22 * right.m22) + (left.m23 * right.m32);
            result.m23 = (left.m20 * right.m03) + (left.m21 * right.m13) + (left.m22 * right.m23) + (left.m23 * right.m33);
            result.m30 = (left.m30 * right.m00) + (left.m31 * right.m10) + (left.m32 * right.m20) + (left.m33 * right.m30);
            result.m31 = (left.m30 * right.m01) + (left.m31 * right.m11) + (left.m32 * right.m21) + (left.m33 * right.m31);
            result.m32 = (left.m30 * right.m02) + (left.m31 * right.m12) + (left.m32 * right.m22) + (left.m33 * right.m32);
            result.m33 = (left.m30 * right.m03) + (left.m31 * right.m13) + (left.m32 * right.m23) + (left.m33 * right.m33);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains a multiplication of <see cref="Matrix"/> and a scalar.
        /// </summary>
        /// <param name="matrix">The matrix to scale.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>Result of the matrix multiplication with a scalar.</returns>
        public static Matrix Multiply(Matrix matrix, float scaleFactor)
        {
            Negate(ref matrix, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains a multiplication of <see cref="Matrix"/> and a scalar.
        /// </summary>
        /// <param name="matrix">The matrix to scale.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">Result of the matrix multiplication with a scalar as an output parameter.</param>
        public static void Multiply(ref Matrix matrix, float scaleFactor, out Matrix result)
        {
            result.m00 = matrix.m00 * scaleFactor;
            result.m01 = matrix.m01 * scaleFactor;
            result.m02 = matrix.m02 * scaleFactor;
            result.m03 = matrix.m03 * scaleFactor;
            result.m10 = matrix.m10 * scaleFactor;
            result.m11 = matrix.m11 * scaleFactor;
            result.m12 = matrix.m12 * scaleFactor;
            result.m13 = matrix.m13 * scaleFactor;
            result.m20 = matrix.m20 * scaleFactor;
            result.m21 = matrix.m21 * scaleFactor;
            result.m22 = matrix.m22 * scaleFactor;
            result.m23 = matrix.m23 * scaleFactor;
            result.m30 = matrix.m30 * scaleFactor;
            result.m31 = matrix.m31 * scaleFactor;
            result.m32 = matrix.m32 * scaleFactor;
            result.m33 = matrix.m33 * scaleFactor;
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by the elements of another matrix.
        /// </summary>
        /// <param name="left">Source <see cref="Matrix"/>.</param>
        /// <param name="right">Divisor <see cref="Matrix"/>.</param>
        /// <returns>The result of dividing the matrix.</returns>
        public static Matrix Divide(Matrix left, Matrix right)
        {
            Divide(ref left, ref right, out Matrix result);
            return result;
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by the elements of another matrix.
        /// </summary>
        /// <param name="left">Source <see cref="Matrix"/>.</param>
        /// <param name="right">Divisor <see cref="Matrix"/>.</param>
        /// <param name="result">The result of dividing the matrix as an output parameter.</param>
        public static void Divide(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result.m00 = left.m00 / right.m00;
            result.m01 = left.m01 / right.m01;
            result.m02 = left.m02 / right.m02;
            result.m03 = left.m03 / right.m03;
            result.m10 = left.m10 / right.m10;
            result.m11 = left.m11 / right.m11;
            result.m12 = left.m12 / right.m12;
            result.m13 = left.m13 / right.m13;
            result.m20 = left.m20 / right.m20;
            result.m21 = left.m21 / right.m21;
            result.m22 = left.m22 / right.m22;
            result.m23 = left.m23 / right.m23;
            result.m30 = left.m30 / right.m30;
            result.m31 = left.m31 / right.m31;
            result.m32 = left.m32 / right.m32;
            result.m33 = left.m33 / right.m33;
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by a scalar.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <returns>The result of dividing a matrix by a scalar.</returns>
        public static Matrix Divide(Matrix matrix, float divider)
        {
            Divide(ref matrix, divider, out Matrix result);
            return result;
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by a scalar.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <param name="result">The result of dividing a matrix by a scalar as an output parameter.</param>
        public static void Divide(ref Matrix matrix, float divider, out Matrix result)
        {
            float num = 1f / divider;
            result.m00 = matrix.m00 * num;
            result.m01 = matrix.m01 * num;
            result.m02 = matrix.m02 * num;
            result.m03 = matrix.m03 * num;
            result.m10 = matrix.m10 * num;
            result.m11 = matrix.m11 * num;
            result.m12 = matrix.m12 * num;
            result.m13 = matrix.m13 * num;
            result.m20 = matrix.m20 * num;
            result.m21 = matrix.m21 * num;
            result.m22 = matrix.m22 * num;
            result.m23 = matrix.m23 * num;
            result.m30 = matrix.m30 * num;
            result.m31 = matrix.m31 * num;
            result.m32 = matrix.m32 * num;
            result.m33 = matrix.m33 * num;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains linear interpolation of the values in specified matrixes.
        /// </summary>
        /// <param name="a">The matrix to interpolate from.</param>
        /// <param name="b">The matrix to interpolate to.</param>
        /// <param name="t">Weighting value between 0 and 1.</param>
        /// <returns>>The result of linear interpolation of the specified matrixes.</returns>
        public static Matrix Lerp(Matrix a, Matrix b, float t)
        {
            Lerp(ref a, ref b, t, out a);
            return a;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains linear interpolation of the values in specified matrixes.
        /// </summary>
        /// <param name="a">The matrix to interpolate from.</param>
        /// <param name="b">The matrix to interpolate to.</param>
        /// <param name="t">Weighting value between 0 and 1.</param>
        /// <param name="result">The result of linear interpolation of the specified matrixes as an output parameter.</param>
        public static void Lerp(ref Matrix a, ref Matrix b, float t, out Matrix result)
        {
            result.m00 = a.m00 + ((b.m00 - a.m00) * t);
            result.m01 = a.m01 + ((b.m01 - a.m01) * t);
            result.m02 = a.m02 + ((b.m02 - a.m02) * t);
            result.m03 = a.m03 + ((b.m03 - a.m03) * t);
            result.m10 = a.m10 + ((b.m10 - a.m10) * t);
            result.m11 = a.m11 + ((b.m11 - a.m11) * t);
            result.m12 = a.m12 + ((b.m12 - a.m12) * t);
            result.m13 = a.m13 + ((b.m13 - a.m13) * t);
            result.m20 = a.m20 + ((b.m20 - a.m20) * t);
            result.m21 = a.m21 + ((b.m21 - a.m21) * t);
            result.m22 = a.m22 + ((b.m22 - a.m22) * t);
            result.m23 = a.m23 + ((b.m23 - a.m23) * t);
            result.m30 = a.m30 + ((b.m30 - a.m30) * t);
            result.m31 = a.m31 + ((b.m31 - a.m31) * t);
            result.m32 = a.m32 + ((b.m32 - a.m32) * t);
            result.m33 = a.m33 + ((b.m33 - a.m33) * t);
        }

        /// <summary>
        /// Swaps the rows and columns of a matrix.
        /// </summary>
        /// <param name="matrix">The matrix to tranpose.</param>
        /// <returns>A new <see cref="Matrix"/> containing the transposed result.</returns>
        public static Matrix Transpose(Matrix matrix)
        {
            Transpose(ref matrix, out Matrix result);
            return result;
        }

        /// <summary>
        /// Swaps the rows and columns of a matrix.
        /// </summary>
        /// <param name="matrix">The matrix to tranpose.</param>
        /// <returns>A new <see cref="Matrix"/> containing the transposed result.</returns>
        public static void Transpose(ref Matrix matrix, out Matrix result)
        {
            result.m00 = matrix.m00;
            result.m01 = matrix.m10;
            result.m02 = matrix.m20;
            result.m03 = matrix.m30;

            result.m10 = matrix.m01;
            result.m11 = matrix.m11;
            result.m12 = matrix.m21;
            result.m13 = matrix.m31;

            result.m20 = matrix.m02;
            result.m21 = matrix.m12;
            result.m22 = matrix.m22;
            result.m23 = matrix.m32;

            result.m30 = matrix.m03;
            result.m31 = matrix.m13;
            result.m32 = matrix.m23;
            result.m33 = matrix.m33;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains inversion of the specified matrix. 
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/>.</param>
        /// <returns>The inverted matrix.</returns>
        public static Matrix Invert(Matrix matrix)
        {
            Invert(ref matrix, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains inversion of the specified matrix.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/>.</param>
        /// <param name="result">The inverted matrix as output parameter.</param>
        public static void Invert(ref Matrix matrix, out Matrix result)
        {
            int[] colIdx = { 0, 0, 0, 0 };
            int[] rowIdx = { 0, 0, 0, 0 };
            int[] pivotIdx = { -1, -1, -1, -1 };

            // convert the matrix to an array for easy looping
            float[,] inverse =
            {
                { matrix.m00, matrix.m01, matrix.m02, matrix.m03 },
                { matrix.m10, matrix.m11, matrix.m12, matrix.m13 },
                { matrix.m20, matrix.m21, matrix.m22, matrix.m23 },
                { matrix.m30, matrix.m31, matrix.m32, matrix.m33 },
            };
            var icol = 0;
            var irow = 0;
            for (var i = 0; i < 4; i++)
            {
                // Find the largest pivot value
                var maxPivot = 0.0f;
                for (var j = 0; j < 4; j++)
                {
                    if (pivotIdx[j] != 0)
                    {
                        for (var k = 0; k < 4; ++k)
                        {
                            if (pivotIdx[k] == -1)
                            {
                                var absVal = Math.Abs(inverse[j, k]);
                                if (absVal > maxPivot)
                                {
                                    maxPivot = absVal;
                                    irow = j;
                                    icol = k;
                                }
                            }
                            else if (pivotIdx[k] > 0)
                            {
                                result = matrix;
                                return;
                            }
                        }
                    }
                }

                ++pivotIdx[icol];

                // Swap rows over so pivot is on diagonal
                if (irow != icol)
                {
                    for (var k = 0; k < 4; ++k)
                    {
                        var f = inverse[irow, k];
                        inverse[irow, k] = inverse[icol, k];
                        inverse[icol, k] = f;
                    }
                }

                rowIdx[i] = irow;
                colIdx[i] = icol;

                var pivot = inverse[icol, icol];

                // check for singular matrix
                if (pivot == 0.0f)
                {
                    throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
                }

                // Scale row so it has a unit diagonal
                var oneOverPivot = 1.0f / pivot;
                inverse[icol, icol] = 1.0f;
                for (var k = 0; k < 4; ++k)
                {
                    inverse[icol, k] *= oneOverPivot;
                }

                // Do elimination of non-diagonal elements
                for (var j = 0; j < 4; ++j)
                {
                    // check this isn't on the diagonal
                    if (icol != j)
                    {
                        var f = inverse[j, icol];
                        inverse[j, icol] = 0.0f;
                        for (var k = 0; k < 4; ++k)
                        {
                            inverse[j, k] -= inverse[icol, k] * f;
                        }
                    }
                }
            }

            for (var j = 3; j >= 0; --j)
            {
                var ir = rowIdx[j];
                var ic = colIdx[j];
                for (var k = 0; k < 4; ++k)
                {
                    var f = inverse[k, ir];
                    inverse[k, ir] = inverse[k, ic];
                    inverse[k, ic] = f;
                }
            }

            result.m00 = inverse[0, 0];
            result.m01 = inverse[0, 1];
            result.m02 = inverse[0, 2];
            result.m03 = inverse[0, 3];
            result.m10 = inverse[1, 0];
            result.m11 = inverse[1, 1];
            result.m12 = inverse[1, 2];
            result.m13 = inverse[1, 3];
            result.m20 = inverse[2, 0];
            result.m21 = inverse[2, 1];
            result.m22 = inverse[2, 2];
            result.m23 = inverse[2, 3];
            result.m30 = inverse[3, 0];
            result.m31 = inverse[3, 1];
            result.m32 = inverse[3, 2];
            result.m33 = inverse[3, 3];
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains inversion of the specified matrix with high precision.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/>.</param>
        /// <returns>The inverted matrix.</returns>
        public static Matrix InvertPrecise(Matrix matrix)
        {
            InvertPrecise(ref matrix, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains inversion of the specified matrix with high precision.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/>.</param>
        /// <param name="result">The inverted matrix as output parameter.</param>
        public static void InvertPrecise(ref Matrix matrix, out Matrix result)
        {
            double num1 = matrix.m00;
            double num2 = matrix.m01;
            double num3 = matrix.m02;
            double num4 = matrix.m03;
            double num5 = matrix.m10;
            double num6 = matrix.m11;
            double num7 = matrix.m12;
            double num8 = matrix.m13;
            double num9 = matrix.m20;
            double num10 = matrix.m21;
            double num11 = matrix.m22;
            double num12 = matrix.m23;
            double num13 = matrix.m30;
            double num14 = matrix.m31;
            double num15 = matrix.m32;
            double num16 = matrix.m33;

            float num17 = (float)(num11 * num16 - num12 * num15);
            float num18 = (float)(num10 * num16 - num12 * num14);
            float num19 = (float)(num10 * num15 - num11 * num14);
            float num20 = (float)(num9 * num16 - num12 * num13);
            float num21 = (float)(num9 * num15 - num11 * num13);
            float num22 = (float)(num9 * num14 - num10 * num13);
            float num23 = (float)(num6 * num17 - num7 * num18 + num8 * num19);
            float num24 = (float)-(num5 * num17 - num7 * num20 + num8 * num21);
            float num25 = (float)(num5 * num18 - num6 * num20 + num8 * num22);
            float num26 = (float)-(num5 * num19 - num6 * num21 + num7 * num22);
            float num27 = (float)(1.0 / (num1 * num23 + num2 * num24 + num3 * num25 + num4 * num26));

            result.m00 = num23 * num27;
            result.m10 = num24 * num27;
            result.m20 = num25 * num27;
            result.m30 = num26 * num27;
            result.m01 = (float)-(num2 * num17 - num3 * num18 + num4 * num19) * num27;
            result.m11 = (float)(num1 * num17 - num3 * num20 + num4 * num21) * num27;
            result.m21 = (float)-(num1 * num18 - num2 * num20 + num4 * num22) * num27;
            result.m31 = (float)(num1 * num19 - num2 * num21 + num3 * num22) * num27;
            float num28 = (float)(num7 * num16 - num8 * num15);
            float num29 = (float)(num6 * num16 - num8 * num14);
            float num30 = (float)(num6 * num15 - num7 * num14);
            float num31 = (float)(num5 * num16 - num8 * num13);
            float num32 = (float)(num5 * num15 - num7 * num13);
            float num33 = (float)(num5 * num14 - num6 * num13);
            result.m02 = (float)(num2 * num28 - num3 * num29 + num4 * num30) * num27;
            result.m12 = (float)-(num1 * num28 - num3 * num31 + num4 * num32) * num27;
            result.m22 = (float)(num1 * num29 - num2 * num31 + num4 * num33) * num27;
            result.m32 = (float)-(num1 * num30 - num2 * num32 + num3 * num33) * num27;
            float num34 = (float)(num7 * num12 - num8 * num11);
            float num35 = (float)(num6 * num12 - num8 * num10);
            float num36 = (float)(num6 * num11 - num7 * num10);
            float num37 = (float)(num5 * num12 - num8 * num9);
            float num38 = (float)(num5 * num11 - num7 * num9);
            float num39 = (float)(num5 * num10 - num6 * num9);
            result.m03 = (float)-(num2 * num34 - num3 * num35 + num4 * num36) * num27;
            result.m13 = (float)(num1 * num34 - num3 * num37 + num4 * num38) * num27;
            result.m23 = (float)-(num1 * num35 - num2 * num37 + num4 * num39) * num27;
            result.m33 = (float)(num1 * num36 - num2 * num38 + num3 * num39) * num27;
        }

        /*
        /// <summary>
        /// Creates a new <see cref="Matrix"/> for spherical billboarding that rotates around specified object position.
        /// </summary>
        /// <param name="objectPosition">Position of billboard object. It will rotate around that vector.</param>
        /// <param name="cameraPosition">The camera position.</param>
        /// <param name="cameraUpVector">The camera up vector.</param>
        /// <param name="cameraForwardVector">Optional camera forward vector.</param>
        /// <returns>The <see cref="Matrix"/> for spherical billboarding.</returns>
        public static Matrix CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition,
            Vector3 cameraUpVector, Vector3? cameraForwardVector)
        {

            // Delegate to the other overload of the function to do the work
            CreateBillboard(ref objectPosition, ref cameraPosition, ref cameraUpVector, cameraForwardVector, out Matrix result);

            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> for spherical billboarding that rotates around specified object position.
        /// </summary>
        /// <param name="objectPosition">Position of billboard object. It will rotate around that vector.</param>
        /// <param name="cameraPosition">The camera position.</param>
        /// <param name="cameraUpVector">The camera up vector.</param>
        /// <param name="cameraForwardVector">Optional camera forward vector.</param>
        /// <param name="result">The <see cref="Matrix"/> for spherical billboarding as an output parameter.</param>
        public static void CreateBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition,
            ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix result)
        {
            Vector3 vector;
            vector.x = objectPosition.x - cameraPosition.x;
            vector.y = objectPosition.y - cameraPosition.y;
            vector.z = objectPosition.z - cameraPosition.z;
            float num = vector.LengthSquared();
            if (num < 0.0001f)
            {
                vector = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
            }
            else
            {
                Vector3.Multiply(ref vector, 1f / ((float)Math.Sqrt(num)), out vector);
            }
            Vector3.Cross(ref cameraUpVector, ref vector, out Vector3 vector3);
            vector3.Normalize();
            Vector3.Cross(ref vector, ref vector3, out Vector3 vector2);
            result.m00 = vector3.x;
            result.m01 = vector3.y;
            result.m02 = vector3.z;
            result.m03 = 0;
            result.m10 = vector2.x;
            result.m11 = vector2.y;
            result.m12 = vector2.z;
            result.m13 = 0;
            result.m20 = vector.x;
            result.m21 = vector.y;
            result.m22 = vector.z;
            result.m23 = 0;
            result.m30 = objectPosition.x;
            result.m31 = objectPosition.y;
            result.m32 = objectPosition.z;
            result.m33 = 1;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> for cylindrical billboarding that rotates around specified axis.
        /// </summary>
        /// <param name="objectPosition">Object position the billboard will rotate around.</param>
        /// <param name="cameraPosition">Camera position.</param>
        /// <param name="rotateAxis">Axis of billboard for rotation.</param>
        /// <param name="cameraForwardVector">Optional camera forward vector.</param>
        /// <param name="objectForwardVector">Optional object forward vector.</param>
        /// <returns>The <see cref="Matrix"/> for cylindrical billboarding.</returns>
        public static Matrix CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition,
            Vector3 rotateAxis, Nullable<Vector3> cameraForwardVector, Nullable<Vector3> objectForwardVector)
        {
            CreateConstrainedBillboard(ref objectPosition, ref cameraPosition, ref rotateAxis,
                cameraForwardVector, objectForwardVector, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> for cylindrical billboarding that rotates around specified axis.
        /// </summary>
        /// <param name="objectPosition">Object position the billboard will rotate around.</param>
        /// <param name="cameraPosition">Camera position.</param>
        /// <param name="rotateAxis">Axis of billboard for rotation.</param>
        /// <param name="cameraForwardVector">Optional camera forward vector.</param>
        /// <param name="objectForwardVector">Optional object forward vector.</param>
        /// <param name="result">The <see cref="Matrix"/> for cylindrical billboarding as an output parameter.</param>
        public static void CreateConstrainedBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition,
            ref Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector, out Matrix result)
        {
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            vector2.x = objectPosition.x - cameraPosition.x;
            vector2.y = objectPosition.y - cameraPosition.y;
            vector2.z = objectPosition.z - cameraPosition.z;
            float num2 = vector2.LengthSquared();
            if (num2 < 0.0001f)
            {
                vector2 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
            }
            else
            {
                Vector3.Multiply(ref vector2, 1f / ((float)Math.Sqrt(num2)), out vector2);
            }
            Vector3 vector4 = rotateAxis;
            Vector3.Dot(ref rotateAxis, ref vector2, out float num);
            if (Math.Abs(num) > 0.9982547f)
            {
                if (objectForwardVector.HasValue)
                {
                    vector = objectForwardVector.Value;
                    Vector3.Dot(ref rotateAxis, ref vector, out num);
                    if (Math.Abs(num) > 0.9982547f)
                    {
                        num = ((rotateAxis.x * Vector3.Forward.x) + (rotateAxis.y * Vector3.Forward.y)) + (rotateAxis.z * Vector3.Forward.z);
                        vector = (Math.Abs(num) > 0.9982547f) ? Vector3.Right : Vector3.Forward;
                    }
                }
                else
                {
                    num = ((rotateAxis.x * Vector3.Forward.x) + (rotateAxis.y * Vector3.Forward.y)) + (rotateAxis.z * Vector3.Forward.z);
                    vector = (Math.Abs(num) > 0.9982547f) ? Vector3.Right : Vector3.Forward;
                }
                Vector3.Cross(ref rotateAxis, ref vector, out vector3);
                vector3.Normalize();
                Vector3.Cross(ref vector3, ref rotateAxis, out vector);
                vector.Normalize();
            }
            else
            {
                Vector3.Cross(ref rotateAxis, ref vector2, out vector3);
                vector3.Normalize();
                Vector3.Cross(ref vector3, ref vector4, out vector);
                vector.Normalize();
            }
            result.m00 = vector3.x;
            result.m01 = vector3.y;
            result.m02 = vector3.z;
            result.m03 = 0;
            result.m10 = vector4.x;
            result.m11 = vector4.y;
            result.m12 = vector4.z;
            result.m13 = 0;
            result.m20 = vector.x;
            result.m21 = vector.y;
            result.m22 = vector.z;
            result.m23 = 0;
            result.m30 = objectPosition.x;
            result.m31 = objectPosition.y;
            result.m32 = objectPosition.z;
            result.m33 = 1;

        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains the rotation moment around specified axis.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The rotation <see cref="Matrix"/>.</returns>
        public static Matrix CreateFromAxisAngle(Vector3 axis, float angle)
        {
            CreateFromAxisAngle(ref axis, angle, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains the rotation moment around specified axis.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> as an output parameter.</param>
        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Matrix result)
        {
            float x = axis.x;
            float y = axis.y;
            float z = axis.z;
            float num2 = (float)Math.Sin(angle);
            float num = (float)Math.Cos(angle);
            float num11 = x * x;
            float num10 = y * y;
            float num9 = z * z;
            float num8 = x * y;
            float num7 = x * z;
            float num6 = y * z;
            result.m00 = num11 + (num * (1f - num11));
            result.m01 = (num8 - (num * num8)) + (num2 * z);
            result.m02 = (num7 - (num * num7)) - (num2 * y);
            result.m03 = 0;
            result.m10 = (num8 - (num * num8)) - (num2 * z);
            result.m11 = num10 + (num * (1f - num10));
            result.m12 = (num6 - (num * num6)) + (num2 * x);
            result.m13 = 0;
            result.m20 = (num7 - (num * num7)) + (num2 * y);
            result.m21 = (num6 - (num * num6)) - (num2 * x);
            result.m22 = num9 + (num * (1f - num9));
            result.m23 = 0;
            result.m30 = 0;
            result.m31 = 0;
            result.m32 = 0;
            result.m33 = 1;
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> from a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion"><see cref="Quaternion"/> of rotation moment.</param>
        /// <returns>The rotation <see cref="Matrix"/>.</returns>
        public static Matrix CreateFromQuaternion(Quaternion quaternion)
        {
            CreateFromQuaternion(ref quaternion, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> from a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion"><see cref="Quaternion"/> of rotation moment.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> as an output parameter.</param>
        public static void CreateFromQuaternion(ref Quaternion quaternion, out Matrix result)
        {
            float num9 = quaternion.x * quaternion.x;
            float num8 = quaternion.y * quaternion.y;
            float num7 = quaternion.z * quaternion.z;
            float num6 = quaternion.x * quaternion.y;
            float num5 = quaternion.z * quaternion.w;
            float num4 = quaternion.z * quaternion.x;
            float num3 = quaternion.y * quaternion.w;
            float num2 = quaternion.y * quaternion.z;
            float num = quaternion.x * quaternion.w;
            result.m00 = 1f - (2f * (num8 + num7));
            result.m01 = 2f * (num6 + num5);
            result.m02 = 2f * (num4 - num3);
            result.m03 = 0f;
            result.m10 = 2f * (num6 - num5);
            result.m11 = 1f - (2f * (num7 + num9));
            result.m12 = 2f * (num2 + num);
            result.m13 = 0f;
            result.m20 = 2f * (num4 + num3);
            result.m21 = 2f * (num2 - num);
            result.m22 = 1f - (2f * (num8 + num9));
            result.m23 = 0f;
            result.m30 = 0f;
            result.m31 = 0f;
            result.m32 = 0f;
            result.m33 = 1f;
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> from the specified yaw, pitch and roll values.
        /// </summary>
        /// <param name="yaw">The yaw rotation value in radians.</param>
        /// <param name="pitch">The pitch rotation value in radians.</param>
        /// <param name="roll">The roll rotation value in radians.</param>
        /// <returns>The rotation <see cref="Matrix"/>.</returns>
        /// <remarks>For more information about yaw, pitch and roll visit http://en.wikipedia.org/wiki/Euler_angles.
        /// </remarks>
		public static Matrix CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            CreateFromYawPitchRoll(yaw, pitch, roll, out Matrix matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> from the specified yaw, pitch and roll values.
        /// </summary>
        /// <param name="yaw">The yaw rotation value in radians.</param>
        /// <param name="pitch">The pitch rotation value in radians.</param>
        /// <param name="roll">The roll rotation value in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> as an output parameter.</param>
        /// <remarks>For more information about yaw, pitch and roll visit http://en.wikipedia.org/wiki/Euler_angles.
        /// </remarks>
		public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Matrix result)
        {
            Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out Quaternion quaternion);
            CreateFromQuaternion(ref quaternion, out result);
        }

        /// <summary>
        /// Creates a new viewing <see cref="Matrix"/>.
        /// </summary>
        /// <param name="cameraPosition">Position of the camera.</param>
        /// <param name="cameraTarget">Lookup vector of the camera.</param>
        /// <param name="cameraUpVector">The direction of the upper edge of the camera.</param>
        /// <returns>The viewing <see cref="Matrix"/>.</returns>
        public static Matrix CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
        {
            CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector, out Matrix matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new viewing <see cref="Matrix"/>.
        /// </summary>
        /// <param name="cameraPosition">Position of the camera.</param>
        /// <param name="cameraTarget">Lookup vector of the camera.</param>
        /// <param name="cameraUpVector">The direction of the upper edge of the camera.</param>
        /// <param name="result">The viewing <see cref="Matrix"/> as an output parameter.</param>
        public static void CreateLookAt(ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix result)
        {
            var vector = Vector3.Normalize(cameraPosition - cameraTarget);
            var vector2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector));
            var vector3 = Vector3.Cross(vector, vector2);
            result.m00 = vector2.x;
            result.m01 = vector3.x;
            result.m02 = vector.x;
            result.m03 = 0f;
            result.m10 = vector2.y;
            result.m11 = vector3.y;
            result.m12 = vector.y;
            result.m13 = 0f;
            result.m20 = vector2.z;
            result.m21 = vector3.z;
            result.m22 = vector.z;
            result.m23 = 0f;
            result.m30 = -Vector3.Dot(vector2, cameraPosition);
            result.m31 = -Vector3.Dot(vector3, cameraPosition);
            result.m32 = -Vector3.Dot(vector, cameraPosition);
            result.m33 = 1f;
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for orthographic view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <returns>The new projection <see cref="Matrix"/> for orthographic view.</returns>
        public static Matrix CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane)
        {
            CreateOrthographic(width, height, zNearPlane, zFarPlane, out Matrix matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for orthographic view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <param name="result">The new projection <see cref="Matrix"/> for orthographic view as an output parameter.</param>
        public static void CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane, out Matrix result)
        {
            result.m00 = 2f / width;
            result.m01 = result.m02 = result.m03 = 0f;
            result.m11 = 2f / height;
            result.m10 = result.m12 = result.m13 = 0f;
            result.m22 = 1f / (zNearPlane - zFarPlane);
            result.m20 = result.m21 = result.m23 = 0f;
            result.m30 = result.m31 = 0f;
            result.m32 = zNearPlane / (zNearPlane - zFarPlane);
            result.m33 = 1f;
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized orthographic view.
        /// </summary>
        /// <param name="left">Lower x-value at the near plane.</param>
        /// <param name="right">Upper x-value at the near plane.</param>
        /// <param name="bottom">Lower y-coordinate at the near plane.</param>
        /// <param name="top">Upper y-value at the near plane.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <returns>The new projection <see cref="Matrix"/> for customized orthographic view.</returns>
        public static Matrix CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane)
        {
            CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane, out Matrix matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized orthographic view.
        /// </summary>
        /// <param name="viewingVolume">The viewing volume.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <returns>The new projection <see cref="Matrix"/> for customized orthographic view.</returns>
        public static Matrix CreateOrthographicOffCenter(Rectangle viewingVolume, float zNearPlane, float zFarPlane)
        {
            CreateOrthographicOffCenter(viewingVolume.Left, viewingVolume.Right, viewingVolume.Bottom, viewingVolume.Top, zNearPlane, zFarPlane, out Matrix matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized orthographic view.
        /// </summary>
        /// <param name="left">Lower x-value at the near plane.</param>
        /// <param name="right">Upper x-value at the near plane.</param>
        /// <param name="bottom">Lower y-coordinate at the near plane.</param>
        /// <param name="top">Upper y-value at the near plane.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <param name="result">The new projection <see cref="Matrix"/> for customized orthographic view as an output parameter.</param>
        public static void CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane, out Matrix result)
        {
            result.m00 = (float)(2.0 / (right - (double)left));
            result.m01 = 0.0f;
            result.m02 = 0.0f;
            result.m03 = 0.0f;
            result.m10 = 0.0f;
            result.m11 = (float)(2.0 / (top - (double)bottom));
            result.m12 = 0.0f;
            result.m13 = 0.0f;
            result.m20 = 0.0f;
            result.m21 = 0.0f;
            result.m22 = (float)(1.0 / (zNearPlane - (double)zFarPlane));
            result.m23 = 0.0f;
            result.m30 = (float)((left + (double)right) / (left - (double)right));
            result.m31 = (float)((top + (double)bottom) / (bottom - (double)top));
            result.m32 = (float)(zNearPlane / (zNearPlane - (double)zFarPlane));
            result.m33 = 1.0f;
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for perspective view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane.</param>
        /// <returns>The new projection <see cref="Matrix"/> for perspective view.</returns>
        public static Matrix CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
        {
            CreatePerspective(width, height, nearPlaneDistance, farPlaneDistance, out Matrix matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for perspective view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane.</param>
        /// <param name="result">The new projection <see cref="Matrix"/> for perspective view as an output parameter.</param>
        public static void CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
        {
            if (nearPlaneDistance <= 0f)
            {
                throw new ArgumentException("nearPlaneDistance <= 0");
            }
            if (farPlaneDistance <= 0f)
            {
                throw new ArgumentException("farPlaneDistance <= 0");
            }
            if (nearPlaneDistance >= farPlaneDistance)
            {
                throw new ArgumentException("nearPlaneDistance >= farPlaneDistance");
            }
            result.m00 = (2f * nearPlaneDistance) / width;
            result.m01 = result.m02 = result.m03 = 0f;
            result.m11 = (2f * nearPlaneDistance) / height;
            result.m10 = result.m12 = result.m13 = 0f;
            result.m22 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.m20 = result.m21 = 0f;
            result.m23 = -1f;
            result.m30 = result.m31 = result.m33 = 0f;
            result.m32 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for perspective view with field of view.
        /// </summary>
        /// <param name="fieldOfView">Field of view in the y direction in radians.</param>
        /// <param name="aspectRatio">Width divided by height of the viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane.</param>
        /// <returns>The new projection <see cref="Matrix"/> for perspective view with FOV.</returns>
        public static Matrix CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
        {
            CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlaneDistance, farPlaneDistance, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for perspective view with field of view.
        /// </summary>
        /// <param name="fieldOfView">Field of view in the y direction in radians.</param>
        /// <param name="aspectRatio">Width divided by height of the viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance of the near plane.</param>
        /// <param name="farPlaneDistance">Distance of the far plane.</param>
        /// <param name="result">The new projection <see cref="Matrix"/> for perspective view with FOV as an output parameter.</param>
        public static void CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
        {
            if ((fieldOfView <= 0f) || (fieldOfView >= 3.141593f))
            {
                throw new ArgumentException("fieldOfView <= 0 or >= PI");
            }
            if (nearPlaneDistance <= 0f)
            {
                throw new ArgumentException("nearPlaneDistance <= 0");
            }
            if (farPlaneDistance <= 0f)
            {
                throw new ArgumentException("farPlaneDistance <= 0");
            }
            if (nearPlaneDistance >= farPlaneDistance)
            {
                throw new ArgumentException("nearPlaneDistance >= farPlaneDistance");
            }
            float num = 1f / ((float)Math.Tan(fieldOfView * 0.5f));
            float num9 = num / aspectRatio;
            result.m00 = num9;
            result.m01 = result.m02 = result.m03 = 0;
            result.m11 = num;
            result.m10 = result.m12 = result.m13 = 0;
            result.m20 = result.m21 = 0f;
            result.m22 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.m23 = -1;
            result.m30 = result.m31 = result.m33 = 0;
            result.m32 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized perspective view.
        /// </summary>
        /// <param name="left">Lower x-value at the near plane.</param>
        /// <param name="right">Upper x-value at the near plane.</param>
        /// <param name="bottom">Lower y-coordinate at the near plane.</param>
        /// <param name="top">Upper y-value at the near plane.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane.</param>
        /// <returns>The new <see cref="Matrix"/> for customized perspective view.</returns>
        public static Matrix CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
        {
            CreatePerspectiveOffCenter(left, right, bottom, top, nearPlaneDistance, farPlaneDistance, out Matrix result);
            return result;
        }
        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized perspective view.
        /// </summary>
        /// <param name="viewingVolume">The viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane.</param>
        /// <returns>The new <see cref="Matrix"/> for customized perspective view.</returns>
        public static Matrix CreatePerspectiveOffCenter(Rectangle viewingVolume, float nearPlaneDistance, float farPlaneDistance)
        {
            CreatePerspectiveOffCenter(viewingVolume.Left, viewingVolume.Right, viewingVolume.Bottom, viewingVolume.Top, nearPlaneDistance, farPlaneDistance, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized perspective view.
        /// </summary>
        /// <param name="left">Lower x-value at the near plane.</param>
        /// <param name="right">Upper x-value at the near plane.</param>
        /// <param name="bottom">Lower y-coordinate at the near plane.</param>
        /// <param name="top">Upper y-value at the near plane.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane.</param>
        /// <param name="result">The new <see cref="Matrix"/> for customized perspective view as an output parameter.</param>
        public static void CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
        {
            if (nearPlaneDistance <= 0f)
            {
                throw new ArgumentException("nearPlaneDistance <= 0");
            }
            if (farPlaneDistance <= 0f)
            {
                throw new ArgumentException("farPlaneDistance <= 0");
            }
            if (nearPlaneDistance >= farPlaneDistance)
            {
                throw new ArgumentException("nearPlaneDistance >= farPlaneDistance");
            }
            result.m00 = (2f * nearPlaneDistance) / (right - left);
            result.m01 = result.m02 = result.m03 = 0;
            result.m11 = (2f * nearPlaneDistance) / (top - bottom);
            result.m10 = result.m12 = result.m13 = 0;
            result.m20 = (left + right) / (right - left);
            result.m21 = (top + bottom) / (top - bottom);
            result.m22 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.m23 = -1;
            result.m32 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            result.m30 = result.m31 = result.m33 = 0;
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around X axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>The rotation <see cref="Matrix"/> around X axis.</returns>
        public static Matrix CreateRotationX(float radians)
        {
            CreateRotationX(radians, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around X axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> around X axis as an output parameter.</param>
        public static void CreateRotationX(float radians, out Matrix result)
        {
            result = Matrix.Identity;

            var val1 = (float)Math.Cos(radians);
            var val2 = (float)Math.Sin(radians);

            result.m11 = val1;
            result.m12 = val2;
            result.m21 = -val2;
            result.m22 = val1;
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around Y axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>The rotation <see cref="Matrix"/> around Y axis.</returns>
        public static Matrix CreateRotationY(float radians)
        {
            CreateRotationY(radians, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around Y axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> around Y axis as an output parameter.</param>
        public static void CreateRotationY(float radians, out Matrix result)
        {
            result = Matrix.Identity;

            var val1 = (float)Math.Cos(radians);
            var val2 = (float)Math.Sin(radians);

            result.m00 = val1;
            result.m02 = -val2;
            result.m20 = val2;
            result.m22 = val1;
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around Z axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>The rotation <see cref="Matrix"/> around Z axis.</returns>
        public static Matrix CreateRotationZ(float radians)
        {
            CreateRotationZ(radians, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around Z axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> around Z axis as an output parameter.</param>
        public static void CreateRotationZ(float radians, out Matrix result)
        {
            result = Matrix.Identity;

            var val1 = (float)Math.Cos(radians);
            var val2 = (float)Math.Sin(radians);

            result.m00 = val1;
            result.m01 = val2;
            result.m10 = -val2;
            result.m11 = val1;
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="scale">Scale value for all three axises.</param>
        /// <returns>The scaling <see cref="Matrix"/>.</returns>
        public static Matrix CreateScale(float scale)
        {
            CreateScale(scale, scale, scale, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="scale">Scale value for all three axises.</param>
        /// <param name="result">The scaling <see cref="Matrix"/> as an output parameter.</param>
        public static void CreateScale(float scale, out Matrix result)
        {
            CreateScale(scale, scale, scale, out result);
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="xScale">Scale value for X axis.</param>
        /// <param name="yScale">Scale value for Y axis.</param>
        /// <param name="zScale">Scale value for Z axis.</param>
        /// <returns>The scaling <see cref="Matrix"/>.</returns>
        public static Matrix CreateScale(float xScale, float yScale, float zScale)
        {
            CreateScale(xScale, yScale, zScale, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="xScale">Scale value for X axis.</param>
        /// <param name="yScale">Scale value for Y axis.</param>
        /// <param name="zScale">Scale value for Z axis.</param>
        /// <param name="result">The scaling <see cref="Matrix"/> as an output parameter.</param>
        public static void CreateScale(float xScale, float yScale, float zScale, out Matrix result)
        {
            result.m00 = xScale;
            result.m01 = 0;
            result.m02 = 0;
            result.m03 = 0;
            result.m10 = 0;
            result.m11 = yScale;
            result.m12 = 0;
            result.m13 = 0;
            result.m20 = 0;
            result.m21 = 0;
            result.m22 = zScale;
            result.m23 = 0;
            result.m30 = 0;
            result.m31 = 0;
            result.m32 = 0;
            result.m33 = 1;
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="scales"><see cref="Vector3"/> representing x,y and z scale values.</param>
        /// <returns>The scaling <see cref="Matrix"/>.</returns>
        public static Matrix CreateScale(Vector3 scales)
        {
            CreateScale(ref scales, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="scales"><see cref="Vector3"/> representing x,y and z scale values.</param>
        /// <param name="result">The scaling <see cref="Matrix"/> as an output parameter.</param>
        public static void CreateScale(ref Vector3 scales, out Matrix result)
        {
            result.m00 = scales.x;
            result.m01 = 0;
            result.m02 = 0;
            result.m03 = 0;
            result.m10 = 0;
            result.m11 = scales.y;
            result.m12 = 0;
            result.m13 = 0;
            result.m20 = 0;
            result.m21 = 0;
            result.m22 = scales.z;
            result.m23 = 0;
            result.m30 = 0;
            result.m31 = 0;
            result.m32 = 0;
            result.m33 = 1;
        }


        /// <summary>
        /// Creates a new <see cref="Matrix"/> that flattens geometry into a specified <see cref="Plane"/> as if casting a shadow from a specified light source. 
        /// </summary>
        /// <param name="lightDirection">A vector specifying the direction from which the light that will cast the shadow is coming.</param>
        /// <param name="plane">The plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
        /// <returns>A <see cref="Matrix"/> that can be used to flatten geometry onto the specified plane from the specified direction. </returns>
        public static Matrix CreateShadow(Vector3 lightDirection, Plane plane)
        {
            CreateShadow(ref lightDirection, ref plane, out Matrix result);
            return result;
        }


        /// <summary>
        /// Creates a new <see cref="Matrix"/> that flattens geometry into a specified <see cref="Plane"/> as if casting a shadow from a specified light source. 
        /// </summary>
        /// <param name="lightDirection">A vector specifying the direction from which the light that will cast the shadow is coming.</param>
        /// <param name="plane">The plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
        /// <param name="result">A <see cref="Matrix"/> that can be used to flatten geometry onto the specified plane from the specified direction as an output parameter.</param>
        public static void CreateShadow(ref Vector3 lightDirection, ref Plane plane, out Matrix result)
        {
            float dot = (plane.Normal.x * lightDirection.x) + (plane.Normal.y * lightDirection.y) + (plane.Normal.z * lightDirection.z);
            float x = -plane.Normal.x;
            float y = -plane.Normal.y;
            float z = -plane.Normal.z;
            float d = -plane.D;

            result.m00 = (x * lightDirection.x) + dot;
            result.m01 = x * lightDirection.y;
            result.m02 = x * lightDirection.z;
            result.m03 = 0;
            result.m10 = y * lightDirection.x;
            result.m11 = (y * lightDirection.y) + dot;
            result.m12 = y * lightDirection.z;
            result.m13 = 0;
            result.m20 = z * lightDirection.x;
            result.m21 = z * lightDirection.y;
            result.m22 = (z * lightDirection.z) + dot;
            result.m23 = 0;
            result.m30 = d * lightDirection.x;
            result.m31 = d * lightDirection.y;
            result.m32 = d * lightDirection.z;
            result.m33 = dot;
        }

        /// <summary>
        /// Creates a new translation <see cref="Matrix"/>.
        /// </summary>
        /// <param name="xPosition">X coordinate of translation.</param>
        /// <param name="yPosition">Y coordinate of translation.</param>
        /// <param name="zPosition">Z coordinate of translation.</param>
        /// <returns>The translation <see cref="Matrix"/>.</returns>
        public static Matrix CreateTranslation(float xPosition, float yPosition, float zPosition)
        {
            CreateTranslation(xPosition, yPosition, zPosition, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new translation <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">X,Y and Z coordinates of translation.</param>
        /// <param name="result">The translation <see cref="Matrix"/> as an output parameter.</param>
        public static void CreateTranslation(ref Vector3 position, out Matrix result)
        {
            result.m00 = 1;
            result.m01 = 0;
            result.m02 = 0;
            result.m03 = 0;
            result.m10 = 0;
            result.m11 = 1;
            result.m12 = 0;
            result.m13 = 0;
            result.m20 = 0;
            result.m21 = 0;
            result.m22 = 1;
            result.m23 = 0;
            result.m30 = position.x;
            result.m31 = position.y;
            result.m32 = position.z;
            result.m33 = 1;
        }

        /// <summary>
        /// Creates a new translation <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">X,Y and Z coordinates of translation.</param>
        /// <returns>The translation <see cref="Matrix"/>.</returns>
        public static Matrix CreateTranslation(Vector3 position)
        {
            CreateTranslation(ref position, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new translation <see cref="Matrix"/>.
        /// </summary>
        /// <param name="xPosition">X coordinate of translation.</param>
        /// <param name="yPosition">Y coordinate of translation.</param>
        /// <param name="zPosition">Z coordinate of translation.</param>
        /// <param name="result">The translation <see cref="Matrix"/> as an output parameter.</param>
        public static void CreateTranslation(float xPosition, float yPosition, float zPosition, out Matrix result)
        {
            result.m00 = 1;
            result.m01 = 0;
            result.m02 = 0;
            result.m03 = 0;
            result.m10 = 0;
            result.m11 = 1;
            result.m12 = 0;
            result.m13 = 0;
            result.m20 = 0;
            result.m21 = 0;
            result.m22 = 1;
            result.m23 = 0;
            result.m30 = xPosition;
            result.m31 = yPosition;
            result.m32 = zPosition;
            result.m33 = 1;
        }

        /// <summary>
        /// Creates a new reflection <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">The plane that used for reflection calculation.</param>
        /// <returns>The reflection <see cref="Matrix"/>.</returns>
        public static Matrix CreateReflection(Plane value)
        {
            CreateReflection(ref value, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new reflection <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">The plane that used for reflection calculation.</param>
        /// <param name="result">The reflection <see cref="Matrix"/> as an output parameter.</param>
        public static void CreateReflection(ref Plane value, out Matrix result)
        {
            Plane.Normalize(ref value, out Plane plane);
            float x = plane.Normal.x;
            float y = plane.Normal.y;
            float z = plane.Normal.z;
            float num3 = -2f * x;
            float num2 = -2f * y;
            float num = -2f * z;
            result.m00 = (num3 * x) + 1f;
            result.m01 = num2 * x;
            result.m02 = num * x;
            result.m03 = 0;
            result.m10 = num3 * y;
            result.m11 = (num2 * y) + 1;
            result.m12 = num * y;
            result.m13 = 0;
            result.m20 = num3 * z;
            result.m21 = num2 * z;
            result.m22 = (num * z) + 1;
            result.m23 = 0;
            result.m30 = num3 * plane.D;
            result.m31 = num2 * plane.D;
            result.m32 = num * plane.D;
            result.m33 = 1;
        }

        /// <summary>
        /// Creates a new world <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">The position vector.</param>
        /// <param name="forward">The forward direction vector.</param>
        /// <param name="up">The upward direction vector. Usually <see cref="Vector3.Up"/>.</param>
        /// <returns>The world <see cref="Matrix"/>.</returns>
        public static Matrix CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
        {
            CreateWorld(ref position, ref forward, ref up, out Matrix ret);
            return ret;
        }

        /// <summary>
        /// Creates a new world <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">The position vector.</param>
        /// <param name="forward">The forward direction vector.</param>
        /// <param name="up">The upward direction vector. Usually <see cref="Vector3.Up"/>.</param>
        /// <param name="result">The world <see cref="Matrix"/> as an output parameter.</param>
        public static void CreateWorld(ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix result)
        {
            Vector3.Normalize(ref forward, out Vector3 z);
            Vector3.Cross(ref forward, ref up, out Vector3 x);
            Vector3.Cross(ref x, ref forward, out Vector3 y);
            x.Normalize();
            y.Normalize();

            result = new Matrix();
            result.Right = x;
            result.Up = y;
            result.Forward = z;
            result.Translation = position;
            result.m33 = 1f;
        }

        /// <summary>
        /// Decomposes this matrix to translation, rotation and scale elements. Returns <c>true</c> if matrix can be decomposed; <c>false</c> otherwise.
        /// </summary>
        /// <param name="scale">Scale vector as an output parameter.</param>
        /// <param name="rotation">Rotation quaternion as an output parameter.</param>
        /// <param name="translation">Translation vector as an output parameter.</param>
        /// <returns><c>true</c> if matrix can be decomposed; <c>false</c> otherwise.</returns>
        public bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            translation.x = this.m30;
            translation.y = this.m31;
            translation.z = this.m32;

            float xs = (Math.Sign(m00 * m01 * m02 * m03) < 0) ? -1 : 1;
            float ys = (Math.Sign(m10 * m11 * m12 * m13) < 0) ? -1 : 1;
            float zs = (Math.Sign(m20 * m21 * m22 * m23) < 0) ? -1 : 1;

            scale.x = xs * (float)Math.Sqrt(this.m00 * this.m00 + this.m01 * this.m01 + this.m02 * this.m02);
            scale.y = ys * (float)Math.Sqrt(this.m10 * this.m10 + this.m11 * this.m11 + this.m12 * this.m12);
            scale.z = zs * (float)Math.Sqrt(this.m20 * this.m20 + this.m21 * this.m21 + this.m22 * this.m22);

            if (scale.x == 0.0 || scale.y == 0.0 || scale.z == 0.0)
            {
                rotation = Quaternion.Identity;
                return false;
            }

            Matrix m1 = new Matrix(this.m00 / scale.x, m01 / scale.x, m02 / scale.x, 0,
                                   this.m10 / scale.y, m11 / scale.y, m12 / scale.y, 0,
                                   this.m20 / scale.z, m21 / scale.z, m22 / scale.z, 0,
                                   0, 0, 0, 1);

            rotation = Quaternion.CreateFromRotationMatrix(m1);
            return true;
        }
        */

        /// <summary>
        /// Compares whether current instance is equal to a specified <see cref="Matrix"/> without any tolerance.
        /// </summary>
        /// <param name="other">The <see cref="Matrix"/> to compare with.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Matrix other)
        {
            return
                Row0 == other.Row0 &&
                Row1 == other.Row1 &&
                Row2 == other.Row2 &&
                Row3 == other.Row3;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/> without any tolerance.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Matrix)
            {
                return Equals((Matrix)obj);
            }
            return false;
        }

        /// <summary>
        /// Gets the hash code of this <see cref="Matrix"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="Matrix"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Row0.GetHashCode();
                hashCode = (hashCode * 397) ^ Row1.GetHashCode();
                hashCode = (hashCode * 397) ^ Row2.GetHashCode();
                hashCode = (hashCode * 397) ^ Row3.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="Matrix"/>.
        /// </summary>
        /// <returns>The string representation of the matrix.</returns>
        public override string ToString()
        {
            return $"{Row0}\n{Row1}\n{Row2}\n{Row3}";
        }

        public static Matrix operator +(Matrix left, Matrix right) => Add(left, right);
        public static Matrix operator -(Matrix left, Matrix right) => Subtract(left, right);

        public static Matrix operator -(Matrix matrix) => Negate(matrix);

        public static Matrix operator *(Matrix left, Matrix right) => Multiply(left, right);
        public static Matrix operator *(Matrix matrix, float scaleFactor) => Multiply(matrix, scaleFactor);
        public static Matrix operator *(float scaleFactor, Matrix matrix) => Multiply(matrix, scaleFactor);

        public static Matrix operator /(Matrix left, Matrix right) => Divide(left, right);
        public static Matrix operator /(Matrix matrix, float divider) => Divide(matrix, divider);

        public static bool operator ==(Matrix left, Matrix right) => left.Equals(right);
        public static bool operator !=(Matrix left, Matrix right) => !left.Equals(right);

        /// <summary>
        /// Cast the matrix to a <see cref="OpenTK.Matrix4"/>.
        /// </summary>
        /// <param name="matrix">The matrix to cast.</param>
        /// <returns>The casted instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator OpenTK.Matrix4(Matrix matrix)
        {
            return Unsafe.ReinterpretCast<Matrix, OpenTK.Matrix4>(matrix);
        }
    }
}