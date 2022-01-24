using AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, Category>();
            CreateMap<SourceDTO, Source>();
            CreateMap<ImageDTO, SourceContentImage>();
            CreateMap<SourceContentDTO, SourceContent>();
            CreateMap<Category, CategoryDTO>().ForMember(sd=>sd.SendedContentCount,o=>o.MapFrom(x=>x.SourceContents.Where(x=>x.SendOutForPost==true).Count())).ForMember(sd=>sd.SourceContentsDTO,o=>o.MapFrom(x=>x.SourceContents));
            CreateMap<CategoryDTO, Category>();
            CreateMap<Source, SourceDTO>().ForMember(sd => sd.CategoryCount, o => o.MapFrom(x => x.Categories.Count()));
            CreateMap<SourceContentImage, ImageDTO>();
            CreateMap<SourceContent, SourceContentDTO>().ForMember(sd=>sd.CategoryName,o=>o.MapFrom(x=>x.Category.Name)).ForMember(sd => sd.SourceName, o => o.MapFrom(x => x.Category.Source.Name)).ForMember(sd => sd.SourceId, o => o.MapFrom(x => x.Category.Source.Id));
            CreateMap<SourceContentDTO, SourceContent>();
            CreateMap<SourceWithCategoryCountDTO, Source>();
            CreateMap<Source, SourceWithCategoryCountDTO>()
                .ForMember(sd => sd.CatrgoryCount, o => o.MapFrom(x=>x.Categories.Count));
            CreateMap<Source, SelectboxSourceDTO>().ReverseMap();
            CreateMap<Category, CategoryAddOrUpdateDTO>().ReverseMap();
            CreateMap<Source, SourceAddOrUpdateDTO>().ReverseMap();
            CreateMap<SourceContentAddOrUpdateDTO, SourceContent>().ReverseMap().ForMember(sd => sd.SourceId, o => o.MapFrom(x => x.Category.SourceId));
            CreateMap<InstaAutoPost.UI.Data.Entities.Concrete.Email, MailDTO>().ReverseMap();
            CreateMap<EmailAccountOptions, MailAuthenticate>().ReverseMap();
            CreateMap<EmailOptions, MailOptionsDTO>().ReverseMap();



        }
    }
}
