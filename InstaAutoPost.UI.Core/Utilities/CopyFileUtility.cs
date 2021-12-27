using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InstaAutoPost.UI.Core.Utilities
{
    public static class CopyFileUtility
    {
        public static bool CopyFile(string source,string destination,string fileName)
        {
            bool control = false;
            string sourceFile = System.IO.Path.Combine(source, fileName);
            string destFile = System.IO.Path.Combine(destination, fileName);
            FileInfo sourceInfo = new FileInfo(sourceFile);
            if (sourceInfo.Exists == true)
            {
                File.Copy(sourceFile, destFile, true);
                control = true;
            }
            return control;
        }
    }
}
