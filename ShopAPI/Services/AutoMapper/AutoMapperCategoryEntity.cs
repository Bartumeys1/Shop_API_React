

using AutoMapper;
using DAL.Entities;
using Services.Models.Categories;

namespace Services.AutoMapper
{
    public class AutoMapperCategoryEntityCategoryEntity : Profile
    {
        public AutoMapperCategoryEntityCategoryEntity()
        {
            CreateMap<CreateCategoryVM, CategoryEntity>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => AutoMapperHalper.GenerateSlug(src.Name)));

        }
    }

    public class asd : Profile
    {
        public asd()
        {
            CreateMap<CategoryEntity, ResponseCategoryVM>();
        }
    }
}
