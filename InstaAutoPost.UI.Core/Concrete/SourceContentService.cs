﻿using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                .Get(x => x.CategoryId == categoryId&&x.IsDeleted==false)
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
                    CategoryId=x.CategoryId
                }).OrderByDescending(x => x.ContentInsertAt).ToList();
            return sourcontent;

        }
        public SourceContent GetSourceContentById(int id)
        {
            return _uow.GetRepository<SourceContent>().Get(x => x.Id == id&&x.IsDeleted==false).FirstOrDefault();
        }
        public string RemoveSourceContent(int id)
        {
            SourceContent sourceContent = GetSourceContentById(id);
            _uow.GetRepository<SourceContent>().Remove(sourceContent);
            int result = _uow.SaveChanges();
            string sResult = result == 0 ? "Hata! kaynak silinemedi" : "Kaynak başarıyla silindi";
            return sResult;
        }
    }
}
