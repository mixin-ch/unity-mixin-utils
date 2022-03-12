using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mixin
{
    public static class EasyLogger
    {
        /// <summary>
        /// The method Out outputs the text to the console.
        /// You can give a color enum as param for colored output
        /// </summary>
        /// <param name="text"></param>
        public static void Log(this string text, Color color)
        {
            Debug.Log($"<color={Colors.GetMixinColor().ToHex()}>#Mixin </color><color={color.ToHex()}>{text}</color>");
        }

        public static void Log(this string text) => Log(text, Color.white);

        public static void LogError(this string text) =>
            Debug.LogError($"<color={Colors.GetMixinColor().ToHex()}>#Mixin </color>{text}");
        public static void LogWarning(this string text) =>
            Debug.LogWarning($"<color={Colors.GetMixinColor().ToHex()}>#Mixin </color>{text}");

    }
}