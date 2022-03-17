using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringAndIntManager 
{
    public static string FormatThousand(this int number)
    {
        if (number == 0)
            return "0";
        else
            return number.ToString("#,#");
    }

    public static string FormatThousand(this long number)
    {
        if (number == 0)
            return "0";
        else
            return number.ToString("#,#");
    }

}
