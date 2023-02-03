using AutoMapper;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.Products;

namespace Services.Services
{
    public class ProductServices : IProductService
    {
        private readonly IProductRepository _repository;
       // private readonly IImageService _imageRepository;
        private readonly IMapper _mapper;
        public ProductServices(IProductRepository productRepository, /*IImageService imageRepository,*/ IMapper mapper)
        {
            _repository = productRepository;
            //_imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> AddProductAsync(CreateProductVM model)
        {
            //ProductEntity productEntity = new ProductEntity();
            //productEntity.Name = model.ProductName;
            //productEntity.Description = model.ProductDescription;
            //productEntity.Price = model.Price;
            //productEntity.CategoryId = model.CategoryId;
            //productEntity.DateCreated = DateTime.UtcNow;
            ProductEntity productEntity = _mapper.Map<ProductEntity>(model);

            await _repository.Create(productEntity);
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Продукт створено успішно."
            };
        }

        public async Task<ServiceResponse> DeletetAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null)
                return new ServiceResponse { 
                    IsSuccess = false,
                    Message = "Продукт не знайдено"
                };

            await _repository.Delete(id);

            return new ServiceResponse { 
            IsSuccess = true,
            Message = "Продукт Видалено"
            };
        }

        public async Task<ServiceResponse> GetProductByCategory(ProductsByCategoryVM model)
        {
            var result = await _repository.Products.Where(p => p.CategoryId == model.CategoryId).ToListAsync();
            if (result == null)
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Продуктів не знайдено."
                };

            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Продукти завантажено успішно",
                Payload = result
            };
        }

        public async Task<ServiceResponse> GetProductByIdAsync(int id)
        {
            var result = await _repository.GetById(id);

           var resu = await GetProductVM(id);
            if (result == null)
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Продукт не знайдено"
                };

            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Продукт знайдено.",
                Payload = result
            };
        }

        private async Task<ProductVM> GetProductVM(int id)
        {
            var productEntity = await _repository.GetById(id);
            ProductVM productVM  = _mapper.Map<ProductVM>(productEntity);
            return productVM;
        } 
    }
}
