using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class ImagesService : IImagesService
    {
        private readonly IUnitOfWork _uow;
        public ImagesService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        public int AddImages(string url,int sourceContentId)
        {
            SourceContentImage image = new SourceContentImage
            {
                InsertedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                ImageLink = url,
                SourceContentId=sourceContentId
            };
            _uow.GetRepository<SourceContentImage>().Add(image);
            return _uow.SaveChanges();

        }
    }
}
