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
    public class OrderRepository: IRepository<Order>
    {
        private ApplicationDbContext _context { get; set; }

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Orders;
        }

        public async Task<IEnumerable<Order>> GetAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var entity = await _context.Orders.SingleOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        //public DbEntityEntry<Order> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Order could not be added.");
            }
        }

        public void Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Orders.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Orders.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<Order> orders)
        {
            _context.AddRange(orders);
            
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Orders.AnyAsync(e => e.Id == id);
        }
    }
}