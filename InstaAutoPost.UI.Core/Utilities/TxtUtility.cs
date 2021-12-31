using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InstaAutoPost.UI.Core.Utilities
{
    public static class TxtUtility
    {
        public static string CreateTxtDocument(string rooth, string name, string content,string title,string tags=null)
        {
            string fullrooth = default;
                fullrooth = rooth + @"\" + name;
                TextWriter writer = new StreamWriter(fullrooth + ".txt");
                writer.WriteLine(title);
                writer.WriteLine(content);
                if (tags != null)
                    writer.WriteLine(tags);
                writer.Close();
                Log.Logger.Information($"Text dosyası başarıyla oluşturuldu.  - {fullrooth} - {title}");
                return fullrooth;
        }
    }
}
