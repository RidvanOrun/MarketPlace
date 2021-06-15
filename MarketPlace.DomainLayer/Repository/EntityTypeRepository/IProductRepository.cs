using MarketPlace.DomainLayer.Entities.Concrete;
using MarketPlace.DomainLayer.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPlace.DomainLayer.Repository.EntityTypeRepository
{
    public interface IProductRepository:IBaseRepository<Product>
    {

    }
}
