using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InstaAutoPost.UI.Core.Utilities
{
    public static class RemoveFileUtility
    {
        public static void RemoveFile(string fileRooth)
        {
            try
            {
                if (fileRooth != null)
                {
                    if (File.Exists(fileRooth))
                    {
                        File.Delete(fileRooth);
                        Log.Logger.Information($"{fileRooth} - Dosya Başarıyla Silindi - {DateTime.Now}");
                    }
                }
            }
            catch (IOException ioExp)
            {
                Log.Logger.Error($"{ioExp.Message} - Dosya Silinirken Hata Oluştu -{fileRooth}- {DateTime.Now}");
            }
        }
    }
}
