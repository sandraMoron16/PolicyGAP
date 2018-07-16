using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal PolicyDbContext _context;

        public GenericRepository(PolicyDbContext context)
        {
           
            _context = context;
            
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _context.Set<T>();
            return query;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
