using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;
using Services.Models.Categories;

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
        public async Task<IActionResult> CreateAsync([FromBody]CreateCategoryVM model)
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
            ServiceResponse result = await _categoryService.DeleteAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            ServiceResponse result = await _categoryService.GetByIdAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            ServiceResponse result = await _categoryService.GetAllCategoriesAsync();
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
