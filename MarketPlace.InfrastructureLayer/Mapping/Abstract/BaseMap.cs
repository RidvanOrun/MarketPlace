using MarketPlace.DomainLayer.Entities.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPlace.InfrastructureLayer.Mapping.Abstract
{
    public abstract class BaseMap<T>:IEntityTypeConfiguration<T> where T : class, IBaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder) 
        {
            builder.Property(x => x.CreateDate).IsRequired(true);
        
        }
    }
}
