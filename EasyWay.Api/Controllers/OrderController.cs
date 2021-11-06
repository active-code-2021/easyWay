using EasyWay.Core.Entities;
using EasyWay.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace EasyWay.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get() =>
            _orderRepository.Get();

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<Order> Get(string id)
        {
            var order = _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            _orderRepository.Create(order);

            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Order orderIn)
        {
            var order = _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderRepository.Update(id, orderIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var order = _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderRepository.Remove(order.Id);

            return NoContent();
        }

        [HttpGet("deliveryMan/{id:length(24)}", Name = "GetOrderByDeliveryMan")]
        public ActionResult<List<Order>> GetByDeliverId(string id)
        {
            return _orderRepository.GetByDeliveryManId(id);
        }
    }
}
