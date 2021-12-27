using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.CharacterConverter
{
   public static class CharacterConvertGenerator
    {
       

        public static string TurkishToEnglish(string text)
        {
            text = text.Replace('ç', 'c')
                .Replace('ğ', 'g')
                .Replace('ı', 'i')
                .Replace('İ', 'I')
                .Replace('ö', 'o')
                .Replace('ş', 's')
                .Replace('ü', 'u');

            return text;
        }
        public static string RemovePunctuation(string text)
        {
            text = text.Replace("!", "")
                .Replace(" ","")
                 .Replace("/", "")
                 .Replace("'", "")
                 .Replace("+", "")
                 .Replace("$", "")
                 .Replace("#", "")
                 .Replace("%", "")
                 .Replace("&", "")
                 .Replace("[", "")
                 .Replace("(", "")
                 .Replace(")", "")
                 .Replace("]", "")
                 .Replace("=", "")
                 .Replace("?", "")
                 .Replace("*", "")
                 .Replace("\\", "")
                 .Replace("-", "")
                 .Replace("_", "")
                 .Replace(".", "")
                 .Replace(",", "")
                 .Replace(";", "")
                 .Replace(":", "")
                 .Replace("\"", "")
                 .Replace("<", "")
                 .Replace(">", "")
                 .Replace("|", "");
            return text;
        }
    }
}
