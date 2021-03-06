using DocuSign.eSign.Model;
using EasyWay.Core;
using EasyWay.Core.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace EasyWay.Data
{
    public class OrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Order>(settings.orderCollectionName);
        }

        public List<Order> Get() =>
            _orders.Find(order => true).ToList();
        public List<Order> DoneOrNot()
        {
            var order = _orders.Find(o =>!o.DoneOrNot);
            return order.ToList();
        }
        public List<Order> HasDeliverymanId()
        {
            var order = _orders.Find(o =>o.DeliverymanId==null);
            return order.ToList();
        }
        public IEnumerable<IGrouping<string, Order>> getRoute()
        {
            var route = _orders.Find(o =>o.DeliverymanId!=null);
            return route.ToList().OrderBy(r=>r.DeliverymanNum).GroupBy(o => o.DeliverymanId);
        }
        public Order getWarehouse()
        {
            Order warehuose = _orders.Find(o => o.Id.Equals("61e84b7bad23421e2c4ba6d9")).FirstOrDefault();
            return warehuose;
        }
   
    public Order Get(string id) =>
            _orders.Find<Order>(order => order.Id == id).FirstOrDefault();

        public List<Order> GetByDeliveryManId(string id) =>
            _orders.Find<Order>(order => order.DeliverymanId == id).ToEnumerable().OrderBy(x=>x.DeliverymanNum).ToList();

        public Order Create(Order order)
        {
            
            _orders.InsertOne(order);
            return order;
        }
       

    public void Update(string id, Order orderIn) =>
            _orders.ReplaceOne(order => order.Id == id, orderIn);
        public void Remove(Order orderIn) =>
            _orders.DeleteOne(order => order.Id == orderIn.Id);

        public void Remove(string id) =>
            _orders.DeleteOne(order => order.Id == id);
    }
}
