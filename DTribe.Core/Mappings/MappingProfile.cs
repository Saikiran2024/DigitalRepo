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
            CreateMap<GlobalCategories,GlobalCategoriesDTO>();
            CreateMap<GlobalCategoryItems, GlobalCategoryItemDTO>();
            CreateMap<Categories, CategoriesDTO>();
            CreateMap<CategoryItem, CategoryItemDTO>();
            CreateMap<CategoryItemDTO, CategoryItem>();
        }
    }
}
