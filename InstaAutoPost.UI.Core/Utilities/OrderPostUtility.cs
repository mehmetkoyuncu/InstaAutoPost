using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.Utilities
{
    public static class OrderPostUtility
    {
        public static void Order(int willDeletedContentId=0)
        {
            PostService postService = new PostService();
            if (willDeletedContentId != 0) {
                postService.RemoveByContentId(willDeletedContentId);
            }
            List<PostDTO> postListDTO = postService.GetAll();
            List<Post> postList = Mapping.Mapper.Map<List<Post>>(postListDTO);
            List<Post> notSpecialPosts = postList.Where(x => x.IsSpecialPost == false).ToList();
            int removeResult = postService.RemoveRange(notSpecialPosts);
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
