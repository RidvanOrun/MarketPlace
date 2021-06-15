using Autofac;
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
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}
