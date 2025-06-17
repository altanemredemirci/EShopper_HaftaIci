using CORE.Entities;
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
    public class CartDal : GenericRepository<Cart, DataContext>, ICartDal
    {
        private readonly DataContext _context;
        public CartDal(DataContext context) : base(context)
        {
            _context = context;
        }

        public Task AddToCartAsync(Cart cart, int productId, int quantity)
        {
            var product = _context.Products.Find(productId);
            var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                cartItem.Tutar();
            }
            else
            {
                cart.CartItems.Add(new CartItem()
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Total = product.Price * quantity
                });                
            }

            return _context.SaveChangesAsync();            
        }

        public Task DeleteFromCartAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public override Task<Cart> GetOneAsync(Expression<Func<Cart, bool>> filter)
        {
            var cart = _context.Carts.Include(i => i.CartItems).ThenInclude(i=> i.Product).ThenInclude(i=> i.Images).FirstOrDefaultAsync(filter);
            return cart;
        }
    }
}
