using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RetailSystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}