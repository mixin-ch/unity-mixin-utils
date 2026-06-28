using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
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

        private static string _mixinPrefix = $"<color={_mixinColorHex}>#Mixin</color>";

        public static string GetFormattedMessage(string filePath, string text, Color classColor, Color textColor)
        {
            string className = Path.GetFileNameWithoutExtension(filePath);
            return $"<color={classColor.ToHex()}>[{className}]</color> <color={textColor.ToHex()}>{text}</color>";
        }

        /// <summary>
        /// The easy way to Debug.Log()! <br></br>
        /// You can give a Color as param for colored Output.
        /// </summary>
        /// <param name="text"></param>
        public static void Log(this string text, Color color, [CallerFilePath] string filePath = "")
        {
            Debug.Log($"{GetFormattedMessage(filePath, text, Color.yellow, color)}");
        }

        /// <inheritdoc cref="Log(string, Color)"/>
        public static void Log(this string text, [CallerFilePath] string filePath = "")
        {
            Debug.Log($"{GetFormattedMessage(filePath, text, Color.yellow, Color.white)}");
        }

        public static void LogWithoutPrefix(this string text)
        {
            text.LogWithoutPrefix(Color.white);
        }

        public static void LogWithoutPrefix(this string text, Color color)
        {
            Debug.Log($"<color={color.ToHex()}>{text}</color>");
        }

        /// <summary>
        /// Adds loading points to the text.
        /// </summary>
        /// <param name="text"></param>
        public static void LogProgress(this string text, [CallerFilePath] string filePath = "")
        {
            Debug.Log($"{GetFormattedMessage(filePath, text, Color.gray, Color.gray)}...");
        }

        public static void LogSuccess(this string text, [CallerFilePath] string filePath = "")
        {
            Debug.Log($"{GetFormattedMessage(filePath, text, Color.green, Color.white)}");
        }

        public static void LogWarning(this string text, [CallerFilePath] string filePath = "")
        {
            Debug.LogWarning($"{GetFormattedMessage(filePath, text, Color.orange, Color.white)}");
        }


        public static void LogError(this string text, [CallerFilePath] string filePath = "")
        {
            Debug.LogError($"{GetFormattedMessage(filePath, text, Color.red, Color.white)}");
        }

        public static void LogObject(this object obj)
        {
            if (obj == null)
            {
                Log("Object is null.");
                return;
            }

            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            string output = "";
            output += $"Logging fields of <b>{type.Name}</b>\n";

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(obj);
                output += $"{field.Name}: {value}\n";
            }

            Log(output);
        }

        public static void LogObjectFields(this object obj, FieldInfo[] fields)
        {
            if (obj == null)
            {
                Log("Object is null.");
                return;
            }
            Type type = obj.GetType();
            string output = "";
            output += $"Logging specified fields of <b>{type.Name}</b>\n";
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(obj);
                output += $"{field.Name}: {value}\n";
            }
            Log(output);
        }
    }
}