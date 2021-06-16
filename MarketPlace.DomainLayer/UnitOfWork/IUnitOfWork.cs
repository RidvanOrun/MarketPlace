using MarketPlace.DomainLayer.Repository.EntityTypeRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DomainLayer.UnitOfWork
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task Commit(); //Başaralı bir işlememin sonucunda tüm değişikliklerin veri tabanına kaydolmasını sağlar.

    }
}
