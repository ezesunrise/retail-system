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
    public class InvoiceRepository : IRepository<Invoice>
    {
        private ApplicationDbContext _context { get; set; }

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Invoice> GetAll()
        {
            return _context.Invoices;
        }

        public async Task<IEnumerable<Invoice>> GetAsync()
        {
            return await _context.Invoices.ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            var entity = await _context.Invoices.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Invoice> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(Invoice invoice)
        {
            try
            {
                _context.Invoices.Add(invoice);
                

            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(Invoice invoice)
        {
            _context.Entry(invoice).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Invoices.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Invoices.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }

        public void AddRange(IEnumerable<Invoice> invoices)
        {
            _context.AddRange(invoices);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Invoices.AnyAsync(e => e.Id == id);
        }
    }
}