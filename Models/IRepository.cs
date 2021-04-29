using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookKeeperBot.Models
{
    public interface IRepository<T>
    {
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(int id);

        Task<T> GetWithIncludeAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> include);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task LoadProperty(T entity, Expression<Func<T, object>> include);
    }
}