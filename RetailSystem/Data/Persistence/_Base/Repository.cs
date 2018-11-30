
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RetailSystem.Data;
using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace RetailSystem.Data
{
    public class Repository<T> : IRepository<T> where T: Entity
    {
        private ApplicationDbContext _context { get; set; }

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected virtual IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).AsEnumerable();
        }

        public IEnumerable<T> Get(ISpecification<T> spec)
        {
            var query = spec.Includes
                .Aggregate(_context.Set<T>().AsQueryable(), (current, include) => current.Include(include));

            //modify the IQueryable to include any string-based include statements
            var secResult = spec.IncludeStrings
                .Aggregate(query, (current, include) => current.Include(include));

            return secResult.Where(spec.Criteria).AsEnumerable();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public EntityEntry<T> Entry(T entity) {
            return _context.Entry(entity);
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Set<T>().Remove(entity);
            return true;
        }
        
        public void AddRange(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Set<T>().AnyAsync(e => e.Id == id);
        }
    }
}