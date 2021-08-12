using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface ICategoryService
    {
        List<CategoryDTO> GetSourceWithCategoriesById(int id);
        List<CategoryDTO> GetAllCategories();
        string RemoveCategory(int id);
        Category GetById(int id);
        Category GetByRSSURL(string rssUrl);
        int AddCategory(string name, string url,int sourceId);
        public void RunRssGenerator(string url, string name);

    }
}
