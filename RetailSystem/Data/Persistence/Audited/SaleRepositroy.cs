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
    public class SaleRepository: IRepository<Sale>
    {
        private ApplicationDbContext _context { get; set; }

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Sale> GetAll()
        {
            return _context.Sales;
        }

        public async Task<IEnumerable<Sale>> GetAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            var entity = await _context.Sales.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Sale> Entry(T entity) {
        //    return _context.Entry(entity);
        //}
        
        public void Add(Sale sale)
        {
            try
            {
                _context.Sales.Add(sale);
                
                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(Sale sale)
        {
            _context.Entry(sale).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Sales.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Sales.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<Sale> sales)
        {
            _context.AddRange(sales);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Sales.AnyAsync(e => e.Id == id);
        }
    }
}