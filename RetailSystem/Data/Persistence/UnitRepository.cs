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
    public class UnitRepository : IRepository<Unit>
    {
        private ApplicationDbContext _context { get; set; }

        public UnitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Unit>> GetAsync(Expression<Func<Unit, bool>> predicate)
        {
            return await _context.Units.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            return await _context.Units.ToListAsync();
        }

        public async Task<Unit> GetByIdAsync(int id)
        {
            var entity = await _context.Units.FindAsync(id);
            return entity;
        }

        public void Add(Unit entity)
        {
            _context.Units.Add(entity);
        }

        public void Update(Unit entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Unit entity)
        {
            _context.Units.Remove(entity);
        }

        public void AddRange(IEnumerable<Unit> entities)
        {
            _context.Units.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Unit> entities)
        {
            _context.Units.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Units.AnyAsync(e => e.Id == id);
        }
    }
}