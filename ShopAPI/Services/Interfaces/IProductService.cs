using Microsoft.AspNetCore.Mvc;
using Services.Models.Products;


namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse> AddProductAsync(CreateProductVM model);
        Task<ServiceResponse> GetProductByCategory(ProductsByCategoryVM model);
        Task<ServiceResponse> GetProductByIdAsync(int id);
        Task<ServiceResponse> DeletetAsync(int id);
    }
}
