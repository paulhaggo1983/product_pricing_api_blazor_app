using product_pricing.Data;
using product_pricing.Services;

namespace product_pricing.Tests
{
    public class ProductPricingServiceTests
    {
        private ProductPricingService _service = null!;
        private DataStore _dataStore = null!;

        // Create fresh service and datastore before each test
        [SetUp]
        public void Setup()
        {
            _dataStore = new DataStore();
            _service = new ProductPricingService(_dataStore);
        }

        // Discount should reduce price correctly
        [Test]
        public void ApplyDiscount_ReturnsCorrectPrice()
        {
            var product = _service.GetProduct(1);
            Assert.That(product, Is.Not.Null);

            product!.CurrentPrice = 100m;

            var result = _service.ApplyDiscount(product, 10m);

            Assert.That(result.original, Is.EqualTo(100m));
            Assert.That(result.discounted, Is.EqualTo(90m));
        }

        // Discount below 0 should throw
        [Test]
        public void ApplyDiscount_Throws_WhenBelowZero()
        {
            var product = _service.GetProduct(1);
            Assert.That(product, Is.Not.Null);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _service.ApplyDiscount(product!, -1m));
        }

        // Discount above 100 should throw
        [Test]
        public void ApplyDiscount_Throws_WhenAboveHundred()
        {
            var product = _service.GetProduct(1);
            Assert.That(product, Is.Not.Null);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _service.ApplyDiscount(product!, 101m));
        }

        // Updating price should change CurrentPrice
        [Test]
        public void UpdatePrice_ChangesPrice()
        {
            var product = _service.GetProduct(1);
            Assert.That(product, Is.Not.Null);

            _service.UpdatePrice(product!, 55.25m);

            Assert.That(product.CurrentPrice, Is.EqualTo(55.25m));
        }

        // LastUpdated should change after price update
        [Test]
        public void UpdatePrice_UpdatesTimestamp()
        {
            var product = _service.GetProduct(1);
            Assert.That(product, Is.Not.Null);

            var oldTime = product!.LastUpdated;

            _service.UpdatePrice(product, 44.10m);

            Assert.That(product.LastUpdated, Is.GreaterThan(oldTime));
        }

        // Updating price should add a history record
        [Test]
        public void UpdatePrice_AddsHistory()
        {
            var product = _service.GetProduct(1);
            Assert.That(product, Is.Not.Null);

            var count = product!.PriceHistory.Count;

            _service.UpdatePrice(product, 77.99m);

            Assert.That(product.PriceHistory.Count, Is.EqualTo(count + 1));
            Assert.That(product.PriceHistory[0].Price, Is.EqualTo(77.99m));
        }

        // Negative price should throw
        [Test]
        public void UpdatePrice_Throws_WhenNegative()
        {
            var product = _service.GetProduct(1);
            Assert.That(product, Is.Not.Null);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _service.UpdatePrice(product!, -5m));
        }
    }
}