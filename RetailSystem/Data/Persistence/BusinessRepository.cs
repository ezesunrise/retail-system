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
    public class BusinessRepository : IRepository<Business>
    {
        private ApplicationDbContext _context { get; set; }

        public BusinessRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Business>> GetAsync(Expression<Func<Business, bool>> predicate)
        {
            return await _context.Businesses.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Business>> GetAllAsync()
        {
            return await _context.Businesses.ToListAsync();
        }

        public async Task<Business> GetByIdAsync(int id)
        {
            var entity = await _context.Businesses.FindAsync(id);
            return entity;
        }

        public void Add(Business entity)
        {
            _context.Businesses.Add(entity);
        }

        public void Update(Business entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Business entity)
        {
            _context.Businesses.Remove(entity);
        }

        public void AddRange(IEnumerable<Business> entities)
        {
            _context.Businesses.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Business> entities)
        {
            _context.Businesses.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Businesses.AnyAsync(e => e.Id == id);
        }
    }
}