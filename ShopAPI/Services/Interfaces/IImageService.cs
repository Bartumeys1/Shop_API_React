using Microsoft.AspNetCore.Http;
using Services.Models.Categories;
using Services.Models.Images;

namespace Services.Interfaces
{
    public interface IImageService
    {
        Task<ServiceResponse> AddProductImage(UploadImageVM model, HttpRequest Request);
        Task<ServiceResponse> AddCategoryImage(CreateCategoryVM model);
        Task<ServiceResponse> GetImageUrl(string imageName, HttpRequest Request);
    }
}
