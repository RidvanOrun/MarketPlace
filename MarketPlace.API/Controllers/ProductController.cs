using MarketPlace.API.Model.DTOs;
using MarketPlace.API.Services.Interface;
using MarketPlace.DomainLayer.Entities.Concrete;
using MarketPlace.DomainLayer.Enums;
using MarketPlace.DomainLayer.Repository.EntityTypeRepository;
using MarketPlace.DomainLayer.UnitOfWork;
using MarketPlace.InfrastructureLayer.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

       /// <summary>
       /// id den güncellenecek yere getirme
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>

        [HttpGet("{id:int}",Name ="GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            ProductDTO productDTO = await _productService.GetById(id);

            return Ok(productDTO);
        }


        [HttpPost]
        public async Task Create([FromBody] ProductDTO productDTO) 
        {
            if (productDTO!=null)
            {
                await _productService.Create(productDTO);
            }
        }

        /// <summary>
        /// bilgileri güncelleme
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "UpdateProduct")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, ProductDTO productDTO)
        {
            if (id != productDTO.id) return BadRequest();

            await _productService.Update(productDTO);

            return CreatedAtAction(nameof(Get), productDTO);

        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            ProductDTO productDTO = await _productService.GetById(id);
            await _productService.Delete(productDTO);
        }



    }
}
