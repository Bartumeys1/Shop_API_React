using Microsoft.AspNetCore.Http;

namespace Services.Models.Images
{
    public class UploadImageVM
    {
        public int Priority { get; set; }
        public int ProductId { get; set; }
        public IFormFile Image { get; set; }
    }
}
