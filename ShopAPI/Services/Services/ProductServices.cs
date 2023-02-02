using DAL;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.Products;

namespace Services.Services
{
    public class ProductServices:IProductService
    {
        private readonly IProductRepository _repository;
        private readonly AppEFContext _context;
        public ProductServices(IProductRepository productRepository, AppEFContext context)
        {
            _repository= productRepository;
            _context= context;
        }

        public async Task<ServiceResponse> AddProductAsync(CreateProductVM model)              
        {
            ProductEntity productEntity = new ProductEntity();
        productEntity.Name = model.ProductName;
            productEntity.Description = model.ProductDescription;
            productEntity.Price = model.Price;
            productEntity.CategoryId = model.CategoryId;
            productEntity.DateCreated = DateTime.UtcNow;

            await _repository.Create(productEntity);
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Продукт створено успішно."
            };
        }

        public async Task<ServiceResponse> GetProductByCategory(ProductsByCategoryVM model)
        {
            var result = await _context.Products.Select(p=>p.CategoryId == model.CategoryId).ToListAsync();

            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Продукти завантажено успішно",
                Payload = result
            };
        }
    }
}
