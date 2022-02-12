using EasyWay.Core;
using EasyWay.Core.Entities;
using EasyWay.Data;
using Microsoft.AspNetCore.Cors;//current
using EasyWay.Services;//incoming
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EasyWay.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;
        private IDistanceMatrixSettings _settings;
        //
        private DeliveryManRepository _deliveryManRepository;
        public OrderController(OrderRepository orderRepository, IDistanceMatrixSettings settings, DeliveryManRepository deliveryManRepository)
        {
            _orderRepository = orderRepository;
            _settings = settings;
            _deliveryManRepository = deliveryManRepository;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get() =>
            _orderRepository.Get();

        [HttpGet]
        [Route("DoneOrders")]
        public ActionResult<int> DoneOrders() =>
            _orderRepository.DoneOrNot().Count;
        [HttpGet]
        [Route("HasDeliverymanId")]
        public ActionResult<int> HasDeliverymanId() =>
              _orderRepository.HasDeliverymanId().Count;
        [HttpGet]
        [Route("GroupRoute")]
        public IEnumerable<IGrouping<string, Order>> GroupRoute() =>
         _orderRepository.getRoute();
        [HttpGet]
        [Route("Route")]
        public async Task<List<string>> CalculateRouteAsync()=>
       
            await new RouteService(_orderRepository, _settings, _deliveryManRepository).CalculateRoutes();
        

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
            //order.SetAddress(order.addressLon, order.addressLat);
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
        //[HttpPut("{id:length(24)}", Name = "UpdateOrderDone")]
        [HttpPut("confirm-order/{id}")]
        public ActionResult<Order> Update(string id)
        {
            var order = _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound();
            }
            order.DoneOrNot = true;
            _orderRepository.Update(id,order);

           
            return _orderRepository.Get(id);
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
