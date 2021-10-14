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
        #region Url ile birlikte kaynak ekle
        public int Add(string name, string image, string url)
        {
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
            return _uow.SaveChanges();
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

        public Source GetById(int id)
        {
            return _uow.GetRepository<Source>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }
        #region URL'e göre Kaynağı getir
        public Source GetByURL(string url, string name)
        {
            return _uow.GetRepository<Source>().Get(x => (x.URL == url || x.Name == name) && x.IsDeleted == false).FirstOrDefault();
        }
        #endregion
        #region Kaynak Ekle
        public int AddSource(string name, string image, string contentRootPath)
        {
            ImageUtility imageU = new ImageUtility();
            string imgSrc = imageU.Download(image, name, ImageFormat.Png, contentRootPath);

            Source source = new Source()
            {
                InsertedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Name = name==null?name:name.Trim(),
                Image = imgSrc==null?imgSrc:imgSrc.Replace(" ",""),
                IsDeleted = false
            };
            _uow.GetRepository<Source>().Add(source);
            return _uow.SaveChanges();
        }
        #endregion
        #region Kaynağı Düzenle
        public int EditSource(int id, string name, string image, string contentRootPath)
        {
            string imgSrc = null;
            Source updateSource = GetById(id);
            if (updateSource.Image != image)
            {
                ImageUtility imageU = new ImageUtility();
                imgSrc = imageU.Download(image, name, ImageFormat.Png, contentRootPath);
                updateSource.Image = imgSrc;
            }
            updateSource.Name = name;
            updateSource.UpdatedAt = DateTime.Now;
            _uow.GetRepository<Source>().Update(updateSource);
            return _uow.SaveChanges();
        }
        #endregion
        #region Kaynağı Sil
        public int RemoveSource(int id)
        {
            int result = 0;
            try
            {
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
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;

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
