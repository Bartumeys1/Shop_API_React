using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.Images;
using Services.Models.Products;
using Services.Settings;

namespace Services.Services
{
    public class ProductServices : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProductImageRepository _imageRepository;
        public ProductServices(IProductRepository productRepository, IMapper mapper, IProductImageRepository imageRepository)
        {
            _repository = productRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        public async Task<ServiceResponse> AddProductAsync(CreateProductVM model)
        {

            ProductEntity productEntity = _mapper.Map<ProductEntity>(model);

            await _repository.Create(productEntity);
            return GetServiceResponse(true, "Продукт створено успішно.");
        }


        public async Task<ServiceResponse> DeletetAsync(int id)
        {
            ProductEntity product = await _repository.GetById(id);
            if (product == null)
                return GetServiceResponse(false, "Продукт не знайдено");

            await _repository.Delete(id);

            return GetServiceResponse(true, "Продукт видалено");
        }

        public async Task<ServiceResponse> GetAllProductImagesAsync(int id, HttpRequest request)
        {
            ProductEntity result = await _repository.GetById(id);
            if (result == null)
                return GetServiceResponse(false, "Продук не знайдено");

            List<ProductImagesEntity> res = await _imageRepository.Images.Where(i => i.ProductId == id).ToListAsync();
            List<ResponseProductImageVM> imageVm = _mapper.Map<List<ResponseProductImageVM>>(res);
            foreach (var image in imageVm)
                image.ImageUrl = GetImageUrl(image.Name, ImageSettings.PRODUCTS_FOLDER, request);

            return GetServiceResponse(true, "Фотографії успішно завантажено", imageVm);
        }

        public async Task<ServiceResponse> GetProductByCategory(ProductsByCategoryVM model, HttpRequest request)
        {
            var result = await _repository.Products.Include(x => x.Images).Where(p => p.CategoryId == model.CategoryId).ToListAsync();
            if (result == null)
                return GetServiceResponse(false, "Продукт не знайдено");

            List<ProductVM> productsVM = _mapper.Map<List<ProductVM>>(result);
            foreach (var product in productsVM)
                foreach (var image in product.Images)
                    image.ImageUrl = GetImageUrl(image.Name, ImageSettings.PRODUCTS_FOLDER, request);


            return GetServiceResponse(true, "Продукти завантажено успішно", productsVM);
        }

        public async Task<ServiceResponse> GetProductByIdAsync(int id, HttpRequest request)
        {
            ProductEntity entity = await _repository.Products.Include(x=>x.Images).Where(p=>p.Id == id).FirstOrDefaultAsync();
            if (entity == null)
                return GetServiceResponse(false, "Продукт не знайдено");

            ProductVM productVM = _mapper.Map<ProductVM>(entity);
            foreach (var image in productVM.Images)
                image.ImageUrl = GetImageUrl(image.Name, ImageSettings.PRODUCTS_FOLDER, request);

            return GetServiceResponse(true, "Продукт знайдено.", productVM);
        }

        public async Task<ServiceResponse> SetProductImageByIdAsync(UploadImageVM model)
        {
            ProductEntity res = await _repository.GetById(model.ProductId);
            if (res == null)
                return GetServiceResponse(false, "Продукт не знайдено");

            var fileName = await SaveFormFileInFolder(ImageSettings.PRODUCTS_FOLDER, model.Image);
            if (fileName == null)
                return GetServiceResponse(false, "Щось пішло не так при збереженні фотографії продукту");

            int lastPriority = await _imageRepository.Images.Where(x => x.ProductId == model.ProductId).CountAsync();
            ProductImagesEntity imagesEntity = _mapper.Map<ProductImagesEntity>(model);
            imagesEntity.Name = fileName;
            imagesEntity.Priority = lastPriority + 1;
            await _imageRepository.Create(imagesEntity);

            return GetServiceResponse(true, "Фотографія успішно збережена");
        }
        public async Task<ServiceResponse> GetProductBySlugAsync(string slug, HttpRequest request)
        {
            ProductEntity product = await _repository.Products.Include(x => x.Images).Where(x => x.Slug == slug).FirstOrDefaultAsync();
            if (product == null)
                return GetServiceResponse(false, "Продукт не знайдено");

            ProductVM productVM = _mapper.Map<ProductVM>(product);
            foreach (var image in productVM.Images)
                image.ImageUrl = GetImageUrl(image.Name, ImageSettings.PRODUCTS_FOLDER, request);
            return GetServiceResponse(true, "Ok", productVM);
        }

        private ServiceResponse GetServiceResponse(bool isSuccess, string message, object payload = null)
        {
            return new ServiceResponse
            {
                IsSuccess = isSuccess,
                Message = message,
                Payload = payload
            };
        }

        private async Task<string> SaveFormFileInFolder(string folder, IFormFile image)
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
        private string GetImageUrl(string imageName, string folder, HttpRequest Request)
        {
            string port = string.Empty;
            if (Request.Host.Port != null)
                port = ":" + Request.Host.Port.ToString();
            string url = $@"{Request.Scheme}://{Request.Host.Host}{port}/{folder}/{imageName}";
            return url;
        }

    }
}
