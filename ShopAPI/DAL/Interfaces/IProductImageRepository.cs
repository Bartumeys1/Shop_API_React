using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProductImageRepository:IGenericRepository<ProductImagesEntity,int>
    {
        IQueryable<ProductImagesEntity> Images { get; }
    }
}
