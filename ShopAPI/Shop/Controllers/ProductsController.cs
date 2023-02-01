using DAL.Data.ViewModels;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Products;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repositoryProduct; 
        private readonly ICategoryRepository _repositoryCategory; 
        public ProductsController( IProductRepository productRepository  , ICategoryRepository categoryRepository)
        {
            _repositoryProduct = productRepository;
            _repositoryCategory = categoryRepository;
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImageAsync([FromForm] ProductUploadImageViewModels model)
        {
            string fileName = string.Empty;
            if(model.Image != null) {
                string fileExt = Path.GetExtension(model.Image.FileName);
                string dir = Path.Combine(Directory.GetCurrentDirectory(),"images");
                fileName = Path.GetRandomFileName()+fileExt;
                using (var stream = System.IO.File.Create(Path.Combine(dir, fileName)))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            string port = string.Empty;
            if(Request.Host.Port !=null)
                 port= ":"+Request.Host.Port.ToString();
            string url = $@"{Request.Scheme}://{Request.Host.Host}{port}/images/{fileName}";
            return Ok(url);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddProductAsync([FromBody] CreateProductVM model)
        {

            CategoryEntity categ = await _repositoryCategory.GetById(model.CategoryId);

            ProductEntity productEntity = new ProductEntity();
            productEntity.Name = model.ProductName;
            productEntity.Description = model.ProductDescription;
            productEntity.Price = model.Price;
            productEntity.Category = categ;
            productEntity.DateCreated = DateTime.Now.ToUniversalTime(); 
             await _repositoryProduct.Create(productEntity);

            return Ok(productEntity);
        }
    }
}
