using CORE;
using CORE.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.EfCore
{
    public class GenericRepository<T, TContext> : IRepository<T>
        where T : class
        where TContext : DbContext
    {
        private readonly TContext _context;

        public GenericRepository(TContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity); //DbSet<Product> Products
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();

        }

        public async Task<T> FindAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public virtual async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            //AsQueryable: Sql sorgusunun bitmediğini ve devamının gelebileceğini belirtir
            var models = _context.Set<T>().AsQueryable();

            if (filter != null)
            {
                models = models.Where(filter);
            }

            return await models.ToListAsync();
        }

        public virtual async Task<T> GetOneAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
