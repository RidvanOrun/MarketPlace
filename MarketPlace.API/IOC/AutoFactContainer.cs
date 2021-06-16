using Autofac;
using AutoMapper;
using MarketPlace.API.Mapper;
using MarketPlace.API.Services.Concrete;
using MarketPlace.API.Services.Interface;
using MarketPlace.DomainLayer.Repository.EntityTypeRepository;
using MarketPlace.DomainLayer.UnitOfWork;
using MarketPlace.InfrastructureLayer.Repository.EntityTypeRepository;
using MarketPlace.InfrastructureLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.API.IOC
{
    public class AutoFactContainer:Module
    {
        protected override void Load(ContainerBuilder builder) 
        {
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();           
        }
    }
}
