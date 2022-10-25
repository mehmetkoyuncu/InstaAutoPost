using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Utilities;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IUnitOfWork _uow;
        public SocialMediaService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        public int Add(SocialMediaDTO socialMedia)
        {
            try
            {
                var check = CheckAccounts(socialMedia);
                if (check == false)
                {
                    int result = default;
                    SocialMediaAccounts socialMediaAccounts = new SocialMediaAccounts()
                    {
                        InsertedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Name = socialMedia.Name,
                        Icon = socialMedia.Icon,
                        AccountNameOrMail = socialMedia.AccountNameOrMail,
                        Password = socialMedia.Password,
                        IsDeleted = false
                    };
                    _uow.GetRepository<SocialMediaAccounts>().Add(socialMediaAccounts);
                    result = _uow.SaveChanges();
                    if (result > 0)
                        Log.Logger.Information($"Sosyal Medya eklendi.  - { socialMedia.Name}");
                    else
                    {
                        Log.Logger.Error($"Hata! Sosyal Medya eklenirken hata oluştu.  - { socialMedia.Name}");
                        throw new Exception($"Hata! Sosyal Medya eklenirken hata oluştu.  - { socialMedia.Name}");
                    }
                    return result;
                }
                else
                {
                    Log.Logger.Error($"Hata! Aynı hesaba ait sosyal medya bulunmaktadır.  - { socialMedia.Name}");
                    throw new Exception("Aynı hesaba ait sosyal medya bulunmaktadır");
                }

            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Sosyal Medya eklenirken hata oluştu.  -{socialMedia.Name} {exMessage}");
                throw;
            }
        }

        public int Edit(SocialMediaDTO socialMedia)
        {
            try
            {
                var check = CheckAccounts(socialMedia);
                if (check == false)
                {
                    int result = default;
                    SocialMediaAccounts socialMediaAccount = GetById(socialMedia.Id);
                    socialMediaAccount.Name = socialMedia.Name == null ? socialMedia.Name : socialMedia.Name.Trim();
                    socialMediaAccount.AccountNameOrMail = socialMedia.AccountNameOrMail == null ? socialMedia.AccountNameOrMail : socialMedia.AccountNameOrMail.Trim();
                    socialMediaAccount.Password = socialMedia.Password == null ? socialMedia.Password : socialMedia.Password.Trim();
                    socialMediaAccount.Name = socialMedia.Name == null ? socialMedia.Name : socialMedia.Name.Trim();
                    socialMediaAccount.UpdatedAt = DateTime.Now;
                    _uow.GetRepository<SocialMediaAccounts>().Update(socialMediaAccount);
                    result = _uow.SaveChanges();
                    if (result > 0)
                        Log.Logger.Information($"Sosyal Medya güncellendi.  - {socialMediaAccount.Name}");
                    else
                    {
                        Log.Logger.Error($"Hata! Sosyal Medya güncellenirken hata oluştu.  - { socialMedia.Name}");
                        throw new Exception($"Hata! Sosyal Medya güncellenirken hata oluştu.  - { socialMedia.Name}");
                    }
                    return result;
                }
                else
                {
                    Log.Logger.Error($"Hata! Aynı hesaba ait sosyal medya bulunmaktadır.  - { socialMedia.Name}");
                    throw new Exception("Aynı hesaba ait sosyal medya bulunmaktadır");
                }
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Sosyal Medya eklenirken hata oluştu.  -{socialMedia.Name} {exMessage}");
                throw;
            }
        }

        public List<SocialMediaDTO> GetAll()
        {
            List<SocialMediaAccounts> socialMeadis = _uow.GetRepository<SocialMediaAccounts>().Get(x => x.IsDeleted == false).OrderByDescending(x => x.UpdatedAt).ToList();
            List<SocialMediaDTO> dto = Mapping.Mapper.Map<List<SocialMediaDTO>>(socialMeadis);
            return dto;
        }

        public SocialMediaDTO GetByAccount(SocialMediaDTO socialMedia)
        {

            SocialMediaAccounts socialMediaAccount = _uow.GetRepository<SocialMediaAccounts>().Get(x => x.AccountNameOrMail == socialMedia.AccountNameOrMail&&x.Password==socialMedia.Password && x.IsDeleted == false).FirstOrDefault();
            return Mapping.Mapper.Map<SocialMediaAccounts, SocialMediaDTO>(socialMediaAccount);
        }

        public SocialMediaAccounts GetById(int id)
        {
            SocialMediaAccounts account = _uow.GetRepository<SocialMediaAccounts>().Get(x => x.Id==id && x.IsDeleted == false).FirstOrDefault();
            return account;
        }

        public SocialMediaDTO GetDTO(int id)
        {
            SocialMediaAccounts account = _uow.GetRepository<SocialMediaAccounts>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            return Mapping.Mapper.Map<SocialMediaAccounts, SocialMediaDTO>(account);
        }

        public int Remove(int id)
        {
            try
            {
                int result = default;
                SocialMediaAccounts media = GetById(id);
                media.UpdatedAt = DateTime.Now;
                _uow.GetRepository<SocialMediaAccounts>().Remove(media);
                result = _uow.SaveChanges();
                if (result > 0)
                {
                    var mediaCatgory = _uow.GetRepository<SocialMediaAccountsCategoryType>().Get(x => x.SocialMediaAccountId == id&&x.IsDeleted==false).ToList();
                    if (mediaCatgory.Count > 0)
                    {
                        foreach (var item in mediaCatgory)
                        {
                            _uow.GetRepository<SocialMediaAccountsCategoryType>().Remove(item);
                            _uow.SaveChanges();
                        }
                    }
                    OrderPostUtility.Order();
                    Log.Logger.Information($"Sosyal Medya silindi.  - {media.Name}");
                }
                else
                {
                    Log.Logger.Error($"Hata! Sosyal Medya silinirken hata oluştu.  - {media.Name}");
                    throw new Exception("Hata! Sosyal Medya silinirken hata oluştu.");
                }
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Sosyal Medya silinirken hata oluştu. - {exMessage}");
                throw;
            }
        }
        public bool CheckAccounts(SocialMediaDTO socialMedia)
        {
            var check = false;
            var account = _uow.GetRepository<SocialMediaAccounts>().Get(x => x.Name == socialMedia.Name && x.AccountNameOrMail == socialMedia.AccountNameOrMail && x.Password == socialMedia.Password&&x.Icon==socialMedia.Icon&&x.IsDeleted==false).FirstOrDefault();
            if (account != null)
                check = true;
            return check;
        }

    }
}
