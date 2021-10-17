﻿using EasyWay.Core.Entities;
using EasyWay.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyWay.Services
{
    
    public class RouteService
    {
     
        public OrderRepository _OrderRepository;
        //public CustomerRepository _customerRepository;

        public RouteService(OrderRepository orderRepository/*, CustomerRepository customerRepository*/)
        {
            _OrderRepository = orderRepository;
            //_customerRepository = customerRepository;
        }

        public async Task MatrixAsync()
        {//הזמנות שלא בוצעו
            var order = _OrderRepository.DoneOrNot();
            //רשימת הכתובות
            var addresses =string.Join('|', order.Select(o => $"{o.Address}"));
            //var customer = _customerRepository.Get();
            ////להביא את כל הכתובות של הלקוחות שהחבילה שלהם עדין לא נמסרה

            //var addresses = customer.Join(order, c => c.Id, o => o.CustomerId, (c, o) => c.Lat_lng);

            //var places = string.Join('|', addresses.Select(address => $"{address.Lat},{address.Lng}"));

            var apiKey = "AIzaSyBFVQTB-gOzy3rhID9yuz8ejN_QL70qCqQ";
            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={addresses}&destinations={addresses}&key={apiKey}";

            //TODO: use httpClient to get the distance matrix
             HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }
  
    }
}
