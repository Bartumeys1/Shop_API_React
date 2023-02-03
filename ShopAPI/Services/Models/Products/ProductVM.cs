using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Products
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public float Price{ get; set; }
        public string Description { get; set; }

        public ICollection<string> ImagesUrl { get; set; }

    }
}
