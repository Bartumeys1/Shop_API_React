
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.Categories;
using System.Reflection.Metadata.Ecma335;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) {
        
            _categoryRepository = categoryRepository;
        }

        public async Task<ServiceResponse> CreateAsync(CreateCategoryVM model) // mapper
        {
            CategoryEntity category = new CategoryEntity() {
                Name = model.Name,
                Image = model.ImageBase64,
                DateCreated = DateTime.Now.ToUniversalTime()
            };                
            try
            {
                await _categoryRepository.Create(category);
            }
            catch (Exception er)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = er.Message
                };
            }

            return new ServiceResponse() {

                IsSuccess = true,
                Payload = category,
                Message = "Категорія створена і добавлена."
            };
        }



        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            CategoryEntity category = await _categoryRepository.GetById(id);
            if(category == null)
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Такої категорії немає."
                };
           
                await _categoryRepository.Delete(id);

            return new ServiceResponse()
            {
                IsSuccess = true,
                Message = $"Категорію \"{category.Name}\" видалено успішно."
            };
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            CategoryEntity category = await _categoryRepository.GetById(id);
            if (category == null)
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Такої категорії не знайдено."
                };

            return new ServiceResponse
            {
                IsSuccess = true,
                Payload = category,
                Message = $"Знайдено категорію \"{category.Name}\""
            };
        }

        public async Task<ServiceResponse> GetAllCategoriesAsync(HttpRequest request)
        {
             var result = await _categoryRepository.GetAll().ToListAsync();
            List< ResponseCategoryVM> categories = new List<ResponseCategoryVM>();
            foreach (var item in result)
            {
                ResponseCategoryVM done = new ResponseCategoryVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    ImageUrl = GeCategoryImageUrl(item.Image, request)
                };
                categories.Add(done);
            }

            if (result.Count == 0)
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Категорій не знайдено."
                };

            return new ServiceResponse
            {
                IsSuccess = true,
                Payload = categories,
                Message = $"Категорії успішно знайдено."
            };
        }

        private string GeCategoryImageUrl(string imageName , HttpRequest Request)
        {
            string port = string.Empty;
            if (Request.Host.Port != null)
                port = ":" + Request.Host.Port.ToString();
            string url = $@"{Request.Scheme}://{Request.Host.Host}{port}/images/Category/{imageName}";
            return url;
        }

        public async Task<ServiceResponse> ReserveAndRecoverAsync(int id)
        {

                CategoryEntity category = await _categoryRepository.GetById(id);
                if (category == null)
                {
                    return new ServiceResponse { IsSuccess = false, Message = "Ошибка! Щось пішло не так." };
                }

                category.IsDelete = !category.IsDelete;
                await _categoryRepository.Update(category);

                return new ServiceResponse()
                {
                    IsSuccess = true,
                    Message = "Операцію успішно виконано."
                };
        }

    }
}
