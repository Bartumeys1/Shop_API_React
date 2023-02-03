
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models.Categories;
using Services.Models.Images;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpPost]
        [Route("UploadProductImage")]
        public async Task<IActionResult> UploadProductImageAsync([FromForm] UploadImageVM model)
        {
            var result = await _imageService.AddProductImage(model, Request);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost]
        [Route("UploadCategoryImage")]
        public async Task<IActionResult> UploadCategoryImageAsync([FromForm] CreateCategoryVM model)
        {

            var result = await _imageService.AddCategoryImage(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("GetImageUrl")]
        public async Task<IActionResult> UploadCategoryImageAsync(string name)
        {

            var result = await _imageService.GetImageUrl(name, Request);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


    }
}
