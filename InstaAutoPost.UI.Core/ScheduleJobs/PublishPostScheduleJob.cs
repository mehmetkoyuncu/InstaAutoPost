using Hangfire;
using InstaAutoPost.SendPostBot.UI;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
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

namespace InstaAutoPost.UI.Core.ScheduleJobs
{
    public static class PublishPostScheduleJob
    {
        [Obsolete]
        public static void RunJob(string environment, string cron)
        {
            RecurringJob.AddOrUpdate(() => PublishPost(environment, null), cron, TimeZoneInfo.Local);
        }

        public static void PublishPost(string environment, SourceContent sendedContent = null)
        {
            SourceContentService contentService = new SourceContentService();
            PostService postService = new PostService();
            MailService mailService = new MailService();
            if (sendedContent == null)
            {
                List<PostDTO> postListDTO = postService.GetAll();
                List<Post> postList = Mapping.Mapper.Map<List<Post>>(postListDTO);
                List<Post> notSpecialPosts = postList.Where(x => x.IsSpecialPost == false).ToList();

                int result = postService.RemoveRange(notSpecialPosts);
                var data = postService.GetDataOfWillLoad();
                int rangeResult = 0;
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        foreach (var item2 in item.CategoryType.Category)
                        {
                            foreach (var item3 in item2.SourceContents.Select((value, i) => new { i, value }))
                            {
                                var contentId = postService.GetByContentId(item3.value.Id);
                                if (contentId == null)
                                {
                                  
                                    PostDTO postDTO = new PostDTO();
                                    postDTO.InsertedAt = DateTime.Now;
                                    postDTO.SocialMediaAccountsCategoryTypeId = item.Id;
                                    postDTO.UpdatedAt = DateTime.Now;
                                    postDTO.ContentId = item3.value.Id;
                                    var order = ControlOrder();
                                    if (order != 0)
                                    {
                                        postDTO.OrderNumber = order;
                                        rangeResult = postService.Add(postDTO);
                                    }
                                }

                            }
                        }
                    }
                    if (rangeResult > 0)
                    {

                        var newPosts = postService.GetFirstPost();
                        var content = contentService.GetSourceContentById(newPosts.ContentId);
                        var accounts = contentService.GetAccountsByContentId(content.Id);

                        var instagramBot = new SeleniumMain().Publish(content, environment, accounts);
                        if (instagramBot)
                        {
                            SourceContentDTO sourceContentDTO = Mapping.Mapper.Map<SourceContentDTO>(content);
                            content.SendOutForPost = true;
                            content.PostTime = DateTime.Now;
                            if (newPosts.IsSpecialPost == true)
                            {
                                Post post = Mapping.Mapper.Map<PostDTO, Post>(newPosts);
                                postService.Remove(post.Id);
                            }
                            int updateResult = contentService.UpdateSourceContent(content);
                            mailService.SendMailAutoForSourceContent(sourceContentDTO, environment);
                            OrderPostUtility.Order();
                            Log.Logger.Information($"İçerik başarıyla post edildi.  - {sourceContentDTO.Title} - {sourceContentDTO.Id}");
                        }
                        else
                            Log.Logger.Error($"İçerikler Post edilirken bir hata oluştu.  - {content.Title} - {content.Id}");
                    }
                }

            }
            else
            {
                var accounts = contentService.GetAccountsByContentId(sendedContent.Id);
                var instagramBot = new SeleniumMain().Publish(sendedContent, environment, accounts);
                if (instagramBot)
                {

                    SourceContentDTO sourceContentDTO = Mapping.Mapper.Map<SourceContentDTO>(sendedContent);
                    sendedContent.SendOutForPost = true;
                    sendedContent.PostTime = DateTime.Now;
                    var newPosts = postService.GetByContentId(sendedContent.Id);
                    if (newPosts != null)
                    {
                        if (newPosts.IsSpecialPost == true)
                        {
                            postService.Remove(newPosts.Id);
                        }
                    }

                    int updateResult = contentService.UpdateSourceContent(sendedContent);
                    mailService.SendMailAutoForSourceContent(sourceContentDTO, environment);
                    OrderPostUtility.Order();
                    Log.Logger.Information($"İçerik başarıyla post edildi.  - {sourceContentDTO.Title} - {sourceContentDTO.Id}");
                }
                else
                    Log.Logger.Error($"İçerikler Post edilirken bir hata oluştu.  - {sendedContent.Title} - {sendedContent.Id}");
            }
        }

        public static int ControlOrder()
        {
            PostService postService = new PostService();
            var currentOrder = 1;
            List<int> orders = postService.GetAll().Select(x => x.OrderNumber).ToList();
            int order = 1;
            for (int i = 0; i < orders.Count() + 1; i++)
            {
                if (orders.Contains(currentOrder))
                {
                    currentOrder++;
                }

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
