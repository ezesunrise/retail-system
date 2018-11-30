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
    public class LocationRepository: IRepository<Location>
    {
        private ApplicationDbContext _context { get; set; }

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Location> GetAll()
        {
            return _context.Locations;
        }

        public async Task<IEnumerable<Location>> GetAsync()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            var entity = await _context.Locations.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Location> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(Location location)
        {
            try
            {
                _context.Locations.Add(location);
                
                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(Location location)
        {
            _context.Entry(location).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Locations.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Locations.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<Location> locations)
        {
            _context.AddRange(locations);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Locations.AnyAsync(e => e.Id == id);
        }
    }
}