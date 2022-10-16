using RotaryHeart.Lib.SerializableDictionary;

namespace Mixin.Utils
{
    /// <summary>
    /// This is a simple extention for SerializableDictionaryBase to shorten the name.
    /// Yes, It really is a Serializable Dictionary!
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class MixinDictionary<TKey, TValue> : SerializableDictionaryBase<TKey, TValue>
    {
    }
}
