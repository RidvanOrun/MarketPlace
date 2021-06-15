using MarketPlace.DomainLayer.Entities.Concrete;
using MarketPlace.InfrastructureLayer.Mapping.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPlace.InfrastructureLayer.Mapping.Concrete
{
    public class ProductMap:BaseMap<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.ProductName).IsRequired(true);
            builder.Property(x => x.Price).IsRequired(true);
            builder.Property(x => x.Description).HasMaxLength(25).IsRequired(true);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }

    }
}
