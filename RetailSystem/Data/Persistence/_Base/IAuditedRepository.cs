using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RetailSystem.Data
{
    public interface IAuditedRepository<T> : IRepository<T> where T: AuditedEntity
    {
        //IQueryable<T> GetAll();
        //Task<T> Get(int id);

        ////DbEntityEntry<T> Entry(T entity);

        //Task AddOrUpdate(T item);
        //Task AddRange(IEnumerable<T> items);

        //Task Remove(int id);
        //Task RemoveRange(IEnumerable<int> ids);
    }
}