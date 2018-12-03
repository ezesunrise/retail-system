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
    public class ItemRepository : IRepository<Item>
    {
        private ApplicationDbContext _context { get; set; }

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAsync(Expression<Func<Item, bool>> predicate)
        {
            return await _context.Items.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            var entity = await _context.Items.FindAsync(id);
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