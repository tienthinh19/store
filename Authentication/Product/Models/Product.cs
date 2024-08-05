namespace ProductService.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public double Discount { get; set; }
        public int CategoryId { get; set; }
    }
}
