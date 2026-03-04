namespace product_pricing.Dtos
{

        public class ApplyDiscountDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public decimal OriginalPrice { get; set; }
            public decimal DiscountedPrice { get; set; }
        }

    
}
