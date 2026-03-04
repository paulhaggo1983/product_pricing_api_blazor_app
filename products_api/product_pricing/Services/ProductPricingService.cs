using product_pricing.Data;
using product_pricing.Models;

namespace product_pricing.Services
{
    public class ProductPricingService
    {
        private readonly DataStore _store;

        public ProductPricingService(DataStore store)
        {
            _store = store;
        }

        public IReadOnlyList<Product> GetProducts()
            => _store.Products;

        public Product? GetProduct(int id)
            => _store.Products.FirstOrDefault(p => p.Id == id);

        public (decimal original, decimal discounted) ApplyDiscount(Product product, decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentOutOfRangeException(nameof(percentage));

            var original = product.CurrentPrice;
            var discounted = original - (original * (percentage / 100m));

            return (original, decimal.Round(discounted, 2));
        }

        public void UpdatePrice(Product product, decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(newPrice));

            product.CurrentPrice = newPrice;
            product.LastUpdated = DateTime.UtcNow;

            product.PriceHistory.Insert(0, new PriceHistoryEntry
            {
                Price = newPrice,
                Date = DateOnly.FromDateTime(product.LastUpdated)
            });
        }

    }
}
