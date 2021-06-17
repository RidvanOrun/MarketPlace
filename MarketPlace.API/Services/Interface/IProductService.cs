using MarketPlace.API.Model.DTOs;
using MarketPlace.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.API.Services.Interface
{
    public interface IProductService
    {
        Task Create(ProductDTO productDTO);
        Task Update(ProductDTO productDTO);
        Task Delete(ProductDTO productDTO);

        Task<List<Product>> ProductList();
        Task<ProductDTO> GetById(int id);
       

    }
}
