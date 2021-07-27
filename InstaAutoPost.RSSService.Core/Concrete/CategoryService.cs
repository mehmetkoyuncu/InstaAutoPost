using InstaAutoPost.RSSService.Core.Abstract;
using InstaAutoPost.RSSService.Data.Context;
using InstaAutoPost.RSSService.Data.Entities.Concrete;
using InstaAutoPost.RSSService.Data.UnitOfWork.Abstract;
using InstaAutoPost.RSSService.Data.UnitOfWork.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace InstaAutoPost.RSSService.Core.Concrete
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
