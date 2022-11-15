using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mixin.Utils
{
    public static class TextFormatter
    {
        public static double GetPercentage(double currentValue, double maxValue) =>
            GetPercentage(currentValue, maxValue, 0);

        public static double GetPercentage(double currentValue, double maxValue, int decimals)
        {
            double percentage = currentValue / maxValue * 100;
            return RoundValueToDecimals(percentage, decimals);
        }

        public static double RoundValueToDecimals(double value, int decimals)
        {
            return System.Math.Round(value, decimals);
        }

        public static string GetDateString(DateTime date)
        {
            // Converts the object to string
            return string.Format("{0:D}", date);
        }
    }
}
