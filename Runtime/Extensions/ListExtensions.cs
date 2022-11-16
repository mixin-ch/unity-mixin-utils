
using System.Collections.Generic;

namespace Mixin.Utils
{
    /// <summary>
    /// Extensions for <see cref="List{T}"/>.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Shuffles the list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> toShuffle, System.Random random)
        {
            var count = toShuffle.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = random.Next(i, count);
                var tmp = toShuffle[i];
                toShuffle[i] = toShuffle[r];
                toShuffle[r] = tmp;
            }
        }

        /// <summary>
        /// Picks a random element from the list.
        /// </summary>
        public static T PickRandom<T>(this IList<T> list, System.Random random)
        {
            return list[random.Next(list.Count)];
        }

        /// <summary>
        /// Picks a random element from the list.
        /// Removes that element.
        /// </summary>
        public static T PopRandom<T>(this IList<T> list, System.Random random)
        {
            int i = random.Next(list.Count);
            T t = list[i];
            list.RemoveAt(i);
            return t;
        }
    }
}
