

using AutoMapper;
using DAL.Entities;
using Services.Models.Images;
using Services.Models.Products;

namespace Services.AutoMapper
{
    public class ProductImage:Profile
    {
        public ProductImage()
        {
            CreateMap<UploadImageVM, ProductImagesEntity>();
        }
    }
}
