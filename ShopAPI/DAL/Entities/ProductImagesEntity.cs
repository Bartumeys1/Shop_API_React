using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("tblProductImages")]
    public class ProductImagesEntity :BaseEntity<int>
    {
        public int Priority { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }
}
