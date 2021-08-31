using AutoMapper;
using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Utilities;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class SourceContentService : ISourceContentService
    {
        IUnitOfWork _uow;
        public SourceContentService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        public int AddSourceContent(List<SourceContent> sourceContent)
        {
            _uow.GetRepository<SourceContent>().AddList(sourceContent);
            return _uow.SaveChanges();
        }
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
                    CategoryId = x.CategoryId
                }).OrderByDescending(x => x.ContentInsertAt).ToList();
            return sourcontent;

        }
        public SourceContent GetSourceContentById(int id)
        {
            return _uow.GetRepository<SourceContent>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }

        public List<SourceContentDTO> GetSourceContentList()
        {
            List<SourceContent> sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x=>x.Category).Include(x=>x.Category.Source).ToList();
            List<SourceContentDTO> sourceContents = Mapping.Mapper.Map<List<SourceContentDTO>>(sourceContentList);
            return sourceContents;
        }
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
        public List<SelectboxCategoryDTO> GetCategoriesIdAndName(int sourceId)
        {
            List<SelectboxCategoryDTO> sources = _uow.GetRepository<Category>()
                .Get(x => x.IsDeleted == false&&x.SourceId==sourceId)
                .Select(x => new SelectboxCategoryDTO()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            return sources;
        }


        public int RemoveSourceContent(int id)
        {
            SourceContent sourceContent = GetSourceContentById(id);
            _uow.GetRepository<SourceContent>().Remove(sourceContent);
            int result = _uow.SaveChanges();
            return result;
        }

        public int AddSourceContent(SourceContentDTO sourcContentDTO,string contentRootPath)
        {
            SourceContent sourceContent = Mapping.Mapper.Map<SourceContentDTO, SourceContent>(sourcContentDTO);
            ImageUtility imageU = new ImageUtility();
            string imgSrc = imageU.Download(sourceContent.imageURL, sourceContent.Title, ImageFormat.Jpeg, contentRootPath);
            sourceContent.imageURL = imgSrc;
            sourceContent.UpdatedAt = DateTime.Now;
            sourceContent.InsertedAt = DateTime.Now;
            sourceContent.SendOutForPost = false;
            sourceContent.IsDeleted = false;
            sourceContent.ContentInsertAt = DateTime.Now;
            _uow.GetRepository<SourceContent>().Add(sourceContent);
            return _uow.SaveChanges();
        }

        public int EditSourceContent(SourceContentDTO sourceContentDTO,string contentRootPath)
        {
            SourceContent selectedSourceContent = GetSourceContentById(sourceContentDTO.Id);
                ImageUtility imageU = new ImageUtility();
                string imgSrc = imageU.Download(sourceContentDTO.imageURL, sourceContentDTO.Title, ImageFormat.Jpeg, contentRootPath);
                selectedSourceContent.imageURL = imgSrc;

            selectedSourceContent.CategoryId = sourceContentDTO.CategoryId;
            selectedSourceContent.Description = sourceContentDTO.Description;
            selectedSourceContent.Tags = sourceContentDTO.Tags;
            selectedSourceContent.Title = sourceContentDTO.Title;
            selectedSourceContent.UpdatedAt = DateTime.Now;
            _uow.GetRepository<SourceContent>().Update(selectedSourceContent);
            return _uow.SaveChanges();
        }
    }
}
