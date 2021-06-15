using MarketPlace.DomainLayer.Enums;
using System.Collections.Generic;

namespace MarketPlace.DomainLayer.Entities.Concrete
{
    public class Category:BaseEntity<int>
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}