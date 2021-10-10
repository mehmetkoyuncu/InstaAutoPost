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
                    CategoryId = x.CategoryId,

                }).OrderByDescending(x => x.ContentInsertAt).ToList();
            return sourcontent;

        }
        public SourceContentDTO GetSurceContentDTO(int id)
        {
            var sourcontent = _uow.GetRepository<SourceContent>()
              .Get(x => x.Id == id && x.IsDeleted == false)
              .Include(x => x.Category)
              .Include(x => x.Category.Source).FirstOrDefault();
            return Mapping.Mapper.Map<SourceContentDTO>(sourcontent);
        }
        public SourceContent GetSourceContentById(int id)
        {
            return _uow.GetRepository<SourceContent>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }

        public List<SourceContentDTO> GetSourceContentList()
        {
            List<SourceContent> sourceContentList = _uow.GetRepository<SourceContent>().Get(x => x.IsDeleted == false).Include(x=>x.Category).Include(x=>x.Category.Source).OrderByDescending(x=>x.UpdatedAt).ToList();
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
            if (selectedSourceContent.imageURL != sourceContentDTO.imageURL)
            {
                ImageUtility imageU = new ImageUtility();
                string imgSrc = imageU.Download(sourceContentDTO.imageURL, sourceContentDTO.Title, ImageFormat.Jpeg, contentRootPath);
                selectedSourceContent.imageURL = imgSrc;
            }

            selectedSourceContent.CategoryId = sourceContentDTO.CategoryId;
            selectedSourceContent.Description = sourceContentDTO.Description;
            selectedSourceContent.Tags = sourceContentDTO.Tags;
            selectedSourceContent.Title = sourceContentDTO.Title;
            selectedSourceContent.UpdatedAt = DateTime.Now;
            _uow.GetRepository<SourceContent>().Update(selectedSourceContent);
            return _uow.SaveChanges();
        }


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
                default:
                    sourceContentList = null;
                    break;
            }

            List<SourceContentDTO> sourceContents = Mapping.Mapper.Map<List<SourceContentDTO>>(sourceContentList);
            return sourceContents;

        }
    }
}
