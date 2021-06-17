using MarketPlace.DomainLayer.Entities.Interface;
using MarketPlace.DomainLayer.Enums;
using MarketPlace.DomainLayer.Repository.BaseRepository;
using MarketPlace.InfrastructureLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.InfrastructureLayer.Repository.BaseRepository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> _table;

        public BaseRepository(ApplicationDbContext context)
        {
            this._context = context;// => dışarıdan gelen context bağlantısını _context ile eşitledik.
            this._table = _context.Set<T>();// Tablolarımızı her defasında yazmamak için bu consructor method ile _context ile tanımlandı.
        }

        public async Task Add(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {          
            
            entity.Status = Status.Passive; // => Status passsive haline getirdik.
            entity.DeleteDate = DateTime.Now; // passsive alınma tarihini o anlık tarih yaptık.
            _context.SaveChangesAsync();
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> expression) => await _table.Where(expression).FirstOrDefaultAsync();

        public async Task<List<T>> Get(Expression<Func<T, bool>> expression) => await _table.Where(expression).ToListAsync();


        public void Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }
    }
}
