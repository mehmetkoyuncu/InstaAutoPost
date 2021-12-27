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
            string fullRooth = System.IO.Path.Combine(rooth,folderName);
            string result="";
            if (!Directory.Exists(fullRooth))
            {
               result= Directory.CreateDirectory(fullRooth).FullName;
            }
            return result;
        }
    }
}
