using Ecommerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class CartRepository : ICartRepository
    {

        Context context;
        public CartRepository(Context _context)
        {
            context = _context;
        }
        public List<Cart> GetAll()
        {
            return context.Cart.ToList();
        }
        public Cart GetById(int id)
        {
            return context.Cart.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Cart cart)
        {
            context.Cart.Add(cart);
            return context.SaveChanges();
        }

        public int Update(int id, Cart cart)
        {
            Cart oldcart = GetById(id);
            if (oldcart != null)
            {
                oldcart.CartQuantity = cart.CartQuantity;
                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            Cart oldcart = GetById(id);
            context.Cart.Remove(oldcart);
            return context.SaveChanges();
        }

    }
}
