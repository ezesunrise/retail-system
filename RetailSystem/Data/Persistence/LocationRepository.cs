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
    public class LocationRepository : IRepository<Location>
    {
        private ApplicationDbContext _context { get; set; }

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAsync(Expression<Func<Location, bool>> predicate)
        {
            return await _context.Locations.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            var entity = await _context.Locations.FindAsync(id);
            return entity;
        }

        public void Add(Location entity)
        {
            _context.Locations.Add(entity);
        }

        public void Update(Location entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Location entity)
        {
            _context.Locations.Remove(entity);
        }

        public void AddRange(IEnumerable<Location> entities)
        {
            _context.Locations.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Location> entities)
        {
            _context.Locations.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Locations.AnyAsync(e => e.Id == id);
        }
    }
}