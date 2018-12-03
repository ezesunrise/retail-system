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
    public class SubCategoryRepository : IRepository<SubCategory>
    {
        private ApplicationDbContext _context { get; set; }

        public SubCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubCategory>> GetAsync(Expression<Func<SubCategory, bool>> predicate)
        {
            return await _context.SubCategories.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories.ToListAsync();
        }

        public async Task<SubCategory> GetByIdAsync(int id)
        {
            var entity = await _context.SubCategories.FindAsync(id);
            return entity;
        }

        public void Add(SubCategory entity)
        {
            _context.SubCategories.Add(entity);
        }

        public void Update(SubCategory entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(SubCategory entity)
        {
            _context.SubCategories.Remove(entity);
        }

        public void AddRange(IEnumerable<SubCategory> entities)
        {
            _context.SubCategories.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<SubCategory> entities)
        {
            _context.SubCategories.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.SubCategories.AnyAsync(e => e.Id == id);
        }
    }
}