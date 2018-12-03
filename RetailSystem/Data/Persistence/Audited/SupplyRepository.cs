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
    public class SupplyRepository : IAuditedRepository<Supply>
    {
        private ApplicationDbContext _context { get; set; }

        public SupplyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supply>> GetAsync(Expression<Func<Supply, bool>> predicate)
        {
            return await _context.Supplies.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Supply>> GetAllAsync()
        {
            return await _context.Supplies.ToListAsync();
        }

        public async Task<Supply> GetByIdAsync(int id)
        {
            var entity = await _context.Supplies
                .Include(s => s.SupplyItems)
                .SingleOrDefaultAsync(s => s.Id == id);
            return entity;
        }

        public void Add(Supply entity)
        {
            _context.Supplies.Add(entity);
        }

        public void Update(Supply entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Supply entity)
        {
            _context.Supplies.Remove(entity);
        }

        public void AddRange(IEnumerable<Supply> entities)
        {
            _context.Supplies.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Supply> entities)
        {
            _context.Supplies.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Supplies.AnyAsync(e => e.Id == id);
        }
    }
}