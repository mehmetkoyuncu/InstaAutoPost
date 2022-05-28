using Hangfire;
using InstaAutoPost.SendPostBot.UI;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.ScheduleJobs
{
    public static class PublishPostScheduleJob
    {
        [Obsolete]
        public static void RunJob(string environment, string cron)
        {
            RecurringJob.AddOrUpdate(() => PublishPost(environment, null), cron);
        }
        public static void PublishPost(string environment, SourceContent sendedContent = null)
        {
            PostService postService = new PostService();
            if (sendedContent == null)
            {
                //var specialPostsDTO = postService.GetSpecialPosts();
                List<PostDTO> postListDTO = postService.GetAll();
                //List<Post> specialPosts = Mapping.Mapper.Map<List<Post>>(specialPostsDTO);
                List<Post> postList = Mapping.Mapper.Map<List<Post>>(postListDTO);
                List<Post> notSpecialPosts = postList.Where(x => x.IsSpecialPost == false).ToList();
                int result = postService.RemoveRange(notSpecialPosts);
                var data = postService.GetDataOfWillLoad();
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        foreach (var item2 in item.CategoryType.Category)
                        {
                            foreach (var item3 in item2.SourceContents.Select((value, i) => new { i, value }))
                            {
                                PostDTO postDTO = new PostDTO();
                                postDTO.InsertedAt = DateTime.Now;
                                postDTO.SocialMediaAccountsCategoryTypeId = item.Id;
                                postDTO.UpdatedAt = DateTime.Now;
                                postDTO.ContentId = item3.value.Id;
                                var order = ControlOrder((item3.i + 1));
                                if (order != 0)
                                    postDTO.OrderNumber = order;
                                postService.Add(postDTO);
                            }
                        }
                    }
                }
                SourceContentService contentService = new SourceContentService();
                var newPosts = postService.GetFirstPost();
                var content = new SourceContentService().GetSourceContentById(newPosts.ContentId);
                MailService mailService = new MailService();
                var instagramBot = new SeleniumMain().Publish(content, environment);
                if (instagramBot)
                {
                    SourceContentDTO sourceContentDTO = Mapping.Mapper.Map<SourceContentDTO>(content);
                    content.SendOutForPost = true;
                    int updateResult = contentService.UpdateSourceContent(content);
                    mailService.SendMailAutoForSourceContent(sourceContentDTO, environment);
                    Log.Logger.Information($"İçerik başarıyla post edildi.  - {sourceContentDTO.Title} - {sourceContentDTO.Id}");
                }
                else
                {
                    
                    Log.Logger.Information($"Post edilirken bir hata oluştu.  - {content.Title} - {content.Id}");
                    throw(new Exception(""));
                }
            }
        }

        public static int ControlOrder(int currentOrder)
        {
            int order = 0;
            PostService postService = new PostService();
            List<int> orders = postService.GetAll().Select(x => x.OrderNumber).ToList();
            foreach (var item in orders)
            {
                if (orders.Contains(currentOrder))
                    currentOrder++;
                else
                {
                    order = currentOrder;
                    break;
                }

            }
            return order;
        }
    }
}
