using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberFormatter
{
    static Dictionary<string, long> numberFormattingPairs = new Dictionary<string, long>();

    public static void init()
    {
        if (numberFormattingPairs.Count < 1)
        {
            numberFormattingPairs.Add("k", 1000);
            numberFormattingPairs.Add("m", 1000000);
            numberFormattingPairs.Add("b", 10000000000);
        }
    }


    public static string formatNumber(float number)
    {
        init();
        string formattedNumber = null;

        foreach (var numberFormat in numberFormattingPairs)
        {
            if (numberFormat.Value > number)
                continue;

            formattedNumber = (number / numberFormat.Value).ToString("0.0") + numberFormat.Key;
        }

        if (formattedNumber == null)
            formattedNumber = number.ToString("0.0");

        return formattedNumber;
    }
}
