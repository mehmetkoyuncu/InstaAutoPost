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
            List<Category> categories = _uow.GetRepository<Category>().Get(x => x.SourceId == id && x.IsDeleted == false).OrderByDescending(x => x.UpdatedAt).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).ToList();
            List<CategoryDTO> categoryDTOs = Mapping.Mapper.Map<List<CategoryDTO>>(categories);
            foreach (var item in categoryDTOs)
            {
                int sourceContentCount = item.SourceContentsDTO.Count;
                if (sourceContentCount != 0)
                {
                    int sendedSource = item.SourceContentsDTO.Where(x => x.SendOutForPost == true).Count();
                    int sendedPercent =Convert.ToInt32(Math.Ceiling(Convert.ToDouble((100 * sendedSource) / sourceContentCount)));
                    item.SendedPostPercent = sendedPercent;
                }
                else
                {
                    item.SendedPostPercent = 0;
                }

            }
            return categoryDTOs;
        }
    }
}