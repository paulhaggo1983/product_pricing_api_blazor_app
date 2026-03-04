namespace product_pricing.Dtos
{
    public class ProductHistoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<PriceHistoryItemDto> PriceHistory { get; set; } = new();
    }

    public class PriceHistoryItemDto
    {
        public decimal Price { get; set; }
        public DateOnly Date { get; set; }
    }
}
