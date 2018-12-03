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
    public class SaleRepository : IAuditedRepository<Sale>
    {
        private ApplicationDbContext _context { get; set; }

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> GetAsync(Expression<Func<Sale, bool>> predicate)
        {
            return await _context.Sales.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            var entity = await _context.Sales
                .Include(s => s.SaleItems)
                .SingleOrDefaultAsync(s => s.Id == id);
            return entity;
        }

        public void Add(Sale entity)
        {
            _context.Sales.Add(entity);
        }

        public void Update(Sale entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Sale entity)
        {
            _context.Sales.Remove(entity);
        }

        public void AddRange(IEnumerable<Sale> entities)
        {
            _context.Sales.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Sale> entities)
        {
            _context.Sales.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Sales.AnyAsync(e => e.Id == id);
        }
    }
}