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
    public class ReportGroupRepository: IRepository<ReportGroup>
    {
        private ApplicationDbContext _context { get; set; }

        public ReportGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<ReportGroup> GetAll()
        {
            return _context.ReportGroups;
        }

        public async Task<IEnumerable<ReportGroup>> GetAsync()
        {
            return await _context.ReportGroups.ToListAsync();
        }

        public async Task<ReportGroup> GetByIdAsync(int id)
        {
            var entity = await _context.ReportGroups.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<ReportGroup> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(ReportGroup report)
        {
            try
            {
                _context.ReportGroups.Add(report);
                
                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(ReportGroup report)
        {
            _context.Entry(report).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.ReportGroups.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.ReportGroups.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<ReportGroup> reports)
        {
            _context.AddRange(reports);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.ReportGroups.AnyAsync(e => e.Id == id);
        }
    }
}