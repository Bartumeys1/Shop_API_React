
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProductRepository:GenericRepository<ProductEntity> , IProductRepository
    {
        public ProductRepository(AppEFContext context):base(context){}

        public IQueryable<ProductEntity> Products => GetAll();
    }
}
