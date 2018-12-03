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
    public class TransferRepository : IAuditedRepository<Transfer>
    {
        private ApplicationDbContext _context { get; set; }

        public TransferRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transfer>> GetAsync(Expression<Func<Transfer, bool>> predicate)
        {
            return await _context.Transfers.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Transfer>> GetAllAsync()
        {
            return await _context.Transfers.ToListAsync();
        }

        public async Task<Transfer> GetByIdAsync(int id)
        {
            var entity = await _context.Transfers
                .Include(s => s.TransferItems)
                .SingleOrDefaultAsync(s => s.Id == id);
            return entity;
        }

        public void Add(Transfer entity)
        {
            _context.Transfers.Add(entity);
        }

        public void Update(Transfer entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Transfer entity)
        {
            _context.Transfers.Remove(entity);
        }

        public void AddRange(IEnumerable<Transfer> entities)
        {
            _context.Transfers.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Transfer> entities)
        {
            _context.Transfers.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Transfers.AnyAsync(e => e.Id == id);
        }
    }
}