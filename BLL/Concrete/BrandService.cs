using BLL.Abstract;
using CORE.Entities;
using DAL;
using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class BrandService : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandService(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public async Task CreateAsync(Brand entity)
        {
            await _brandDal.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _brandDal.DeleteAsync(id);
        }

        public Task<Brand> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Brand>> GetAllAsync(Expression<Func<Brand, bool>> filter = null)
        {
            return await _brandDal.GetAllAsync(filter);
        }

        public Task<Brand> GetOneAsync(Expression<Func<Brand, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Brand> GetProductsByBrandAsync(int id)
        {
            return await _brandDal.GetProductsByBrandAsync(id);
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
