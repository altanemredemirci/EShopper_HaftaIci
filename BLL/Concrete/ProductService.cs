using BLL.Abstract;
using CORE.Entities;
using DAL;
using DAL.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace BLL.Concrete
{
    public class ProductService : IProductService
	{
		private readonly IProductDal _productDal;

		public ProductService(IProductDal productDal)
		{
            _productDal = productDal;
		}

        public Task CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(Expression<Func<Product, bool>> filter = null)
        {
            return await _productDal.GetAllAsync(filter);
        }

        public Task<Product> GetOneAsync(Expression<Func<Product, bool>> filter)
        {
            return _productDal.GetOneAsync(filter);
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
