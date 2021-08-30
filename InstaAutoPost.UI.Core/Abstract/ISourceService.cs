﻿using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Entities.Concrete;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface ISourceService
    {
        int Add(string name, string image,string url);
        int AddSource(string name, string image,string contentRootPath);
        int RemoveSource (int id);
        Source GetById(int id);
        List<Source> GetByName(string name);
        int EditSource(int id, string name, string image,string contentRootPath);
        List<SourceDTO> GetAllSources();
        List<Source> GetDeletedSource();
        SourceWithCategoryCountDTO GetSourceWithCategoryCount(int id);
        List<SelectboxSourceDTO> GetSourcesForSelectBox();
        List<SourceDTO> Filter(int orderId, string searchText);

    }
}
