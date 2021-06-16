using MarketPlace.API.Model.DTOs;
using MarketPlace.API.Services.Interface;
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

        [HttpPost]
        public async Task Create([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO != null)
            {
                await _categoryService.Create(categoryDTO);
            }
        }

        [HttpPut]
        public async Task Update([FromBody] CategoryDTO categoryDTO)
        {

            if (categoryDTO != null)
            {
                await _categoryService.Create(categoryDTO);
            }

        }

        [HttpDelete]
        public async Task Delete([FromBody] CategoryDTO categoryDTO)
        {
            await _categoryService.Delete(categoryDTO);
        }




    }
}
