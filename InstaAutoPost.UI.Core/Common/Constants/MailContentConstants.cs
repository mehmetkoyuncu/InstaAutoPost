using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Common.Constants
{
    public class MailContentConstants
    {
        public const string AutoJobContent = "<h3>{{{RemovedContent}}} adet içerik başarıyla silindi.</h3> <small> Bu içerikler ve görselleri ile tekrar işlem yapılamaz</small><h2 style='text-align: right;'><br></h2><h2 style='text-align: right;'><span style='font-family: Impact;'>INSTAAUTOPOST</span></h2>";
        public const string PullRSSContent = "<h3>{{{CategoryName}}} kategorisine ait {{{RSSAddedCount}}} adet içerik otomatik olarak eklendi.</h3><p><b>Kaynak Adı</b> : {{{SourceName}}}</p><p><b>Kategori Adı</b> : {{{CategoryName}}}</p><p><b>Eklenen İçerik Sayısı</b> : {{{RSSAddedCount}}}</p><h2 style='text-align: right;'><br></h2><h2 style='text-align: right;'><span style='font-family: Impact;'>INSTAAUTOPOST</span></h2>";
    }
}
