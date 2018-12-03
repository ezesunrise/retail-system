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
    public class CustomerRepository : IRepository<Customer>
    {
        private ApplicationDbContext _context { get; set; }

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await _context.Customers.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var entity = await _context.Customers.FindAsync(id);
            return entity;
        }

        public void Add(Customer entity)
        {
            _context.Customers.Add(entity);
        }

        public void Update(Customer entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Customer entity)
        {
            _context.Customers.Remove(entity);
        }

        public void AddRange(IEnumerable<Customer> entities)
        {
            _context.Customers.AddRange(entities);

        }

        public void RemoveRange(IEnumerable<Customer> entities)
        {
            _context.Customers.RemoveRange(entities);
        }

        public Task<bool> Exists(int id)
        {
            return _context.Customers.AnyAsync(e => e.Id == id);
        }
    }
}