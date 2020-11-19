//using System;
//using System.Collections.Generic;
//using System.Text;
//using AutoMapper;
//using Winner.Models.Response;

//namespace Winner.Models.AutoMapper.Profiles
//{
//    public class ProductClassProfiles:Profile
//    {
//        public ProductClassProfiles()
//        {
//            CreateMap<ProductClassDto, ProductClass>()
//                .ForMember(_ => _.Id, _ => _.Ignore())
//                .ForMember(_ => _.ParentId, _ => _.Ignore())
//                .ForMember(_ => _.ClassLevel, _ => _.Ignore())
//                .ForMember(_ => _.Sort, _ => _.Ignore())
//                .ForMember(_ => _.ClassName, _ => _.Ignore())
//                .ForMember(_ => _.ClassRemark, _ => _.Ignore())
//                .ForMember(_ => _.KeyTitle, _ => _.Ignore())
//                .ForMember(_ => _.Keywords, _ => _.Ignore())
//                .ForMember(_ => _.Description, _ => _.Ignore())
//                .ForMember(_ => _.SmallPicture, _ => _.Ignore())
//                .ForMember(_ => _.IsShow, _ => _.Ignore());

//            CreateMap<ProductClass, ProductClassDto>()
//                .ForMember(_ => _.Id, _ => _.Ignore())
//                .ForMember(_ => _.ParentId, _ => _.Ignore())
//                .ForMember(_ => _.ClassLevel, _ => _.Ignore())
//                .ForMember(_ => _.Sort, _ => _.Ignore())
//                .ForMember(_ => _.ClassName, _ => _.Ignore())
//                .ForMember(_ => _.ClassRemark, _ => _.Ignore())
//                .ForMember(_ => _.KeyTitle, _ => _.Ignore())
//                .ForMember(_ => _.Keywords, _ => _.Ignore())
//                .ForMember(_ => _.Description, _ => _.Ignore())
//                .ForMember(_ => _.SmallPicture, _ => _.Ignore())
//                .ForMember(_ => _.IsShow, _ => _.Ignore());
//        }
//    }
//}
