using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using MovieStore.Data;

namespace MovieStore.Services
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private MovieStoreDbContext _context;
        private DbSet<T> _dbSet;

        public BaseRepository()
        {
            _context = new MovieStoreDbContext();
            _dbSet = _context.Set<T>();
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(T entity)
        {
            _dbSet.Remove(entity);
            int result = _context.SaveChanges();
            return result > 0;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }
        public T UpdateN(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
