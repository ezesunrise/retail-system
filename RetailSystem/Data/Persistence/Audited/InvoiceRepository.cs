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
    public class InvoiceRepository : IAuditedRepository<Invoice>
    {
        private ApplicationDbContext _context { get; set; }

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAsync(Expression<Func<Invoice, bool>> predicate)
        {
            return await _context.Invoices.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices.ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            var entity = await _context.Invoices
                .Include(s => s.InvoiceItems)
                .SingleOrDefaultAsync(s => s.Id == id);
            return entity;
        }

        public void Add(Invoice entity)
        {
            _context.Invoices.Add(entity);
        }

        public void Update(Invoice entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Invoice entity)
        {
            _context.Invoices.Remove(entity);
        }

        public void AddRange(IEnumerable<Invoice> entities)
        {
            _context.Invoices.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Invoice> entities)
        {
            _context.Invoices.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Invoices.AnyAsync(e => e.Id == id);
        }
    }
}