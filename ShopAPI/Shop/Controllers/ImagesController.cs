using DAL.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        [HttpPost]
        [Route("UploadProductImage")]
        public async Task<IActionResult> UploadProductImageAsync([FromForm] UploadImageViewModels model)
        {
            string fileName = string.Empty;
            if (model.Image != null)
            {
                string fileExt = Path.GetExtension(model.Image.FileName);
                string dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
                fileName = Path.GetRandomFileName() + fileExt;
                using (var stream = System.IO.File.Create(Path.Combine(dir, fileName)))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            string port = string.Empty;
            if (Request.Host.Port != null)
                port = ":" + Request.Host.Port.ToString();
            string url = $@"{Request.Scheme}://{Request.Host.Host}{port}/images/{fileName}";
            return Ok(url);
        }

        [HttpPost]
        [Route("UploadCategoryImage")]
        public async Task<IActionResult> UploadCategoryImageAsync([FromForm] UploadImageViewModels model)
        {
            string fileName = string.Empty;
            if (model.Image != null)
            {
                string fileExt = Path.GetExtension(model.Image.FileName);
                string dir = Path.Combine(Directory.GetCurrentDirectory(), "images/Category");

                if(!Directory.Exists(dir))
                    Directory.CreateDirectory(dir); 

                fileName = Path.GetRandomFileName() + fileExt;
                using (var stream = System.IO.File.Create(Path.Combine(dir, fileName)))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            string port = string.Empty;
            if (Request.Host.Port != null)
                port = ":" + Request.Host.Port.ToString();
            string url = $@"{Request.Scheme}://{Request.Host.Host}{port}/images/Category/{fileName}";
            return Ok(url);
        }
    }
}
