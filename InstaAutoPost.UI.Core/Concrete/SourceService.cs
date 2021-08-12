using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
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
    public class SourceService : ISourceService
    {
        private readonly IUnitOfWork uow;
        public SourceService()
        {
            uow = new EFUnitOfWork(new RSSContextEF());
        }

        public int Add(string name, string image,string url)
        {
            Source source = new Source()
            {
                InsertedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Name = name,
                Image = image,
                URL=url,
                IsDeleted=false
            };
            uow.GetRepository<Source>().Add(source); 
            return uow.SaveChanges();
        }

        public string DeleteById(int id)
        {
            Source source = GetById(id);
            source.UpdatedAt = DateTime.Now;
            uow.GetRepository<Source>().Remove(source);
            int result = uow.SaveChanges();
            string sResult = result == 0 ? "Hata! kaynak silinemedi" : "Kaynak başarıyla silindi";
            return sResult;
        }

        public List<SourceDTO> GetAll()
        {
            List<Source> sourceList = uow.GetRepository<Source>().Get(x => x.IsDeleted == false).OrderByDescending(x=>x.UpdatedAt).ToList();
            return Mapping.Mapper.Map<List<SourceDTO>>(sourceList);
        }

        public Source GetById(int id)
        {
            return uow.GetRepository<Source>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }
        public Source GetByURL(string url,string name)
        {
            return uow.GetRepository<Source>().Get(x => (x.URL == url||x.Name==name) && x.IsDeleted == false).FirstOrDefault();
        }


        public List<Source> GetByName(string name)
        {
            return uow.GetRepository<Source>().Get(x => x.Name.ToLower().Contains(name.ToLower()) && x.IsDeleted == false).ToList();
        }

        public List<Source> GetDeletedSource()
        {
            return uow.GetRepository<Source>().Get(x => x.IsDeleted == true).ToList();
        }

        public SourceWithCategoryCountDTO GetSourceWithCategoryCount(int id)
        {
            Source source = uow.GetRepository<Source>().Get(x => x.IsDeleted == false && x.Id == id).Include(x=>x.Categories.Where(x=>x.IsDeleted==false)).FirstOrDefault();
            SourceWithCategoryCountDTO sourceDTO=Mapping.Mapper.Map<SourceWithCategoryCountDTO>(source);
            return sourceDTO;
        }

        public string Update(string image, string name, int id)
        {
            Source updateSource = GetById(id);
            updateSource.Image = image;
            updateSource.Name = name;
            updateSource.UpdatedAt = DateTime.Now;
            uow.GetRepository<Source>().Update(updateSource);
            int result = uow.SaveChanges();
            string sResult = result == 0 ? "Hata! kaynak güncellenemedi" : "Kaynak başarıyla güncellendi";
            return sResult;
        }

    }
}
