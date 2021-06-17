using MarketPlace.API.Model.DTOs;
using MarketPlace.API.Services.Interface;
using MarketPlace.DomainLayer.Entities.Concrete;
using MarketPlace.DomainLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryService.CategoryList());
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            CategoryDTO categoryDTO = await _categoryService.GetById(id);

            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task Create([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO != null)
            {
                await _categoryService.Create(categoryDTO);
            }
        }
        [HttpPut("{id}", Name = "UpdateCategory")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.id) return BadRequest();

            await _categoryService.Update(categoryDTO);

            return CreatedAtAction(nameof(Get), categoryDTO);

        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var category = await _categoryService.GetById(id);
            await _categoryService.Delete(category);
        }


    }
}
