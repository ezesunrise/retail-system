using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace RetailSystem.Data
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(int id);

        void Add(T item);

        void Update(T item);

        void AddRange(IEnumerable<T> items);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> items);

        Task<bool> Exists(int id);
    }
}