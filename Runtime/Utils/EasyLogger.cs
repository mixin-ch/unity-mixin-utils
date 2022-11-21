using UnityEngine;

namespace Mixin.Utils
{
    /// <summary>
    /// An Extention for the <see cref="Debug"/>.<br></br>
    /// You can now do string.Log()
    /// </summary>
    public static class EasyLogger
    {
        /// <inheritdoc cref="MixinCollection.GetMixinColor()"/>
        private static Color _mixinColor = MixinCollection.GetMixinColor();

        /// <inheritdoc cref="ColorExtensions.GetMixinColor()"/>
        private static string _mixinColorHex = _mixinColor.ToHex();

        /// <summary>
        /// The easy way to Debug.Log()! <br></br>
        /// You can give a Color as param for colored Output.
        /// </summary>
        /// <param name="text"></param>
        public static void Log(this string text, Color color)
        {
            Debug.Log($"<color={_mixinColorHex}>#Mixin </color><color={color.ToHex()}>{text}</color>");
        }

        /// <inheritdoc cref="Log(string, Color)"/>
        public static void Log(this string text)
        {
            Log(text, Color.white);
        }

        /// <summary>
        /// Adds loading points to the text.
        /// </summary>
        /// <param name="text"></param>
        public static void LogProgress(this string text)
        {
            Log(text + "...", Color.gray);
        }

        public static void LogSuccess(this string text)
        {
            Log(text, Color.green);
        }

        public static void LogWarning(this string text)
        {
            Debug.LogWarning($"<color={_mixinColorHex}>#Mixin </color>{text}");
        }


        public static void LogError(this string text)
        {
            Debug.LogError($"<color={_mixinColorHex}>#Mixin </color>{text}");
        }
    }
}