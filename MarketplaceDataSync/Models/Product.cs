namespace MarketplaceDataSync.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string MarketplaceId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
