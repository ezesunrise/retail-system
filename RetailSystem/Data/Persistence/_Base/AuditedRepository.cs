
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RetailSystem.Data;
using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RetailSystem.Data
{
    public class AuditedRepository<T>: IRepository<T> where T: AuditedEntity
    {
        private ApplicationDbContext _context { get; set; }

        public AuditedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
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

        public EntityEntry<T> Entry(T entity)
        {
            return _context.Entry(entity);
        }

        public void Add(T item)
        {
            try
            {
                _context.Set<T>().Add(item);
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
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

            try
            {
                _context.Set<T>().Remove(entity);
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
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