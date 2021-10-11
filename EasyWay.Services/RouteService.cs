using EasyWay.Core.Entities;
using EasyWay.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWay.Services
{
    class RouteService
    {
        public OrderRepository _OrderRepository;
        public CustomerRepository _customerRepository;

        public RouteService(OrderRepository orderRepository, CustomerRepository customerRepository)
        {
            _OrderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public void matrix()
        {//הזמנות שלא בוצעו
            var order = _OrderRepository.doneOrNot();
            var customer = _customerRepository.Get();
            //להביא את כל הכתובות של הלקוחות שהחבילה שלהם עדין לא נמסרה

            var result = customer.Join(order, c => c.Id, o => o.CustomerId, (c, o) => c.Address);
  
            



        }
    }
}
