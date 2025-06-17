using BLL.Abstract;
using CORE.Entities;
using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartDal _cartDal;

        public CartService(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public Task AddToCartAsync(Cart cart, int productId, int quantity)
        {
            return _cartDal.AddToCartAsync(cart,productId,quantity);

        }

        public Task CreateAsync(Cart entity)
        {
            return _cartDal.CreateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _cartDal.DeleteAsync(id);

        }

        public Task DeleteFromCartAsync(int productId)
        {
            return _cartDal.DeleteFromCartAsync(productId);
        }

        public Task<Cart> FindAsync(int id)
        {
            return _cartDal.FindAsync(id);
        }

        public Task<IReadOnlyList<Cart>> GetAllAsync(Expression<Func<Cart, bool>> filter = null)
        {
            return _cartDal.GetAllAsync(filter);
        }

        public Task<Cart> GetOneAsync(Expression<Func<Cart, bool>> filter)
        {
            return _cartDal.GetOneAsync(filter);
        }

        public Task UpdateAsync()
        {
            return _cartDal.UpdateAsync();
        }
    }
}
