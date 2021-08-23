using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.RSSService;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
            List<Category> categories = _uow.GetRepository<Category>().Get(x => x.Id == id && x.IsDeleted == false).OrderByDescending(x => x.UpdatedAt).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).ToList();
            List<CategoryDTO> categoryDTOs = Mapping.Mapper.Map<List<CategoryDTO>>(categories);
            foreach (var item in categoryDTOs)
            {
                int sourceContentCount = item.SourceContentsDTO.Count;
                if (sourceContentCount != 0)
                {
                    int sendedSource = item.SourceContentsDTO.Where(x => x.SendOutForPost == true).Count();
                    int sendedPercent = Convert.ToInt32(Math.Ceiling(Convert.ToDouble((100 * sendedSource) / sourceContentCount)));
                    item.SendedPostPercent = sendedPercent;
                }
                else
                    item.SendedPostPercent = 0;
            }
            return categoryDTOs;
        }
        public Category GetById(int id)
        {
            return _uow.GetRepository<Category>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }
        public string RemoveCategory(int id)
        {
            Category category = GetById(id);
            category.UpdatedAt = DateTime.Now;
            _uow.GetRepository<Category>().Remove(category);
            int result = _uow.SaveChanges();
            string sResult = result == 0 ? "Hata! kaynak silinemedi" : "Kaynak başarıyla silindi";
            return sResult;
        }

        public int AddCategory(string name, string url, int sourceId)
        {
            _uow.GetRepository<Category>().Add(new Category
            {
                Name = name.Trim(),
                Link = url.Trim(),
                InsertedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                SourceId = sourceId
            });
            return _uow.SaveChanges();
        }

        public List<CategoryDTO> GetAllCategories()
        {
            List<Category> categories = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).OrderByDescending(x => x.UpdatedAt).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).ToList();
            List<CategoryDTO> categoryDTOs = Mapping.Mapper.Map<List<CategoryDTO>>(categories);
            foreach (var item in categoryDTOs)
            {
                int sourceContentCount = item.SourceContentsDTO.Count;
                if (sourceContentCount != 0)
                {
                    int sendedSource = item.SourceContentsDTO.Where(x => x.SendOutForPost == true).Count();
                    int sendedPercent = Convert.ToInt32(Math.Ceiling(Convert.ToDouble((100 * sendedSource) / sourceContentCount)));
                    item.SendedPostPercent = sendedPercent;
                }
                else
                    item.SendedPostPercent = 0;
            }
            return categoryDTOs;
        }

        public Category GetByRSSURL(string rssUrl)
        {
            return _uow.GetRepository<Category>().Get(x => x.Link == rssUrl && x.IsDeleted == false).FirstOrDefault();
        }

        public List<CategoryDTO> GetAllCategoryBySourceId(int id)
        {
            List<Category> categories = _uow.GetRepository<Category>().Get(x => x.SourceId == id && x.IsDeleted == false).OrderByDescending(x => x.UpdatedAt).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).ToList();

            List<CategoryDTO> categoryDTOs = Mapping.Mapper.Map<List<CategoryDTO>>(categories);
            foreach (var item in categoryDTOs)
            {
                int sourceContentCount = item.SourceContentsDTO.Count;
                if (sourceContentCount != 0)
                {
                    int sendedSource = item.SourceContentsDTO.Where(x => x.SendOutForPost == true).Count();
                    int sendedPercent = Convert.ToInt32(Math.Ceiling(Convert.ToDouble((100 * sendedSource) / sourceContentCount)));
                    item.SendedPostPercent = sendedPercent;
                }
                else
                    item.SendedPostPercent = 0;
            }
            return categoryDTOs;


        }
    }
}