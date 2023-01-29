
using System.Collections.Generic;

namespace Mixin.Utils
{
    /// <summary>
    /// Extensions for <see cref="MixinDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class MixinDictionaryExtensions
    {
        /// <summary>
        /// Creates a copy of a MixinDictionary.
        /// </summary>
        public static MixinDictionary<TKey, TValue> Copy<TKey, TValue>(this MixinDictionary<TKey, TValue> dictionary)
        {
            MixinDictionary<TKey, TValue> newDictionary = new MixinDictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                newDictionary.Add(pair.Key, pair.Value);
            return newDictionary;
        }

        /// <summary>
        /// Converts any IDictionary to MixinDictionary.
        /// </summary>
        public static MixinDictionary<TKey, TValue> ConvertToMixinDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            MixinDictionary<TKey, TValue> newDictionary = new MixinDictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                newDictionary.Add(pair.Key, pair.Value);
            return newDictionary;
        }

        /// <inheritdoc cref="DictionaryExtensions.ConvertValueToDouble{TKey}(Dictionary{TKey, float}"/>
        public static MixinDictionary<TKey, double> ConvertValueToDouble<TKey>(this MixinDictionary<TKey, float> dictionary)
        {
            return dictionary.ConvertToDictionary().ConvertValueToDouble().ConvertToMixinDictionary();
        }

        /// <inheritdoc cref="ConvertValueToDouble{TKey}(MixinDictionary{TKey, float})" />
        public static MixinDictionary<TKey, double> ConvertValueToDouble<TKey>(this MixinDictionary<TKey, long> dictionary)
        {
            return dictionary.ConvertToDictionary().ConvertValueToDouble().ConvertToMixinDictionary();
        }

        /// <inheritdoc cref="ConvertValueToDouble{TKey}(MixinDictionary{TKey, float})" />
        public static MixinDictionary<TKey, double> ConvertValueToDouble<TKey>(this MixinDictionary<TKey, int> dictionary)
        {
            return dictionary.ConvertToDictionary().ConvertValueToDouble().ConvertToMixinDictionary();
        }

        /// <inheritdoc cref="DictionaryExtensions.ConvertValueToFloat{TKey}(Dictionary{TKey, double}"/>
        public static MixinDictionary<TKey, float> ConvertValueToFloat<TKey>(this MixinDictionary<TKey, double> dictionary)
        {
            return dictionary.ConvertToDictionary().ConvertValueToFloat().ConvertToMixinDictionary();
        }

        /// <inheritdoc cref="ConvertValueToFloat{TKey}(MixinDictionary{TKey, double})" />
        public static MixinDictionary<TKey, float> ConvertValueToFloat<TKey>(this MixinDictionary<TKey, long> dictionary)
        {
            return dictionary.ConvertToDictionary().ConvertValueToFloat().ConvertToMixinDictionary();
        }

        /// <inheritdoc cref="ConvertValueToFloat{TKey}(MixinDictionary{TKey, double})" />
        public static MixinDictionary<TKey, float> ConvertValueToFloat<TKey>(this MixinDictionary<TKey, int> dictionary)
        {
            return dictionary.ConvertToDictionary().ConvertValueToFloat().ConvertToMixinDictionary();
        }

        /// <inheritdoc cref="DictionaryExtensions.ConvertValueToLong{TKey}(Dictionary{TKey, int}"/>
        public static MixinDictionary<TKey, long> ConvertValueToLong<TKey>(this MixinDictionary<TKey, int> dictionary)
        {
            return dictionary.ConvertToDictionary().ConvertValueToLong().ConvertToMixinDictionary();
        }

        /// <inheritdoc cref="DictionaryExtensions.ConvertValueToInt{TKey}(Dictionary{TKey, long}"/>
        public static MixinDictionary<TKey, int> ConvertValueToInt<TKey>(this MixinDictionary<TKey, long> dictionary)
        {
            return dictionary.ConvertToDictionary().ConvertValueToInt().ConvertToMixinDictionary();
        }
    }
}
