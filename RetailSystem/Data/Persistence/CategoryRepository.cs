using Microsoft.EntityFrameworkCore;
using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RetailSystem.Data
{
    public class CategoryRepository: IRepository<Category>
    {
        private ApplicationDbContext _context { get; set; }

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Category> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Categories.Remove(entity);
            return true;
        }
        
        public void AddRange(IEnumerable<Category> categories)
        {
            _context.AddRange(categories);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Categories.AnyAsync(e => e.Id == id);
        }
    }
}