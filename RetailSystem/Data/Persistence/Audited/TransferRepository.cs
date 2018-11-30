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
    public class TransferRepository: IRepository<Transfer>
    {
        private ApplicationDbContext _context { get; set; }

        public TransferRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Transfer> GetAll()
        {
            return _context.Transfers;
        }

        public async Task<IEnumerable<Transfer>> GetAsync()
        {
            return await _context.Transfers.ToListAsync();
        }

        public async Task<Transfer> GetByIdAsync(int id)
        {
            var entity = await _context.Transfers.FindAsync(id);
            return entity;
        }

        //public DbEntityEntry<Transfer> Entry(T entity) {
        //    return _context.Entry(entity);
        //}

        public void Add(Transfer transfer)
        {
            try
            {
                _context.Transfers.Add(transfer);
                
                
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not be added.");
            }
        }

        public void Update(Transfer transfer)
        {
            _context.Entry(transfer).State = EntityState.Modified;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _context.Transfers.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            try
            {
                _context.Transfers.Remove(entity);
                return true;
            }
            catch (DataException)
            {
                throw new DataException("An unexpected error occured. Could not delete.");
            }
        }
        
        public void AddRange(IEnumerable<Transfer> transfers)
        {
            _context.AddRange(transfers);
        }

        public Task RemoveRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            return _context.Transfers.AnyAsync(e => e.Id == id);
        }
    }
}