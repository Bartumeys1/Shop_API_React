using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Services.Models.Categories;
using Services;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse> CreateAsync(CreateCategoryVM model );
        Task<ServiceResponse> DeleteAsync(int id );
        Task<ServiceResponse> GetByIdAsync(int id, HttpRequest request);
        Task<ServiceResponse> GetBySlugAsync(string id, HttpRequest request);
        Task<ServiceResponse> GetAllCategoriesAsync(HttpRequest request);
        Task<ServiceResponse> ReserveAndRecoverAsync(int id);

    }
}
