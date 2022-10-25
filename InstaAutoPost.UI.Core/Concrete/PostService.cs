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
    public class PostService : IPostService
    {
        IUnitOfWork _uow;
        public PostService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        public int Add(PostDTO dto)
        {
            int result = default;
            try
            {
                
                Post model = new Post()
                {
                    InsertedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    SocialMediaAccountsCategoryTypeId = dto.SocialMediaAccountsCategoryTypeId,
                    ContentId = dto.ContentId,
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
                }
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Post eklenirken hata oluştu. - {exMessage}");
                return result;
            }
        }
        public int AddRange(List<PostDTO> postListDTO)
        {
            int result = default;
            try
            {
               
                var posts = Mapping.Mapper.Map<List<PostDTO>, List<Post>>(postListDTO);
                _uow.GetRepository<Post>().AddList(posts);
                result = _uow.SaveChanges();
                if (result > 0)
                    Log.Logger.Information($"{result} adet Post eklendi.");
                else
                {
                  
                    Log.Logger.Error($"Hata! Post eklenirken hata oluştu.");
                }
                return result;
            }
            catch (Exception exMessage)
            {
                result = 0;
                Log.Logger.Error($"Hata! Post eklenirken hata oluştu. - {exMessage}");
                return result;
            }
        }

        public int Edit(Post dto)
        {
            int result = default;
            try
            {
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
                }
                return result;
            }
            catch (Exception exMessage)
            {

                Log.Logger.Error($"Hata! Post güncellenirken hata oluştu. - {exMessage}");
                return result;
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
        public Post GetByOrderNumber(int orderNumber)
        {
            Post categoryType = _uow.GetRepository<Post>().Get(x => x.OrderNumber == orderNumber && x.IsDeleted == false).FirstOrDefault();
            return categoryType;
        }
        public Post GetByContentId(int id)
        {
            Post categoryType = _uow.GetRepository<Post>().Get(x => x.ContentId == id && x.IsDeleted == false).FirstOrDefault();
            return categoryType;
        }

        public PostDTO GetDTO(int id)
        {
            Post categoryType = _uow.GetRepository<Post>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            return Mapping.Mapper.Map<Post, PostDTO>(categoryType);
        }
        public PostDTO GetFirstPost()
        {
            Post categoryType = _uow.GetRepository<Post>().Get(x => x.OrderNumber == 1).FirstOrDefault();
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
            var categoryType = _uow.GetRepository<SocialMediaAccountsCategoryType>().Get(x => x.IsDeleted == false).Include(x => x.CategoryType).ThenInclude(x => x.Category.Where(x => x.IsDeleted == false)).ThenInclude(x => x.SourceContents.Where(x => x.IsDeleted == false && x.SendOutForPost == false&&!x.Description.Contains("İşte")&&!x.Description.Contains("Aktüel")&&!x.Title.Contains("?")).OrderByDescending(x=>x.ContentInsertAt)).Include(x => x.SocialMediaAccounts).ToList();
            return categoryType;
        }

        public int Remove(int id)
        {
            try
            {
                int result = default;
                Post post = GetById(id);
                post.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Post>().HardDelete(post);
                result = _uow.SaveChanges();
                if (result > 0)
                {
                    if (result > 0)
                    {
                        Log.Logger.Information($"Post silindi.  - { post.Id} -");
                        OrderPostUtility.Order();
                    }

                }
                else
                    Log.Logger.Error($"Hata! Post silinirken hata oluştu.  - { post.Id} - ");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Post silinirken hata oluştu. - {exMessage}");
                throw;
            }
        }
        public int RemoveByContentId(int id)
        {
            try
            {
                int result = default;
                var sourceContent=_uow.GetRepository<SourceContent>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
                if (sourceContent.SendOutForPost == false)
                {
                    Post post = GetByContentId(id);
                    if (post != null)
                    {
                        _uow.GetRepository<Post>().HardDelete(post);
                        result = _uow.SaveChanges();
                        if (result > 0)
                        {
                           Log.Logger.Information($"Post silindi.  - { post.Id} -");
                            OrderPostUtility.Order();
                        }
                        else
                            Log.Logger.Error($"Hata! Post silinirken hata oluştu.  - { post.Id} - ");
                    }
                    
                }
               
                
                return result;
            }

            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Post silinirken hata oluştu. - {exMessage}");
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

        public PostStatistics GetPostStatistics()
        {
            SourceContentService contentService = new SourceContentService();
            PostStatistics statistics = new PostStatistics()
            {
                TotalSourceContent = contentService.GetSourceContentCount(),
                SendedPostCount = contentService.GetPublishedSourceContentCount(),
                NotSendedPostCount = contentService.GetNotPublishedSourceContentCount(),
                SendedPostCountToday = contentService.GetPublishedSourceContentInToday(),
                SocialMediaCount = contentService.GetSocialMediaAccountsCount(),
                SocialMediaCountToday = contentService.GetSocialMediaAccountsCountToday(),
                LastPublishedContents = contentService.GetLastPublishedContent()
            };
            return statistics;
        }
        public List<PostViewModelDTO> GetPostList(int quantity = 10)
        {
           
            List<Post> postList = _uow.GetRepository<Post>().Get(x => x.IsDeleted == false).Include(x => x.SocialMediaAccountsCategoryType).ThenInclude(x=>x.SocialMediaAccounts).OrderBy(x => x.OrderNumber).Take(quantity).ToList();
            List<int> postIds = postList.Select(x => x.ContentId).ToList();
            List<SourceContent> contentList = _uow.GetRepository<SourceContent>().Get(t => postIds.Contains(t.Id)&&t.IsDeleted==false).Include(x => x.Category).ThenInclude(x => x.Source).ToList();
            List<PostViewModelDTO> postListDTO = new List<PostViewModelDTO>();
            
            
            foreach (var item in postList)
            {
                SourceContent sourceContent = contentList.Where(x => x.Id == item.ContentId).FirstOrDefault();
                postListDTO.Add(new PostViewModelDTO()
                {
                    SocialMediaAccountsCategoryType = new PostSocialMediaAccountsCategoryTypeViewModelDTO
                    {
                        SocialMediaAccounts = new PostSocialMediaViewModelDTO()
                        {
                            AccountNameOrMail = item.SocialMediaAccountsCategoryType.SocialMediaAccounts.AccountNameOrMail,
                            Icon = item.SocialMediaAccountsCategoryType.SocialMediaAccounts.Icon,
                            Id = item.SocialMediaAccountsCategoryType.SocialMediaAccounts.Id,
                            Name = item.SocialMediaAccountsCategoryType.SocialMediaAccounts.Name,
                        },
                        Id = item.SocialMediaAccountsCategoryType.Id
                    },
                    OrderNumber = item.OrderNumber,
                    Id = item.Id,
                    SourceContent = new PostSourceContentViewModelDTO()
                    {
                        Id = sourceContent.Id,
                        imageURL = sourceContent.imageURL,
                        ContentInsertAt = sourceContent.ContentInsertAt,
                        Title = sourceContent.Title,
                        Description = sourceContent.Description,
                        Tags = sourceContent.Tags,
                        Category = new PostCategoryViewModelDTO
                        {
                            Id = sourceContent.Category.Id,
                            Name = sourceContent.Category.Name,
                            Tags = sourceContent.Category.Tags,
                            Source = new PostSourceViewModelDTO()
                            {
                                Id = sourceContent.Category.Source.Id,
                                Image = sourceContent.Category.Source.Image,
                                Name = sourceContent.Category.Source.Name
                            }
                        }
                    },
                });
            }
            return postListDTO;
        }

        public int ChangeOrder(int postId,int order)
        {
            try
            {


                int result = default;
                var post = GetById(postId);
               
                if (post.OrderNumber != order)
                {
                    var oldOrderPost = GetByOrderNumber(order);

                    oldOrderPost.UpdatedAt = DateTime.Now;
                    oldOrderPost.OrderNumber = post.OrderNumber;
                    post.IsSpecialPost = true;
                    _uow.GetRepository<Post>().Update(oldOrderPost);

                    post.UpdatedAt = DateTime.Now;
                    post.OrderNumber = order;
                    post.IsSpecialPost = true;
                    _uow.GetRepository<Post>().Update(post);

                    result = _uow.SaveChanges();
                    if (result > 0)
                        Log.Logger.Information($"Post sıra numarası değiştirildi.  - {post.OrderNumber}");
                    else
                    {
                        Log.Logger.Error($"Hata! Post sıra numarası değiştirilirken hata oluştu. -  { post.OrderNumber}");
                        throw new Exception($"Hata! Post sıra numarası değiştirilirken hata oluştu.  -  { post.OrderNumber}");
                    }

                }

                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Hata! Post sıra numarası değiştirilirken hata oluştu.  -  { postId}");
                throw;
            }
        }

    }
}
