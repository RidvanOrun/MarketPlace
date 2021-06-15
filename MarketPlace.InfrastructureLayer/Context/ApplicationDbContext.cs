using MarketPlace.DomainLayer.Entities.Concrete;
using MarketPlace.InfrastructureLayer.Mapping.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPlace.InfrastructureLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) // => Mapping olarak oluşturduğumuz kuralları burada override ederek Db'ye gönderiyoruz.
        {
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new ProductMap());

            base.OnModelCreating(builder);
        }

        
    } // Asp.Net.Core Db bağlantısı için oluşturulmuştur.



}
