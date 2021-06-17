using AutoMapper;
using MarketPlace.API.Model.DTOs;
using MarketPlace.API.Services.Interface;
using MarketPlace.DomainLayer.Entities.Concrete;
using MarketPlace.DomainLayer.Enums;
using MarketPlace.DomainLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.API.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(ProductDTO productDTO)
        {
            Product product = _mapper.Map<ProductDTO, Product>(productDTO);            

            await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.Commit();
        }

        public async Task Delete(ProductDTO productDTO)
        {
            var product = await _unitOfWork.ProductRepository.FirstOrDefault(x => x.id == productDTO.id);
            if (product!=null)
            {
                _unitOfWork.ProductRepository.Delete(product);
                await _unitOfWork.Commit();
            }
           
        }

        public async Task<List<Product>> ProductList()
        {
            return await _unitOfWork.ProductRepository.Get(x => x.Status != Status.Passive);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var product = await _unitOfWork.ProductRepository.FirstOrDefault(x => x.id == productDTO.id);

            if (product!= null)
            {
                product.id = productDTO.id;
                product.ProductName = productDTO.ProductName;
                product.Price = productDTO.Price;
                product.CategoryId = productDTO.CategoryId;
                product.Description = productDTO.Description;
            }            

            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.Commit();
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.FirstOrDefault(x => x.id == id);
            return _mapper.Map<ProductDTO>(product);
        }

    }
}
