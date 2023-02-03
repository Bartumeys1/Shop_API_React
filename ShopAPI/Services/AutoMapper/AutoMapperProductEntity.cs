using AutoMapper;
using DAL.Entities;
using Services.Models.Products;

namespace Services.AutoMapper
{
    public class AutoMapperProductEntityProduct : Profile
    {
        public AutoMapperProductEntityProduct()
        {
            CreateMap<ProductEntity, ProductVM>();
        }
    }

    public class AutoMapperProductEntityCreateProductVM : Profile
    {
        public AutoMapperProductEntityCreateProductVM()
        {
            CreateMap<CreateProductVM, ProductEntity>()
                .ForMember(dest => dest.DateCreated , opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
