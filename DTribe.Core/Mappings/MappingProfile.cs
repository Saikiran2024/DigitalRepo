using AutoMapper;
using DTribe.Core.DTO;
using DTribe.Core.Entities;
using DTribe.DB.Entities;

namespace DTribe.Core.Mappings
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCategoriesDTO, UserCategories>().ReverseMap();
            CreateMap<Categories, CategoriesDTO>().ReverseMap();
           

            CreateMap<GlobalSectionCategoriesDTO, GlobalCategories>().ReverseMap();
            CreateMap<GlobalCategoriesDTO, GlobalCategories>().ReverseMap();
            CreateMap<SectionDTO, Section>().ReverseMap();
            //CreateMap<UserCategoriesSearchBySPDTO, UserCategoriesSearchResult>().ReverseMap();
            CreateMap<UserInfoDTO, UserInfo>().ReverseMap();
            CreateMap<UserCategoriesSearchResult, UserCategories>().ReverseMap();
            CreateMap<UserCategoriesSearchResult, UserCategoriesSearchBySPDTO>()
            .ForMember(dest => dest.PostedTime, opt => opt.MapFrom<PostedTimeResolver>());
        }
    }
}
