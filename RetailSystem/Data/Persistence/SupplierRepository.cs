using Microsoft.EntityFrameworkCore;
using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RetailSystem.Data
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private ApplicationDbContext _context { get; set; }

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetAsync(Expression<Func<Supplier, bool>> predicate)
        {
            return await _context.Suppliers.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            var entity = await _context.Suppliers.FindAsync(id);
            return entity;
        }

        public void Add(Supplier entity)
        {
            _context.Suppliers.Add(entity);
        }

        public void Update(Supplier entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Supplier entity)
        {
            _context.Suppliers.Remove(entity);
        }

        public void AddRange(IEnumerable<Supplier> entities)
        {
            _context.Suppliers.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Supplier> entities)
        {
            _context.Suppliers.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Suppliers.AnyAsync(e => e.Id == id);
        }
    }
}