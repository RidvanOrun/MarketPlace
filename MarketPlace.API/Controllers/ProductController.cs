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
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IProductRepository productRepository,IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok(await _unitOfWork.ProductRepository.Get(x => x.Status != Status.Passive));          
        }

    }
}
