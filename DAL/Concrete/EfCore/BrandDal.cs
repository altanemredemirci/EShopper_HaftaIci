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
    public class BrandDal : GenericRepository<Brand, DataContext>, IBrandDal
    {
        private readonly DataContext _context;

        public BrandDal(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IReadOnlyList<Brand>> GetAllAsync(Expression<Func<Brand, bool>> filter = null)
        {
            return await _context.Brands.Include(i => i.Products).ToListAsync();
        }

        public async Task<Brand> GetProductsByBrandAsync(int id)
        {
            return await _context.Brands
                .Include(b => b.Products)
                .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
