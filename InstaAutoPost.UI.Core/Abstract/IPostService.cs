using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface IPostService
    {
        int Add(PostDTO dto);
        int Remove(int id);
        Post GetById(int id);
        int Edit(Post dto);
        List<PostDTO> GetAll();
        List<PostDTO> GetListQuantity(int quantity);
        PostDTO GetDTO(int id);
    }
}
