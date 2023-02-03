using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Entities
{
    [Table("tblCategories")]
    public class CategoryEntity:BaseEntity<int>
    {
        public string  Image { get; set; }

        public virtual ICollection<ProductEntity> Products { get; set; }

    }
}
