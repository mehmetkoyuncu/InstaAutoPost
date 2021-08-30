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

        public List<SourceDTO> GetAllSources()
        {
            List<Source> sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false).Include(x=>x.Categories.Where(x=>x.IsDeleted==false)).OrderByDescending(x => x.UpdatedAt).ToList();
            List<SourceDTO> sourceDTOS=Mapping.Mapper.Map<List<SourceDTO>>(sourceList);
            return sourceDTOS;
        }

        public Source GetById(int id)
        {
            return _uow.GetRepository<Source>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }
        public Source GetByURL(string url, string name)
        {
            return _uow.GetRepository<Source>().Get(x => (x.URL == url || x.Name == name) && x.IsDeleted == false).FirstOrDefault();
        }


        public List<Source> GetByName(string name)
        {
            return _uow.GetRepository<Source>().Get(x => x.Name.ToLower().Contains(name.ToLower()) && x.IsDeleted == false).ToList();
        }

        public List<Source> GetDeletedSource()
        {
            return _uow.GetRepository<Source>().Get(x => x.IsDeleted == true).ToList();
        }

        public SourceWithCategoryCountDTO GetSourceWithCategoryCount(int id)
        {
            Source source = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Id == id).Include(x => x.Categories.Where(x => x.IsDeleted == false)).FirstOrDefault();
            SourceWithCategoryCountDTO sourceDTO = Mapping.Mapper.Map<SourceWithCategoryCountDTO>(source);
            return sourceDTO;
        }


        public List<SelectboxSourceDTO> GetSourcesForSelectBox()
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

        public int AddSource(string name, string image,string contentRootPath)
        {
            ImageUtility imageU = new ImageUtility();
            string imgSrc = imageU.Download(image, name, ImageFormat.Png,contentRootPath);

            Source source = new Source()
            {
                InsertedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Name = name,
                Image = imgSrc.Substring(0,5) + Guid.NewGuid().ToString(),
                IsDeleted = false
            };
            _uow.GetRepository<Source>().Add(source);
            return _uow.SaveChanges();
        }

        public int EditSource(int id, string name, string image,string contentRootPath)
        {
            ImageUtility imageU = new ImageUtility();
            string imgSrc = imageU.Download(image, name, ImageFormat.Png, contentRootPath);
            Source updateSource = GetById(id);
            updateSource.Image = imgSrc.Substring(0,5)+Guid.NewGuid().ToString();
            updateSource.Name = name;
            updateSource.UpdatedAt = DateTime.Now;
            _uow.GetRepository<Source>().Update(updateSource);
           return _uow.SaveChanges();
        }

        public int RemoveSource(int id)
        {
            Source source = GetById(id);
            source.UpdatedAt = DateTime.Now;
            _uow.GetRepository<Source>().Remove(source);
            return _uow.SaveChanges();
        }

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
                        sourceList = _uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Name.ToLower().Contains(searchText.ToLower())).Include(x => x.Categories.Where(x => x.IsDeleted == false)).OrderBy(x => x.Categories.Where(x=>x.IsDeleted==false).Count()).ToList();
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
    }
}
