using Ecommerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class OrderRepository : IOrderRepository
    {
        Context context;
        public OrderRepository(Context _context)
        {
            context = _context;
        }
        public List<Order> GetAll()
        {
            return context.Order.ToList();
        }
        public Order GetById(int id)
        {
            return context.Order.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Order order)
        {
            context.Order.Add(order);
            return context.SaveChanges();
        }

        public int Update(int id, Order order)
        {
            Order oldorder = GetById(id);
            if (oldorder != null)
            {
                oldorder.TotalPrice = order.TotalPrice;
                oldorder.OrderDate = order.OrderDate;
                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            Order order = GetById(id);
            context.Order.Remove(order);
            return context.SaveChanges();
        }

    }
}

