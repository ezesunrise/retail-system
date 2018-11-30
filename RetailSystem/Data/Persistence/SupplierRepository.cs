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
    public class SupplierRepository: IRepository<Supplier>
    {
        private ApplicationDbContext _context { get; set; }

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Supplier> GetAll()
        {
            return _context.Suppliers;
        }

        public async Task<IEnumerable<Supplier>> GetAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            var entity = await _context.Suppliers.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Supplier> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(Supplier supplier)
        {
            try
            {
                _context.Suppliers.Add(supplier);
                
                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(Supplier supplier)
        {
            _context.Entry(supplier).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Suppliers.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Suppliers.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<Supplier> suppliers)
        {
            _context.AddRange(suppliers);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Suppliers.AnyAsync(e => e.Id == id);
        }
    }
}