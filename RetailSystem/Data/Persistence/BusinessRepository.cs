using Microsoft.EntityFrameworkCore;
using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RetailSystem.Data
{
    public class BusinessRepository: IRepository<Business>
    {
        private ApplicationDbContext _context { get; set; }

        public BusinessRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Business> GetAll()
        {
            return _context.Businesses;
        }

        public async Task<IEnumerable<Business>> GetAsync()
        {
            return await _context.Businesses.ToListAsync();
        }

        public async Task<Business> GetByIdAsync(int id)
        {
            var entity = await _context.Businesses.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Business> Entry(T entity) {
        //    return _context.Entry(entity);
        //}
        
        public void Add(Business business)
        {
            try
            {
                _context.Businesses.Add(business);                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(Business business)
        {
            _context.Entry(business).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Businesses.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Businesses.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<Business> businesses)
        {
            _context.AddRange(businesses);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Businesses.AnyAsync(e => e.Id == id);
        }
    }
}