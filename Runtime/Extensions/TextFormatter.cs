using System;

namespace Mixin.Utils
{
    /// <summary>
    /// Fast and easy text formatting.
    /// </summary>
    public static class TextFormatter
    {
        /// <inheritdoc cref="GetPercentage(double, double, int)"/>
        public static double GetPercentage(double currentValue, double maxValue) =>
            GetPercentage(currentValue, maxValue, 0);

        /// <summary>
        /// Get the percentage value.
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="decimals">By default it returns natural numbers, 0 decimals</param>
        /// <returns></returns>
        public static double GetPercentage(double currentValue, double maxValue, int decimals)
        {
            double percentage = currentValue / maxValue * 100;
            return RoundValueToDecimals(percentage, decimals);
        }

        /// <summary>
        /// Rounds the number to specific decimals.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static double RoundValueToDecimals(double value, int decimals)
        {
            return System.Math.Round(value, decimals);
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

        /// <summary>
        /// 1000 => 1'000
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatThousand(this int number)
        {
            if (number == 0)
                return "0";
            else
                return number.ToString("#,#");
        }

        /// <inheritdoc cref="FormatThousand(int)"/>
        public static string FormatThousand(this long number)
        {
            if (number == 0)
                return "0";
            else
                return number.ToString("#,#");
        }
    }
}
