using System;

namespace Mixin.Utils
{
    /// <summary>
    /// Fast and easy text formatting.
    /// </summary>
    public static class TextFormatter
    {
        /// <inheritdoc cref="GetPercentage(float, float, int)"/>
        public static int GetPercentage(float currentValue, float maxValue) =>
            (int)GetPercentage(currentValue, maxValue, 0);

        /// <summary>
        /// Get the percentage value with rounded numbers.
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="decimals">Rounds to specified number of decimals.</param>
        /// <returns></returns>
        public static float GetPercentage(float currentValue, float maxValue, int decimals)
        {
            float percentage = currentValue / maxValue * 100;
            return RoundValueToDecimals(percentage, decimals);
        }

        /// <summary>
        /// Returns the Percentage without rounding the value. <br></br>
        /// => 26.9273985768762
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static float GetPercentageWithoutRound(float currentValue, float maxValue)
        {
            return GetFraction(currentValue,maxValue) * 100;
        }

        /// <summary>
        /// Get the Fraction of two numbers. <br></br>
        /// => 0.2
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static float GetFraction(float currentValue, float maxValue)
        {
            return currentValue / maxValue;
        }

        /// <summary>
        /// Rounds the value to the specified number of decimals.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static float RoundValueToDecimals(float value, int decimals)
        {
            return (float)System.Math.Round(value, decimals);
        }

        /// <summary>
        /// Convert DateTime to Human Readable String. <br></br>
        /// => 30. April 2022
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDateString(DateTime date)
        {
            // Converts the object to string
            return string.Format("{0:D}", date);
        }

        /// <inheritdoc cref="FormatThousand(long)"/>
        public static string FormatThousand(this int number) =>
            FormatThousand(number);

        /// <summary>
        /// 1000 => 1'000
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatThousand(this long number)
        {
            if (number == 0)
                return "0";
            else
                return number.ToString("#,#");
        }
    }
}
