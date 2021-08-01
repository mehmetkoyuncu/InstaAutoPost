using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class CategoryService : ICategoryService
    {
        IUnitOfWork __uow;
        public CategoryService()
        {
            __uow = new EFUnitOfWork(new RSSContextEF());
        }
        public List<Category> GetCategoriesById(int id)
        {
            List<Category> categoryList = __uow.GetRepository<Category>().Get(x => x.SourceId == id && x.IsDeleted == false).ToList();
            return categoryList;
        }

    }
}
