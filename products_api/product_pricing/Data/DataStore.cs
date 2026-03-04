using product_pricing.Models;

namespace product_pricing.Data
{
    public class DataStore
    {
        private readonly List<Product> _products;

        public DataStore()
        {
            var now = DateTime.UtcNow;

            _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Product A",
                CurrentPrice = 100m,
                LastUpdated = now.AddDays(-1),
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Price = 120m, Date = new DateOnly(2024, 9, 1) },
                    new PriceHistoryEntry { Price = 110m, Date = new DateOnly(2024, 8, 15) },
                    new PriceHistoryEntry { Price = 100m, Date = new DateOnly(2024, 8, 10) },
                }
            },
            new Product
            {
                Id = 2,
                Name = "Product B",
                CurrentPrice = 200m,
                LastUpdated = now.AddDays(-2)
            }
        };
        }

        public List<Product> Products => _products;
    }
}
