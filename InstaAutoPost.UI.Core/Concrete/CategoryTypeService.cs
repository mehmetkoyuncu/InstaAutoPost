using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class CategoryTypeService : ICategoryTypeService
    {
        private readonly IUnitOfWork _uow;
        public CategoryTypeService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        public int Add(string name)
        {
            int result = default;
            var type=_uow.GetRepository<CategoryType>().Get(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (type == null)
            {
                CategoryType categoryType = new CategoryType()
                {
                    InsertedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Name = name,
                    IsDeleted = false
                };
                _uow.GetRepository<CategoryType>().Add(categoryType);
                result = _uow.SaveChanges();
                if (result > 0)
                    Log.Logger.Information($"Kategori tipi eklendi.  - {name}");
                else
                    Log.Logger.Error($"Hata! Kategori tipi eklenirken hata oluştu.  - {name}");
                return result;
            }
            else
            {
                result = -1;
                Log.Logger.Error($"Hata! Aynı isme sahip Kategori tibi bulunmaktadır.  - {name}");
                return result;
            }
          
        }

        public int EditSource(int id, string name)
        {
            try
            {
                int result = default;
               
                var type = _uow.GetRepository<CategoryType>().Get(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
                if (type == null)
                {
                    CategoryType categoryType = GetById(id);
                    categoryType.Name = name;
                    categoryType.UpdatedAt = DateTime.Now;
                    _uow.GetRepository<CategoryType>().Update(categoryType);
                    result = _uow.SaveChanges();
                    if (result > 0)
                        Log.Logger.Information($"Kategori Tipi güncellendi.  - {categoryType.Name}");
                    else
                        Log.Logger.Error($"Hata! Kategori Tipi güncellenirken hata oluştu.  - {categoryType.Name}");
                    return result;
                }
                else
                {
                    result = -1;
                    Log.Logger.Error($"Hata! Aynı isme sahip Kategori tibi bulunmaktadır.  - {name}");
                    return result;
                }

            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Kategori Tipi eklenirken hata oluştu.  -{name} {exMessage}");
                throw;
            }
        }

        public List<CategoryTypeDTO> GetAllCategoryType()
        {
            List<CategoryType> categoryType = _uow.GetRepository<CategoryType>().Get(x => x.IsDeleted == false).OrderByDescending(x => x.UpdatedAt).ToList();
            List<CategoryTypeDTO> categoryTypeDTOS = Mapping.Mapper.Map<List<CategoryTypeDTO>>(categoryType);
            return categoryTypeDTOS;
        }

        public CategoryType GetById(int id)
        {
            CategoryType categoryType = _uow.GetRepository<CategoryType>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            return categoryType;
        }

        public CategoryTypeDTO GetCategoryTypeDTO(int id)
        {
            CategoryType categoryType = _uow.GetRepository<CategoryType>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            return Mapping.Mapper.Map<CategoryType, CategoryTypeDTO>(categoryType);
        }

        public int RemoveCategoryType(int id)
        {
            try
            {
                int result = default;
                CategoryType categoryType = GetById(id);
                categoryType.UpdatedAt = DateTime.Now;
                _uow.GetRepository<CategoryType>().Remove(categoryType);
                result = _uow.SaveChanges();
                if (result > 0)
                {
                    if (result > 0)
                    {
                        var mediaCatgory = _uow.GetRepository<SocialMediaAccountsCategoryType>().Get(x => x.CategoryTypeId == id).ToList();
                        if (mediaCatgory.Count > 0)
                        {
                            foreach (var item in mediaCatgory)
                            {
                                _uow.GetRepository<SocialMediaAccountsCategoryType>().Remove(item);
                                _uow.SaveChanges();
                            }
                        }
                        var category= _uow.GetRepository<Category>().Get(x => x.CategoryTypeId == id).ToList();
                        if (category.Count > 0)
                        {
                            foreach (var item in category)
                            {
                                _uow.GetRepository<Category>().Remove(item);
                                _uow.SaveChanges();
                            }
                        }
                    }
                    Log.Logger.Information($"Kategori Tipi silindi.  - {categoryType.Name}");
                }
                else
                    Log.Logger.Error($"Hata! Kategori Tipi silinirken hata oluştu.  - {categoryType.Name}");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Kategori Tipi silinirken hata oluştu. - {exMessage}");
                throw;
            }

        }
        public CategoryTypeDTO GetCategoryTypeByName(string name)
        {
            CategoryType categoryType = _uow.GetRepository<CategoryType>().Get(x => x.Name==name && x.IsDeleted == false).FirstOrDefault();
            return Mapping.Mapper.Map<CategoryType, CategoryTypeDTO>(categoryType);
        }
    }
}
