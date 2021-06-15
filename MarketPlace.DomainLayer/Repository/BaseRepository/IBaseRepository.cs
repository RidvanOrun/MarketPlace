using MarketPlace.DomainLayer.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DomainLayer.Repository.BaseRepository
{
    public interface IBaseRepository<T> where T:IBaseEntity
    {
        Task<List<T>> Get(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefault(Expression<Func<T, bool>> expression);

        Task Add(T entity);

        void Update(T entity);
        void Delete(T entity);
    }
}
