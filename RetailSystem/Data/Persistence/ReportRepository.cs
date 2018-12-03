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
    public class ReportGroupRepository : IRepository<ReportGroup>
    {
        private ApplicationDbContext _context { get; set; }

        public ReportGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReportGroup>> GetAsync(Expression<Func<ReportGroup, bool>> predicate)
        {
            return await _context.ReportGroups.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<ReportGroup>> GetAllAsync()
        {
            return await _context.ReportGroups.ToListAsync();
        }

        public async Task<ReportGroup> GetByIdAsync(int id)
        {
            var entity = await _context.ReportGroups.FindAsync(id);
            return entity;
        }

        public void Add(ReportGroup entity)
        {
            _context.ReportGroups.Add(entity);
        }

        public void Update(ReportGroup entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(ReportGroup entity)
        {
            _context.ReportGroups.Remove(entity);
        }

        public void AddRange(IEnumerable<ReportGroup> entities)
        {
            _context.ReportGroups.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<ReportGroup> entities)
        {
            _context.ReportGroups.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.ReportGroups.AnyAsync(e => e.Id == id);
        }
    }
}