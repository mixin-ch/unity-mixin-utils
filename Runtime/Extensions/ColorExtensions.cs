
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mixin.Utils
{
    /// <summary>
    /// Extensions for <see cref="Color"/>.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Inverses the RGB values.
        /// </summary>
        public static Color Inverse(this Color color)
        {
            return new Color(
                1 - color.r
                , 1 - color.g
                , 1 - color.b
                , color.a
                );
        }

        /// <summary>
        /// Mixes the RGB values of two colors.
        /// "fraction" determines what fraction of the "current" color is kept.
        /// </summary>
        public static Color MixRGB(this Color current, Color next, float fraction)
        {
            if (fraction < 0 || fraction > 1)
                throw new ArgumentException($"Parameter \"fraction\" was \"{fraction}\", but must be between 0 and 1.");

            float left = 1 - fraction;
            return new Color(
                current.r * left + next.r * fraction
                , current.g * left + next.g * fraction
                , current.b * left + next.b * fraction
                , current.a * left + next.a * fraction
                );
        }

        /// <summary>
        /// Mixes the HSV values of two colors.
        /// "fraction" determines what fraction of the "current" color is kept.
        /// </summary>
        public static Color MixHSV(this Color current, Color next, float fraction)
        {
            float left = 1 - fraction;

            Vector2 hueVector = Vector2.zero;
            float saturation = 0;
            float value = 0;
            float alpha = 0;

            float h;
            float s;
            float v;

            Color.RGBToHSV(current, out h, out s, out v);

            hueVector += h.RevolutionToVector2() * left;
            saturation += s * left;
            value += v * left;
            alpha += current.a * left;

            Color.RGBToHSV(next, out h, out s, out v);

            hueVector += h.RevolutionToVector2() * fraction;
            saturation += s * fraction;
            value += v * fraction;
            alpha += next.a * fraction;

            Color color = Color.HSVToRGB(hueVector.Vector2ToRevolution(), saturation, value);
            color.a = alpha;

            return color;
        }

        /// <summary>
        /// Mixes the RGB values of multiple colors with their own weights.
        /// </summary>
        public static Color MixWeightedRGB(this IDictionary<Color, float> dict)
        {
            float sum = 0;

            foreach (float weight in dict.Values)
                sum += weight;

            float r = 0;
            float g = 0;
            float b = 0;
            float a = 0;

            foreach (Color color in dict.Keys)
            {
                float influence = dict[color] / sum;
                r += influence * color.r;
                g += influence * color.g;
                b += influence * color.b;
                a += influence * color.a;
            }

            return new Color(r, g, b, a);
        }

        /// <summary>
        /// Mixes the HSV values of multiple colors with their own weights.
        /// </summary>
        public static Color MixWeightedHSV(this IDictionary<Color, float> dict)
        {
            float sum = 0;

            foreach (float weight in dict.Values)
                sum += weight;

            Vector2 hueVector = Vector2.zero;
            float saturation = 0;
            float value = 0;
            float alpha = 0;

            foreach (Color c in dict.Keys)
            {
                float h;
                float s;
                float v;

                Color.RGBToHSV(c, out h, out s, out v);

                float influence = dict[c] / sum;

                hueVector += h.RevolutionToVector2() * influence;
                saturation += s * influence;
                value += v * influence;
                alpha += c.a * influence;
            }

            Color color = Color.HSVToRGB(hueVector.Vector2ToRevolution(), saturation, value);
            color.a = alpha;

            return color;
        }

        /// <summary>
        /// Convert a Color to Hex String.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHex(this Color color)
        {
            return $"#{ColorUtility.ToHtmlStringRGB(color)}";
        }

        /// <summary>
        /// This just returns the official Mixin Color. <br></br>
        /// Currently Color.red.
        /// </summary>
        /// <returns></returns>
        public static Color GetMixinColor()
        {
            return Color.red;
        }

        /// <summary>
        /// This method returns a string with [color] tags. <br></br>
        /// Very useful for TextMeshPro Texts. They will easily be colorized.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string Colorize(this string text, Color color)
        {
            return $"<color={color.ToHex()}>{text}</color>";
        }

        /// <inheritdoc cref="Colorize(string, Color)"/>
        public static string ColorizeWhite(this string text)
        {
            return text.Colorize(Color.white);
        }

        /// <inheritdoc cref="Colorize(string, Color)"/>
        public static string ColorizeBlack(this string text)
        {
            return text.Colorize(Color.black);
        }

        /// <summary>
        /// You can set a custom Saturation for the Color.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="saturation"></param>
        /// <returns></returns>
        public static Color SetSaturation(this Color color, float saturation)
        {
            float h;
            float s;
            float v;

            Color.RGBToHSV(color, out h, out s, out v);

            s = saturation;

            return Color.HSVToRGB(h, s, v);
        }
    }
}
