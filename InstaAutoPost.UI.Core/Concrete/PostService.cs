using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
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
    public class PostService : IPostService
    {
        IUnitOfWork _uow;
        public PostService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        public int Add(PostDTO dto)
        {
            try
            {
                int result = default;
                Post model = new Post()
                {
                    InsertedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    SocialMediaAccountsCategoryTypeId = dto.SocialMediaAccountsCategoryTypeId,
                    ContentId=dto.ContentId,
                    OrderNumber = dto.OrderNumber,
                    IsDeleted = false,
                };
                _uow.GetRepository<Post>().Add(model);
                result = _uow.SaveChanges();
                if (result > 0)
                    Log.Logger.Information($"Post eklendi.  - { dto.SocialMediaAccountsCategoryTypeId} - {dto.ContentId}");
                else
                {
                    Log.Logger.Error($"Hata! Post eklenirken hata oluştu. -  { dto.SocialMediaAccountsCategoryTypeId} - {dto.ContentId}");
                    throw new Exception($"Hata! Post eklenirken hata oluştu.  -  { dto.SocialMediaAccountsCategoryTypeId} - {dto.ContentId}");
                }
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Post eklenirken hata oluştu. - {exMessage}");
                throw;
            }
        }

        public int Edit(Post dto)
        {
            try
            {
                int result = default;
                var post = GetById(dto.Id);
                post.InsertedAt = DateTime.Now;
                post.UpdatedAt = DateTime.Now;
                post.SocialMediaAccountsCategoryTypeId = dto.SocialMediaAccountsCategoryTypeId;
                post.OrderNumber = dto.OrderNumber;
                post.IsDeleted = false;

                _uow.GetRepository<Post>().Update(post);
                result = _uow.SaveChanges();
                if (result > 0)
                    Log.Logger.Information($"Post eklendi.  - { dto.SocialMediaAccountsCategoryTypeId}");
                else
                {
                    Log.Logger.Error($"Hata! Post eklenirken hata oluştu. -  { dto.SocialMediaAccountsCategoryTypeId}");
                    throw new Exception($"Hata! Post eklenirken hata oluştu.  -  { dto.SocialMediaAccountsCategoryTypeId}");
                }
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Post güncellenirken hata oluştu. - {exMessage}");
                throw;
            }
        }

        public List<PostDTO> GetAll()
        {
            List<Post> categoryType = _uow.GetRepository<Post>().Get(x => x.IsDeleted == false).OrderBy(x => x.OrderNumber).ToList();
            List<PostDTO> categoryTypeDTOS = Mapping.Mapper.Map<List<PostDTO>>(categoryType);
            return categoryTypeDTOS;
        }
        public Post GetById(int id)
        {
            Post categoryType = _uow.GetRepository<Post>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            return categoryType;
        }

        public PostDTO GetDTO(int id)
        {
            Post categoryType = _uow.GetRepository<Post>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            return Mapping.Mapper.Map<Post, PostDTO>(categoryType);
        }
        public PostDTO GetFirstPost()
        {
            Post categoryType = _uow.GetRepository<Post>().Get(x => x.OrderNumber==1).FirstOrDefault();
            return Mapping.Mapper.Map<Post, PostDTO>(categoryType);
        }

        public List<PostDTO> GetListQuantity(int quantity)
        {
            List<Post> categoryType = _uow.GetRepository<Post>().Get(x => x.IsDeleted == false).Take(quantity).OrderBy(x => x.OrderNumber).ToList();
            List<PostDTO> categoryTypeDTOS = Mapping.Mapper.Map<List<PostDTO>>(categoryType);
            return categoryTypeDTOS; ;
        }
        public List<SocialMediaAccountsCategoryType> GetDataOfWillLoad()
        {
            var categoryType = _uow.GetRepository<SocialMediaAccountsCategoryType>().Get(x => x.IsDeleted == false).Include(x=>x.CategoryType).ThenInclude(x=>x.Category.Where(x=>x.IsDeleted==false)).ThenInclude(x=>x.SourceContents.Where(x=>x.IsDeleted==false&&x.SendOutForPost==false)).Include(x=>x.SocialMediaAccounts).ToList();
            categoryType = categoryType.OrderBy(x => x.UpdatedAt).ToList();
            return categoryType;
        }

        public int Remove(int id)
        {
            try
            {
                int result = default;
                Post categoryType = GetById(id);
                categoryType.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Post>().HardDelete(categoryType);
                result = _uow.SaveChanges();
                if (result > 0)
                {
                    if (result > 0)
                    {
                        Log.Logger.Information($"Kategori Tipi silindi.  - { categoryType.SocialMediaAccountsCategoryTypeId} -");
                    }
                    
                }
                else
                    Log.Logger.Error($"Hata! Kategori Tipi silinirken hata oluştu.  - { categoryType.SocialMediaAccountsCategoryTypeId} - ");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Kategori Tipi silinirken hata oluştu. - {exMessage}");
                throw;
            }
        }
        public int RemoveRange(List<Post> postList)
        {
            try
            {
                int result = default;
                    _uow.GetRepository<Post>().RemoveRange(postList);
                    result = _uow.SaveChanges();

                
                if (result > 0)
                {
                    if (result > 0)
                        Log.Logger.Information($"Kategori Tipi silindi.  - {postList}");

                }
                else
                    Log.Logger.Error($"Hata! Kategori Tipi silinirken hata oluştu.  -{postList}");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Kategori Tipi silinirken hata oluştu. - {exMessage}");
                throw;
            }
        }
    }
}
