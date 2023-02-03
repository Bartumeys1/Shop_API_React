

namespace Services.Models.Products
{
    public class CreateProductVM
    {
        public string Name { get; set;}
        public string Description { get; set;}
        public float Price { get; set;}
        public int CategoryId { get; set; }
    }
}
