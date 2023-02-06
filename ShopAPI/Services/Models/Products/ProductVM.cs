

namespace Services.Models.Products
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public float Price{ get; set; }
        public string Description { get; set; }

        public ICollection<ResponseProductImageVM> Images { get; set; }

    }
}
