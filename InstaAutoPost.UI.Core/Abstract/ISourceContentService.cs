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
        int AddSourceContent(SourceContentDTO sourcContent,string contentRootPath);
        List<SourceContentDTO> GetSourceContent(int categoryId);
        SourceContentDTO GetSurceContentDTO(int id);
        List<SourceContentDTO> GetSourceContentList();
        List<SelectboxSourceCategoryDTO> GetCategoriesIdAndName(int sourceId);
        List<SelectboxSourceDTO> GetSourcesIdAndName();
        int RemoveSourceContent(int id);
        SourceContent GetSourceContentById(int id);
        int EditSourceContent(SourceContentDTO sourceContentDTO,string contentRootPath);
        List<SourceContentDTO> Filter(int categoryId, int orderId, string searchText);
    }
}
