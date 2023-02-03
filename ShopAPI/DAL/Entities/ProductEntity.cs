using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("tblProducts")]
    public class ProductEntity : BaseEntity<int>
    {
        public float Price { get; set; }   
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
        public virtual IQueryable<ProductImagesEntity> Images { get; set; }
    }
}
