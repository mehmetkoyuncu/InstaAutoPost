using System;
using System.Collections.Generic;
using System.Text;
using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.RSSService;
using InstaAutoPost.UI.Data.Entities.Concrete;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class RssRunnerService : IRssRunnerService
    {
        public RssResultDTO RunRssGenerator(string url, int categoryTypeId, string environment)
        {
            try
            {
                string typeName = new CategoryTypeService().GetById(categoryTypeId).Name;
                RssFeedGenerator generator = new RssFeedGenerator(url, typeName, environment);
                Log.Logger.Information($"RssGenerator otomatik olarak çalıştırıldı - {categoryTypeId}");
                return generator.RSSCreator();

            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata ! RssGenerator otomatik olarak çalıştırılırken hata oluştu -  {exMessage} - {categoryTypeId}");
                throw;
            }

        }
    }
}
