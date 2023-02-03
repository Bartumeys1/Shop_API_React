
using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.Categories;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IImageService  imageService, IMapper mapper)
        {

            _categoryRepository = categoryRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> CreateAsync(CreateCategoryVM model )
        {
             var res = await _imageService.AddCategoryImage(model);
            if (!res.IsSuccess)
                return res;

            CategoryEntity category=_mapper.Map<CategoryEntity>(model);
            category.Image = (string)res.Payload;
               
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



        public async Task<ServiceResponse> DeleteAsync(int id , HttpRequest request)
        {
            CategoryEntity category = await _categoryRepository.GetById(id);
            if(category == null)
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Такої категорії немає."
                };

             await _categoryRepository.Delete(id);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "images\\Category", category.Image);
            File.Delete(fullPath);

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
                    ImageUrl = _imageService.GetImageUrl(item.Image, request).Result.Payload as string
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
