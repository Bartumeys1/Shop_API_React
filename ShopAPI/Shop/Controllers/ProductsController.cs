using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;
using Services.Models.Products;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductService productService, IProductRepository productRepository)
        {
            _productService = productService;
            _productRepository= productRepository;
        }



        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddProductAsync([FromBody] CreateProductVM model)
        {
            ServiceResponse res = await _productService.AddProductAsync(model);

            if (res.IsSuccess)
                return Ok(res);

            return BadRequest(res);
        }

        [HttpPost]
        [Route("GetAllByCategory")]
        public async Task<IActionResult> GetProductByCategory([FromBody] ProductsByCategoryVM model)
        {
          // var result =  await _productService.GetProductByCategory(model);

            var result = await _productRepository.Products.Where(p => p.CategoryId == model.CategoryId).ToListAsync();

            var resp = new ServiceResponse
            {
                IsSuccess = true,
                Payload = result
            };
            return Ok(resp);
        }
    }
}
