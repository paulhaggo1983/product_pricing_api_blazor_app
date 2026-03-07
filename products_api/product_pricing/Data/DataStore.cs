using product_pricing.Models;

namespace product_pricing.Data
{
    public class DataStore
    {
        private readonly List<Product> _products;

        public DataStore()
        {
            var now = DateTime.UtcNow;
            var random = new Random();

            _products = new List<Product>();

            for (int i = 1; i <= 100; i++)
            {
                var basePrice = Math.Round((decimal)(random.NextDouble() * 99) + 1, 2);

                var product = new Product
                {
                    Id = i,
                    Name = $"Product {i}",
                    CurrentPrice = basePrice,
                    LastUpdated = now.AddDays(-random.Next(0, 30)),
                    PriceHistory = new List<PriceHistoryEntry>()
                };

                int historyCount = random.Next(4, 11);

                for (int j = 0; j < historyCount; j++)
                {
                    product.PriceHistory.Add(new PriceHistoryEntry
                    {
                        Price = basePrice + random.Next(-20, 40),
                        Date = DateOnly.FromDateTime(now.AddDays(-random.Next(5, 60)))
                    });
                }

                _products.Add(product);
            }
        }

        public List<Product> Products => _products;
    }
}