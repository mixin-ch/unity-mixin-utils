
using System.Collections.Generic;

namespace Mixin.Utils
{
    /// <summary>
    /// Extensions for <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    static class DictionaryExtensions
    {
        /// <summary>
        /// Returns the value or defalt if key not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static V SaveGet<K, V>(this Dictionary<K, V> dict, K key)
        {
            if (dict.ContainsKey(key))
                return dict[key];

            return default;
        }

        /// <summary>
        /// Picks a random key element from the dictionary.
        /// Weight is dependent on the value.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static TKey PickWeightedRandom<TKey>(this IDictionary<TKey, double> dictionary, System.Random random)
        {
            double sum = 0;

            foreach (double weight in dictionary.Values)
                sum += weight.LowerBound(0);

            double point = random.NextDouble() * sum;

            foreach (TKey key in dictionary.Keys)
            {
                point -= dictionary[key].LowerBound(0);

                if (point < 0)
                    return key;
            }

            return default;
        }

        /// <summary>
        /// Converts all values of the dictionary to double.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static IDictionary<TKey, double> ConvertValueToDouble<TKey>(this IDictionary<TKey, float> dictionary)
        {
            IDictionary<TKey, double> newDictionary = new Dictionary<TKey, double>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, dictionary[key]);
            return newDictionary;
        }

        /// <inheritdoc cref="ConvertValueToDouble{TKey}(IDictionary{TKey, float})" />
        public static IDictionary<TKey, double> ConvertValueToDouble<TKey>(this IDictionary<TKey, long> dictionary)
        {
            IDictionary<TKey, double> newDictionary = new Dictionary<TKey, double>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, dictionary[key]);
            return newDictionary;
        }

        /// <inheritdoc cref="ConvertValueToDouble{TKey}(IDictionary{TKey, float})" />
        public static IDictionary<TKey, double> ConvertValueToDouble<TKey>(this IDictionary<TKey, int> dictionary)
        {
            IDictionary<TKey, double> newDictionary = new Dictionary<TKey, double>();
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
        public static IDictionary<TKey, float> ConvertValueToFloat<TKey>(this IDictionary<TKey, double> dictionary)
        {
            IDictionary<TKey, float> newDictionary = new Dictionary<TKey, float>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, (float)dictionary[key]);
            return newDictionary;
        }

        /// <inheritdoc cref="ConvertValueToFloat{TKey}(IDictionary{TKey, double})" />
        public static IDictionary<TKey, float> ConvertValueToFloat<TKey>(this IDictionary<TKey, long> dictionary)
        {
            IDictionary<TKey, float> newDictionary = new Dictionary<TKey, float>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, dictionary[key]);
            return newDictionary;
        }

        /// <inheritdoc cref="ConvertValueToFloat{TKey}(IDictionary{TKey, double})" />
        public static IDictionary<TKey, float> ConvertValueToFloat<TKey>(this IDictionary<TKey, int> dictionary)
        {
            IDictionary<TKey, float> newDictionary = new Dictionary<TKey, float>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, dictionary[key]);
            return newDictionary;
        }

        /// <summary>
        /// Converts all values of the dictionary to long.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static IDictionary<TKey, long> ConvertValueToLong<TKey>(this IDictionary<TKey, int> dictionary)
        {
            IDictionary<TKey, long> newDictionary = new Dictionary<TKey, long>();
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
        public static IDictionary<TKey, int> ConvertValueToInt<TKey>(this IDictionary<TKey, long> dictionary)
        {
            IDictionary<TKey, int> newDictionary = new Dictionary<TKey, int>();
            foreach (TKey key in dictionary.Keys)
                newDictionary.Add(key, (int)dictionary[key]);
            return newDictionary;
        }

        /// <inheritdoc cref="PickWeightedRandom{TKey}(IDictionary{TKey, double}, System.Random)" />
        public static TKey PickWeightedRandom<TKey>(this IDictionary<TKey, float> dictionary, System.Random random)
        {
            return PickWeightedRandom(dictionary.ConvertValueToDouble(), random);
        }

        /// <inheritdoc cref="PickWeightedRandom{TKey}(IDictionary{TKey, double}, System.Random)" />
        public static TKey PickWeightedRandom<TKey>(this IDictionary<TKey, long> dictionary, System.Random random)
        {
            return PickWeightedRandom(dictionary.ConvertValueToDouble(), random);
        }

        /// <inheritdoc cref="PickWeightedRandom{TKey}(IDictionary{TKey, double}, System.Random)" />
        public static TKey PickWeightedRandom<TKey>(this IDictionary<TKey, int> dictionary, System.Random random)
        {
            return PickWeightedRandom(dictionary.ConvertValueToDouble(), random);
        }
    }
}
