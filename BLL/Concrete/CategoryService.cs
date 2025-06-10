using BLL.Abstract;
using CORE.Entities;
using DAL;
using DAL.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
	public class CategoryService:ICategoryService
	{
		private readonly ICategoryDal _categoryDal;

        public CategoryService(ICategoryDal categoryDal)
        {
			_categoryDal = categoryDal;
        }

        public async Task CreateAsync(Category entity)
        {
          await _categoryDal.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryDal.DeleteAsync(id);
        }

        public async Task<Category> FindAsync(int id)
        {
            return await _categoryDal.FindAsync(id);
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync(Expression<Func<Category, bool>> filter = null)
		{
			return await _categoryDal.GetAllAsync(filter);
		}

		public async Task<IReadOnlyList<Category>> GetCategories()
		{
			return await _categoryDal.GetCategories();
		}

        public async Task<Category> GetOneAsync(Expression<Func<Category, bool>> filter)
        {
            return await _categoryDal.GetOneAsync(filter);
        }

        public async Task UpdateAsync()
        {
            await _categoryDal.UpdateAsync();
        }
    }
}
