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
    /// Represents an RGBA color with 4 floating-point components.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Color : IEquatable<Color>
    {
        /// <summary>
        /// Gets the color with RGBA of (0, 0, 0, 0).
        /// </summary>
        public static Color Transparent => new Color(0f, 0f, 0f, 0f);

        /// <summary>
        /// Gets the color with RGBA of (0, 0, 0, 1).
        /// </summary>
        public static Color Black => new Color(0f, 0f, 0f, 1f);

        /// <summary>
        /// Gets the color with RGBA of (0.5, 0.5, 0.5, 1).
        /// </summary>
        public static Color Grey => new Color(0.5f, 0.5f, 0.5f, 1f);

        /// <summary>
        /// Gets the color with RGBA of (1, 1, 1, 1).
        /// </summary>
        public static Color White => new Color(1f, 1f, 1f, 1f);

        /// <summary>
        /// Gets the color with RGBA of (1, 0, 0, 1).
        /// </summary>
        public static Color Red => new Color(1f, 0f, 0f, 1f);

        /// <summary>
        /// Gets the color with RGBA of (1, 1, 0, 1).
        /// </summary>
        public static Color Yellow => new Color(1f, 1f, 0f, 1f);

        /// <summary>
        /// Gets the color with RGBA of (0, 1, 0, 1).
        /// </summary>
        public static Color Green => new Color(0f, 1f, 0f, 1f);

        /// <summary>
        /// Gets the color with RGBA of (0, 1, 1, 1).
        /// </summary>
        public static Color Cyan => new Color(0f, 1f, 1f, 1f);

        /// <summary>
        /// Gets the color with RGBA of (0, 0, 1, 1).
        /// </summary>
        public static Color Blue => new Color(0f, 0f, 1f, 1f);

        /// <summary>
        /// Gets the color with RGBA of (1, 0, 1, 1).
        /// </summary>
        public static Color Magenta => new Color(1f, 0f, 1f, 1f);

        /// <summary>
        /// The red component.
        /// </summary>
        public float r;

        /// <summary>
        /// The green component.
        /// </summary>
        public float g;

        /// <summary>
        /// The blue component.
        /// </summary>
        public float b;

        /// <summary>
        /// The alpha component.
        /// </summary>
        public float a;

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public Color(byte r, byte g, byte b, byte a)
        {
            this.r = r / (float)byte.MaxValue;
            this.g = g / (float)byte.MaxValue;
            this.b = b / (float)byte.MaxValue;
            this.a = a / (float)byte.MaxValue;
        }

        /// <summary>
        /// Gets or sets the value at an index of the color.
        /// </summary>
        /// <param name="index">The index of the component from the color.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or greater than 3.</exception>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return r;
                    case 1: return g;
                    case 2: return b;
                    case 3: return a;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: r = value; break;
                    case 1: g = value; break;
                    case 2: b = value; break;
                    case 3: a = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// The grayscale value of the color.
        /// </summary>
        public float Greyscale => (r + g + b) / 3f;

        /// <summary>
        /// Converts this color to an integer representation with 8 bits per channel.
        /// </summary>
        public int ToArgb()
        {
            uint value =
                ((uint)(a * byte.MaxValue) << 24) |
                ((uint)(r * byte.MaxValue) << 16) |
                ((uint)(g * byte.MaxValue) << 8) |
                (uint)(b * byte.MaxValue);
            return unchecked((int)value);
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// </summary>
        /// <param name="a">The first color.</param>
        /// <param name="b">The second color.</param>
        /// <param name="t">Weighting value.</param>
        public static Color Lerp(Color a, Color b, float t)
        {
            return new Color(
                (t * (b.r - a.r)) + a.r,
                (t * (b.g - a.g)) + a.g,
                (t * (b.b - a.b)) + a.b,
                (t * (b.a - a.a)) + a.a
            );
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">The first color.</param>
        /// <param name="b">The second color.</param>
        /// <param name="t">Weighting value.</param>
        public static Color LerpClamped(Color a, Color b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Color(
                (t * (b.r - a.r)) + a.r,
                (t * (b.g - a.g)) + a.g,
                (t * (b.b - a.b)) + a.b,
                (t * (b.a - a.a)) + a.a
            );
        }

        /// <summary>
        /// Transforms a sRGB color to a linear color.
        /// </summary>
        /// <param name="srgb">A sRGB color to transform.</param>
        public static Color FromSrgb(Color srgb)
        {
            return new Color(
                Mathf.SrgbToLinear(srgb.r),
                Mathf.SrgbToLinear(srgb.g),
                Mathf.SrgbToLinear(srgb.b),
                srgb.a);
        }

        /// <summary>
        /// Transforms a linear color to a sRGB color.
        /// </summary>
        /// <param name="rgb">A linear color to transform.</param>
        public static Color ToSrgb(Color linear)
        {
            return new Color(
                Mathf.LinearToSrgb(linear.r),
                Mathf.LinearToSrgb(linear.g),
                Mathf.LinearToSrgb(linear.b),
                linear.a);
        }

        /// <summary>
        /// Converts HSL color values to an RGB color.
        /// </summary>
        /// <param name="h">The hue value.</param>
        /// <param name="s">The saturation value [0, 1].</param>
        /// <param name="l">The lightness value [0, 1].</param>
        /// <param name="a">The alpha value [0, 1].</param>
        public static Color FromHSL(float h, float s, float l, float a)
        {
            h = Mathf.Cycle(h, 1.0f) * (360f / 60f);

            float c = (1f - Math.Abs((2f * l) - 1f)) * s;
            float x = c * (1f - Math.Abs((h % 2f) - 1f));
            float m = l - (c / 2f);

            float r, g, b;
            if (h < 1f)
            {
                r = c;
                g = x;
                b = 0f;
            }
            else if (h < 2f)
            {
                r = x;
                g = c;
                b = 0f;
            }
            else if (h < 3f)
            {
                r = 0f;
                g = c;
                b = x;
            }
            else if (h < 4f)
            {
                r = 0f;
                g = x;
                b = c;
            }
            else if (h < 5f)
            {
                r = x;
                g = 0f;
                b = c;
            }
            else
            {
                r = c;
                g = 0f;
                b = x;
            }

            return new Color(r + m, g + m, b + m, a);
        }

        /// <summary>
        /// Converts RGB color values to HSL color values.
        /// </summary>
        /// <param name="rgb">Color value to convert.</param>
        public static Vector4 ToHSL(Color rgb)
        {
            float max = Mathf.Max(rgb.r, Mathf.Max(rgb.g, rgb.b));
            float min = Mathf.Min(rgb.r, Mathf.Min(rgb.g, rgb.b));
            float diff = max - min;

            float h = 0f;
            if (diff > float.Epsilon)
            {
                if (max == rgb.r)
                {
                    h = ((rgb.g - rgb.b) / diff);
                }
                else if (max == rgb.g)
                {
                    h = ((rgb.b - rgb.r) / diff) + 2f;
                }
                else if (max == rgb.b)
                {
                    h = ((rgb.r - rgb.g) / diff) + 4f;
                }
            }

            float hue = Mathf.Cycle(h * (60f / 360f), 1.0f);

            float lightness = (max + min) / 2f;

            float saturation = 0f;
            if (lightness != 0f && lightness != 1f)
            {
                saturation = diff / (1f - Math.Abs((2f * lightness) - 1f));
            }

            return new Vector4(hue, saturation, lightness, rgb.a);
        }

        /// <summary>
        /// Converts HSV color values to an RGB color.
        /// </summary>
        /// <param name="h">The hue value.</param>
        /// <param name="s">The saturation value [0, 1].</param>
        /// <param name="v">The brightness value [0, 1].</param>
        /// <param name="a">The alpha value [0, 1].</param>
        public static Color FromHSV(float h, float s, float v, float a)
        {
            h = Mathf.Cycle(h, 1.0f) * (360f / 60f);

            float c = v * s;
            float x = c * (1f - Math.Abs((h % 2f) - 1f));
            float m = v - c;

            float r, g, b;
            if (h < 1f)
            {
                r = c;
                g = x;
                b = 0f;
            }
            else if (h < 2f)
            {
                r = x;
                g = c;
                b = 0f;
            }
            else if (h < 3f)
            {
                r = 0f;
                g = c;
                b = x;
            }
            else if (h < 4f)
            {
                r = 0f;
                g = x;
                b = c;
            }
            else if (h < 5f)
            {
                r = x;
                g = 0f;
                b = c;
            }
            else
            {
                r = c;
                g = 0f;
                b = x;
            }

            return new Color(r + m, g + m, b + m, a);
        }

        /// <summary>
        /// Converts RGB color values to HSV color values.
        /// </summary>
        /// <param name="rgb">Color value to convert.</param>
        public static Vector4 ToHSV(Color rgb)
        {
            float max = Mathf.Max(rgb.r, Mathf.Max(rgb.g, rgb.b));
            float min = Mathf.Min(rgb.r, Mathf.Min(rgb.g, rgb.b));
            float diff = max - min;

            float h = 0f;
            if (diff > float.Epsilon)
            {
                if (max == rgb.r)
                {
                    h = ((rgb.g - rgb.b) / diff);
                }
                else if (max == rgb.g)
                {
                    h = ((rgb.b - rgb.r) / diff) + 2f;
                }
                else if (max == rgb.b)
                {
                    h = ((rgb.r - rgb.g) / diff) + 4f;
                }
            }
            
            float hue = Mathf.Cycle(h * (60f / 360f), 1.0f);

            float saturation = 0f;
            if (max > float.Epsilon)
            {
                saturation = diff / max;
            }

            return new Vector4(hue, saturation, max, rgb.a);
        }
        
        /// <summary>
        /// Compares whether this color structure is equal to the specified color.
        /// </summary>
        /// <param name="other">The color to compare with.</param>
        /// <returns>True if both colors contain the same components, false otherwise.</returns>
        public bool Equals(Color other)
        {
            return
                r == other.r &&
                g == other.g &&
                b == other.b &&
                a == other.a;
        }

        /// <summary>
        /// Compares whether this color structure is equal to the specified object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True obj is a color with the same components as this color, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Color) && Equals((Color)obj);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return ToArgb();
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this instance.
        /// </summary>
        public override string ToString()
        {
            const string format = "F2";
            return $"({r.ToString(format)}, {g.ToString(format)}, {b.ToString(format)}, {a.ToString(format)})";
        }

        /// <summary>
        /// Multiplies the color by a scalar.
        /// </summary>
        /// <param name="color">The color to scale.</param>
        /// <param name="scale">The scalar value.</param>
        public static Color operator *(Color color, float scale)
        {
            color.r *= scale;
            color.g *= scale;
            color.b *= scale;
            color.a *= scale;
            return color;
        }

        /// <summary>
        /// Multiplies the color by a scalar.
        /// </summary>
        /// <param name="color">The color to scale.</param>
        /// <param name="scale">The scalar value.</param>
        public static Color operator *(float scale, Color color)
        {
            color.r *= scale;
            color.g *= scale;
            color.b *= scale;
            color.a *= scale;
            return color;
        }

        /// <summary>
        /// Divides the color by a scalar.
        /// </summary>
        /// <param name="color">The color to scale.</param>
        /// <param name="divider">The scalar value to divide by.</param>
        public static Color operator /(Color color, float divider)
        {
            float scale = 1f / divider;
            color.r *= scale;
            color.g *= scale;
            color.b *= scale;
            color.a *= scale;
            return color;
        }

        /// <summary>
        /// Compares the specified colors for equality.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if left is equal to right, false otherwise.</returns>
        public static bool operator ==(Color left, Color right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares the specified colors for inequality.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if left is not equal to right, false otherwise.</returns>
        public static bool operator !=(Color left, Color right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Cast the color to a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        public static implicit operator Vector4(Color color)
        {
            return new Vector4(color.r, color.g, color.b, color.a);
        }

        /// <summary>
        /// Cast the vector to a <see cref="OpenTK.Vector4"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator OpenTK.Vector4(Color color)
        {
            return Unsafe.ReinterpretCast<Color, OpenTK.Vector4>(color);
        }
    }
}
