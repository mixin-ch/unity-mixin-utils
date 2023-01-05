
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mixin.Utils
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class IEnumerableExtensions
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

        /// <inheritdoc cref="Shuffle[T](IList[T], System.Random)"/>
        public static void Shuffle<T>(this IList<T> toShuffle)
        {
            toShuffle.Shuffle(new System.Random());
        }

        /// <summary>
        /// Picks a random element from the enumerable.
        /// </summary>
        public static T PickRandom<T>(this IEnumerable<T> enumerable, System.Random random)
        {
            return enumerable.ElementAt(random.Next(enumerable.Count()));
        }

        /// <inheritdoc cref="PickRandom[T](IEnumerable[T], System.Random)"/>
        public static T PickRandom<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.PickRandom(new System.Random());
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

        /// <inheritdoc cref="PopRandom[T](IList[T], System.Random)"/>
        public static T PopRandom<T>(this IList<T> list)
        {
            return list.PopRandom(new System.Random());
        }

        /// <summary>
        /// Turns the Enumerable into a readable string.
        /// </summary>
        public static string Stringify<T>(this IEnumerable<T> list)
        {
            List<string> stringList = new List<string>();
            foreach (T t in list)
                if (t != null)
                    stringList.Add(t.ToString());
            return $"[{string.Join(", ", stringList)}]";
        }

        /// <summary>
        /// Checks if Enumerable has duplicate entries.
        /// </summary>
        public static bool HasDuplicates<T>(this IEnumerable<T> iEnumerable)
        {
            return iEnumerable.Count() != iEnumerable.Distinct().Count();
        }

        /// <summary>
        /// Checks if Enumerable has 0 entries.
        /// </summary>
        public static bool IsEmpty<T>(this IEnumerable<T> iEnumerable)
        {
            return iEnumerable.Count() == 0;
        }

        /// <summary>
        /// Gets the last entry.
        /// </summary>
        public static T GetLast<T>(this IEnumerable<T> iEnumerable)
        {
            if (iEnumerable.IsEmpty())
                throw new IndexOutOfRangeException();

            return iEnumerable.ElementAt(iEnumerable.Count() - 1);
        }

        /// <summary>
        /// Removes the last entry.
        /// </summary>
        public static void RemoveLast<T>(this IList<T> iList)
        {
            if (iList.IsEmpty())
                throw new IndexOutOfRangeException();

            iList.RemoveAt(iList.Count() - 1);
        }
    }
}
