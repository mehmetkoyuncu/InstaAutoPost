using AutoMapper;
using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.CharacterConverter;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Utilities;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class SourceContentService : ISourceContentService
    {
        #region Constructor
        IUnitOfWork _uow;
        public SourceContentService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        #endregion
        #region İçerik listesi oluştur
        public int AddSourceContent(List<SourceContent> sourceContent)
        {
            try
            {
                int result = default;
                _uow.GetRepository<SourceContent>().AddList(sourceContent);
                result= _uow.SaveChanges();
                if (result > 0)
                    Log.Logger.Information($"İçerik eklendi(İçerik Listesi).  - {result}");
                else
                    Log.Logger.Error($"Hata! İçerik eklenirken hata oluştu(İçerik Listesi).  - {result}");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! İçerik eklenirken hata oluştu(İçerik Listesi).  - {exMessage}");
                throw;
            }
        }
        #endregion
        #region Kategori Id'ye göre içeriği getir
        public List<SourceContentDTO> GetSourceContent(int categoryId)
        {
            var sourcontent = _uow.GetRepository<SourceContent>()
                .Get(x => x.CategoryId == categoryId && x.IsDeleted == false)
                .Include(x => x.Category)
                .Include(x => x.Category.Source)
                .Select(x => new SourceContentDTO()
                {
                    Id = x.Id,
                    ContentInsertAt = x.ContentInsertAt,
                    CategoryName = x.Category.Name,
                    Description = x.Description,
                    imageURL = x.imageURL,
                    SendOutForPost = x.SendOutForPost,
                    SourceName = x.Category.Source.Name,
                    Title = x.Title,
                    CategoryId = x.CategoryId,
                    IsCreatedFolder=x.IsCreatedFolder

                }).OrderByDescending(x => x.ContentInsertAt).ToList();
            return sourcontent;

        }
        #endregion
        #region İçerik İd'ye göre bir içerik DTO getir
        public SourceContentDTO GetSurceContentDTO(int id)
        {
            var sourcontent = _uow.GetRepository<SourceContent>()
              .Get(x => x.Id == id && x.IsDeleted == false)
              .Include(x => x.Category)
              .Include(x => x.Category.Source).FirstOrDefault();
            return Mapping.Mapper.Map<SourceContentDTO>(sourcontent);
        }
        #endregion
        #region Id'ye göre içeriği getir
        public SourceContent GetSourceContentById(int id)
        {
            return _uow.GetRepository<SourceContent>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }
        #endregion
        #region İçerik listesini getir
        public List<SourceContentDTO> GetSourceContentList(int next=0,int quantity=10)
        {
            List<SourceContent> sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x=>x.Category.Source).OrderByDescending(x=>x.UpdatedAt).Skip(next).Take(quantity).Include(x => x.Category).ToList();
            List<SourceContentDTO> sourceContents = Mapping.Mapper.Map<List<SourceContentDTO>>(sourceContentList);
            return sourceContents;
        }
        #endregion
        #region Kaynak listesini getir
        public List<SelectboxSourceDTO> GetSourcesIdAndName()
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
        #region Kategori listesini getir
        public List<SelectBoxCategoryDTO> GetCategoriesIdAndName(int sourceId)
        {
            List<SelectBoxCategoryDTO> sources = _uow.GetRepository<Category>()
                .Get(x => x.IsDeleted == false&&x.SourceId==sourceId)
                .Select(x => new SelectBoxCategoryDTO()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            return sources;
        }
        #endregion
        #region içeriği sil
        public int RemoveSourceContent(int id)
        {
          
            try
            {
                int result = default;
                SourceContent sourceContent = GetSourceContentById(id);
                _uow.GetRepository<SourceContent>().Remove(sourceContent);
                result = _uow.SaveChanges();
                if(result>0)
                    Log.Logger.Information($"İçerik silindi.  - {sourceContent.Title}");
                else
                    Log.Logger.Error($"Hata! İçerik silinirken hata oluştu.  - {sourceContent.Title}");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! İçerik silinirken hata oluştu.  - {exMessage}");
                throw;
            }
        }
        #endregion
        #region İçerik ekle
        public int AddSourceContent(SourceContentAddOrUpdateDTO sourceContentDTO,string contentRootPath)
        {
            try
            {
                int result = default;
                ImageUtility imageU = new ImageUtility();
                string imgSrc = imageU.Download(sourceContentDTO.imageURL, sourceContentDTO.Title, ImageFormat.Jpeg, contentRootPath, isContent: true, content: sourceContentDTO.Description);
                SourceContent sourceContent = new SourceContent()
                {
                    imageURL = imgSrc,
                    UpdatedAt = DateTime.Now,
                    InsertedAt = DateTime.Now,
                    SendOutForPost = false,
                    IsDeleted = false,
                    ContentInsertAt = DateTime.Now,
                    CategoryId = sourceContentDTO.CategoryId,
                    Tags = sourceContentDTO.Tags,
                    Title = sourceContentDTO.Title,
                    Description = sourceContentDTO.Description,
                };
                _uow.GetRepository<SourceContent>().Add(sourceContent);
                result = _uow.SaveChanges();
                if (result > 0)
                    Log.Logger.Information($"İçerik eklendi.  - {sourceContent.Title}");
                else
                    Log.Logger.Error($"Hata! İçerik eklenirken hata oluştu.  - {sourceContent.Title}");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! İçerik eklenirken hata oluştu.  - {exMessage}");
                throw;
            }
        }
        #endregion
        #region İçerik düzenle
        public int EditSourceContent(int id,SourceContentAddOrUpdateDTO sourceContentDTO,string contentRootPath)
        {
            try
            {
                int result = default;
                SourceContent selectedSourceContent = GetSourceContentById(id);
                if (selectedSourceContent.imageURL != sourceContentDTO.imageURL)
                {
                    ImageUtility imageU = new ImageUtility();
                    string imgSrc = imageU.Download(sourceContentDTO.imageURL, sourceContentDTO.Title, ImageFormat.Jpeg, contentRootPath, isContent: true, content: sourceContentDTO.Description);
                    selectedSourceContent.imageURL = imgSrc;
                }
                selectedSourceContent.CategoryId = sourceContentDTO.CategoryId;
                selectedSourceContent.Description = sourceContentDTO.Description;
                selectedSourceContent.Tags = sourceContentDTO.Tags;
                selectedSourceContent.Title = sourceContentDTO.Title;
                selectedSourceContent.UpdatedAt = DateTime.Now;
                _uow.GetRepository<SourceContent>().Update(selectedSourceContent);
                result= _uow.SaveChanges();
                if (result > 0)
                    Log.Logger.Information($"İçerik güncellendi(Edit).  - {sourceContentDTO.Title}");
                else
                    Log.Logger.Error($"Hata! İçerik güncellenirken hata oluştu(Edit).  - {sourceContentDTO.Title}");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! İçerik güncellenirken hata oluştu(Edit).  - {sourceContentDTO.Title} - {exMessage}");
                throw;
            }
         
        }
        #endregion
        #region Filtre
        public List<SourceContentDTO> Filter(int categoryId, int orderId, string searchText)
        {
            List<SourceContent> sourceContentList = null;
            switch (orderId)
            {
                case -1:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x=>x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    else 
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    break;
                case 0:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    break;
                case 1:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.Title).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.Title).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.Title).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderBy(x => x.Title).ToList();
                    break;
                case 2:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.Title).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.Title).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.Title).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderByDescending(x => x.Title).ToList();
                    break;
                case 3:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.Description).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.Description).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.Description).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderBy(x => x.Description).ToList();
                    break;
                case 4:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.Description).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.Description).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.Description).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderByDescending(x => x.Description).ToList();
                    break;
                case 5:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.SendOutForPost==false).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.SendOutForPost == false).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.SendOutForPost == false).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderBy(x => x.SendOutForPost == false).ToList();
                    break;
                case 6:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.SendOutForPost == true).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.SendOutForPost == true).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.SendOutForPost == true).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderBy(x => x.SendOutForPost == true).ToList();
                    break;
                case 7:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.UpdatedAt).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.UpdatedAt).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.UpdatedAt).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderBy(x => x.UpdatedAt).ToList();
                    break;
                case 8:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderByDescending(x => x.UpdatedAt).ToList();
                    break;
                case 9:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.InsertedAt).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.InsertedAt).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.InsertedAt).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderBy(x => x.InsertedAt).ToList();
                    break;
                case 10:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.InsertedAt).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.InsertedAt).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.InsertedAt).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderByDescending(x => x.InsertedAt).ToList();
                    break;
                case 11:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.ContentInsertAt).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.ContentInsertAt).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.ContentInsertAt).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderBy(x => x.ContentInsertAt).ToList();
                    break;
                case 12:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.ContentInsertAt).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.ContentInsertAt).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.ContentInsertAt).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderByDescending(x => x.ContentInsertAt).ToList();
                    break;
                case 13:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.IsCreatedFolder).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.IsCreatedFolder).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderByDescending(x => x.IsCreatedFolder).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderByDescending(x => x.IsCreatedFolder).ToList();
                    break;
                case 14:
                    if (categoryId == -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.IsCreatedFolder).ToList();
                    else if (categoryId == -1 && !string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower())).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.IsCreatedFolder).ToList();
                    else if (categoryId > -1 && string.IsNullOrWhiteSpace(searchText))
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Category.Source).OrderBy(x => x.IsCreatedFolder).ToList();
                    else
                        sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false && x.Title.ToLower().Contains(searchText.ToLower()) && x.CategoryId == categoryId).Include(x => x.Category.Source).OrderBy(x => x.IsCreatedFolder).ToList();
                    break;
                default:
                    sourceContentList = null;
                    break;
            }

            List<SourceContentDTO> sourceContents = Mapping.Mapper.Map<List<SourceContentDTO>>(sourceContentList);
            return sourceContents;

        }
        #endregion
        #region Kategori Id'ye göre içeriği getir
        public SourceContentAddOrUpdateDTO GetSourceContentDTOById(int id)
        {
            SourceContent source = _uow.GetRepository<SourceContent>().Get(x => x.Id == id && x.IsDeleted == false).Include(x=>x.Category.Source).FirstOrDefault();
            return Mapping.Mapper.Map<SourceContent, SourceContentAddOrUpdateDTO>(source);
        }
        #endregion
        #region İçerik sayısı getir
        public int GetSourceContentCount()
        {
            int contentCount = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Count();
            return contentCount;
        }
        #endregion
        #region İçeriklerin belli bir kısmını getir
        public List<SourceContentDTO> GetSourceContentFilter(List<SourceContentDTO> contentList, int next = 0, int quantity = 10)
        {
            List<SourceContentDTO> sourceContents = contentList.Skip(next).Take(quantity).ToList();
            return sourceContents;
        }
        #endregion
        #region Tüm başlıkları getir
        public List<string> GetAll()
        {
            var allContent = _uow.GetRepository<SourceContent>().GetAll().Select(x => x.Title.Trim()).ToList();
            return allContent;
        }
        #endregion
        #region Durumu paylaşılmadı olan ilk içeriği getir
        public SourceContent GetFirstContentNotSended()
        {
            var content = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false&&x.SendOutForPost==false).OrderBy(x => x.InsertedAt).FirstOrDefault();
            return content;
        }
        #endregion
        #region İçeriği güncelle
        public int UpdateSourceContent(SourceContent content)
        {
            try
            {
                int result = default;
                var resultContent = GetSourceContentById(content.Id);
                resultContent.imageURL = content.imageURL;
                resultContent.UpdatedAt = DateTime.Now;
                resultContent.Title = content.Title;
                resultContent.Tags = content.Tags;
                resultContent.SendOutForPost = content.SendOutForPost;
                resultContent.ContentInsertAt = content.ContentInsertAt;
                resultContent.IsCreatedFolder = content.IsCreatedFolder;
                _uow.GetRepository<SourceContent>().Update(resultContent);
                result= _uow.SaveChanges();
                if (result > 0)
                    Log.Logger.Information($"İçerik güncellendi(Update).  - {content.Title}");
                else
                    Log.Logger.Error($"Hata! İçerik güncellenirken hata oluştu(Update).  - {content.Title}");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! İçerik güncellenirken hata oluştu(Edit).  - {content.Title} - {exMessage}");
                throw;
            }

           
        }
        #endregion
        #region Klasör Oluştur
        public bool CreateFolder(int id,string contentRooth)
        {
            try
            {
                CategoryService cService = new CategoryService();

                //İçeriği getir
                SourceContent sourceContent = GetSourceContentById(id);
                //Klasör başlığı oluştur
                string titleNew = sourceContent.Title != null&&sourceContent.Title.Length>5 ? sourceContent.Title.Substring(0, 5)+DateTime.Now.ToString() : sourceContent.Title+DateTime.Now.ToShortDateString();
                //Klasör başlığı düzenle
                titleNew = CharacterConvertGenerator.TurkishToEnglish(titleNew);
                titleNew = CharacterConvertGenerator.RemovePunctuation(titleNew);
                //Contents klasörüsü oluştur
                var firstFolderName = "Contents_" + DateTime.Now.ToShortDateString();
                string resultRooth = FolderUtility.CreateFolder(firstFolderName, contentRooth);
                //Contents klasörünün altına ilgi klasörü oluştur
               var contentRoothContents = contentRooth + @"\"+firstFolderName;
                 resultRooth= FolderUtility.CreateFolder(titleNew, contentRoothContents);
                //Etiketleri getir ve birleştir
                var category = cService.GetById(sourceContent.CategoryId);
                var categoryTags=category.Tags!=null?category.Tags.Split(",").ToList():null;
                List<string> tags = sourceContent.Tags!=null?sourceContent.Tags.Split(",").ToList():new List<string>();
                if(categoryTags!=null&&tags!=null)
                tags.AddRange(categoryTags);
                tags = tags.Select(x => x.Trim()).ToList();
                //Txt dosyası oluştur
                string tagsResult = "";
                if (tags.Count>0)
                {
                    string charpText = "";
                    tags = tags.Select(x => Path.Combine(charpText, " #" + x)).ToList();
                     tagsResult = string.Join(null, tags);
                }
                string fileResultRooth = TxtUtility.CreateTxtDocument(resultRooth, titleNew + "Txt", sourceContent.Description, sourceContent.Title,tagsResult);
                //Resim dosyası kopyalayıp ilgili dizine yapıştır
                var fileName = sourceContent.imageURL;
                var imageSource = contentRooth + @"\wwwroot\images\";
                var imageDestination = resultRooth;
                CopyFileUtility.CopyFile(imageSource, imageDestination,fileName);
                //Klasörü aç
                var psi = new System.Diagnostics.ProcessStartInfo() { FileName = resultRooth, UseShellExecute = true };
                System.Diagnostics.Process.Start(psi);
                sourceContent.IsCreatedFolder = true;
                int updateControl = UpdateSourceContent(sourceContent);
                return true;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Klasör veya dosya oluşturulurken hata oluştu.  - {exMessage}");
                return false;
            }
        }
        #endregion
        #region Paylaşıldı Olarak İşaretle
        public bool ShareMarkPost(int id)
        {
            try
            {
                bool control = false;
                SourceContent content = GetSourceContentById(id);
                content.SendOutForPost = true;
                var result = UpdateSourceContent(content);
                if (result > 0)
                    control = true;
                if (result > 0)
                    Log.Logger.Information($"İçerik paylaşıldı olarak işaretlendi.  - {content.Title}");
                else
                    Log.Logger.Error($"Hata! İçerik paylaşıldı olarak işaretlenirken hata oluştu.  - {content.Title}");
                return control;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"İçerik paylaşıldı olarak işaretlenirken hata oluştu.  - {exMessage}");
                throw;
            }
           
        }
        #endregion
        #region Silinmeyen içeriklerin  başlığını  getir
        public List<string> GetNotDeletedContentsName()
        {
            List<string> sourceContents = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Select(x=>x.Title).ToList();
            return sourceContents;
        }
        #endregion
        #region Silinmeyen içerikleri getir
        public List<SourceContent> GetSourceContenListNotDeleted()
        {
            List<SourceContent> sourceContents = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).ToList();
            return sourceContents;
        }
        #endregion
    }
}
