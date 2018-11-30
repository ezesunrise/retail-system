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
    public class PurchaseRepository: IRepository<Purchase>
    {
        private ApplicationDbContext _context { get; set; }

        public PurchaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Purchase> GetAll()
        {
            return _context.Purchases;
        }

        public async Task<IEnumerable<Purchase>> GetAsync()
        {
            return await _context.Purchases.ToListAsync();
        }

        public async Task<Purchase> GetByIdAsync(int id)
        {
            var entity = await _context.Purchases.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Purchase> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(Purchase purchase)
        {
            try
            {
                _context.Purchases.Add(purchase);
                
                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Purchase could not be added.");
            }
        }

        public void Update(Purchase purchase)
        {
            _context.Entry(purchase).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Purchases.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Purchases.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<Purchase> purchases)
        {
            _context.AddRange(purchases);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Purchases.AnyAsync(e => e.Id == id);
        }
    }
}