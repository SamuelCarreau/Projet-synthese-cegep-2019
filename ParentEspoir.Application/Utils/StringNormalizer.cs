using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ParentEspoir.Application
{
    /*Cette classe permet de "normaliser" les strings, c'est a dire
     rendre celle-ci lisible peut importe sont encodings*/
    public static class StringNormalizer
    {
        public static string Normalize(string text)
        {
            if (text == null) return "";

            var normalizedString = RemoveDiacritics(text);
            normalizedString = normalizedString.ToUpper();
            normalizedString = GetOnlyAlpabeticsChar(normalizedString);

            return normalizedString;
        }

        private static string GetOnlyAlpabeticsChar(string normalizedString)
        {
            var result = new StringBuilder();
            for (int i = 0; i < normalizedString.Length; i++)
            {
                if (char.IsUpper(normalizedString[i]))
                {
                    result.Append(normalizedString[i]);
                }
            }

            return result.ToString();
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
