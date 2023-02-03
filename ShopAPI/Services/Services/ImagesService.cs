
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;
using Services.Models.Categories;
using Services.Models.Images;
using System.Text.RegularExpressions;

namespace Services.Services
{
    public class ImageService:IImageService
    {
        public ImageService(IProductImageRepository imageRepository, IProductService productService)
        {
            _imageRepository = imageRepository;
            _productService = productService;

            _productFolder = "images/Products";
            _categoryFolder = "images/Category";
        }

        private readonly IProductImageRepository _imageRepository;
        private readonly IProductService _productService;

        private readonly string _productFolder;
        private readonly string _categoryFolder;


        public async Task<ServiceResponse> AddProductImage(UploadImageVM model, HttpRequest Request)
        {
            ServiceResponse res = await _productService.GetProductByIdAsync(model.ProductId);
            if (!res.IsSuccess)
                return res;

            string fileName = await SaveFormFileInFolder(_productFolder, model.Image);

            await _imageRepository.Create(new DAL.Entities.ProductImagesEntity
            {
                Name = fileName,
                Priority = model.Priority,
                ProductId = model.ProductId,
                DateCreated = DateTime.UtcNow,

            });

            string imageDirictory = GetImageUrl(fileName, _productFolder, Request);
            return new ServiceResponse()
            {
                IsSuccess = true,
                Message = "Файл успішно створено.",
                Payload = imageDirictory
            };
        }

        public async Task<ServiceResponse> AddCategoryImage(CreateCategoryVM model)
        {
            string fileName = await  SaveBase64InFolder(_categoryFolder , model.ImageBase64);
            if (fileName == null)
            {
                return new ServiceResponse { IsSuccess = false, Message = "Помилка при збереженні файлу" };
            }
            return new ServiceResponse { 
            IsSuccess = true,
            Message = "Файл успішно збережено",
            Payload= fileName};
        }

        public async Task<ServiceResponse> GetImageUrl(string imageName , HttpRequest Request)
        {
            string dir = Path.Combine(Directory.GetCurrentDirectory(), "images");

            var result =  Directory.GetFiles(dir, imageName, SearchOption.AllDirectories).ToList();

            if (result.Count <= 0)
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = $"Файл {imageName} не знайдено."
                };

            string currentImagePath = GetNameCurrentFolder(result[0]);
            string url = GetImageUrl(imageName, currentImagePath , Request);
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = $"Файл {imageName} успішно знайдено.",
                Payload = url
            };

        }


        private string GetNameCurrentFolder(string fullImagePath)
        {
            FileInfo image = new FileInfo(fullImagePath);
            List<string> folders = new List<string>{ _categoryFolder, _productFolder };
            foreach (var path in folders)
            {
                string folder = path.Substring(path.LastIndexOf('/') + 1);
                var res = image.DirectoryName.Contains(folder);
                if (res)
                    return path;
            }
            return "";
        }

        private async Task<string> SaveBase64InFolder(string folder, string imageBase64 , string Name="testName")
        {
            //test
            //imageBase64 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAApgAAAKYB3X3/OAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAANCSURBVEiJtZZPbBtFFMZ/M7ubXdtdb1xSFyeilBapySVU8h8OoFaooFSqiihIVIpQBKci6KEg9Q6H9kovIHoCIVQJJCKE1ENFjnAgcaSGC6rEnxBwA04Tx43t2FnvDAfjkNibxgHxnWb2e/u992bee7tCa00YFsffekFY+nUzFtjW0LrvjRXrCDIAaPLlW0nHL0SsZtVoaF98mLrx3pdhOqLtYPHChahZcYYO7KvPFxvRl5XPp1sN3adWiD1ZAqD6XYK1b/dvE5IWryTt2udLFedwc1+9kLp+vbbpoDh+6TklxBeAi9TL0taeWpdmZzQDry0AcO+jQ12RyohqqoYoo8RDwJrU+qXkjWtfi8Xxt58BdQuwQs9qC/afLwCw8tnQbqYAPsgxE1S6F3EAIXux2oQFKm0ihMsOF71dHYx+f3NND68ghCu1YIoePPQN1pGRABkJ6Bus96CutRZMydTl+TvuiRW1m3n0eDl0vRPcEysqdXn+jsQPsrHMquGeXEaY4Yk4wxWcY5V/9scqOMOVUFthatyTy8QyqwZ+kDURKoMWxNKr2EeqVKcTNOajqKoBgOE28U4tdQl5p5bwCw7BWquaZSzAPlwjlithJtp3pTImSqQRrb2Z8PHGigD4RZuNX6JYj6wj7O4TFLbCO/Mn/m8R+h6rYSUb3ekokRY6f/YukArN979jcW+V/S8g0eT/N3VN3kTqWbQ428m9/8k0P/1aIhF36PccEl6EhOcAUCrXKZXXWS3XKd2vc/TRBG9O5ELC17MmWubD2nKhUKZa26Ba2+D3P+4/MNCFwg59oWVeYhkzgN/JDR8deKBoD7Y+ljEjGZ0sosXVTvbc6RHirr2reNy1OXd6pJsQ+gqjk8VWFYmHrwBzW/n+uMPFiRwHB2I7ih8ciHFxIkd/3Omk5tCDV1t+2nNu5sxxpDFNx+huNhVT3/zMDz8usXC3ddaHBj1GHj/As08fwTS7Kt1HBTmyN29vdwAw+/wbwLVOJ3uAD1wi/dUH7Qei66PfyuRj4Ik9is+hglfbkbfR3cnZm7chlUWLdwmprtCohX4HUtlOcQjLYCu+fzGJH2QRKvP3UNz8bWk1qMxjGTOMThZ3kvgLI5AzFfo379UAAAAASUVORK5CYII=";

            var base64Data = Regex.Match(imageBase64, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;

            var binData = Convert.FromBase64String(base64Data);
            string dir = Path.Combine(Directory.GetCurrentDirectory(), folder);

            string fileName = Path.GetRandomFileName() + ".jpeg";
            using (var stream = new MemoryStream(binData))
            {
                await File.WriteAllBytesAsync(Path.Combine(dir, fileName), binData);
            }

            return fileName;
        }


        private async Task<string> SaveFormFileInFolder(string folder  , IFormFile image )
        {
            string fileName = string.Empty;
            if (image != null)
            {
                string fileExt = Path.GetExtension(image.FileName);
                string dir = Path.Combine(Directory.GetCurrentDirectory(), folder);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                fileName = Path.GetRandomFileName() + fileExt;
                using (var stream = System.IO.File.Create(Path.Combine(dir, fileName)))
                {
                    await image.CopyToAsync(stream);
                }
            }

            return fileName;
        }

        private string GetImageUrl(string imageName, string folder , HttpRequest Request)
        {
            string port = string.Empty;
            if (Request.Host.Port != null)
                port = ":" + Request.Host.Port.ToString();
            string url = $@"{Request.Scheme}://{Request.Host.Host}{port}/{folder}/{imageName}";
            return url;
        }

      
    }
}
