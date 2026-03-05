using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPricing.Shared.Dtos
{

        public class ProductListItemDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public decimal Price { get; set; }
            public DateTime LastUpdated { get; set; }
        }

  
}
