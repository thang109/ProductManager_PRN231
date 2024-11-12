using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using Repository;
using EStoreAPI.Model;

namespace EStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            return Ok(await _orderRepository.GetOrder(id));
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(int productId, int quantity, decimal discount,OrderModel orderModel)
        {
            try
            {
                var order = new Order
                {
                    MemberId = orderModel.MemberId,
                    OrderDate = orderModel.OrderDate,
                    RequiredDate = orderModel.RequiredDate,
                    ShippedDate = orderModel.ShippedDate,
                    Freight = orderModel.Freight
                };
                var result = await _orderRepository.CreateOrder(productId, quantity, discount, order);
                if (result > 0)
                {
                    return Ok(order);
                }
                return BadRequest("Failed to add order.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrderByMemberId(int memberId)
        {
            return Ok(await _orderRepository.GetOrderByMemberId(memberId));
        }
    }
}
