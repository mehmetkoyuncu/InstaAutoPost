using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Entities.Concrete;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface ICategoryService
    {
        List<CategoryDTO> GetAllCategories();
        int RemoveCategory(int id);
        Category GetById(int id);
        Category GetByRSSURL(string rssUrl);
        int AddCategory(string name, string url, int sourceId);
        List<CategoryDTO> GetAllCategoryBySourceId(int id);
        List<SelectboxSourceDTO> GetSourcesIdandName();
        int EditCategory(int id, string name, int sourceId);
        int AddCategory(string name, int sourceId);
        List<CategoryDTO> ApplyOrderCategoryList(int sourceId, int orderId);



    }
}
