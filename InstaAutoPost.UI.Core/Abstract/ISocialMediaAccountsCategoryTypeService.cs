using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface ISocialMediaAccountsCategoryTypeService
    {
        int Add(SocialMediaAccountsCategoryTypeDTO dto);
        int Remove(int id);
        SocialMediaAccountsCategoryType GetById(int id);
        int Edit(SocialMediaAccountsCategoryTypeDTO dto);
        List<SocialMediaAccountsCategoryTypeDTO> GetAll();
        SocialMediaAccountsCategoryTypeDTO GetDTO(int id);
    }
}
