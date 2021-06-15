using MarketPlace.DomainLayer.Entities.Concrete;
using MarketPlace.DomainLayer.Repository.EntityTypeRepository;
using MarketPlace.InfrastructureLayer.Context;
using MarketPlace.InfrastructureLayer.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPlace.InfrastructureLayer.Repository.EntityTypeRepository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

    }
}
