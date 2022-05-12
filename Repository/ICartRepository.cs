using Ecommerce.Models;
using System.Collections.Generic;

namespace Ecommerce.Repository
{
    public interface ICartRepository
    {
        int Delete(int id);
        List<Cart> GetAll();
        Cart GetById(int id);
        int Insert(Cart cart);
        int Update(int id, Cart cart);
    }
}