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
    public class SubCategoryRepository: IRepository<SubCategory>
    {
        private ApplicationDbContext _context { get; set; }

        public SubCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<SubCategory> GetAll()
        {
            return _context.SubCategories;
        }

        public async Task<IEnumerable<SubCategory>> GetAsync()
        {
            return await _context.SubCategories.ToListAsync();
        }

        public async Task<SubCategory> GetByIdAsync(int id)
        {
            var entity = await _context.SubCategories.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<SubCategory> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(SubCategory subCategory)
        {
            try
            {
                _context.SubCategories.Add(subCategory);
                
                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(SubCategory subCategory)
        {
            _context.Entry(subCategory).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.SubCategories.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.SubCategories.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<SubCategory> subCategories)
        {
            _context.AddRange(subCategories);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.SubCategories.AnyAsync(e => e.Id == id);
        }
    }
}