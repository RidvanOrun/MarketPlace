using MarketPlace.API.Model.DTOs;
using MarketPlace.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.API.Services.Interface
{
    public interface ICategoryService
    {
        Task Create(CategoryDTO categoryDTO);
        Task Update(CategoryDTO categoryDTO);
        Task Delete(CategoryDTO categoryDTO);

        Task<List<Category>> CategoryList();

        Task<CategoryDTO> GetById(int id);

    }
}
