using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.CharacterConverter
{
   public static class CharacterConvertGenerator
    {
       

        public static string TurkishToEnglish(string text)
        {
            try
            {
                text = text.Replace('ç', 'c')
               .Replace('ğ', 'g')
               .Replace('ı', 'i')
               .Replace('İ', 'I')
               .Replace('ö', 'o')
               .Replace('ş', 's')
               .Replace('ü', 'u');
                Log.Logger.Information($"Kaynak görseli dosyası isminde Türkçe karakterler kaldırıldı. - {text}");
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Kaynak görseli dosyası isminde Türkçe karakterler kaldırılırken hata oluştu. - {text} - {exMessage}");
            }
            return text;
        }
        public static string RemovePunctuation(string text)
        {
            try
            {
                text = text.Replace("!", "")
                               .Replace(" ", "")
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
                Log.Logger.Information($"Kaynak görseli dosyası isminde gereksiz karakterler kaldırıldı - {text}");
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Kaynak görseli dosyası isminde gereksiz karakterler kaldırılırken hata oluştu. - {text} - {exMessage}");
            }
            return text;
        }
    }
}
