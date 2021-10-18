using InstaAutoPost.UI.Core.Common.DTOS;
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
        int AddSource(SourceAddOrUpdateDTO sourceDTO, string contentRootPath);
        int RemoveSource (int id);
        Source GetById(int id);
        int EditSource(int id, SourceAddOrUpdateDTO source, string contentRootPath);
        List<SourceDTO> GetAllSources();
        List<SourceDTO> Filter(int orderId, string searchText);
        SourceAddOrUpdateDTO GetSourceById(int id);

    }
}
