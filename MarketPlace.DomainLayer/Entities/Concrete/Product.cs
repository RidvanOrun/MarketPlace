using MarketPlace.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarketPlace.DomainLayer.Entities.Concrete
{
    public class Product:BaseEntity<int>
    {
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}
