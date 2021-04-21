using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Winner.Models.Response;

namespace Winner.Models.AutoMapper
{
    public class AutoMapperConfiguration:Profile
    {
        public AutoMapperConfiguration()
        {

            CreateMap<ProductDto, Products>();

            CreateMap<ProductDto, ProductDto>();

            CreateMap<ProductClassDto, ProductClass>();
            //.ForMember(_ => _.ParentId, opt => opt.MapFrom(a=>a.ParentId));//如果两边的命名不一样的时候，用这个转换一下
            //.ForMember(_ => _.Id, _ => _.Ignore())//Ignore是忽略的意思，不映射
            //.ForMember(_ => _.ParentId, _ => _.Ignore())
            //.ForMember(_ => _.ClassLevel, _ => _.Ignore())
            //.ForMember(_ => _.Sort, _ => _.Ignore())
            //.ForMember(_ => _.ClassName, _ => _.Ignore())
            //.ForMember(_ => _.ClassRemark, _ => _.Ignore())
            //.ForMember(_ => _.KeyTitle, _ => _.Ignore())
            //.ForMember(_ => _.Keywords, _ => _.Ignore())
            //.ForMember(_ => _.Description, _ => _.Ignore())
            //.ForMember(_ => _.Picture, _ => _.Ignore())
            //.ForMember(_ => _.IsShow, _ => _.Ignore());

            CreateMap<ProductClass, ProductClassDto>();
                //.ForMember(_ => _.Id, _ => _.Ignore())
                //.ForMember(_ => _.ParentId, _ => _.Ignore())
                //.ForMember(_ => _.ClassLevel, _ => _.Ignore())
                //.ForMember(_ => _.Sort, _ => _.Ignore())
                //.ForMember(_ => _.ClassName, _ => _.Ignore())
                //.ForMember(_ => _.ClassRemark, _ => _.Ignore())
                //.ForMember(_ => _.KeyTitle, _ => _.Ignore())
                //.ForMember(_ => _.Keywords, _ => _.Ignore())
                //.ForMember(_ => _.Description, _ => _.Ignore())
                //.ForMember(_ => _.Picture, _ => _.Ignore())
                //.ForMember(_ => _.IsShow, _ => _.Ignore());
        }
    }
}
