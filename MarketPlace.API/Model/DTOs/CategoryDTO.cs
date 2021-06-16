using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.API.Model.DTOs
{
    public class CategoryDTO
    {
        public int id { get; set; }
        public string CategoryName { get; set; }        
        public string Description { get; set; }
    }
}
