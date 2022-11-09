
using System.Collections.Generic;

namespace Mixin.Utils
{
    /// <summary>
    /// Extensions for <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Converts any IDictionary to to Dictionary.
        /// </summary>
        public static Dictionary<TKey, TValue> ConvertToDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new Dictionary<TKey, TValue>(dictionary);
        }

        /// <summary>
        /// Converts all values of the dictionary to double.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static Dictionary<TKey, double> ConvertValueToDouble<TKey>(this Dictionary<TKey, float> dictionary)
        {
            Dictionary<TKey, double> newDictionary = new Dictionary<TKey, double>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, dictionary[key]);
            return newDictionary;
        }

        /// <inheritdoc cref="ConvertValueToDouble{TKey}(Dictionary{TKey, float})" />
        public static Dictionary<TKey, double> ConvertValueToDouble<TKey>(this Dictionary<TKey, long> dictionary)
        {
            Dictionary<TKey, double> newDictionary = new Dictionary<TKey, double>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, dictionary[key]);
            return newDictionary;
        }

        /// <inheritdoc cref="ConvertValueToDouble{TKey}(Dictionary{TKey, float})" />
        public static Dictionary<TKey, double> ConvertValueToDouble<TKey>(this Dictionary<TKey, int> dictionary)
        {
            Dictionary<TKey, double> newDictionary = new Dictionary<TKey, double>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, dictionary[key]);
            return newDictionary;
        }

        /// <summary>
        /// Converts all values of the dictionary to float.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static Dictionary<TKey, float> ConvertValueToFloat<TKey>(this Dictionary<TKey, double> dictionary)
        {
            Dictionary<TKey, float> newDictionary = new Dictionary<TKey, float>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, (float)dictionary[key]);
            return newDictionary;
        }

        /// <inheritdoc cref="ConvertValueToFloat{TKey}(Dictionary{TKey, double})" />
        public static Dictionary<TKey, float> ConvertValueToFloat<TKey>(this Dictionary<TKey, long> dictionary)
        {
            Dictionary<TKey, float> newDictionary = new Dictionary<TKey, float>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, dictionary[key]);
            return newDictionary;
        }

        /// <inheritdoc cref="ConvertValueToFloat{TKey}(Dictionary{TKey, double})" />
        public static Dictionary<TKey, float> ConvertValueToFloat<TKey>(this Dictionary<TKey, int> dictionary)
        {
            Dictionary<TKey, float> newDictionary = new Dictionary<TKey, float>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, dictionary[key]);
            return newDictionary;
        }

        /// <summary>
        /// Converts all values of the dictionary to long.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static Dictionary<TKey, long> ConvertValueToLong<TKey>(this Dictionary<TKey, int> dictionary)
        {
            Dictionary<TKey, long> newDictionary = new Dictionary<TKey, long>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, dictionary[key]);
            return newDictionary;
        }

        /// <summary>
        /// Converts all values of the dictionary to int.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static Dictionary<TKey, int> ConvertValueToInt<TKey>(this Dictionary<TKey, long> dictionary)
        {
            Dictionary<TKey, int> newDictionary = new Dictionary<TKey, int>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, (int)dictionary[key]);
            return newDictionary;
        }
    }
}
