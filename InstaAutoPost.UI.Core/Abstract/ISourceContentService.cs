using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface ISourceContentService
    {
        int AddSourceContent(List<SourceContent> sourceContent);
        List<SourceContentDTO> GetSourceContent(int categoryId);
        string RemoveSourceContent(int id);
        SourceContent GetSourceContentById(int id);
    }
}
