using Microsoft.EntityFrameworkCore;
using RetailSystem.Data;
using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RetailSystem.Services
{
    public class ItemService
    {
        private ApplicationDbContext _context { get; set; }

        public ItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetItems(Expression<Func<Item, bool>> predicate)
        {
            return await _context.Items.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            var entity = await _context.Items
                .Include(i => i.LocationItems)
                .SingleOrDefaultAsync(s => s.Id == id);
            return entity;
        }

        public void Add(Item entity)
        {
            _context.Items.Add(entity);
        }

        public void Update(Item entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Item entity)
        {
            _context.Items.Remove(entity);
        }

        public void AddRange(IEnumerable<Item> entities)
        {
            _context.Items.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Item> entities)
        {
            _context.Items.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Items.AnyAsync(e => e.Id == id);
        }
    }
}
