using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mixin
{
    public static class Colors
    {
        public static string ToHex(this Color color)
        {
            return $"#{ColorUtility.ToHtmlStringRGB(color)}";
        }

        public static Color GetMixinColor()
        {
            return Color.red;
        }

        public static string Colorize(this string text, Color color)
        {
            return $"<color={color.ToHex()}>{text}</color>";
        }

        public static string ColorizeWhite(this string text)
        {
            return text.Colorize(Color.white);
        }

        public static string ColorizeBlack(this string text)
        {
            return text.Colorize(Color.black);
        }

        public static Color SetSaturation(this Color color, float saturation)
        {
            float h;
            float s;
            float v;

            Color.RGBToHSV(color, out h, out s, out v);

            s = saturation;

            return Color.HSVToRGB(h, s, v);
        }

        public static Color SetAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}
