using Services.Models.Categories;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse> CreateAsync(CreateCategoryVM model);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<ServiceResponse> GetByIdAsync(int id);
        Task<ServiceResponse> GetAllCategoriesAsync();
    }
}
