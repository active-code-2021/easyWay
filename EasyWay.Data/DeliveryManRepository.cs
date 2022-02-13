using EasyWay.Core;
using EasyWay.Core.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyWay.Data
{
    public class DeliveryManRepository
    {
        private readonly IMongoCollection<DeliveryMan> _deliveryMans;
       // static int cnt=0;

        public DeliveryManRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _deliveryMans = database.GetCollection<DeliveryMan>(settings.deliveryManCollectionName);
        }

        public List<DeliveryMan> Get() =>
            _deliveryMans.Find(deliveryMan => true).ToList();

        static int[] arr = new int[3];
        public string GetId(int deliveyman)
        {
            //  //get the deliveryManId                      
            //var rnd = new Random();
            //int cnt = rnd.Next(0, deliveyman);
            //  while (arr[cnt] != 0)
            //  {
            //      cnt = rnd.Next(0, deliveyman);
            //  }
            //  arr[cnt]++;

            //  //if(cnt>=3)
            //  // {
            //  //     return "";
            //  // }
            //  var deliveryMan = _deliveryMans.Find(new BsonDocument()).Project(new BsonDocument { { "id", 1 } }).Skip(cnt).FirstOrDefault();


            //   return deliveryMan.GetValue("_id").ToString();
            return null;
        }
        public List< DeliveryMan> GetActiveDeliveryman() =>
            _deliveryMans.Find<DeliveryMan>(deliveryMan => deliveryMan.Active).ToList();

        public DeliveryMan Get(string id) =>
            _deliveryMans.Find<DeliveryMan>(deliveryMan => deliveryMan.Id == id).FirstOrDefault();

        public DeliveryMan GetByEmail(string email) =>
            _deliveryMans.Find<DeliveryMan>(deliveryMan => deliveryMan.Email == email).FirstOrDefault();

        public DeliveryMan Create(DeliveryMan deliveryMan)
        {
            _deliveryMans.InsertOne(deliveryMan);
            return deliveryMan;
        }
       
        public  bool DeliveryExists(string email)
        {
            var a = _deliveryMans.Find(d => d.Email == email).Count();
           if (a >0)
                    return true;
            return false;

        }
        public void Update(string id, DeliveryMan deliveryManIn) =>
            _deliveryMans.ReplaceOne(deliveryMan => deliveryMan.Id == id, deliveryManIn);

        public void Remove(DeliveryMan deliveryManIn) =>
            _deliveryMans.DeleteOne(deliveryMan => deliveryMan.Id == deliveryManIn.Id);

        public void Remove(string id) =>
            _deliveryMans.DeleteOne(deliveryMan => deliveryMan.Id == id);
    }
}
