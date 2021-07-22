using InstaAutoPost.RSSService.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.RSSService.Core.Abstract
{
    public interface ISourceService
    {
        string Add(Source source);
        string DeleteById(int id);
        Source GetById(int id);
        List<Source> GetByName(string name);
        string Update(Source source,int id);
        List<Source> GetAll();
        List<Source> GetDeletedSource();
    }
}
