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
        int AddSourceContent(SourceContentAddOrUpdateDTO sourceContentDTO, string contentRootPath);
        List<SourceContentDTO> GetSourceContent(int categoryId);
        SourceContentDTO GetSurceContentDTO(int id);
        List<SourceContentDTO> GetSourceContentList(int next = 0, int quantity = 10);
        List<SelectBoxCategoryDTO> GetCategoriesIdAndName(int sourceId);
        List<SelectboxSourceDTO> GetSourcesIdAndName();
        int RemoveSourceContent(int id);
        SourceContent GetSourceContentById(int id);
        int EditSourceContent(int id, SourceContentAddOrUpdateDTO sourceContentDTO, string contentRootPath,int categoryId=0);
        List<SourceContentDTO> Filter(int categoryId, int orderId, string searchText);
        SourceContentAddOrUpdateDTO GetSourceContentDTOById(int id);
        int GetSourceContentCount();
        List<SourceContentDTO> GetSourceContentFilter(List<SourceContentDTO> contentList, int next = 0, int quantity = 10);
        bool CreateFolder(int id,string contentRooth);
        bool ShareMarkPost(int id,string contentRoot);

    }
}
