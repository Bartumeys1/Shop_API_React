using DAL.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
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

            string post = string.Empty;
            if(Request.Host.Port !=null)
                 post= ":"+Request.Host.Port.ToString();
            string url = $@"{Request.Scheme}://{Request.Host.Host}{post}/images/{fileName}";
            return Ok(url);
        }
    }
}
