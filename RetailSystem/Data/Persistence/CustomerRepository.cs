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
    public class CustomerRepository: IRepository<Customer>
    {
        private ApplicationDbContext _context { get; set; }

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Customer> GetAll()
        {
            return _context.Customers;
        }

        public async Task<IEnumerable<Customer>> GetAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var entity = await _context.Customers.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Customer> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                
                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Customers.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Customers.Remove(entity);
                
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<Customer> customers)
        {
            _context.AddRange(customers);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Customers.AnyAsync(e => e.Id == id);
        }
    }
}