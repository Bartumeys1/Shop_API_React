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
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }   
        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
    }
}
