using EasyWay.Core.Entities;
using EasyWay.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWay.Api.Controllers
{
    public class RouteController : Controller
    {
        ////public IActionResult Index()
        ////{
        ////    return View();
        ////}
        private readonly AddressRepository _addressRepository;

        public RouteController(AddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        
        [HttpGet]
        
        [HttpGet("{id:length(24)}", Name = "GetAddress")]
        public ActionResult<Address> Get(string id)
        {
            var address = _addressRepository.Get(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        [HttpPost]
        public ActionResult<Address> Create(Address address)
        {
            _addressRepository.Create(address);

            return CreatedAtRoute("GetAddress", new { id = address.Id.ToString() }, address);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Address addressIn)
        {
            var address = _addressRepository.Get(id);

            if (address == null)
            {
                return NotFound();
            }

            _addressRepository.Update(id, addressIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var address = _addressRepository.Get(id);

            if (address == null)
            {
                return NotFound();
            }

            _addressRepository.Remove(address.Id);

            return NoContent();
        }
    }
}
