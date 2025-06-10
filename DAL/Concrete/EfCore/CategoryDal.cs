using CORE.Entities;
using CORE.Repositories;
using DAL.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.EfCore
{
    public class CategoryDal : GenericRepository<Category, DataContext>, ICategoryDal
    {
        private readonly DataContext _context;

        public CategoryDal(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IReadOnlyList<Category>> GetAllAsync(Expression<Func<Category, bool>> filter = null)
        {
            var categories = _context.Categories.Include(i => i.BrandCategories).ThenInclude(i => i.Brand).AsQueryable();

            if (filter != null)
            {
                categories = categories.Where(filter);
            }

            return await categories.ToListAsync();
        }

        public async Task<IReadOnlyList<Category>> GetCategories()
        {
                var categories = await _context.Categories.Include(i => i.BrandCategories).ThenInclude(i => i.Brand).ThenInclude(i => i.Products).ThenInclude(i => i.Images).ToListAsync();

                return categories;
            
        }
    }
}
