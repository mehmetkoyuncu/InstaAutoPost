using InstaAutoPost.UI.Core.Common.DTOS;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface IRssRunnerService
    {
        RssResultDTO RunRssGenerator(string url, string name, IHostEnvironment environment);
    }
}
