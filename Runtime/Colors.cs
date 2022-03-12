using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mixin { 
public static class Colors
{
        public static string ToHex(this Color color) =>
            $"#{ColorUtility.ToHtmlStringRGB(color)}";

        public static Color GetMixinColor() =>
            Color.red;

        public static string Colorize(this string text, Color color)
       => $"<color={color.ToHex()}>{text}</color>";
        public static string ColorizeWhite(this string text)
            => text.Colorize(Color.white);
        public static string ColorizeBlack(this string text)
            => text.Colorize(Color.black);

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
