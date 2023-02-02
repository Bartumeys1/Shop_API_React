

namespace Services.Models.Products
{
    public class CreateProductVM
    {
        public string ProductName { get; set;}
        public string ProductDescription { get; set;}
        public float Price { get; set;}
        public int CategoryId { get; set; }
    }
}
