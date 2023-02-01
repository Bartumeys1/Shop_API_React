
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProductRepository: IGenericRepository<ProductEntity,int>
    {
        IQueryable<ProductEntity> Products { get; }
    }
}
