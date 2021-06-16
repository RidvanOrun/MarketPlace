using MarketPlace.DomainLayer.Repository.EntityTypeRepository;
using MarketPlace.DomainLayer.UnitOfWork;
using MarketPlace.InfrastructureLayer.Context;
using MarketPlace.InfrastructureLayer.Repository.EntityTypeRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.InfrastructureLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db ?? throw new ArgumentNullException("Database bağlantısında hata yaşandı");
        }

        private IProductRepository _productRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null) _productRepository = new ProductRepository(_db);
                return _productRepository;          
            }

        }
        private ICategoryRepository _categoryRepository;
        public ICategoryRepository CategoryRepository 
        {
            get
            {
                if (_categoryRepository == null) _categoryRepository = new CategoryRepository(_db);
                return _categoryRepository;
            }
        }

        public async Task Commit() => await _db.SaveChangesAsync();

        private bool isDisposing = false;
        public async ValueTask DisposeAsync()
        {
            if (!isDisposing)
            {
                isDisposing = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this); // => Nesnemizi tamamıyla temizlenmesini sağlayacak.
            }
        }
        private async Task DisposeAsync(bool disposing)
        {
            if (disposing) await _db.DisposeAsync(); // => Üretilen db nesnemizi dispose ettik.
        }

    }
}
