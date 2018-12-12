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
        /// The left vector formed from the first row -m00, -m01, -m02 elements.
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
        /// The right vector formed from the first row m00, m01, m02 elements.
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
        /// The down vector formed from the second row -m10, -m11, -m12 elements.
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
        /// The upper vector formed from the second row m10, m11, m12 elements.
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
        /// The forward vector formed from the third row -m20, -m21, -m22 elements.
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
        /// The backward vector formed from the third row m20, m21, m22 elements.
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
        /// The determinant of this matrix.
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
                float num1 = m33;

                float num18 = (num6 * num1) - (num5 * num2);
                float num17 = (num7 * num1) - (num5 * num3);
                float num16 = (num7 * num2) - (num6 * num3);
                float num15 = (num8 * num1) - (num5 * num4);
                float num14 = (num8 * num2) - (num6 * num4);
                float num13 = (num8 * num3) - (num7 * num4);

                return
                    (num22 * ((num11 * num18) - (num10 * num17) + (num9 * num16))) -
                    (num21 * ((num12 * num18) - (num10 * num15) + (num9 * num14))) +
                    (num20 * ((num12 * num17) - (num11 * num15) + (num9 * num13))) -
                    (num19 * ((num12 * num16) - (num11 * num14) + (num10 * num13))
                );
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
            switch (index)
            {
                case 0: Row0 = row; break;
                case 1: Row1 = row; break;
                case 2: Row2 = row; break;
                case 3: Row3 = row; break;
                default:
                    throw new IndexOutOfRangeException();
            }
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
        /// Creates a new matrix which contains sum of two matrixes.
        /// </summary>
        /// <param name="left">The left operand of the addition.</param>
        /// <param name="right">The right operand of the addition.</param>
        public static Matrix Add(Matrix left, Matrix right)
        {
            Add(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Creates a new matrix which contains sum of two matrixes.
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
        /// Creates a new matrix which contains difference of two matrixes.
        /// </summary>
        /// <param name="left">The left operand of the subtraction.</param>
        /// <param name="right">The right operand of the subtraction.</param>
        /// <returns>The result of the matrix subtraction.</returns>
        public static Matrix Subtract(Matrix left, Matrix right)
        {
            Subtract(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Creates a new matrix which contains difference of two matrixes.
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
            Negate(ref matrix, out matrix);
            return matrix;
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
        /// Creates a new matrix that contains a multiplication of two matrix.
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
        /// Creates a new matrix that contains a multiplication of two matrices.
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
        /// Creates a new matrix that contains a multiplication of a matrix and a scalar.
        /// </summary>
        /// <param name="matrix">The matrix to scale.</param>
        /// <param name="scale">Scalar value.</param>
        /// <returns>Result of the matrix multiplication with a scalar.</returns>
        public static Matrix Multiply(Matrix matrix, float scale)
        {
            Multiply(ref matrix, scale, out matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new matrix that contains a multiplication of a matrix and a scalar.
        /// </summary>
        /// <param name="matrix">The matrix to scale.</param>
        /// <param name="scale">Scalar value.</param>
        /// <param name="result">Result of the matrix multiplication with a scalar as an output parameter.</param>
        public static void Multiply(ref Matrix matrix, float scale, out Matrix result)
        {
            result.m00 = matrix.m00 * scale;
            result.m01 = matrix.m01 * scale;
            result.m02 = matrix.m02 * scale;
            result.m03 = matrix.m03 * scale;
            result.m10 = matrix.m10 * scale;
            result.m11 = matrix.m11 * scale;
            result.m12 = matrix.m12 * scale;
            result.m13 = matrix.m13 * scale;
            result.m20 = matrix.m20 * scale;
            result.m21 = matrix.m21 * scale;
            result.m22 = matrix.m22 * scale;
            result.m23 = matrix.m23 * scale;
            result.m30 = matrix.m30 * scale;
            result.m31 = matrix.m31 * scale;
            result.m32 = matrix.m32 * scale;
            result.m33 = matrix.m33 * scale;
        }

        /// <summary>
        /// Divides the elements of a matrix by the elements of another matrix.
        /// </summary>
        /// <param name="left">The matrix of numerators.</param>
        /// <param name="right">The matrix of denominators.</param>
        /// <returns>The result of dividing the matrix.</returns>
        public static Matrix Divide(Matrix left, Matrix right)
        {
            Divide(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Divides the elements of a matrix by the elements of another matrix.
        /// </summary>
        /// <param name="left">The matrix of numerators.</param>
        /// <param name="right">The matrix of denominators.</param>
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

        /// Divides the elements of a matrix by a scalar.
        /// </summary>
        /// <param name="matrix">The matrix to divide.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <returns>The result of dividing a matrix by a scalar.</returns>
        public static Matrix Divide(Matrix matrix, float divider)
        {
            Divide(ref matrix, divider, out matrix);
            return matrix;
        }

        /// <summary>
        /// Divides the elements of a matrix by a scalar.
        /// </summary>
        /// <param name="matrix">The matrix to divide.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <param name="result">The result of dividing a matrix by a scalar as an output parameter.</param>
        public static void Divide(ref Matrix matrix, float divider, out Matrix result)
        {
            float scale = 1f / divider;
            result.m00 = matrix.m00 * scale;
            result.m01 = matrix.m01 * scale;
            result.m02 = matrix.m02 * scale;
            result.m03 = matrix.m03 * scale;
            result.m10 = matrix.m10 * scale;
            result.m11 = matrix.m11 * scale;
            result.m12 = matrix.m12 * scale;
            result.m13 = matrix.m13 * scale;
            result.m20 = matrix.m20 * scale;
            result.m21 = matrix.m21 * scale;
            result.m22 = matrix.m22 * scale;
            result.m23 = matrix.m23 * scale;
            result.m30 = matrix.m30 * scale;
            result.m31 = matrix.m31 * scale;
            result.m32 = matrix.m32 * scale;
            result.m33 = matrix.m33 * scale;
        }

        /// <summary>
        /// Creates a new matrix that contains linear interpolation of the values in specified matrixes.
        /// </summary>
        /// <param name="a">The matrix to interpolate from.</param>
        /// <param name="b">The matrix to interpolate to.</param>
        /// <param name="t">Weighting value between 0 and 1.</param>
        public static Matrix Lerp(Matrix a, Matrix b, float t)
        {
            Lerp(ref a, ref b, t, out a);
            return a;
        }

        /// <summary>
        /// Creates a new matrix that contains linear interpolation of the values in specified matrixes.
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
        public static Matrix Transpose(Matrix matrix)
        {
            Transpose(ref matrix, out Matrix result);
            return result;
        }

        /// <summary>
        /// Swaps the rows and columns of a matrix.
        /// </summary>
        /// <param name="matrix">The matrix to tranpose.</param>
        /// <returns>The result as an output parameter.</returns>
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
        /// Creates a new matrix which contains inversion of the specified matrix. 
        /// </summary>
        /// <param name="matrix">The matrix to inver.</param>
        public static Matrix Invert(Matrix matrix)
        {
            Invert(ref matrix, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new matrix which contains inversion of the specified matrix. 
        /// </summary>
        /// <param name="matrix">The matrix to inver.</param>
        /// <param name="result">The inverted matrix as output parameter.</param>
        public static void Invert(ref Matrix matrix, out Matrix result)
        {
            float num1 = matrix.m00;
            float num2 = matrix.m01;
            float num3 = matrix.m02;
            float num4 = matrix.m03;
            float num5 = matrix.m10;
            float num6 = matrix.m11;
            float num7 = matrix.m12;
            float num8 = matrix.m13;
            float num9 = matrix.m20;
            float num10 = matrix.m21;
            float num11 = matrix.m22;
            float num12 = matrix.m23;
            float num13 = matrix.m30;
            float num14 = matrix.m31;
            float num15 = matrix.m32;
            float num16 = matrix.m33;

            float num17 = num11 * num16 - num12 * num15;
            float num18 = num10 * num16 - num12 * num14;
            float num19 = num10 * num15 - num11 * num14;
            float num20 = num9 * num16 - num12 * num13;
            float num21 = num9 * num15 - num11 * num13;
            float num22 = num9 * num14 - num10 * num13;
            float num23 = num6 * num17 - num7 * num18 + num8 * num19;
            float num24 = -(num5 * num17 - num7 * num20 + num8 * num21);
            float num25 = num5 * num18 - num6 * num20 + num8 * num22;
            float num26 = -(num5 * num19 - num6 * num21 + num7 * num22);
            float num27 = 1.0f / (num1 * num23 + num2 * num24 + num3 * num25 + num4 * num26);

            result.m00 = num23 * num27;
            result.m10 = num24 * num27;
            result.m20 = num25 * num27;
            result.m30 = num26 * num27;
            result.m01 = -(num2 * num17 - num3 * num18 + num4 * num19) * num27;
            result.m11 = (num1 * num17 - num3 * num20 + num4 * num21) * num27;
            result.m21 = -(num1 * num18 - num2 * num20 + num4 * num22) * num27;
            result.m31 = (num1 * num19 - num2 * num21 + num3 * num22) * num27;
            float num28 = num7 * num16 - num8 * num15;
            float num29 = num6 * num16 - num8 * num14;
            float num30 = num6 * num15 - num7 * num14;
            float num31 = num5 * num16 - num8 * num13;
            float num32 = num5 * num15 - num7 * num13;
            float num33 = num5 * num14 - num6 * num13;
            result.m02 = (num2 * num28 - num3 * num29 + num4 * num30) * num27;
            result.m12 = -(num1 * num28 - num3 * num31 + num4 * num32) * num27;
            result.m22 = (num1 * num29 - num2 * num31 + num4 * num33) * num27;
            result.m32 = -(num1 * num30 - num2 * num32 + num3 * num33) * num27;
            float num34 = num7 * num12 - num8 * num11;
            float num35 = num6 * num12 - num8 * num10;
            float num36 = num6 * num11 - num7 * num10;
            float num37 = num5 * num12 - num8 * num9;
            float num38 = num5 * num11 - num7 * num9;
            float num39 = num5 * num10 - num6 * num9;
            result.m03 = -(num2 * num34 - num3 * num35 + num4 * num36) * num27;
            result.m13 = (num1 * num34 - num3 * num37 + num4 * num38) * num27;
            result.m23 = -(num1 * num35 - num2 * num37 + num4 * num39) * num27;
            result.m33 = (num1 * num36 - num2 * num38 + num3 * num39) * num27;
        }

        /// <summary>
        /// Creates a new matrix which contains inversion of the specified matrix with greater precision.
        /// </summary>
        /// <param name="matrix">The matrix to inver.</param>
        /// <returns>The inverted matrix.</returns>
        public static Matrix InvertPrecise(Matrix matrix)
        {
            InvertPrecise(ref matrix, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new matrix which contains inversion of the specified matrix with greater precision.
        /// </summary>
        /// <param name="matrix">The matrix to inver.</param>
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

            double num17 = num11 * num16 - num12 * num15;
            double num18 = num10 * num16 - num12 * num14;
            double num19 = num10 * num15 - num11 * num14;
            double num20 = num9 * num16 - num12 * num13;
            double num21 = num9 * num15 - num11 * num13;
            double num22 = num9 * num14 - num10 * num13;
            double num23 = num6 * num17 - num7 * num18 + num8 * num19;
            double num24 = -(num5 * num17 - num7 * num20 + num8 * num21);
            double num25 = num5 * num18 - num6 * num20 + num8 * num22;
            double num26 = -(num5 * num19 - num6 * num21 + num7 * num22);
            double num27 = 1.0 / (num1 * num23 + num2 * num24 + num3 * num25 + num4 * num26);

            result.m00 = (float)(num23 * num27);
            result.m10 = (float)(num24 * num27);
            result.m20 = (float)(num25 * num27);
            result.m30 = (float)(num26 * num27);
            result.m01 = (float)(-(num2 * num17 - num3 * num18 + num4 * num19) * num27);
            result.m11 = (float)((num1 * num17 - num3 * num20 + num4 * num21) * num27);
            result.m21 = (float)(-(num1 * num18 - num2 * num20 + num4 * num22) * num27);
            result.m31 = (float)((num1 * num19 - num2 * num21 + num3 * num22) * num27);
            double num28 = num7 * num16 - num8 * num15;
            double num29 = num6 * num16 - num8 * num14;
            double num30 = num6 * num15 - num7 * num14;
            double num31 = num5 * num16 - num8 * num13;
            double num32 = num5 * num15 - num7 * num13;
            double num33 = num5 * num14 - num6 * num13;
            result.m02 = (float)((num2 * num28 - num3 * num29 + num4 * num30) * num27);
            result.m12 = (float)(-(num1 * num28 - num3 * num31 + num4 * num32) * num27);
            result.m22 = (float)((num1 * num29 - num2 * num31 + num4 * num33) * num27);
            result.m32 = (float)(-(num1 * num30 - num2 * num32 + num3 * num33) * num27);
            double num34 = num7 * num12 - num8 * num11;
            double num35 = num6 * num12 - num8 * num10;
            double num36 = num6 * num11 - num7 * num10;
            double num37 = num5 * num12 - num8 * num9;
            double num38 = num5 * num11 - num7 * num9;
            double num39 = num5 * num10 - num6 * num9;
            result.m03 = (float)(-(num2 * num34 - num3 * num35 + num4 * num36) * num27);
            result.m13 = (float)((num1 * num34 - num3 * num37 + num4 * num38) * num27);
            result.m23 = (float)(-(num1 * num35 - num2 * num37 + num4 * num39) * num27);
            result.m33 = (float)((num1 * num36 - num2 * num38 + num3 * num39) * num27);
        }

        /// <summary>
        /// Creates a new matrix for spherical billboarding.
        /// </summary>
        /// <param name="position">Position of billboard object.</param>
        /// <param name="targetDirection">The direction to face along.</param>
        /// <param name="up">The upwards direction.</param>
        public static Matrix CreateBillboard(Vector3 position, Vector3 targetDirection, Vector3 up)
        {
            CreateBillboard(ref position, ref targetDirection, ref up, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new matrix for spherical billboarding.
        /// </summary>
        /// <param name="position">Position of billboard object.</param>
        /// <param name="targetDirection">The direction to face along.</param>
        /// <param name="up">The camera up vector.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void CreateBillboard(ref Vector3 position, ref Vector3 targetDirection, ref Vector3 up, out Matrix result)
        {
            Vector3 f = -targetDirection;
            Vector3 u = up;

            Vector3.OrthoNormalize(ref f, ref u, out Vector3 r);
            
            result.m00 = r.x;
            result.m01 = r.y;
            result.m02 = r.z;
            result.m03 = 0f;

            result.m10 = u.x;
            result.m11 = u.y;
            result.m12 = u.z;
            result.m13 = 0f;

            result.m20 = f.x;
            result.m21 = f.y;
            result.m22 = f.z;
            result.m23 = 0f;

            result.m30 = position.x;
            result.m31 = position.y;
            result.m32 = position.z;
            result.m33 = 1f;
        }

        /// <summary>
        /// Creates a new rotation matrix.
        /// </summary>
        /// <param name="direction">The direction to look along.</param>
        /// <param name="up">The direction of the upper edge of the camera.</param>
        public static Matrix CreateLookAt(Vector3 direction, Vector3 up)
        {
            CreateLookAt(ref direction, ref up, out Matrix matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new rotation matrix.
        /// </summary>
        /// <param name="direction">The direction to look along.</param>
        /// <param name="up">The direction of the upper edge of the camera.</param>
        /// <param name="result">The viewing <see cref="Matrix"/> as an output parameter.</param>
        public static void CreateLookAt(ref Vector3 direction, ref Vector3 up, out Matrix result)
        {
            Vector3 f = direction;
            Vector3 u = up;
            Vector3.OrthoNormalize(ref f, ref u, out Vector3 r);

            result.m00 = u.x;
            result.m01 = r.x;
            result.m02 = f.x;
            result.m03 = 0f;

            result.m10 = u.y;
            result.m11 = r.y;
            result.m12 = f.y;
            result.m13 = 0f;

            result.m20 = u.z;
            result.m21 = r.z;
            result.m22 = f.z;
            result.m23 = 0f;

            result.m30 = 0f;
            result.m31 = 0f;
            result.m32 = 0f;
            result.m33 = 1f;
        }

        /// <summary>
        /// Creates a new rotation matrix.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        public static Matrix CreateFromAxisAngle(Vector3 axis, float angle)
        {
            CreateFromAxisAngle(ref axis, angle, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new rotation matrix.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Matrix result)
        {
            Vector3.Normalize(ref axis, out Vector3 a);

            float xx = a.x * a.x;
            float xy = a.x * a.y;

            float yy = a.y * a.y;
            float yz = a.y * a.z;

            float zz = a.z * a.z;
            float xz = a.x * a.z;

            float sin = (float)Math.Sin(angle);
            float cos = (float)Math.Cos(angle);

            result.m00 = xx + (cos * (1f - xx));
            result.m01 = xy - (cos * xy) + (sin * a.z);
            result.m02 = xz - (cos * xz) - (sin * a.y);
            result.m03 = 0;

            result.m10 = xy - (cos * xy) - (sin * a.z);
            result.m11 = yy + (cos * (1f - yy));
            result.m12 = yz - (cos * yz) + (sin * a.x);
            result.m13 = 0;

            result.m20 = xz - (cos * xz) + (sin * a.y);
            result.m21 = yz - (cos * yz) - (sin * a.x);
            result.m22 = zz + (cos * (1f - zz));
            result.m23 = 0;

            result.m30 = 0;
            result.m31 = 0;
            result.m32 = 0;
            result.m33 = 1;
        }

        /// <summary>
        /// Creates a new rotation matrix.
        /// </summary>
        /// <param name="q">The rotation.</param>
        public static Matrix CreateFromQuaternion(Quaternion q)
        {
            CreateFromQuaternion(ref q, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new rotation matrix.
        /// </summary>
        /// <param name="q">The rotation.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void CreateFromQuaternion(ref Quaternion q, out Matrix result)
        {
            float xx = q.x * q.x;
            float xy = q.x * q.y;
            float xz = q.z * q.x;
            float xw = q.x * q.w;

            float yy = q.y * q.y;
            float yz = q.y * q.z;
            float yw = q.y * q.w;

            float zz = q.z * q.z;
            float zw = q.z * q.w;

            result.m00 = 1f - (2f * (yy + zz));
            result.m01 = 2f * (xy + zw);
            result.m02 = 2f * (xz - yw);
            result.m03 = 0f;

            result.m10 = 2f * (xy - zw);
            result.m11 = 1f - (2f * (xx + zz));
            result.m12 = 2f * (yz + xw);
            result.m13 = 0f;

            result.m20 = 2f * (xz + yw);
            result.m21 = 2f * (yz - xw);
            result.m22 = 1f - (2f * (xx + yy));
            result.m23 = 0f;

            result.m30 = 0f;
            result.m31 = 0f;
            result.m32 = 0f;
            result.m33 = 1f;
        }

        /// <summary>
        /// Creates a new projection matrix for an orthographic view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="near">Depth of the near plane.</param>
        /// <param name="far">Depth of the far plane.</param>
        public static Matrix CreateOrthographic(float width, float height, float near, float far)
        {
            CreateOrthographic(width, height, near, far, out Matrix matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new projection matrix for an orthographic view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="near">Depth of the near plane.</param>
        /// <param name="far">Depth of the far plane.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void CreateOrthographic(float width, float height, float near, float far, out Matrix result)
        {
            result.m00 = 2f / width;
            result.m01 = 0f;
            result.m02 = 0f;
            result.m03 = 0f;

            result.m10 = 0f;
            result.m11 = 2f / height;
            result.m12 = 0f;
            result.m13 = 0f;

            result.m20 = 0f;
            result.m21 = 0f;
            result.m22 = -2f / (far - near);
            result.m23 = 0f;

            result.m30 = 0f;
            result.m31 = 0f;
            result.m32 = -(far + near) / (far - near);
            result.m33 = 1f;
        }

        /// <summary>
        /// Creates a new projection matrix for a customized orthographic view.
        /// </summary>
        /// <param name="left">Lower x coordinate at the near plane.</param>
        /// <param name="right">Upper x coordinate at the near plane.</param>
        /// <param name="bottom">Lower y coordinate at the near plane.</param>
        /// <param name="top">Upper y coordinate at the near plane.</param>
        /// <param name="near">Depth of the near plane.</param>
        /// <param name="far">Depth of the far plane.</param>
        public static Matrix CreateOrthographicOffCenter(float left, float right, float bottom, float top, float near, float far)
        {
            CreateOrthographicOffCenter(left, right, bottom, top, near, far, out Matrix matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new projection matrix for a customized orthographic view.
        /// </summary>
        /// <param name="volume">The viewing volume.</param>
        /// <param name="near">Depth of the near plane.</param>
        /// <param name="far">Depth of the far plane.</param>
        public static Matrix CreateOrthographicOffCenter(Rect volume, float near, float far)
        {
            CreateOrthographicOffCenter(volume.xMin, volume.xMax, volume.yMin, volume.yMax, near, far, out Matrix matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new projection matrix for a customized orthographic view.
        /// </summary>
        /// <param name="left">Lower x coordinate at the near plane.</param>
        /// <param name="right">Upper x coordinate at the near plane.</param>
        /// <param name="bottom">Lower y coordinate at the near plane.</param>
        /// <param name="top">Upper y coordinate at the near plane.</param>
        /// <param name="near">Depth of the near plane.</param>
        /// <param name="far">Depth of the far plane.</param>
        public static void CreateOrthographicOffCenter(float left, float right, float bottom, float top, float near, float far, out Matrix result)
        {
            result.m00 = 2f / (right - left);
            result.m01 = 0f;
            result.m02 = 0f;
            result.m03 = 0f;

            result.m10 = 0f;
            result.m11 = 2f / (top - bottom);
            result.m12 = 0f;
            result.m13 = 0f;

            result.m20 = 0f;
            result.m21 = 0f;
            result.m22 = -2f / (far - near);
            result.m23 = 0f;

            result.m30 = -(right + left) / (right - left);
            result.m31 = -(top + bottom) / (top - bottom);
            result.m32 = -(far + near) / (far - near);
            result.m33 = 1f;
        }

        /// <summary>
        /// Creates a new projection matrix for a perspective view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="near">Distance to the near plane.</param>
        /// <param name="far">Distance to the far plane.</param>
        public static Matrix CreatePerspective(float width, float height, float near, float far)
        {
            CreatePerspective(width, height, near, far, out Matrix matrix);
            return matrix;
        }

        /// <summary>
        /// Creates a new projection matrix for a perspective view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="near">Distance to the near plane.</param>
        /// <param name="far">Distance to the far plane.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void CreatePerspective(float width, float height, float near, float far, out Matrix result)
        {
            result.m00 = (2f * near) / width;
            result.m01 = 0f;
            result.m02 = 0f;
            result.m03 = 0f;

            result.m10 = 0f;
            result.m11 = (2f * near) / height;
            result.m12 = 0f;
            result.m13 = 0f;

            result.m20 = 0f;
            result.m21 = 0f;
            result.m22 = -(far + near) / (far - near);
            result.m23 = -1f;

            result.m30 = 0f;
            result.m31 = 0f;
            result.m32 = (-2f * far * near) / (far - near);
            result.m33 = 0f;
        }

        /// <summary>
        /// Creates a new projection matrix for a perspective view.
        /// </summary>
        /// <param name="fieldOfView">Field of view in the y direction in radians.</param>
        /// <param name="aspectRatio">Width divided by height of the viewing volume.</param>
        /// <param name="near">Distance to the near plane.</param>
        /// <param name="far">Distance to the far plane.</param>
        public static Matrix CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float near, float far)
        {
            CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, near, far, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new projection matrix for a perspective view.
        /// </summary>
        /// <param name="fieldOfView">Field of view in the y direction in radians.</param>
        /// <param name="aspectRatio">Width divided by height of the viewing volume.</param>
        /// <param name="near">Distance to the near plane.</param>
        /// <param name="far">Distance to the far plane.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float near, float far, out Matrix result)
        {
            float yScale = 1f / (float)Math.Tan(0.5f * fieldOfView);
            float xScale = yScale / aspectRatio;

            result.m00 = xScale;
            result.m01 = 0f;
            result.m02 = 0f;
            result.m03 = 0f;

            result.m10 = 0f;
            result.m11 = yScale;
            result.m12 = 0f;
            result.m13 = 0;

            result.m20 = 0f;
            result.m21 = 0f;
            result.m22 = -(far + near) / (far - near);
            result.m23 = -1;

            result.m30 = 0f;
            result.m31 = 0f;
            result.m32 = (-2f * far * near) / (far - near);
            result.m33 = 0f;
        }

        /// <summary>
        /// Creates a new projection matrix for a customized perspective view.
        /// </summary>
        /// <param name="left">Lower x coordinate at the near plane.</param>
        /// <param name="right">Upper x coordinate at the near plane.</param>
        /// <param name="bottom">Lower y coordinate at the near plane.</param>
        /// <param name="top">Upper y coordinate at the near plane.</param>
        /// <param name="near">Distance to the near plane.</param>
        /// <param name="far">Distance to the far plane.</param>
        public static Matrix CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
        {
            CreatePerspectiveOffCenter(left, right, bottom, top, near, far, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new projection matrix for a customized perspective view.
        /// </summary>
        /// <param name="volume">The viewing volume.</param>
        /// <param name="near">Distance to the near plane.</param>
        /// <param name="far">Distance to the far plane.</param>
        public static Matrix CreatePerspectiveOffCenter(Rect volume, float near, float far)
        {
            CreatePerspectiveOffCenter(volume.xMin, volume.xMax, volume.yMin, volume.yMax, near, far, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new projection matrix for a customized perspective view.
        /// </summary>
        /// <param name="left">Lower x coordinate at the near plane.</param>
        /// <param name="right">Upper x coordinate at the near plane.</param>
        /// <param name="bottom">Lower y coordinate at the near plane.</param>
        /// <param name="top">Upper y coordinate at the near plane.</param>
        /// <param name="near">Distance to the near plane.</param>
        /// <param name="far">Distance to the far plane.</param>
        public static void CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far, out Matrix result)
        {
            result.m00 = (2f * near) / (right - left);
            result.m01 = 0f;
            result.m02 = 0f;
            result.m03 = 0f;

            result.m10 = 0f;
            result.m11 = (2f * near) / (top - bottom);
            result.m12 = 0f;
            result.m13 = 0;

            result.m20 = (right + left) / (right - left);
            result.m21 = (top + bottom) / (top - bottom);
            result.m22 = -(far + near) / (far - near);
            result.m23 = -1;

            result.m30 = 0f;
            result.m31 = 0f;
            result.m32 = (-2f * far * near) / (far - near);
            result.m33 = 0f;
        }

        /// <summary>
        /// Creates a new scaling matrix.
        /// </summary>
        /// <param name="scale">The scale value.</param>
        public static Matrix CreateScale(float scale)
        {
            CreateScale(scale, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new scaling matrix.
        /// </summary>
        /// <param name="scale">The scale value.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void CreateScale(float scale, out Matrix result)
        {
            CreateScale(scale, scale, scale, out result);
        }

        /// <summary>
        /// Creates a new scaling matrix.
        /// </summary>
        /// <param name="x">Scale value for X axis.</param>
        /// <param name="y">Scale value for Y axis.</param>
        /// <param name="z">Scale value for Z axis.</param>
        public static Matrix CreateScale(float x, float y, float z)
        {
            CreateScale(x, y, z, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new scaling matrix.
        /// </summary>
        /// <param name="x">Scale value for X axis.</param>
        /// <param name="y">Scale value for Y axis.</param>
        /// <param name="z">Scale value for Z axis.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void CreateScale(float x, float y, float z, out Matrix result)
        {
            result.m00 = x;
            result.m01 = 0f;
            result.m02 = 0f;
            result.m03 = 0f;

            result.m10 = 0f;
            result.m11 = y;
            result.m12 = 0f;
            result.m13 = 0f;

            result.m20 = 0f;
            result.m21 = 0f;
            result.m22 = z;
            result.m23 = 0f;

            result.m30 = 0f;
            result.m31 = 0f;
            result.m32 = 0f;
            result.m33 = 1f;
        }

        /// <summary>
        /// Creates a new scaling matrix.
        /// </summary>
        /// <param name="scale">The scale for each axis.</param>
        public static Matrix CreateScale(Vector3 scale)
        {
            CreateScale(ref scale, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new scaling matrix.
        /// </summary>
        /// <param name="scale">The scale for each axis.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void CreateScale(ref Vector3 scale, out Matrix result)
        {
            result.m00 = scale.x;
            result.m01 = 0f;
            result.m02 = 0f;
            result.m03 = 0f;

            result.m10 = 0f;
            result.m11 = scale.y;
            result.m12 = 0f;
            result.m13 = 0f;

            result.m20 = 0f;
            result.m21 = 0f;
            result.m22 = scale.z;
            result.m23 = 0f;

            result.m30 = 0f;
            result.m31 = 0f;
            result.m32 = 0f;
            result.m33 = 1f;
        }
        
        /// <summary>
        /// Creates a new translation matrix.
        /// </summary>
        /// <param name="x">X coordinate of translation.</param>
        /// <param name="y">Y coordinate of translation.</param>
        /// <param name="z">Z coordinate of translation.</param>
        public static Matrix CreateTranslation(float x, float y, float z)
        {
            CreateTranslation(x, y, z, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new translation matrix.
        /// </summary>
        /// <param name="x">X coordinate of translation.</param>
        /// <param name="y">Y coordinate of translation.</param>
        /// <param name="x">Z coordinate of translation.</param>
        /// <param name="result">The translation as an output parameter.</param>
        public static void CreateTranslation(float x, float y, float z, out Matrix result)
        {
            result.m00 = 1f;
            result.m01 = 0f;
            result.m02 = 0f;
            result.m03 = 0f;

            result.m10 = 0f;
            result.m11 = 1f;
            result.m12 = 0f;
            result.m13 = 0f;

            result.m20 = 0f;
            result.m21 = 0f;
            result.m22 = 1f;
            result.m23 = 0f;

            result.m30 = x;
            result.m31 = y;
            result.m32 = z;
            result.m33 = 1f;
        }

        /// <summary>
        /// Creates a new translation matrix.
        /// </summary>
        /// <param name="position">The translation.</param>
        /// <returns>The translation <see cref="Matrix"/>.</returns>
        public static Matrix CreateTranslation(Vector3 position)
        {
            CreateTranslation(ref position, out Matrix result);
            return result;
        }

        /// <summary>
        /// Creates a new translation matrix.
        /// </summary>
        /// <param name="position">The translation.</param>
        /// <param name="result">The result as an output parameter.</param>
        public static void CreateTranslation(ref Vector3 position, out Matrix result)
        {
            result.m00 = 1f;
            result.m01 = 0f;
            result.m02 = 0f;
            result.m03 = 0f;

            result.m10 = 0f;
            result.m11 = 1f;
            result.m12 = 0f;
            result.m13 = 0f;

            result.m20 = 0f;
            result.m21 = 0f;
            result.m22 = 1f;
            result.m23 = 0f;

            result.m30 = position.x;
            result.m31 = position.y;
            result.m32 = position.z;
            result.m33 = 1f;
        }
        
        /// <summary>
        /// Decomposes this matrix to translation, rotation and scale elements.
        /// </summary>
        /// <param name="translation">Translation vector as an output parameter.</param>
        /// <param name="scale">Scale vector as an output parameter.</param>
        /// <param name="rotation">Rotation quaternion as an output parameter.</param>
        /// <returns><c>true</c> if matrix can be decomposed, <c>false</c> otherwise.</returns>
        public bool Decompose(out Vector3 translation, out Quaternion rotation, out Vector3 scale)
        {
            translation.x = m30;
            translation.y = m31;
            translation.z = m32;
            
            scale.x = Mathf.Sqrt(m00 * m00 + m01 * m01 + m02 * m02);
            scale.y = Mathf.Sqrt(m10 * m10 + m11 * m11 + m12 * m12);
            scale.z = Mathf.Sqrt(m20 * m20 + m21 * m21 + m22 * m22);

            if (scale.x == 0f || scale.y == 0f || scale.z == 0f)
            {
                rotation = Quaternion.Identity;
                return false;
            }
            
            Vector3 row0 = new Vector3(m00, m01, m02) / scale.x;
            Vector3 row1 = new Vector3(m10, m11, m12) / scale.y;
            Vector3 row2 = new Vector3(m20, m21, m22) / scale.z;

            float trace = 1f + row0.x + row1.y + row2.z;

            if (trace >= 0)
            {
                float s = 2f * (float)Math.Sqrt(trace);
                float invS = 1f / s;

                rotation.w = 0.25f * s;
                rotation.x = invS * (row2.y - row1.z);
                rotation.y = invS * (row0.z - row2.x);
                rotation.z = invS * (row1.x - row0.y);
            }
            else if (row0.x > row1.y && row0.x > row2.z)
            {
                float s = 2f * (float)Math.Sqrt(1f + row0.x - row1.y - row2.z);
                float invS = 1f / s;

                rotation.x = 0.25f * s;
                rotation.y = invS * (row1.x + row0.y);
                rotation.z = invS * (row2.x + row0.z);
                rotation.w = invS * (row1.z - row2.y);
            }
            else if (row1.y > row2.z)
            {
                float s = 2f * (float)Math.Sqrt(1f - row0.x + row1.y - row2.z);
                float invS = 1f / s;

                rotation.y = 0.25f * s;
                rotation.x = invS * (row1.x + row0.y);
                rotation.z = invS * (row2.y + row1.z);
                rotation.w = invS * (row2.x - row0.z);
            }
            else
            {
                float s = 2f * (float)Math.Sqrt(1f - row0.x - row1.y + row2.z);
                float invS = 1f / s;

                rotation.z = 0.25f * s;
                rotation.x = invS * (row2.x + row0.z);
                rotation.y = invS * (row2.y + row1.z);
                rotation.w = invS * (row0.y - row1.x);
            }
            
            return true;
        }

        /// <summary>
        /// Compares whether this instance is equal to another.
        /// </summary>
        /// <param name="other">The instance to compare with.</param>
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
        /// Compares whether this instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Matrix) && Equals((Matrix)obj);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
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
        /// Returns a <see cref="String"/> representation of this instance.
        /// </summary>
        public override string ToString()
        {
            return $"\n{Row0}\n{Row1}\n{Row2}\n{Row3}";
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