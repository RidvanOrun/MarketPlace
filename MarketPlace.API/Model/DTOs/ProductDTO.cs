using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.API.Model.DTOs
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

    }
}
