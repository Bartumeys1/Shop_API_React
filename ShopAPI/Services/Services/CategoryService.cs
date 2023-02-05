
using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.Categories;
using Services.Settings;
using System.Text.RegularExpressions;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {

            _categoryRepository = categoryRepository;

            _mapper = mapper;
        }

        public async Task<ServiceResponse> CreateAsync(CreateCategoryVM model)
        {
            string fileName = await SaveBase64InFolder(ImageSettings.CATEGORY_FOLDER, model.ImageBase64);
            if (fileName == null)
                return GetServiceResponse(false, "Збереження картинки неудачне");

            CategoryEntity category = _mapper.Map<CategoryEntity>(model);
            category.Image = fileName;

            try
            {
                await _categoryRepository.Create(category);
            }
            catch (Exception er)
            {
                return GetServiceResponse(false, er.Message);
            }

            return GetServiceResponse(true, "Категорія створена і добавлена.", category);
        }



        public async Task<ServiceResponse> DeleteAsync(int id, HttpRequest request)
        {
            CategoryEntity category = await _categoryRepository.GetById(id);
            if (category == null)
                return GetServiceResponse(false, "Такої категорії немає.");


            await _categoryRepository.Delete(id);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "images\\Category", category.Image);
            File.Delete(fullPath);

            return GetServiceResponse(true, $"Категорію \"{category.Name}\" видалено успішно.");
        }

        public async Task<ServiceResponse> GetByIdAsync(int id, HttpRequest request)
        {
            CategoryEntity category = await _categoryRepository.GetById(id);
            if (category == null)
                return GetServiceResponse(false, "Такої категорії не знайдено.");

            ResponseCategoryVM categoryVM = _mapper.Map<ResponseCategoryVM>(category);
            categoryVM.ImageUrl = GetImageUrl(categoryVM.Image, ImageSettings.CATEGORY_FOLDER, request);

            return GetServiceResponse(true, $"Знайдено категорію \"{categoryVM.Name}\"", categoryVM);
        }

        public async Task<ServiceResponse> GetAllCategoriesAsync(HttpRequest request)
        {
            var result = await _categoryRepository.GetAll().ToListAsync();
            List<ResponseCategoryVM> relut = _mapper.Map<List<ResponseCategoryVM>>(result);

            foreach (var item in relut)
                item.ImageUrl = GetImageUrl(item.Image, ImageSettings.CATEGORY_FOLDER, request);


            if (result.Count == 0)
                return GetServiceResponse(false, "Категорій не знайдено.");

            return GetServiceResponse(true, "Категорії успішно знайдено.", relut);


        }

        public async Task<ServiceResponse> ReserveAndRecoverAsync(int id)
        {

            CategoryEntity category = await _categoryRepository.GetById(id);
            if (category == null)
                return GetServiceResponse(false, "Ошибка! Щось пішло не так.");


            category.IsDelete = !category.IsDelete;
            await _categoryRepository.Update(category);

            return GetServiceResponse(true, "Операцію успішно виконано.");

        }

        private async Task<string> SaveBase64InFolder(string folder, string imageBase64, string Name = "testName")
        {
            //test
            //imageBase64 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAApgAAAKYB3X3/OAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAANCSURBVEiJtZZPbBtFFMZ/M7ubXdtdb1xSFyeilBapySVU8h8OoFaooFSqiihIVIpQBKci6KEg9Q6H9kovIHoCIVQJJCKE1ENFjnAgcaSGC6rEnxBwA04Tx43t2FnvDAfjkNibxgHxnWb2e/u992bee7tCa00YFsffekFY+nUzFtjW0LrvjRXrCDIAaPLlW0nHL0SsZtVoaF98mLrx3pdhOqLtYPHChahZcYYO7KvPFxvRl5XPp1sN3adWiD1ZAqD6XYK1b/dvE5IWryTt2udLFedwc1+9kLp+vbbpoDh+6TklxBeAi9TL0taeWpdmZzQDry0AcO+jQ12RyohqqoYoo8RDwJrU+qXkjWtfi8Xxt58BdQuwQs9qC/afLwCw8tnQbqYAPsgxE1S6F3EAIXux2oQFKm0ihMsOF71dHYx+f3NND68ghCu1YIoePPQN1pGRABkJ6Bus96CutRZMydTl+TvuiRW1m3n0eDl0vRPcEysqdXn+jsQPsrHMquGeXEaY4Yk4wxWcY5V/9scqOMOVUFthatyTy8QyqwZ+kDURKoMWxNKr2EeqVKcTNOajqKoBgOE28U4tdQl5p5bwCw7BWquaZSzAPlwjlithJtp3pTImSqQRrb2Z8PHGigD4RZuNX6JYj6wj7O4TFLbCO/Mn/m8R+h6rYSUb3ekokRY6f/YukArN979jcW+V/S8g0eT/N3VN3kTqWbQ428m9/8k0P/1aIhF36PccEl6EhOcAUCrXKZXXWS3XKd2vc/TRBG9O5ELC17MmWubD2nKhUKZa26Ba2+D3P+4/MNCFwg59oWVeYhkzgN/JDR8deKBoD7Y+ljEjGZ0sosXVTvbc6RHirr2reNy1OXd6pJsQ+gqjk8VWFYmHrwBzW/n+uMPFiRwHB2I7ih8ciHFxIkd/3Omk5tCDV1t+2nNu5sxxpDFNx+huNhVT3/zMDz8usXC3ddaHBj1GHj/As08fwTS7Kt1HBTmyN29vdwAw+/wbwLVOJ3uAD1wi/dUH7Qei66PfyuRj4Ik9is+hglfbkbfR3cnZm7chlUWLdwmprtCohX4HUtlOcQjLYCu+fzGJH2QRKvP3UNz8bWk1qMxjGTOMThZ3kvgLI5AzFfo379UAAAAASUVORK5CYII=";

            var base64Data = Regex.Match(imageBase64, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;

            var binData = Convert.FromBase64String(base64Data);
            string dir = Path.Combine(Directory.GetCurrentDirectory(), folder);

            string fileName = Path.GetRandomFileName() + ".jpeg";
            using (var stream = new MemoryStream(binData))
            {
                await File.WriteAllBytesAsync(Path.Combine(dir, fileName), binData);
            }

            return fileName;
        }

        private ServiceResponse GetServiceResponse(bool isSuccess, string message, object payload = null)
        {
            return new ServiceResponse
            {
                IsSuccess = isSuccess,
                Message = message,
                Payload = payload
            };
        }
        private string GetImageUrl(string imageName, string folder, HttpRequest Request)
        {
            string port = string.Empty;
            if (Request.Host.Port != null)
                port = ":" + Request.Host.Port.ToString();
            string url = $@"{Request.Scheme}://{Request.Host.Host}{port}/{folder}/{imageName}";
            return url;
        }

    }
}
