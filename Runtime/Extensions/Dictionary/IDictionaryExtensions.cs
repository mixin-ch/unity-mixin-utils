
using System.Collections.Generic;

namespace Mixin.Utils
{
    /// <summary>
    /// Extensions for <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Returns the value or defalt if key not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static V SaveGet<K, V>(this IDictionary<K, V> dictionary, K key)
        {
            if (dictionary.ContainsKey(key))
                return dictionary[key];

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

        /// <inheritdoc cref="PickWeightedRandom{TKey}(IDictionary{TKey, double}, System.Random)" />
        public static TKey PickWeightedRandom<TKey>(this IDictionary<TKey, float> dictionary, System.Random random)
        {
            return PickWeightedRandom(dictionary.ConvertToDictionary().ConvertValueToDouble(), random);
        }

        /// <inheritdoc cref="PickWeightedRandom{TKey}(IDictionary{TKey, double}, System.Random)" />
        public static TKey PickWeightedRandom<TKey>(this IDictionary<TKey, long> dictionary, System.Random random)
        {
            return PickWeightedRandom(dictionary.ConvertToDictionary().ConvertValueToDouble(), random);
        }

        /// <inheritdoc cref="PickWeightedRandom{TKey}(IDictionary{TKey, double}, System.Random)" />
        public static TKey PickWeightedRandom<TKey>(this IDictionary<TKey, int> dictionary, System.Random random)
        {
            return PickWeightedRandom(dictionary.ConvertToDictionary().ConvertValueToDouble(), random);
        }
    }
}
