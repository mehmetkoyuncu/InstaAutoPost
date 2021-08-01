using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
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

        public string Add(SourceDTO sourceDTO)
        {
            sourceDTO.InsertedAt = DateTime.Now;
            sourceDTO.UpdatedAt = DateTime.Now;
            Source source= Mapping.Mapper.Map<Source>(sourceDTO);
            uow.GetRepository<Source>().Add(source);
            int result = uow.SaveChanges();
            string sResult = result == 0 ? "Hata! kaynak eklenemedi" : "Kaynak başarıyla eklendi";
            return sResult;
        }

        public string DeleteById(int id)
        {
            Source source = GetById(id);
            uow.GetRepository<Source>().Remove(source);
            int result = uow.SaveChanges();
            string sResult = result == 0 ? "Hata! kaynak silinemedi" : "Kaynak başarıyla silindi";
            return sResult;
        }

        public List<SourceDTO> GetAll()
        {
            List<Source> sourceList = uow.GetRepository<Source>().Get(x => x.IsDeleted == false).ToList(); 
            return Mapping.Mapper.Map<List<SourceDTO>>(sourceList);
        }

        public Source GetById(int id)
        {
            return uow.GetRepository<Source>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }

        public List<Source> GetByName(string name)
        {
            return uow.GetRepository<Source>().Get(x => x.Name.ToLower().Contains(name.ToLower()) && x.IsDeleted == false).ToList();
        }

        public List<Source> GetDeletedSource()
        {
            return uow.GetRepository<Source>().Get(x => x.IsDeleted == true).ToList();
        }

        public string Update(Source source, int id)
        {
            Source updateSource = GetById(id);
            updateSource.Image = source.Image;
            updateSource.Name = source.Name;
            updateSource.UpdatedAt = DateTime.Now;
            uow.GetRepository<Source>().Update(updateSource);
            int result = uow.SaveChanges();
            string sResult = result == 0 ? "Hata! kaynak güncellenemedi" : "Kaynak başarıyla güncellendi";
            return sResult;
        }
    }
}
