using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProductImageRepository:GenericRepository<ProductImagesEntity>, IProductImageRepository
    {
        public ProductImageRepository(AppEFContext context):base(context){}

        public IQueryable<ProductImagesEntity> Images => GetAll();
    }
}
