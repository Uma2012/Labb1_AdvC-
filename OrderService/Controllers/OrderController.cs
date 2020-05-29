using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }


        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order order)
        {
            var createOrder = _orderRepository.CreateOrder(order);
            if (createOrder != null)
                return Ok(createOrder);
            else
                return BadRequest();
        }

        [HttpDelete]
        public ActionResult<Guid> DeleteOrder(Guid id)
        {
            var wasDeleted = _orderRepository.Delete(id);
            if (wasDeleted)
            {
                return Ok(id);
            }
            else
                return NotFound(id);
        }

       
    }
}