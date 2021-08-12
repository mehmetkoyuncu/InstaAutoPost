using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface ISourceService
    {
        int Add(string name, string image,string url);
        string DeleteById(int id);
        Source GetById(int id);
        List<Source> GetByName(string name);
        string Update(string image,string name, int id);
        List<SourceDTO> GetAll();
        List<Source> GetDeletedSource();
        SourceWithCategoryCountDTO GetSourceWithCategoryCount(int id);
    }
}
