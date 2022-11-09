
using System;

namespace Mixin.Utils
{
    /// <summary>
    /// Mathematical functions and extensions for <see cref="double"/>, <see cref="float"/>, <see cref="long"/>, <see cref="int"/>.
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// Calculates the modulo.
        /// </summary>
        public static double Modulo(this double value, double modulus) { return (value % modulus + modulus) % modulus; }

        /// <summary>
        /// Ensures the value to be at least a certain size.
        /// </summary>
        /// <returns>Returns the greater value.</returns>
        public static double LowerBound(this double value, double min) { return System.Math.Max(min, value); }

        /// <summary>
        /// Ensures the value to be at most a certain size.
        /// </summary>
        /// <returns>Returns the smaller value.</returns>
        public static double UpperBound(this double value, double max) { return System.Math.Min(max, value); }

        /// <summary>
        /// Ensures the value to be inside a certain range.
        /// </summary>
        /// <returns>Returns the bounded value.</returns>
        public static double Between(this double value, double min, double max) { return value.LowerBound(min).UpperBound(max); }

        /// <summary>
        /// Rounds the number.
        /// </summary>
        public static long Round(this double value) { return (long)System.Math.Round(value); }

        /// <summary>
        /// Rounds the number to the given number of decimals.
        /// </summary>
        public static double Round(this double value, int decimals) { return (double)System.Math.Round(value, decimals); }

        /// <summary>
        /// Rounds the number down.
        /// </summary>
        public static long Floor(this double value) { return (long)System.Math.Floor(value); }

        /// <summary>
        /// Rounds the number up.
        /// </summary>
        public static long Ceiling(this double value) { return (long)System.Math.Ceiling(value); }

        /// <summary>
        /// Calculates a multiplier value from the amplifier.
        /// </summary>
        public static double AmplifierToMultiplier(this double value)
        {
            return value >= 0 ? 1 + value : 1 / (1 - value);
        }

        /// <summary>
        /// Calculates an amplifier value from the multiplier.
        /// </summary>
        public static double MultiplierToAmplifier(this double value)
        {
            if (value <= 0)
                throw new ArgumentException($"Parameter \"value\" was \"{value}\", but must be greater than 0.");

            return value >= 1 ? value - 1 : 1 - (1 / value);
        }

        /// <inheritdoc cref="Modulo(double, double)" />
        public static float Modulo(this float value, float modulus) { return (float)Modulo((double)value, modulus); }
        /// <inheritdoc cref="Modulo(double, double)" />
        public static long Modulo(this long value, long modulus) { return (long)System.Math.Round(Modulo((double)value, modulus)); }
        /// <inheritdoc cref="Modulo(double, double)" />
        public static int Modulo(this int value, int modulus) { return (int)Modulo((long)value, modulus); }

        /// <inheritdoc cref="LowerBound(double, double)" />
        public static float LowerBound(this float value, float min) { return (float)LowerBound((double)value, min); }
        /// <inheritdoc cref="LowerBound(double, double)" />
        public static long LowerBound(this long value, long min) { return (long)System.Math.Round(LowerBound((double)value, min)); }
        /// <inheritdoc cref="LowerBound(double, double)" />
        public static int LowerBound(this int value, int min) { return (int)LowerBound((long)value, min); }

        /// <inheritdoc cref="UpperBound(double, double)" />
        public static float UpperBound(this float value, float max) { return (float)UpperBound((double)value, max); }
        /// <inheritdoc cref="UpperBound(double, double)" />
        public static long UpperBound(this long value, long max) { return (long)System.Math.Round(UpperBound((double)value, max)); }
        /// <inheritdoc cref="UpperBound(double, double)" />
        public static int UpperBound(this int value, int max) { return (int)UpperBound((long)value, max); }

        /// <inheritdoc cref="Between(double, double, double)" />
        public static float Between(this float value, float min, float max) { return (float)Between((double)value, min, max); }
        /// <inheritdoc cref="Between(double, double, double)" />
        public static long Between(this long value, float min, long max) { return (long)System.Math.Round(Between((double)value, min, max)); }
        /// <inheritdoc cref="Between(double, double, double)" />
        public static int Between(this int value, float min, int max) { return (int)Between((long)value, min, max); }

        /// <inheritdoc cref="Round(double)" />
        public static long Round(this float value) { return Round((double)value); }

        /// <inheritdoc cref="Round(double, int)" />
        public static double Round(this float value, int decimals) { return Round((double)value, decimals); }

        /// <inheritdoc cref="Floor(double)" />
        public static long Floor(this float value) { return Floor((double)value); }

        /// <inheritdoc cref="Ceiling(double)" />
        public static long Ceiling(this float value) { return Ceiling((double)value); }

        /// <inheritdoc cref="AmplifierToMultiplier(double)" />
        public static float AmplifierToMultiplier(this float value) { return (float)AmplifierToMultiplier((double)value); }

        /// <inheritdoc cref="MultiplierToAmplifier(double)(double)" />
        public static float MultiplierToAmplifier(this float value) { return (float)MultiplierToAmplifier((double)value); }
    }
}
