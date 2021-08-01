using AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, Category>();
            CreateMap<SourceDTO, Source>();
            CreateMap<ImageDTO, Image>();
            CreateMap<SourceContentDTO, SourceContent>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Source, SourceDTO>();
            CreateMap<Image, ImageDTO>();
            CreateMap<SourceContent, SourceContentDTO>();


        }
    }
}
