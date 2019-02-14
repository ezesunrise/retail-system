using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace RetailSystem.Data
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();

        Task<IEnumerable<Item>> GetAsync(Expression<Func<Item, bool>> predicate);

        Task<Item> GetLastAsync(Expression<Func<Item, bool>> predicate);

        Task<Item> GetByIdAsync(int id);

        void Add(Item item);

        void Update(Item item);

        void AddRange(IEnumerable<Item> items);

        void Remove(Item entity);
        void RemoveRange(IEnumerable<Item> items);

        Task<bool> Exists(int id);
    }
}