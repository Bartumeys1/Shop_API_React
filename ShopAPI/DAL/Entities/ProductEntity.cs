using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Entities
{
    [Table("tblProducts")]
    public class ProductEntity : BaseEntity<int>
    {
        public float Price { get; set; }   
        public string Description { get; set; }
        public string Slug { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
        public virtual IQueryable<ProductImagesEntity> Images { get; set; }
    }
}
