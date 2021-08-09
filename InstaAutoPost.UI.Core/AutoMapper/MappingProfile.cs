﻿using AutoMapper;
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
            CreateMap<ImageDTO, Image>();
            CreateMap<SourceContentDTO, SourceContent>();
            CreateMap<Category, CategoryDTO>().ForMember(sd=>sd.SendedContentCount,o=>o.MapFrom(x=>x.SourceContents.Where(x=>x.SendOutForPost==true).Count())).ForMember(sd=>sd.SourceContentsDTO,o=>o.MapFrom(x=>x.SourceContents));
            CreateMap<CategoryDTO, Category>();
            CreateMap<Source, SourceDTO>();
            CreateMap<Image, ImageDTO>();
            CreateMap<SourceContent, SourceContentDTO>();
            CreateMap<SourceWithCategoryCountDTO, Source>();
            CreateMap<Source, SourceWithCategoryCountDTO>()
                .ForMember(sd => sd.CatrgoryCount, o => o.MapFrom(x=>x.Categories.Count));


        }
    }
}
