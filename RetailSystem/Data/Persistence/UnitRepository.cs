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
    public class UnitRepository: IRepository<Unit>
    {
        private ApplicationDbContext _context { get; set; }

        public UnitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Unit> GetAll()
        {
            return _context.Units;
        }

        public async Task<IEnumerable<Unit>> GetAsync()
        {
            return await _context.Units.ToListAsync();
        }

        public async Task<Unit> GetByIdAsync(int id)
        {
            var entity = await _context.Units.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Unit> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(Unit unit)
        {
            try
            {
                _context.Units.Add(unit);
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(Unit unit)
        {
            _context.Entry(unit).State = EntityState.Modified;
        }
        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Units.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Units.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }

        public void AddRange(IEnumerable<Unit> units)
        {
            _context.AddRange(units);
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Units.AnyAsync(e => e.Id == id);
        }
    }
}