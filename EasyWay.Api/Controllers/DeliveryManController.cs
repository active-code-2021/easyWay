using EasyWay.Core.Entities;
using EasyWay.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace EasyWay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryManController : ControllerBase
    {
        private readonly DeliveryManRepository _deliveryManRepository;

        public DeliveryManController(DeliveryManRepository deliveryManRepository)
        {
            _deliveryManRepository = deliveryManRepository;
        }

        [HttpGet]
        public ActionResult<List<DeliveryMan>> Get() =>
            _deliveryManRepository.Get();

        [HttpGet("{id:length(24)}", Name = "GetDeliveryMan")]
        public ActionResult<DeliveryMan> Get(string id)
        {
            var deliveryMan = _deliveryManRepository.Get(id);

            if (deliveryMan == null)
            {
                return NotFound();
            }

            return deliveryMan;
        }
        //[Authorize]
        [HttpGet]
        [Route("deliverybyEmail")]
        //[HttpGet("deliverybyEmail/{deliveryEmail}")]
        public ActionResult<DeliveryMan> DeliveryManByEmail(string deliveryEmail)
        {
            var deliveryMan = _deliveryManRepository.GetByEmail(deliveryEmail);

            if (deliveryMan == null)
            {
                return NotFound();
            }

            return deliveryMan;
        }
        [HttpGet("{email}", Name = "isActive")]
        public ActionResult<bool> DeliveryManIsActive(string email)
        {
            var deliveryMan = _deliveryManRepository.GetByEmail(email);
            
            if (deliveryMan == null)
            {
                return NotFound("You are not allowed to register");
            }

            return true;
        }

        [HttpPost]
        public ActionResult<DeliveryMan> Create(DeliveryMan deliveryMan)
        {
            if (_deliveryManRepository.DeliveryExists(deliveryMan.Email))
                return BadRequest("DeliveryMan is already exists");
            _deliveryManRepository.Create(deliveryMan);

            return CreatedAtRoute("GetDeliveryMan", new { id = deliveryMan.Id.ToString() }, deliveryMan);
        }


       

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, DeliveryMan deliveryManIn)
        {
            var customer = _deliveryManRepository.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            _deliveryManRepository.Update(id, deliveryManIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var customer = _deliveryManRepository.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            _deliveryManRepository.Remove(customer.Id);

            return NoContent();
        }

        
    }
}
