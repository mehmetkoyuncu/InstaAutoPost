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
        #region Id'ye göre kategoriyi getir
        public Category GetById(int id)
        {
            return _uow.GetRepository<Category>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }
        #endregion
        #region Kategoriyi düzenle
        public int EditCategory(int id, string name, int sourceId)
        {
            Category category = GetById(id);
            category.UpdatedAt = DateTime.Now;
            category.Name = name;
            category.SourceId = sourceId;
            _uow.GetRepository<Category>().Update(category);
            return _uow.SaveChanges();
        }
        #endregion
        #region Kategori Sil
        public int RemoveCategory(int id)
        {
            Category category = GetById(id);
            category.UpdatedAt = DateTime.Now;
            _uow.GetRepository<Category>().Remove(category);
            return _uow.SaveChanges();
        }
        #endregion
        #region Kategori Ekle (Link)
        public int AddCategory(string name, string url, int sourceId)
        {
            _uow.GetRepository<Category>().Add(new Category
            {
                Name = name.Trim(),
                Link = url.Trim(),
                InsertedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                SourceId = sourceId,
            });
            return _uow.SaveChanges();
        }
        #endregion
        #region Kategori Ekle
        public int AddCategory(string name, int sourceId)
        {
            _uow.GetRepository<Category>().Add(new Category
            {
                Name = name.Trim(),
                Link = null,
                InsertedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                SourceId = sourceId,
            });
            return _uow.SaveChanges();
        }
        #endregion
        #region Kategorileri Getir
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
        #endregion
        #region Kaynak Listesini Getir
        public List<SelectboxSourceDTO> GetSourcesIdandName()
        {
            List<SelectboxSourceDTO> sources = _uow.GetRepository<Source>()
                .Get(x => x.IsDeleted == false)
                .Select(x => new SelectboxSourceDTO()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            return sources;
        }
        #endregion
        #region Sırala
        public List<CategoryDTO> ApplyOrderCategoryList(int sourceId, int orderId)
        {
            List<Category> categoryList = null;
            switch (orderId)
            {
                case 0:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x=>x.Source).OrderByDescending(x => x.UpdatedAt).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    break;
                case 1:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents).OrderBy(x => x.Name).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents).OrderBy(x => x.Name).ToList();
                    break;
                case 2:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents).OrderByDescending(x => x.Name).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents).OrderByDescending(x => x.Name).ToList();
                    break;
                case 3:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x=>x.Source).Include(x => x.SourceContents).OrderBy(x => x.Source.Name).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x=>x.Source).Include(x => x.SourceContents).OrderBy(x => x.Source.Name).ToList();
                    break;
                case 4:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents).OrderByDescending(x => x.Source.Name).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents).OrderByDescending(x => x.Source.Name).ToList();
                    break;
                case 5:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents).OrderByDescending(x => x.SourceContents.Count()).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents).OrderByDescending(x => x.SourceContents.Count()).ToList();
                    break;
                case 6:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents).OrderBy(x => x.SourceContents.Count()).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents).OrderBy(x => x.SourceContents.Count()).ToList();
                    break;
                case 7:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents).OrderByDescending(x => x.SourceContents.Where(x=>x.SendOutForPost).Count()).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents).OrderByDescending(x => x.SourceContents.Where(x => x.SendOutForPost).Count()).ToList();
                    break;
                case 8:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents).OrderBy(x => x.SourceContents.Where(x => x.SendOutForPost).Count()).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents).OrderBy(x => x.SourceContents.Where(x => x.SendOutForPost).Count()).ToList();
                    break;
                case 9:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents).OrderBy(x => x.UpdatedAt).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).OrderBy(x => x.UpdatedAt).ToList();
                    break;
                case 10:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents).OrderByDescending(x => x.UpdatedAt).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    break;
                case 11:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents).OrderBy(x => x.InsertedAt).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).OrderBy(x => x.InsertedAt).ToList();
                    break;
                case 12:
                    categoryList = sourceId != -1 ? _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents).OrderByDescending(x => x.InsertedAt).ToList() : _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).OrderByDescending(x => x.InsertedAt).ToList();
                    break;
                default:categoryList = null;
                    break;
            }

            List<CategoryDTO> categoryDTOs = Mapping.Mapper.Map<List<CategoryDTO>>(categoryList);
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
        #endregion
        #region, URL'e göre kategoriyi getir
        public Category GetByRSSURL(string rssUrl)
        {
            return _uow.GetRepository<Category>().Get(x => x.Link == rssUrl && x.IsDeleted == false).FirstOrDefault();
        }
        #endregion
        #region Kaynak Id'sine göre kategori listesini getir
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
        #endregion
    }
}