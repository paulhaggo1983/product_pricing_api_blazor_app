using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPricing.Shared.Dtos
{
        public class ApplyDiscountDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public decimal OriginalPrice { get; set; }
            public decimal DiscountedPrice { get; set; }
        }


}
