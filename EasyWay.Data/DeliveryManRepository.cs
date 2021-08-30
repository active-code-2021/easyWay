using EasyWay.Core;
using EasyWay.Core.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace EasyWay.Data
{
    public class DeliveryManRepository
    {
        private readonly IMongoCollection<DeliveryMan> _deliveryMans;

        public DeliveryManRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _deliveryMans = database.GetCollection<DeliveryMan>(settings.deliveryManCollectionName);
        }

        public List<DeliveryMan> Get() =>
            _deliveryMans.Find(deliveryMan => true).ToList();

        public DeliveryMan Get(string id) =>
            _deliveryMans.Find<DeliveryMan>(deliveryMan => deliveryMan.Id == id).FirstOrDefault();

        public DeliveryMan Create(DeliveryMan deliveryMan)
        {
            _deliveryMans.InsertOne(deliveryMan);
            return deliveryMan;
        }

        public void Update(string id, DeliveryMan deliveryManIn) =>
            _deliveryMans.ReplaceOne(deliveryMan => deliveryMan.Id == id, deliveryManIn);

        public void Remove(DeliveryMan deliveryManIn) =>
            _deliveryMans.DeleteOne(deliveryMan => deliveryMan.Id == deliveryManIn.Id);

        public void Remove(string id) =>
            _deliveryMans.DeleteOne(deliveryMan => deliveryMan.Id == id);
    }
}
