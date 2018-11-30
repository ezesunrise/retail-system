using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RetailSystem.Data
{
    public interface IRepository<T> where T: Entity
    {
        Task<IEnumerable<T>> GetAsync();

        Task<T> GetByIdAsync(int id);

        //DbEntityEntry<T> Entry(T entity);

        void Add(T item);

        void Update(T item);

        void AddRange(IEnumerable<T> items);

        Task<bool> Remove(int id);
        Task RemoveRange(IEnumerable<int> ids);

        Task<bool> Exists(int id);
    }
}