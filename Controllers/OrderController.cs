using Ecommerce.Models;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class OrderController : ControllerBase
    {
        IOrderRepository OrderRepository;

        public OrderController(IOrderRepository Order)
        {
            OrderRepository = Order;

        }
        [HttpGet]
        public IActionResult getAll()
        {
            List<Order> OrderList = OrderRepository.GetAll();
            if (OrderList == null)
            {
                return BadRequest("Empty Orders");
            }
            return Ok(OrderList);
        }
        [HttpGet("{id:int}")]

        public IActionResult getByID(int id)
        {
            Order OrderList = OrderRepository.GetById(id);
            if (OrderList == null)
            {
                return BadRequest("there is no Order with this id");
            }
            return Ok(OrderList);
        }

        [HttpPost]
        public IActionResult Insert(Order Order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OrderRepository.Insert(Order);
                    return Ok(" Order is add Successfully");
                }
                catch
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Can't adding to Order");
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Order Order)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    OrderRepository.Update(id, Order);

                    return StatusCode(StatusCodes.Status204NoContent, "Data Saved");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                OrderRepository.Delete(id);
                return Ok("Order Deleted");
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Can't Delete this Order ");
            }


        }
    }
}
