using System;
using System.Diagnostics;
using System.Linq;

namespace ParentEspoir.Application
{
    /// <summary>
    /// This class existe because the function decimal.Parse(...)
    /// doesent work the same way on system with different language.
    /// </summary>
    public static class DecimalParser
    {
        public static bool CanParse(string text)
        {
            if (text == null) return false;

            text = text.Trim().Replace('.', ',');

            int commaCount = text.Count(s => s == ',');

            bool isDigitsOnly = commaCount <= 1 && !(text.Length == 1 && text[0] == ',');

            for (int i = 0; i < text.Length && isDigitsOnly; i++)
            {
                if (i == 0 && text[i] == '-')
                {
                    isDigitsOnly = true;
                }
                else if (text[i] == ',')
                {
                    isDigitsOnly = true;
                }
                else
                {
                    isDigitsOnly = char.IsDigit(text[i]);
                }
            }

            return text.Length > 0 && isDigitsOnly;
        }

        public static decimal Parse(string text)
        {
            Debug.Assert(CanParse(text), "This string cannot be parse");

            text = text.Trim().Replace('.', ',');

            int factor = 1;
            if (text[0] == '-')
            {
                factor = -1;
                text = text.Substring(1);
            }

            int startAtPower = -1;
            for (int i = 0; i < text.Length && text[i] != ','; i++)
            {
                startAtPower++;
            }

            decimal value = 0m;
            int power = startAtPower;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != ',')
                {
                    value += (decimal)(int.Parse(text[i].ToString()) * Math.Pow(10, power));
                    power--;
                }
            }

            return value * factor;
        }
    }
}
