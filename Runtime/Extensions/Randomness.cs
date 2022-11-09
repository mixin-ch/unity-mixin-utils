
using System;

namespace Mixin.Utils
{
    /// <summary>
    /// Functions for randomness and extensions for <see cref="Random"/>.
    /// </summary>
    public static class Randomness
    {
        /// <summary>
        /// Randomly returns either true or false.
        /// </summary>
        /// <param name="probability">Probability to return true.</param>
        public static bool RandomTrue(this Random random, double probability) { return random.NextDouble() < probability; }

        /// <summary>
        /// Returns randomly either floor or ceil dependant on the value.
        /// "3.8" has 80% chance of "4" and 20% chance of "3".
        /// </summary>
        public static long RoundRandom(this Random random, double value)
        {
            return value.Floor() + (random.RandomTrue(value.Modulo(1)) ? 1 : 0);
        }

        /// <summary>
        /// Returns random number in range.
        /// </summary>
        /// <param name="min">inclusive</param>
        /// <param name="max">exclusive</param>
        public static double Range(this Random random, double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Returns random whole number in range.
        /// </summary>
        /// <param name="min">inclusive</param>
        /// <param name="max">inclusive</param>
        public static long Range(this Random random, long min, long max)
        {
            max++;
            long result = random.Next((int)(min >> 32), (int)(max >> 32));
            result = (result << 32);
            result = result | (long)random.Next((int)min, (int)max);
            return result;
        }

        /// <inheritdoc cref="RandomTrue(Random, double)" />
        public static bool RandomTrue(this float probability, Random random) { return RandomTrue(random, (double)probability); }

        /// <inheritdoc cref="RoundRandom(Random, double)" />
        public static long RoundRandom(this float value, Random random) { return RoundRandom(random, (double)value); }

        /// <inheritdoc cref="Range(Random, double, double)" />
        public static double Range(this Random random, float min, float max) { return Range(random, (double)min, (double)max); }

        /// <inheritdoc cref="Range(Random, long, long)" />
        public static int Range(this Random random, int min, int max) { return random.Next(min, max + 1); }
    }
}
