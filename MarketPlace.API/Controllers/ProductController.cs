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
       
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {          
            this._unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.ProductRepository.Get(x => x.Status != Status.Passive));
        }

        [HttpPost]
        public async Task Create([FromBody] Product product) 
        {
            Product newproduct = new Product
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Description=product.Description,
                CategoryId=product.CategoryId

            };

            await _unitOfWork.ProductRepository.Add(newproduct);
            await _unitOfWork.Commit();
        }

        [HttpPut]
        public async Task Update([FromBody] Product product)
        {
            var newproduct = await _unitOfWork.ProductRepository.FirstOrDefault(x => x.id == product.id);

            newproduct.id = product.id;
            newproduct.ProductName = product.ProductName;
            newproduct.Price = product.Price;
            newproduct.CategoryId = product.CategoryId;
            newproduct.Description = product.Description;

            _unitOfWork.ProductRepository.Update(newproduct);
            await _unitOfWork.Commit();
        }



    }
}
