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
            return _uow.GetRepository<Category>()
                .Get(x => x.Id == id && x.IsDeleted == false)
                .Include(x=>x.Source).Include(x=>x.SourceContents.Where(x => x.IsDeleted == false))
                .FirstOrDefault();
        }
     
        #endregion
        #region Kategoriyi düzenle
        public int EditCategory(int id, CategoryImageViewDTO categoryImageView)
        {
            Category category = GetById(id);
            category.UpdatedAt = DateTime.Now;
            category.Name = categoryImageView.Name;
            category.SourceId = categoryImageView.SourceId;
            category.Tags = categoryImageView.Tags.Replace(" ","");
            _uow.GetRepository<Category>().Update(category);
            return _uow.SaveChanges();
        }
        public CategoryImageViewDTO GetCategoryById(int id)
        {
            Category category = _uow.GetRepository<Category>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            return Mapping.Mapper.Map<Category, CategoryImageViewDTO>(category);
        }
        #endregion
        #region Kategori Sil
        public int RemoveCategory(int id)
        {
            int result = 0;
            try
            {
                Category category = GetById(id);
                category.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Category>().Remove(category);
                List<SourceContent> sourceContentList = _uow.GetRepository<SourceContent>()
                    .Get(x => x.CategoryId == category.Id)
                    .ToList();
                foreach (var item in sourceContentList)
                    _uow.GetRepository<SourceContent>().Remove(item);
                result= _uow.SaveChanges();

            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
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
        public int AddCategory(CategoryImageViewDTO categoryImageView)
        {
            _uow.GetRepository<Category>().Add(new Category
            {
                Name = categoryImageView.Name.Trim(),
                Link = null,
                InsertedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                SourceId = categoryImageView.SourceId,
                Tags= categoryImageView.Tags.Replace(" ", "")
        });
            return _uow.SaveChanges();
        }
        #endregion
        #region Tüm Kategorileri Getir
        public List<CategoryDTO> GetAllCategories()
        {
            List<Category> categories = _uow.GetRepository<Category>()
                .Get(x => x.IsDeleted == false)
                .Include(x => x.Source)
                .Include(x => x.SourceContents
                .Where(x => x.IsDeleted == false))
                .OrderByDescending(x => x.UpdatedAt)
                .ToList();
            List<CategoryDTO> categoryDTOs = Mapping.Mapper.Map<List<CategoryDTO>>(categories);
            return SetPercentAndCount(categoryDTOs);
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
        #region Filtre
        public List<CategoryDTO> Filter(int sourceId, int orderId,string searchText)
        {
            List<Category> categoryList = null;
            switch (orderId)
            {
                case -1:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    break;
                case 0:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                       categoryList= _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x=>x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if(sourceId==-1&&!string.IsNullOrWhiteSpace(searchText))
                       categoryList= _uow.GetRepository<Category>().Get(x => x.IsDeleted == false&&x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if(sourceId>-1&& string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId==sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())&&x.SourceId==sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    break;
                case 1:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.Name).ToList();
                    else if (sourceId ==- 1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.Name).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.Name).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.Name).ToList();
                    break;
                case 2:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Name).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Name).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Name).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Name).ToList();
                    break;
                case 3:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.Source.Name).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.Source.Name).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.Source.Name).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.Source.Name).ToList();
                    break;
                case 4:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Source.Name).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Source.Name).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Source.Name).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Source.Name).ToList();
                    break;
                case 5:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.SourceContents.Where(x=>x.IsDeleted==false).Count()).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.SourceContents.Where(x=>x.IsDeleted==false).Count()).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.SourceContents.Where(x=>x.IsDeleted==false).Count()).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.SourceContents.Where(x=>x.IsDeleted==false).Count()).ToList();
                    break;
                case 6:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.SourceContents.Where(x=>x.IsDeleted==false).Count()).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.SourceContents.Where(x=>x.IsDeleted==false).Count()).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.SourceContents.Where(x=>x.IsDeleted==false).Count()).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.SourceContents.Where(x=>x.IsDeleted==false).Count()).ToList();
                    break;
                case 7:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.SourceContents.Where(x => x.SendOutForPost&&x.IsDeleted==false).Count()).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.SourceContents.Where(x => x.SendOutForPost&&x.IsDeleted==false).Count()).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.SourceContents.Where(x => x.SendOutForPost&&x.IsDeleted==false).Count()).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.SourceContents.Where(x => x.SendOutForPost&&x.IsDeleted==false).Count()).ToList();
                    break;
                case 8:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.SourceContents.Where(x => x.SendOutForPost&&x.IsDeleted==false).Count()).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.SourceContents.Where(x => x.SendOutForPost&&x.IsDeleted==false).Count()).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.SourceContents.Where(x => x.SendOutForPost&&x.IsDeleted==false).Count()).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.SourceContents.Where(x => x.SendOutForPost&&x.IsDeleted==false).Count()).ToList();
                    break;
                case 9:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.UpdatedAt).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.UpdatedAt).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.UpdatedAt).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.UpdatedAt).ToList();
                    break;
                case 10:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    break;
                case 11:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.InsertedAt).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.InsertedAt).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.InsertedAt).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderBy(x => x.InsertedAt).ToList();
                    break;
                case 12:
                    if (sourceId == -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.InsertedAt).ToList();
                    else if (sourceId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.InsertedAt).ToList();
                    else if (sourceId > -1 && string.IsNullOrWhiteSpace(searchText))
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.InsertedAt).ToList();
                    else
                        categoryList = _uow.GetRepository<Category>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower()) && x.SourceId == sourceId).Include(x => x.Source).Include(x => x.SourceContents.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.InsertedAt).ToList();    
                    break;
                default:
                    categoryList = null;
                    break;
            }

            List<CategoryDTO> categoryDTOs = Mapping.Mapper.Map<List<CategoryDTO>>(categoryList);
            return SetPercentAndCount(categoryDTOs);

        }
        #endregion
        #region URL'e göre kategoriyi getir
        public Category GetByRSSURL(string rssUrl)
        {
            return _uow.GetRepository<Category>().Get(x => x.Link == rssUrl && x.IsDeleted == false).FirstOrDefault();
        }
        #endregion
        #region Kaynak Id'sine göre kategori listesini getir
        public List<CategoryDTO> GetAllCategoryBySourceId(int id,string searchText)
        {
            List<Category> categories = null;
            if (string.IsNullOrWhiteSpace(searchText))
            {
               categories = _uow.GetRepository<Category>()
                    .Get(x => x.SourceId == id && x.IsDeleted == false)
                    .OrderByDescending(x => x.UpdatedAt)
                    .Include(x => x.Source)
                    .Include(x => x.SourceContents
                    .Where(x => x.IsDeleted == false))
                    .ToList();
            }
            else {
                categories = _uow.GetRepository<Category>()
                    .Get(x => x.SourceId == id && x.IsDeleted == false&&x.Name.ToLower().Contains(searchText.ToLower()))
                    .OrderByDescending(x => x.UpdatedAt)
                    .Include(x => x.Source)
                    .Include(x => x.SourceContents
                    .Where(x => x.IsDeleted == false))
                    .ToList();
            }
            List<CategoryDTO> categoryDTOs = Mapping.Mapper.Map<List<CategoryDTO>>(categories);
            return SetPercentAndCount(categoryDTOs);
        }
        #endregion
        #region Kategori içeriği yüzde ayarları
        private List<CategoryDTO> SetPercentAndCount(List<CategoryDTO> categoryDTOs)
        {
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
        #region Id'ye göre CategoryDTO Getir
        public CategoryDTO GetCategoryDTOById(int id)
        {
            Category category = GetById(id);
            return Mapping.Mapper.Map<Category, CategoryDTO>(category);
        }
        #endregion

    }
}