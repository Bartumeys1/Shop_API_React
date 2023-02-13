using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Images;
using Services.Models.Products;


namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse> AddProductAsync(CreateProductVM model);
        Task<ServiceResponse> GetProductByCategory(ProductsByCategoryVM model, HttpRequest request);
        Task<ServiceResponse> GetProductByIdAsync(int id, HttpRequest request);
        Task<ServiceResponse> GetProductBySlugAsync(string slug, HttpRequest request);
        Task<ServiceResponse> DeletetAsync(int id);
        Task<ServiceResponse> SetProductImageByIdAsync(UploadImageVM model);
        Task<ServiceResponse> GetAllProductImagesAsync(int id , HttpRequest request);
    }
}
