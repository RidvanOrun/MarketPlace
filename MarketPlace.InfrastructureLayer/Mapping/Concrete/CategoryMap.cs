using MarketPlace.DomainLayer.Entities.Concrete;
using MarketPlace.InfrastructureLayer.Mapping.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPlace.InfrastructureLayer.Mapping.Concrete
{
    public class CategoryMap:BaseMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.CategoryName).HasMaxLength(50).IsRequired(true);

            
            base.Configure(builder);
        }

    }
}
