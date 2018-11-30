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
    public class ItemRepository: IRepository<Item>
    {
        private ApplicationDbContext _context { get; set; }

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Item> GetAll()
        {
            return _context.Items;
        }

        public async Task<IEnumerable<Item>> GetAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            var entity = await _context.Items.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Item> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(Item item)
        {
            try
            {
                _context.Items.Add(item);
                
                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Items.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Items.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<Item> items)
        {
            _context.AddRange(items);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Items.AnyAsync(e => e.Id == id);
        }
    }
}