using MarketPlace.API.Model.DTOs;
using MarketPlace.API.Services.Interface;
using MarketPlace.DomainLayer.Entities.Concrete;
using MarketPlace.DomainLayer.Enums;
using MarketPlace.DomainLayer.Repository.EntityTypeRepository;
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
    public class ProductController : ControllerBase
    {
       
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.ProductList());
        }

        [HttpPost]
        public async Task Create([FromBody] ProductDTO productDTO) 
        {
            if (productDTO!=null)
            {
                await _productService.Create(productDTO);
            }
        }

        [HttpPut]
        public async Task Update([FromBody] ProductDTO productDTO)
        {

            if (productDTO!=null)
            {
                await _productService.Update(productDTO);
            }
           
        }

        [HttpDelete]
        public async Task Delete([FromBody] ProductDTO productDTO)
        {
            await _productService.Delete(productDTO);
        }



    }
}
