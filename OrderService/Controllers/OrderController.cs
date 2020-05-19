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
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }


        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] ShoppingCart shoppingCart)
        {
            var createOrder = _orderRepository.CreateOrder(shoppingCart);
            return Ok(createOrder);
        }
    }
}