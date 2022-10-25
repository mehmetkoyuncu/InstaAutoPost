using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
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
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class SocialMediaAccountsCategoryTypeService : ISocialMediaAccountsCategoryTypeService
    {


        private readonly IUnitOfWork _uow;
        public SocialMediaAccountsCategoryTypeService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        public int Add(SocialMediaAccountsCategoryTypeDTO dto)
        {
            try
            {
                var check = CheckAccounts(dto);
                if (check == false)
                {
                    int result = default;
                    SocialMediaAccountsCategoryType model = new SocialMediaAccountsCategoryType()
                    {
                        InsertedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        SocialMediaAccountId=dto.SocialMediaAccountId,
                        CategoryTypeId=dto.CategoryTypeId,
                        IsDeleted = false
                    };
                    _uow.GetRepository<SocialMediaAccountsCategoryType>().Add(model);
                    result = _uow.SaveChanges();
                    if (result > 0)
                    {
                        OrderPostUtility.Order();
                        Log.Logger.Information($"Sosyal medya - kategori ilişkisi eklendi.  - { dto.CategoryTypeId} - {dto.SocialMediaAccountId}");

                    }
                    else
                    {
                        Log.Logger.Error($"Hata! Sosyal medya - kategori ilişkisi eklenirken hata oluştu. - { dto.CategoryTypeId} - {dto.SocialMediaAccountId}");
                        throw new Exception($"Hata! Sosyal medya - kategori ilişkisi eklenirken hata oluştu.  - { dto.CategoryTypeId} - {dto.SocialMediaAccountId}");
                    }
                    return result;
                }
                else
                {
                    Log.Logger.Error($"Hata! Aynı hesaba ait Sosyal medya - kategori ilişkisi bulunmaktadır.  - { dto.CategoryTypeId} - {dto.SocialMediaAccountId}");
                    throw new Exception("Aynı hesaba ait Sosyal medya - kategori ilişkisi bulunmaktadır");
                }

            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Sosyal medya - kategori ilişkisi eklenirken hata oluştu.  - { dto.CategoryTypeId} - {dto.SocialMediaAccountId} {exMessage}");
                throw;
            }
        }

        public int Edit(SocialMediaAccountsCategoryTypeDTO dto)
        {
            try
            {
                var check = CheckAccounts(dto);
                if (check == false)
                {
                    int result = default;
                    SocialMediaAccountsCategoryType relation = GetById(dto.Id);
                    relation.SocialMediaAccountId = dto.SocialMediaAccountId;
                    relation.CategoryTypeId = dto.CategoryTypeId;
                    relation.UpdatedAt = DateTime.Now;
                    _uow.GetRepository<SocialMediaAccountsCategoryType>().Update(relation);
                    result = _uow.SaveChanges();
                    if (result > 0)
                        Log.Logger.Information($"Sosyal medya - kategori ilişkisi güncellendi.  - { dto.CategoryTypeId} - {dto.SocialMediaAccountId}");
                    else
                    {
                        Log.Logger.Error($"Hata! Sosyal medya - kategori ilişkisi güncellenirken hata oluştu.  - { dto.CategoryTypeId} - {dto.SocialMediaAccountId}");
                        throw new Exception($"Hata! Sosyal medya - kategori ilişkisi güncellenirken hata oluştu.  - { dto.CategoryTypeId} - {dto.SocialMediaAccountId}");
                    }
                    return result;
                }
                else
                {
                    Log.Logger.Error($"Hata! Aynı hesaba ait Sosyal medya - kategori ilişkisi bulunmaktadır.  - { dto.CategoryTypeId} - {dto.SocialMediaAccountId}");
                    throw new Exception("Aynı hesaba ait Sosyal medya - kategori ilişkisi bulunmaktadır");
                }
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Sosyal medya - kategori ilişkisi güncellenirken hata oluştu.  -{ dto.CategoryTypeId} - {dto.SocialMediaAccountId} - {exMessage}");
                throw;
            }
        }

        public List<SocialMediaAccountsCategoryTypeDTO> GetAll()
        {
            List<SocialMediaAccountsCategoryType> socialMeadis = _uow.GetRepository<SocialMediaAccountsCategoryType>().Get(x => x.IsDeleted == false).Include(x=>x.CategoryType).Include(x=>x.SocialMediaAccounts).OrderByDescending(x => x.UpdatedAt).ToList();
            List<SocialMediaAccountsCategoryTypeDTO> dto = Mapping.Mapper.Map<List<SocialMediaAccountsCategoryTypeDTO>>(socialMeadis);
            return dto;
        }

        public SocialMediaAccountsCategoryType GetById(int id)
        {
            SocialMediaAccountsCategoryType account = _uow.GetRepository<SocialMediaAccountsCategoryType>().Get(x => x.Id == id && x.IsDeleted == false).Include(x => x.CategoryType).Include(x => x.SocialMediaAccounts).FirstOrDefault();
            return account;
        }

        public SocialMediaAccountsCategoryTypeDTO GetDTO(int id)
        {
            SocialMediaAccountsCategoryType account = _uow.GetRepository<SocialMediaAccountsCategoryType>().Get(x => x.Id == id && x.IsDeleted == false).Include(x => x.CategoryType).Include(x => x.SocialMediaAccounts).FirstOrDefault();
            return Mapping.Mapper.Map<SocialMediaAccountsCategoryType, SocialMediaAccountsCategoryTypeDTO>(account);
        }

        public int Remove(int id)
        {
            try
            {
                int result = default;
                SocialMediaAccountsCategoryType media = GetById(id);
                media.UpdatedAt = DateTime.Now;
                _uow.GetRepository<SocialMediaAccountsCategoryType>().Remove(media);
                result = _uow.SaveChanges();
                if (result > 0)
                {
                    OrderPostUtility.Order();
                    Log.Logger.Information($"Sosyal medya - kategori ilişkisi silindi.  - { media.CategoryTypeId} - {media.SocialMediaAccountId}");
                }
                    
                else
                {
                    Log.Logger.Error($"Hata! Sosyal medya - kategori ilişkisi silinirken hata oluştu.  - { media.CategoryTypeId} - {media.SocialMediaAccountId}");
                    throw new Exception("Hata! Sosyal medya - kategori ilişkisi silinirken hata oluştu.");
                }

                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Sosyal medya - kategori ilişkisi silinirken hata oluştu. - {exMessage}");
                throw;
            }
        }
        public bool CheckAccounts(SocialMediaAccountsCategoryTypeDTO dto)
        {
            var check = false;
            var account = _uow.GetRepository<SocialMediaAccountsCategoryType>().Get(x => x.CategoryTypeId == dto.CategoryTypeId && x.SocialMediaAccountId==dto.SocialMediaAccountId&&x.IsDeleted==false).FirstOrDefault();
            if (account != null)
                check = true;
            return check;
        }
    }
}
