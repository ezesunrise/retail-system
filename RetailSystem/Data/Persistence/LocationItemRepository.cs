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
    public class LocationItemRepository : ICompositeRepository<LocationItem>
    {
        private ApplicationDbContext _context { get; set; }

        public LocationItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocationItem>> GetAllAsync()
        {
            return await _context.LocationItems.ToListAsync();
        }

        public async Task<IEnumerable<LocationItem>> GetAsync(Expression<Func<LocationItem, bool>> predicate)
        {
            return await _context.LocationItems.Where(predicate).ToListAsync();
        }

        public async Task<LocationItem> GetByIdAsync(int locationId, int itemId)
        {
            var entity = await _context.LocationItems.FindAsync(locationId, itemId);
            return entity;
        }

        public void Add(LocationItem entity)
        {
            _context.LocationItems.Add(entity);
        }

        public void Update(LocationItem entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(LocationItem entity)
        {
            _context.LocationItems.Remove(entity);
        }

        public void AddRange(IEnumerable<LocationItem> entities)
        {
            _context.LocationItems.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<LocationItem> entities)
        {
            _context.LocationItems.RemoveRange(entities);
        }

        public Task<bool> Exists(int locationId, int itemId)
        {
            return _context.LocationItems.AnyAsync(e => e.LocationId == locationId && e.ItemId == itemId);
        }
    }
}