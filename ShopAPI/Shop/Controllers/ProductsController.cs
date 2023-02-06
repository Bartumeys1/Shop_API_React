using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;
using Services.Models.Images;
using Services.Models.Products;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
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

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteProductAsync( int id )
        {
             var result = await _productService.DeletetAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


        [HttpGet]
        [Route("GetAllByCategory")]
        public async Task<IActionResult> GetProductByCategory(int id )
        {
            ProductsByCategoryVM model = new ProductsByCategoryVM();
            model.CategoryId = id;
            var result = await _productService.GetProductByCategory(model, Request);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("GetBySlug")]
        public async Task<IActionResult> GetProductBySlugAsync(string slug)
        {
            var result = await _productService.GetProductBySlugAsync(slug);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost]
        [Route("CreateImageById")]
        public async Task<IActionResult> SetProductImageByIdAsync([FromForm]UploadImageVM model)
        {
            var result = await _productService.SetProductImageByIdAsync(model);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("GetAllImages")]
        public async Task<IActionResult> GetAllProductImagesAsync(int id)
        {
            ServiceResponse result = await _productService.GetAllProductImagesAsync(id, Request);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
