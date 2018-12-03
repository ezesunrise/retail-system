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
    public class ManufacturerRepository : IRepository<Manufacturer>
    {
        private ApplicationDbContext _context { get; set; }

        public ManufacturerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manufacturer>> GetAsync(Expression<Func<Manufacturer, bool>> predicate)
        {
            return await _context.Manufacturers.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Manufacturer>> GetAllAsync()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<Manufacturer> GetByIdAsync(int id)
        {
            var entity = await _context.Manufacturers.FindAsync(id);
            return entity;
        }

        public void Add(Manufacturer entity)
        {
            _context.Manufacturers.Add(entity);
        }

        public void Update(Manufacturer entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Manufacturer entity)
        {
            _context.Manufacturers.Remove(entity);
        }

        public void AddRange(IEnumerable<Manufacturer> entities)
        {
            _context.Manufacturers.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Manufacturer> entities)
        {
            _context.Manufacturers.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Manufacturers.AnyAsync(e => e.Id == id);
        }
    }
}