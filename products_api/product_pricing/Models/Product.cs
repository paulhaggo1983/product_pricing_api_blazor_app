namespace product_pricing.Models
{

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal CurrentPrice { get; set; }
        public DateTime LastUpdated { get; set; }

        // Keeping most recent first
        public List<PriceHistoryEntry> PriceHistory { get; set; } = new();
    }
}
