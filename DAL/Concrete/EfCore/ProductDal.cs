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
    public class ProductDal : GenericRepository<Product,DataContext>, IProductDal
    {
        private readonly DataContext _context;

        public ProductDal(DataContext context):base(context)
        {
            _context = context;
        }

        public override async Task<IReadOnlyList<Product>> GetAllAsync(Expression<Func<Product, bool>> filter = null)
        {
                //AsQueryable: Sql sorgusunun bitmediğini ve devamının gelebileceğini belirtir
                var products = _context.Products.Include(i => i.Images).AsQueryable();

                if (filter != null)
                {
                    products = products.Where(filter);
                }

                return await products.ToListAsync();
            
        }

        public override async Task<Product> GetOneAsync(Expression<Func<Product, bool>> filter = null)
        {
                //AsQueryable: Sql sorgusunun bitmediğini ve devamının gelebileceğini belirtir
                var product = _context.Products.Include(i => i.Images).Include(i=>i.Brand).AsQueryable();

                if (filter != null)
                {
                    product = product.Where(filter);
                }

                return await product.FirstOrDefaultAsync();
           
        }
    }
}
