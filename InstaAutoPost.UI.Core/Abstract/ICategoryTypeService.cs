using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface ICategoryTypeService
    {
        int Add(string name,string template);
        int RemoveCategoryType(int id);
        CategoryType GetById(int id);
        int EditSource(int id,string name, string template);
        List<CategoryTypeDTO> GetAllCategoryType();
        CategoryTypeDTO GetCategoryTypeDTO(int id);
        CategoryTypeDTO GetCategoryTypeByName(string name);
    }
}
