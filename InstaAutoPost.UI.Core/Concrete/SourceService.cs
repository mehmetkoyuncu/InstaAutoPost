using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.RSSService;
using InstaAutoPost.UI.Core.Utilities;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class SourceService : ISourceService
    {
        private readonly IUnitOfWork _uow;
        public SourceService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        public Source GetSourceByCategoryLink(string categoryLink)
        {
            var source = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false && x.Link == categoryLink)).FirstOrDefault();
            return source;
        }
        #region Url ile birlikte kaynak ekle
        public int Add(string name, string image, string url)
        {
            try
            {
                int result = default;
                Source source = new Source()
                {
                    InsertedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Name = name,
                    Image = image,
                    URL = url,
                    IsDeleted = false
                };
                _uow.GetRepository<Source>().Add(source);
                result= _uow.SaveChanges();
                if (result > 0)
                    Log.Logger.Information($"Kaynak eklendi.  - {name}");
                else
                    Log.Logger.Error($"Hata! Kaynak eklenirken hata oluştu.  - {name}");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Kaynak eklenirken hata oluştu.  -{name} {exMessage}");
                throw;
            }
          
        }
        #endregion
        #region Tüm kaynakrı getir
        public List<SourceDTO> GetAllSources()
        {
            List<Source> sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
            List<SourceDTO> sourceDTOS = Mapping.Mapper.Map<List<SourceDTO>>(sourceList);
            return sourceDTOS;
        }
        #endregion
        #region Idye göre kaynak getir
        public Source GetById(int id)
        {
            return _uow.GetRepository<Source>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }
        #endregion
        #region URL'e göre Kaynağı getir
        public Source GetByURL(string url, string name)
        {
            return _uow.GetRepository<Source>().Get(x => (x.URL == url || x.Name == name) && x.IsDeleted == false).FirstOrDefault();
        }
        #endregion
        #region Kaynak Ekle
        public int AddSource(SourceAddOrUpdateDTO sourceDTO, string contentRootPath)
        {
            try
            {
                int result = default;
                ImageUtility imageU = new ImageUtility();
                string imgSrc = imageU.Download(sourceDTO.Image, sourceDTO.Name, ImageFormat.Png, contentRootPath);

                Source source = new Source()
                {
                    InsertedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Name = sourceDTO.Name == null ? sourceDTO.Name : sourceDTO.Name.Trim(),
                    Image = imgSrc,
                    IsDeleted = false
                };
                _uow.GetRepository<Source>().Add(source);
                result= _uow.SaveChanges();
                if (result > 0)
                    Log.Logger.Information($"Kaynak eklendi.  - {sourceDTO.Name}");
                else
                    Log.Logger.Error($"Hata! Kaynak eklenirken hata oluştu.  - {sourceDTO.Name}");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Kaynak eklenirken hata oluştu.  -{sourceDTO.Name} {exMessage}");
                throw;
            }
        
           
        }
        #endregion
        #region Kaynağı Düzenle
        public int EditSource(int id, SourceAddOrUpdateDTO source, string contentRootPath)
        {
            try
            {
                int result = default;
                string imgSrc = null;
                Source updateSource = GetById(id);
                if (updateSource.Image != source.Image)
                {
                    ImageUtility imageU = new ImageUtility();
                    imgSrc = imageU.Download(source.Image, source.Name, ImageFormat.Png, contentRootPath);
                    updateSource.Image = imgSrc;
                }
                updateSource.Name = source.Name == null ? source.Name : source.Name.Trim();
                updateSource.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Source>().Update(updateSource);
                result= _uow.SaveChanges();
                if (result > 0)
                    Log.Logger.Information($"Kaynak güncellendi.  - {source.Name}");
                else
                    Log.Logger.Error($"Hata! Kaynak güncellenirken hata oluştu.  - {source.Name}");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Kaynak eklenirken hata oluştu.  -{source.Name} {exMessage}");
                throw;
            }
           
        }
        #endregion
        #region Kaynağı Sil
        public int RemoveSource(int id)
        {
            try
            {

                int result = default;
                Source source = GetById(id);
                source.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Source>().Remove(source);
                List<Category> categoryList = _uow.GetRepository<Category>().Get(x => x.SourceId == source.Id).ToList();
                foreach (var item in categoryList)
                {
                    _uow.GetRepository<Category>().Remove(item);
                    List<SourceContent> sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.CategoryId == item.Id).ToList();
                    foreach (var sourceContent in sourceContentList)
                        _uow.GetRepository<SourceContent>().Remove(sourceContent);
                }
                result = _uow.SaveChanges();
                if (result > 0)
                {
                    OrderPostUtility.Order();
                    Log.Logger.Information($"Kaynak silindi.  - {source.Name}");
                }
                else
                    Log.Logger.Error($"Hata! Kaynak silinirken hata oluştu.  - {source.Name}");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Kaynak silinirken hata oluştu. - {exMessage}");
                throw;
            }
          

        }
        #endregion
        #region Kaynak Filtreleme
        public List<SourceDTO> Filter(int orderId, string searchText)
        {
            List<Source> sourceList = null;
            switch (orderId)
            {
                case -1:
                    if (!string.IsNullOrWhiteSpace(searchText))
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    break;
                case 0:
                    if (!string.IsNullOrWhiteSpace(searchText))
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    break;
                case 1:
                    if (!string.IsNullOrWhiteSpace(searchText))
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderBy(x => x.Name).ToList();
                    else
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderBy(x => x.Name).ToList();
                    break;
                case 2:
                    if (!string.IsNullOrWhiteSpace(searchText))
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Name).ToList();
                    else
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Name).ToList();
                    break;
                case 3:
                    if (!string.IsNullOrWhiteSpace(searchText))
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Categories.Where(x => x.IsDeleted == false).Count()).ToList();
                    else
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.Categories.Where(x => x.IsDeleted == false).Count()).ToList();
                    break;
                case 4:
                    if (!string.IsNullOrWhiteSpace(searchText))
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderBy(x => x.Categories.Where(x => x.IsDeleted == false).Count()).ToList();
                    else
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderBy(x => x.Categories.Where(x => x.IsDeleted == false).Count()).ToList();
                    break;
                case 5:
                    if (!string.IsNullOrWhiteSpace(searchText))
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderBy(x => x.UpdatedAt).ToList();
                    else
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderBy(x => x.UpdatedAt).ToList();
                    break;
                case 6:
                    if (!string.IsNullOrWhiteSpace(searchText))
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    else
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.UpdatedAt).ToList();
                    break;
                case 7:
                    if (!string.IsNullOrWhiteSpace(searchText))
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderBy(x => x.InsertedAt).ToList();
                    else
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderBy(x => x.InsertedAt).ToList();
                    break;
                case 8:
                    if (!string.IsNullOrWhiteSpace(searchText))
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.InsertedAt).ToList();
                    else
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderByDescending(x => x.InsertedAt).ToList();
                    break;
                default:
                    sourceList = null;
                    break;
            }
            List<SourceDTO> sourceDTOS = Mapping.Mapper.Map<List<SourceDTO>>(sourceList);
            return sourceDTOS;
        }
        #endregion
        #region Id'ye göre Kaynağı Getir
        public SourceAddOrUpdateDTO GetSourceById(int id)
        {
            Source source = _uow.GetRepository<Source>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            return Mapping.Mapper.Map<Source, SourceAddOrUpdateDTO>(source);
        }
        #endregion
    }
}
