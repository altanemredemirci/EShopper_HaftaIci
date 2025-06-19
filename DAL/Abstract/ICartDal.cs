using CORE.Entities;
using CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ICartDal : IRepository<Cart>
    {
        Task AddToCartAsync(Cart cart, int productId, int quantity);
        Task DeleteFromCartAsync(int cartId, int productId);
    }
}
