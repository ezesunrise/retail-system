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
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationDbContext _context { get; set; }

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _context.Categories.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            return entity;
        }

        public void Add(Category entity)
        {
            _context.Categories.Add(entity);
        }

        public void Update(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Category entity)
        {
            _context.Categories.Remove(entity);
        }

        public void AddRange(IEnumerable<Category> entities)
        {
            _context.Categories.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Category> entities)
        {
            _context.Categories.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Categories.AnyAsync(e => e.Id == id);
        }
    }
}