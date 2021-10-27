using EasyWay.Core.Entities;
using EasyWay.Data;
using MongoDB.Bson.IO;
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

       // public async Task MatrixAsync()
       public async Task MatrixAsync()
        {//הזמנות שלא בוצעו
            var orders = _OrderRepository.DoneOrNot();
            var distanceMatrix = await CreateDistanceMatrix(orders);
        }

        private static async Task<DistanceMatrix> CreateDistanceMatrix(List<Order> orders)
        {
            //  רשימת הכתובות
            var addresses = string.Join('|', orders.Select(o => $"{o.Address.Coordinates.Latitude},{o.Address.Coordinates.Longitude}"));

            //TODO: move to app settings
            var apiKey = "AIzaSyBFVQTB-gOzy3rhID9yuz8ejN_QL70qCqQ";
            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={addresses}&destinations={addresses}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<DistanceMatrix>(responseBody);
            }
        }
    }
}
