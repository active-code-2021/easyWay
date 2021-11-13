using EasyWay.Core;
using EasyWay.Core.Entities;
using EasyWay.Data;
using EasyWay.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyWay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;
        private IDistanceMatrixSettings _settings;

        public OrderController(OrderRepository orderRepository, IDistanceMatrixSettings settings)
        {
            _orderRepository = orderRepository;
            _settings = settings;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get() =>
            _orderRepository.Get();


        [HttpGet]
        [Route("Route")]
        public async Task<List<string>> CalculateRouteAsync()
        {
            return await new RouteService(_orderRepository, _settings).CalculateRoutes();
        }

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
            order.SetAddress(order.addressLon, order.addressLat);
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
    }
}
