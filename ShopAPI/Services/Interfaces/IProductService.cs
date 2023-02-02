using Microsoft.AspNetCore.Mvc;
using Services.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse> AddProductAsync(CreateProductVM model);
        Task<ServiceResponse> GetProductByCategory(ProductsByCategoryVM model);
    }
}
