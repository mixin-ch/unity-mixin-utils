using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mixin
{
    public static class EasyLogger
    {
        private static Color MixinColor = Colors.GetMixinColor();
        private static string MixinColorHex = MixinColor.ToHex();

        /// <summary>
        /// The method Out outputs the text to the console.
        /// You can give a color enum as param for colored output
        /// </summary>
        /// <param name="text"></param>
        public static void Log(this string text, Color color)
        {
            Debug.Log($"<color={MixinColorHex}>#Mixin </color><color={color.ToHex()}>{text}</color>");
        }

        public static void Log(this string text)
        {
            Log(text, Color.white);
        }

        public static void LogError(this string text)
        {
            Debug.LogError($"<color={MixinColorHex}>#Mixin </color>{text}");
        }

        public static void LogWarning(this string text)
        {
            Debug.LogWarning($"<color={MixinColorHex}>#Mixin </color>{text}");
        }


        /******/
        public static string FormatThousand(this int number)
        {
            if (number == 0)
                return "0";
            else
                return number.ToString("#,#");
        }

        public static string FormatThousand(this long number)
        {
            if (number == 0)
                return "0";
            else
                return number.ToString("#,#");
        }
    }
}