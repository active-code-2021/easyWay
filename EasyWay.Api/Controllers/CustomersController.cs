using EasyWay.Core.Entities;
using EasyWay.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomersApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    [ApiController]

    public class CustomersController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;

        public CustomersController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public ActionResult<List<Customer>> Get() =>
            _customerRepository.Get();

        [HttpGet("{id:length(24)}", Name = "GetCustomer")]
        public ActionResult<Customer> Get(string id)
        {
            var customer = _customerRepository.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            _customerRepository.Create(customer);

            return CreatedAtRoute("GetCustomer", new { id = customer.Id.ToString() }, customer);
        }
        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            _orderRepository.Create(order);

            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Customer customerIn)
        {
            var customer = _customerRepository.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.Update(id, customerIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var customer = _customerRepository.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.Remove(customer.Id);

            return NoContent();
        }
    }
}