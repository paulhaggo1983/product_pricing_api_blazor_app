namespace product_pricing.Dtos
{
    public class UpdatePriceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal NewPrice { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
