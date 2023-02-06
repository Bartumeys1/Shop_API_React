using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models.Categories;
using Services;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryVM model)
        {
            ServiceResponse result = await _categoryService.CreateAsync(model);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            ServiceResponse result = await _categoryService.DeleteAsync(id );
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            ServiceResponse result = await _categoryService.GetByIdAsync(id , Request);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetBySlug")]
        public async Task<IActionResult> GetBySlugAsync(string slug)
        {
            ServiceResponse result = await _categoryService.GetBySlugAsync(slug, Request);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            ServiceResponse result = await _categoryService.GetAllCategoriesAsync(Request);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("ReserveAndRecover")]
        public async Task<IActionResult> ReserveAndRecoverAsync(int id)
        {
            var res = await _categoryService.ReserveAndRecoverAsync(id);

            return Ok(res);
        }
    }
}
