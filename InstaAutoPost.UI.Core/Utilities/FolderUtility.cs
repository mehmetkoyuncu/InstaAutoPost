using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InstaAutoPost.UI.Core.Utilities
{
    public static class FolderUtility
    {
        public static string CreateFolder(string folderName,string rooth)
        {
            string result = default;
                string fullRooth = System.IO.Path.Combine(rooth, folderName);
                if (!Directory.Exists(fullRooth))
                {
                    result = Directory.CreateDirectory(fullRooth).FullName;
                }
                Log.Logger.Information($"Klasör başarıyla oluşturuldu.  - {folderName}");
                return result;
        }
    }
}
