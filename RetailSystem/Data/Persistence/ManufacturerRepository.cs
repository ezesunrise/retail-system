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
    public class ManufacturerRepository: IRepository<Manufacturer>
    {
        private ApplicationDbContext _context { get; set; }

        public ManufacturerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Manufacturer> GetAll()
        {
            return _context.Manufacturers;
        }

        public async Task<IEnumerable<Manufacturer>> GetAsync()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<Manufacturer> GetByIdAsync(int id)
        {
            var entity = await _context.Manufacturers.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Manufacturer> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(Manufacturer manufacturer)
        {
            try
            {
                _context.Manufacturers.Add(manufacturer);
                
                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(Manufacturer manufacturer)
        {
            _context.Entry(manufacturer).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Manufacturers.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Manufacturers.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<Manufacturer> manufacturers)
        {
            _context.AddRange(manufacturers);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Manufacturers.AnyAsync(e => e.Id == id);
        }
    }
}