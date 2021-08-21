using System;
using System.Collections.Generic;
using System.Text;
using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.RSSService;
using Microsoft.Extensions.Hosting;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class RssRunnerService : IRssRunnerService
    {
        public RssResultDTO RunRssGenerator(string url, string name, IHostEnvironment environment)
        {
            RssFeedGenerator generator = new RssFeedGenerator(url, name, environment);
            return generator.RSSCreator();
        }
    }
}
