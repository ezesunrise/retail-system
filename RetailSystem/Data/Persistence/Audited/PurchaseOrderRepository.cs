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
    public class PurchaseOrderRepository : IAuditedRepository<PurchaseOrder>
    {
        private ApplicationDbContext _context { get; set; }

        public PurchaseOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAsync(Expression<Func<PurchaseOrder, bool>> predicate)
        {
            return await _context.Purchases.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllAsync()
        {
            return await _context.Purchases.ToListAsync();
        }

        public async Task<PurchaseOrder> GetByIdAsync(int id)
        {
            var entity = await _context.Purchases
                .Include(s => s.PurchaseOrderItems)
                .SingleOrDefaultAsync(s => s.Id == id);
            return entity;
        }

        public void Add(PurchaseOrder entity)
        {
            _context.Purchases.Add(entity);
        }

        public void Update(PurchaseOrder entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(PurchaseOrder entity)
        {
            _context.Purchases.Remove(entity);
        }

        public void AddRange(IEnumerable<PurchaseOrder> entities)
        {
            _context.Purchases.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<PurchaseOrder> entities)
        {
            _context.Purchases.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Purchases.AnyAsync(e => e.Id == id);
        }
    }
}