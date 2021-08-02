using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class CategoryService : ICategoryService
    {
        IUnitOfWork _uow;
        public CategoryService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        public List<CategoryDTO> GetSourceWithCategoriesById(int id)
        {
            List<Category> categories = _uow.GetRepository<Category>().Get(x => x.SourceId == id && x.IsDeleted == false).Include(x => x.Source).ToList();
            return Mapping.Mapper.Map<List<CategoryDTO>>(categories);
        }
    }
}