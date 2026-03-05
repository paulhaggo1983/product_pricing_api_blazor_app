using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPricing.Shared.Dtos
{

        public class UpdatePriceDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public decimal NewPrice { get; set; }
            public DateTime LastUpdated { get; set; }
        }


}
