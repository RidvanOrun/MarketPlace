using AutoMapper;
using MarketPlace.API.Model.DTOs;
using MarketPlace.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.API.Mapper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
        }
    }
}
