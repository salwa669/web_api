using Ecommerce.Models;
using System.Collections.Generic;

namespace Ecommerce.Repository
{
    public interface IOrderRepository
    {
        int Delete(int id);
        List<Order> GetAll();
        Order GetById(int id);
        int Insert(Order order);
        int Update(int id, Order order);
    }
}