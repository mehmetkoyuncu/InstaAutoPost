using System;
using System.Collections.Generic;
using System.Text;
using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.RSSService;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class RssRunnerService : IRssRunnerService
    {
        public RssResultDTO RunRssGenerator(string url, string name, string environment)
        {
            try
            {
                RssFeedGenerator generator = new RssFeedGenerator(url, name, environment);
                Log.Logger.Information($"RssGenerator otomatik olarak çalıştırıldı - {name}");
                return generator.RSSCreator();

            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata ! RssGenerator otomatik olarak çalıştırılırken hata oluştu -  {exMessage} - {name}");
                throw;
            }

        }
    }
}
