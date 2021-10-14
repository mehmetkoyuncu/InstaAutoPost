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
        CategoryDTO GetCategoryDTOById(int id);
        Category GetByRSSURL(string rssUrl);
        int AddCategory(string name, string url, int sourceId);
        List<CategoryDTO> GetAllCategoryBySourceId(int id, string searchText);
        List<SelectboxSourceDTO> GetSourcesIdandName();
        int EditCategory(int id,CategoryImageViewDTO categoryImageView);
        int AddCategory(CategoryImageViewDTO categoryImageView);
        List<CategoryDTO> Filter(int sourceId, int orderId, string searchText);
        CategoryImageViewDTO GetCategoryById(int id);



    }
}
