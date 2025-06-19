using CORE.Entities;
using CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ICartService:IRepository<Cart>
    {
        Task AddToCartAsync(Cart cart, int productId, int quantity);
        Task DeleteFromCartAsync(int cartId, int productId);
    }
}
