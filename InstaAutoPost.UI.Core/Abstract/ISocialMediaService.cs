using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface ISocialMediaService
    {
        int Add(SocialMediaDTO socialMedia);
        int Remove(int id);
        SocialMediaAccounts GetById(int id);
        int Edit(SocialMediaDTO socialMedia);
        List<SocialMediaDTO> GetAll();
        SocialMediaDTO GetDTO(int id);
        SocialMediaDTO GetByAccount(SocialMediaDTO socialMedia);
    }
}
