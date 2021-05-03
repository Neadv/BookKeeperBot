using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookKeeperBot.Models;
using Microsoft.EntityFrameworkCore;

namespace BookKeeperBot.Data
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly DataContext context;
        private readonly DbSet<T> dbSet;

        public EfRepository(DataContext dataContext)
        {
            context = dataContext;
            dbSet = dataContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            return await dbSet.FirstOrDefaultAsync(filter);
        }

        public async Task<T> GetWithIncludeAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> include = null)
        {
            IQueryable<T> query = dbSet;

            if (include != null)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task LoadProperty(T entity, Expression<Func<T, object>> include)
        {
            await context.Entry<T>(entity).Reference(include).LoadAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}